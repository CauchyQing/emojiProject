using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.UI;

public class playerAttribution : MonoBehaviour
{

    private playerAnimation anim;
    private Rigidbody2D rb;
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
    public LayerMask platformLayer;//ƽ̨����
    public bool isGround;
    public bool isPlatform;//�ж��Ƿ���ƽ̨


    private void Update()
    {
        Check();
    }
    public void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
        isPlatform = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, platformLayer);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }

    private void Start()
    {
        currentOdds = strikeOdds;
        anim= GetComponent<playerAnimation>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(Attack attacker)
    {
        currentOdds += attacker.damage;
        
        //rb.AddForce()
        anim.PlayerHurt();
        OnTakeDamage?.Invoke(attacker.transform);
    }



}
