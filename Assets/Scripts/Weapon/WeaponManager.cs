using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class WeaponManager : Singleton<WeaponManager>
{
    public List<GameObject> weaponPrefabs;

    [Header("武器掉落")]
    //武器初始位置的范围
    public float minX = -20f;
    public float maxX = 20f; 
    public float minY = 30f;
    public float maxY = 40f;
    //场景中同时存在的最大武器数量
    public int maxWeapons = 5;
    //武器掉落之间的最小和最大时间间隔
    public float minInterval = 1f;
    public float maxInterval = 5f;
    //自上次武器掉落以来的当前时间
    private float timer;
    //场景中的当前武器数量
    private int weaponCount;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void InstantiateWeapon(string WeaponName, Transform transform)
    {
        foreach(GameObject prefab in weaponPrefabs)
        {
            if(prefab.GetComponent<WeaponAttri>().GetWeaponName() == WeaponName)
            {
                var instance = Instantiate(prefab, transform);
                weaponCount++;
                break;
            }
        }
    }
    private void DropWeapons()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && weaponCount < maxWeapons)
        {
            //使用随机值重置计时器
            timer = Random.Range(minInterval, maxInterval);

            int index = Random.Range(0, weaponPrefabs.Count);
            GameObject weaponPrefab = weaponPrefabs[index];

            //在随机位置实例化武器Prefab
            Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
            Quaternion rotation = Quaternion.Euler(0,0,0);
            Instantiate(weaponPrefab, position, rotation);
            weaponCount++;
        }
    }

    public void DecreaseWeaponCount()
    {
        if (--weaponCount < 0) weaponCount = 0;    
    }

    private void Update()
    {
        DropWeapons();
    }
}
