using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.UI;

public class playerAttribution : MonoBehaviour
{

    public playerAnimation anim;
    public Rigidbody2D rb;
    public playerController pc;
    
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
    
    public enum STATE { 
        NOTHING,
        DEFEND,
        NORMALATTACK,
        ACCUMULATEATTACK
    }
    public STATE state=STATE.NOTHING;

    private void Update()
    {
        Check();
        updateSTATE();
       
    }
    public void updateSTATE()
    {
        if (pc.isDefend) 
            state = STATE.DEFEND;
        else if (pc.isAccumulate)
            state = STATE.ACCUMULATEATTACK;
        else if (pc.isNormalAttack)
            state = STATE.NORMALATTACK;
        else
            state = STATE.NOTHING;

        
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
        pc=GetComponent<playerController>();
    }

    public void TakeDamage(Attack attacker, STATE s)
    {

        if (s == STATE.NORMALATTACK)
        {
            TakeDamageHelp(attacker, 1);

        }
        else if (s == STATE.ACCUMULATEATTACK && state != STATE.DEFEND)
        {
            TakeDamageHelp(attacker, (float)1.5);
        }

         else if(s==STATE.ACCUMULATEATTACK && state == STATE.DEFEND)
         {
             attacker.pa.currentHealth -= attacker.damage*2;
             OnTakeDamage?.Invoke(this.transform);
             OnTakeDamage?.Invoke(this.transform);

             if (attacker.pa.currentHealth <= 0)
             {
                 attacker.pa.currentHealth = 0;
                 attacker.pa.Ondie?.Invoke();
                 attacker.pa.rb.AddForce(attacker.pa.transform.up * attacker.attackForce , ForceMode2D.Impulse);
                 if (attacker.tf.position.x < transform.position.x)
                     attacker.pa.rb.AddForce(transform.right * attacker.attackForce * 5 , ForceMode2D.Impulse);
                 else
                     attacker.pa.rb.AddForce(-1 * transform.right * attacker.attackForce * 5 , ForceMode2D.Impulse);
                 attacker.pa.anim.PlayerHurt();
             }
             else
             {
                 attacker.pa.rb.AddForce(attacker.pa.transform.up * attacker.attackForce , ForceMode2D.Impulse);
                 if (attacker.tf.position.x < transform.position.x)
                     attacker.pa.rb.AddForce(attacker.pa.transform.right * attacker.attackForce , ForceMode2D.Impulse);
                 else
                     attacker.pa.rb.AddForce(-1 * attacker.pa.transform.right * attacker.attackForce , ForceMode2D.Impulse);
                 attacker.pa.anim.PlayerHurt();
             }
         }

    }

    public void TakeDamageHelp(Attack attacker,float x)
    {
        currentHealth -= attacker.damage*x;
        OnTakeDamage?.Invoke(attacker.transform);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Ondie?.Invoke();
            rb.AddForce(transform.up * attacker.attackForce*x, ForceMode2D.Impulse);
            anim.PlayerHurt();
            if (attacker.tf.position.x < transform.position.x)
                rb.AddForce(transform.right * attacker.attackForce * 5*x, ForceMode2D.Impulse);
            else
                rb.AddForce(-1 * transform.right * attacker.attackForce * 5*x, ForceMode2D.Impulse);
     
        }
        else
        {
            rb.AddForce(transform.up * attacker.attackForce * x, ForceMode2D.Impulse);
            if (attacker.tf.position.x < transform.position.x)
                rb.AddForce(transform.right * attacker.attackForce * x, ForceMode2D.Impulse);
            else
                rb.AddForce(-1 * transform.right * attacker.attackForce * x, ForceMode2D.Impulse);
           
        }
    }
    

}
