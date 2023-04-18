using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerAttribution : MonoBehaviour
{
    [Header("��������")]
    public float strikeOdds;    //��ʼ���ɸ���
    public float currentOdds;   //��ǰ���ɸ���




    public UnityEvent<playerAttribution> OnAttack;  //�����¼�
    public UnityEvent<Transform> OnTakeDamage;  //���˸ı���ɸ���
    public UnityEvent Ondie;


    [Header("������")]
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
