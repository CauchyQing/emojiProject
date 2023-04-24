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
    public float health=100;    //��ʼѪ��
    public float currentHealth;   //��ǰѪ��

    public UnityEvent<playerAttribution> OnAttack;  //�����¼�
    public UnityEvent<Transform> OnTakeDamage;  //�����¼�
    public UnityEvent Ondie;        //�����¼�


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
        currentHealth = health;
        anim= GetComponent<playerAnimation>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(Attack attacker)
    {
        currentHealth -= attacker.damage;
        OnTakeDamage?.Invoke(attacker.transform);
    
        if(currentHealth<=0)
        {
            currentHealth = 0;
            Ondie?.Invoke();
            rb.AddForce(transform.up * attacker.attackForce, ForceMode2D.Impulse);
            if (attacker.tf.position.x < transform.position.x)
                rb.AddForce(transform.right * attacker.attackForce*5, ForceMode2D.Impulse);
            else
                rb.AddForce(-1 * transform.right * attacker.attackForce*5, ForceMode2D.Impulse);
            anim.PlayerHurt();
        }



        rb.AddForce(transform.up*attacker.attackForce, ForceMode2D.Impulse);
        if(attacker.tf.position.x<transform.position.x)
            rb.AddForce(transform.right * attacker.attackForce, ForceMode2D.Impulse);
        else
            rb.AddForce(-1*transform.right * attacker.attackForce, ForceMode2D.Impulse);
        anim.PlayerHurt();
    }
}
