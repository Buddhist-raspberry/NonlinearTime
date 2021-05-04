using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Weapon
{
    private int eggUseMP = 6;
    protected Material originalMaterial;
    public Material speedMaterial;
    protected override void Init()
    {
        weaponType = WeaponType.EGG;
        setUseMP(eggUseMP);
    }

    public override void SpeedUp()
    {
        originalMaterial = GetComponent<MeshRenderer>().material;
        GetComponent<MeshRenderer>().material = speedMaterial;
        // GetComponent<MeshRenderer>().materials[0] = speedUpMaterial;
        Debug.Log("SpeedUp");

        base.SpeedUp();
    }

    protected override void RecoverTimeScale()
    {
        base.RecoverTimeScale();
        Debug.Log("RecoverTime");
        GetComponent<MeshRenderer>().material = originalMaterial;
    }

    private void OnCollisionStay(Collision other)
    {
        if ( 1 << other.collider.gameObject.layer == LayerMask.GetMask("Enemy"))
        {
            // other.collider.GetComponent<EnemyNavigator>().SpeedDown();
        }
    }
}
