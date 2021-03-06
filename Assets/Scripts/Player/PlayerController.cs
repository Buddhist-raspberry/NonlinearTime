using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public float charge;
    public bool canShoot = true;
    public bool action;
    private enum UseStatus{
        CONTROL,WEAPON,MAGIC
    };
    private UseStatus currentUseStatus = UseStatus.WEAPON;
    private MagicController m_magicController;

    [Header("Control")]
    public GameObject controlBall;
    
    [Header("Magic")]
    public Magic ownMagic;

    [Space]
    [Header("Weapon")]
    public Weapon weapon;
    public Transform weaponHolder;
    public LayerMask weaponLayer;
    public LayerMask glowLayer;
    private Transform _selection;

    [Space]
    [Header("UI")]
    public Image indicator;

    [Space]
    [Header("Prefabs")]
    public GameObject bulletPrefab;




    private void Awake()
    {
        instance = this;
        if (weaponHolder.GetComponentInChildren<Weapon>() != null)
            weapon = weaponHolder.GetComponentInChildren<Weapon>();
        m_magicController = GetComponent<MagicController>();
        _selection = null;
    }
    // Update is called once per frame
    void Update()
    {
        //使用武器
        if (CheckStatus(UseStatus.WEAPON)&&Input.GetMouseButtonDown(0))
        {
            StopCoroutine(ActionE(.03f));
            StartCoroutine(ActionE(.03f));
            if (weapon != null){
                switch(weapon.weaponType){
                    case Weapon.WeaponType.GUN: 
                        if(canShoot){
                            ((Gun)weapon).Shoot(SpawnPos(), 
                                Camera.main.transform.rotation, false);
                        }
                        break;
                    default:
                        weapon.Throw();
                        weapon = null;
                        break;
                }
            }
        }
        //抛弃武器
        if (CheckStatus(UseStatus.WEAPON)&&Input.GetMouseButtonDown(1))
        {
            StopCoroutine(ActionE(.4f));
            StartCoroutine(ActionE(.4f));

            if(weapon != null)
            {
                weapon.Release();
                weapon = null;
            }
        }
        

        
        RaycastHit hit;
        bool keepSlected = false;
        //光标选择
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, glowLayer))
        {
            if (hit.transform != _selection) {
                if (_selection != null)
                    _selection.GetComponent<Glowing>().Unselected(); 
            _selection = hit.transform;
            _selection.GetComponent<Glowing>().Selected();
            }
            
            keepSlected = true;

            if (CheckStatus(UseStatus.CONTROL)&&Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                ChronosBehaviour t_chronosBehaviour = hit.transform.GetComponent<ChronosBehaviour>();
                float currentTimeScale = t_chronosBehaviour.GetLocalTimeScale();
                float _factor;
                if (Input.GetAxis("Mouse ScrollWheel") <= 0) _factor = -1;
                else _factor = 1;

                if (currentTimeScale<=1) currentTimeScale += _factor * 0.1f;
                else currentTimeScale += _factor * 1f;
                

                currentTimeScale = Mathf.Max(currentTimeScale, -1);
                currentTimeScale = Mathf.Min(currentTimeScale, 11);

                Debug.Log(currentTimeScale);

                if (currentTimeScale == 0 && _factor < 0) currentTimeScale = -1;
                if (currentTimeScale < 0 && _factor > 0) currentTimeScale = 0;

                t_chronosBehaviour._setSpeed(currentTimeScale);

            }
        }

        if  (!keepSlected && _selection != null) {
            _selection.GetComponent<Glowing>().Unselected();
            _selection = null;
        }

        //捡起武器
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,5, weaponLayer))
        {
            if (Input.GetKeyDown(KeyCode.E) && weapon == null)
            {
                hit.transform.GetComponent<Weapon>().Pickup();
                ChangeUseStatus(UseStatus.WEAPON);
            }
        }

        //武器加速
        if(CheckStatus(UseStatus.CONTROL)&&Input.GetKeyDown(KeyCode.J) && weapon != null)
        {
            weapon.SpeedUp();
        }

        //监听武器切换
        StatusChangeListener();
    }

    IEnumerator ActionE(float time)
    {
        GlobalTimeController.instance.setAction(true);
        yield return new WaitForSecondsRealtime(.06f);
        GlobalTimeController.instance.setAction(false);
    }

    public void ReloadUI(float time)
    {
        indicator.transform.DORotate(new Vector3(0, 0, 90), time, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(() => indicator.transform.DOPunchScale(Vector3.one / 3, .2f, 10, 1).SetUpdate(true));
    }


    Vector3 SpawnPos()
    {
        return Camera.main.transform.position + (Camera.main.transform.forward * .5f) + (Camera.main.transform.up * -.02f);
    }
    
    void StatusChangeListener()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeUseStatus(UseStatus.CONTROL);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeUseStatus(UseStatus.WEAPON);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeUseStatus(UseStatus.MAGIC);
            return;
        }
    }

    void ChangeUseStatus(UseStatus status)
    {
        
        if (status == UseStatus.CONTROL)
        {
            currentUseStatus = UseStatus.CONTROL;
            controlBall.SetActive(true);
            if (weapon != null)
                weapon.gameObject.SetActive(false);
            if (ownMagic != null)
                ownMagic.gameObject.SetActive(false);
            return;
        }
        if (status == UseStatus.WEAPON)
        {
            currentUseStatus = UseStatus.WEAPON;
            controlBall.SetActive(false);
            if (weapon != null)
                weapon.gameObject.SetActive(true);
            if (ownMagic != null)
                ownMagic.gameObject.SetActive(false);
            return;
        }
        if (status == UseStatus.MAGIC)
        {
            currentUseStatus = UseStatus.MAGIC;
            controlBall.SetActive(false);
            if (weapon != null)
                weapon.gameObject.SetActive(false);
            if (ownMagic != null)
                ownMagic.gameObject.SetActive(true);
            return;
        }
    }

    bool CheckStatus(UseStatus status)
    {
        return status == currentUseStatus;    
    }

}
