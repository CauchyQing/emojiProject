/*
 * 这个脚本定义了武器的类型
 * 可定义武器的属性
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

    //TODO: 临时管理武器掉出边界消失
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
