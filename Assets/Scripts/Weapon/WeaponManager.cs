using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WeaponManager : Singleton<WeaponManager>
{
    public List<GameObject> WeaponPrefabs;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void InstantiateWeapon(string WeaponName, Transform transform)
    {
        foreach(GameObject prefab in WeaponPrefabs)
        {
            if(prefab.GetComponent<WeaponAttri>().GetWeaponName() == WeaponName)
            {
                var instance = GameObject.Instantiate(prefab, transform);
                instance.SetActive(true);
                break;
            }
        }
    }
}
