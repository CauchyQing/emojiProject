using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerAttribution : MonoBehaviour
{
    [Header("基本属性")]
    public float strikeOdds;    //初始击飞概率
    public float currentOdds;   //当前击飞概率




    public UnityEvent<playerAttribution> OnAttack;  //攻击事件
    public UnityEvent<Transform> OnTakeDamage;  //受伤改变击飞概率
    public UnityEvent Ondie;


    [Header("检测参数")]
    public float checkRaduis;
    public Vector2 bottomOffset;
    public LayerMask groundLayer;
    public bool isGround;


    private void Update()
    {
        Check();
    }
    public void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }

    private void Start()
    {
        currentOdds = strikeOdds;
    }

    public void TakeDamage(Attack attacker)
    {
        currentOdds += attacker.damage;
        
        OnTakeDamage?.Invoke(attacker.transform);
    }



}
