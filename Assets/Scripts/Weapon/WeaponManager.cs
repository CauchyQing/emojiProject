using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class WeaponManager : Singleton<WeaponManager>
{
    public List<GameObject> weaponPrefabs;

    [Header("��������")]
    //������ʼλ�õķ�Χ
    public float minX = -20f;
    public float maxX = 20f; 
    public float minY = 30f;
    public float maxY = 40f;
    //������ͬʱ���ڵ������������
    public int maxWeapons = 5;
    //��������֮�����С�����ʱ����
    public float minInterval = 1f;
    public float maxInterval = 5f;
    //���ϴ��������������ĵ�ǰʱ��
    private float timer;
    //�����еĵ�ǰ��������
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
            //ʹ�����ֵ���ü�ʱ��
            timer = Random.Range(minInterval, maxInterval);

            int index = Random.Range(0, weaponPrefabs.Count);
            GameObject weaponPrefab = weaponPrefabs[index];

            //�����λ��ʵ��������Prefab
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
