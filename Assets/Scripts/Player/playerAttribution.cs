using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.UI;

public class playerAttribution : MonoBehaviour
{

    private playerAnimation anim;
    private Rigidbody2D rb;
    [Header("基本属性")]
    public float health=100;    //初始血量
    public float currentHealth;   //当前血量

    public UnityEvent<playerAttribution> OnAttack;  //攻击事件
    public UnityEvent<Transform> OnTakeDamage;  //受伤事件
    public UnityEvent Ondie;        //死亡事件


    [Header("检测参数")]
    public float checkRaduis;
    public Vector2 bottomOffset;
    public LayerMask groundLayer;
    public LayerMask platformLayer;//平台检测层
    public bool isGround;
    public bool isPlatform;//判断是否在平台


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
