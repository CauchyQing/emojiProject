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

    public void InstantiateDropWeapon(string WeaponName, Transform transform)
    {
        Debug.Log("!!!DropWeapon!!!");
        foreach(GameObject prefab in weaponPrefabs)
        {
            if(prefab.GetComponent<WeaponAttri>().GetWeaponName() == WeaponName)
            {
                Debug.Log("!!!DropWeapon------>Weaponname" + WeaponName);

                var instance = Instantiate(prefab, transform.position,transform.rotation);
                instance.GetComponent<Collider2D>().enabled = false;
                StartCoroutine(CanNotTakeWeapon(instance));
                weaponCount++;
                break;
            }
        }
    }

    IEnumerator CanNotTakeWeapon(GameObject weapon)
    {
        //�����
        System.Threading.Thread.Sleep(500);
        weapon.GetComponent<Collider2D>().enabled = true;
        yield return 0;
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
