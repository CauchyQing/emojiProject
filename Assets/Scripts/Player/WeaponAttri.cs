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
}
