using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public static PlayerProperty instance { get; protected set; }
    
    public int maxHP = 200;
    public int maxMP = 100;
    int currentHP;
    int currentMP;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentHP = maxHP;
        currentMP = maxMP;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(currentHP);
        // Debug.Log(currentMP);
    }

    public int getPlayHP(){return currentHP;}
    public int getPlayMP(){return currentHP;}
    public void recoverHP(int value)
    {
        currentHP += value;
        currentHP = currentHP <=maxHP?currentHP:maxHP;
        Debug.Log("HP: "+currentHP);
    }
    public void recoverMP(int value)
    {
        currentMP += value;
        currentMP = currentMP <=maxMP?currentMP:maxMP;
        Debug.Log("MP: "+currentMP);
    }
    public void reduceHP(int value)
    {
        currentHP -= value;
        currentHP = currentHP >= 0?currentHP:0;
        Debug.Log("HP: "+currentHP);
    }
    public void reduceMP(int value)
    {
        currentMP -= value;
        currentMP = currentMP >= 0?currentMP:0;
        Debug.Log("MP: "+currentMP);
    }
    
    // public void setPlayerHP(int value)
    // {
    //     currentHP += value;
    //     currentHP = currentHP <=maxHP?currentHP:maxHP;
    //     currentHP = currentHP >= 0?currentHP:0;
    //     Debug.Log(currentHP);
    // }
    // public void setPlayerMP(int value)
    // {
    //     currentMP += value;
    //     currentMP = currentMP <=maxMP?currentMP:maxMP;
    //     currentMP = currentMP >= 0?currentMP:0;
    //     Debug.Log(currentMP);
    // }
}
