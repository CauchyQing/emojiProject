/*
 * ����ű�����������������
 * �ɶ�������������
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttri : MonoBehaviour
{
    public string weaponName;

    public string GetWeaponName()
    {
        return weaponName;
    }

    //TODO: ��ʱ�������������߽���ʧ
    private void Update()
    {
        if(transform.position.y < -100)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        WeaponManager.Instance.DecreaseWeaponCount();
    }
}
