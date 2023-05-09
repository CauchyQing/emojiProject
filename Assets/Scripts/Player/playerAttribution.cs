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

    [Header("��������")]
    public float health = 100;    //��ʼѪ��
    public float currentHealth;   //��ǰѪ��

    public UnityEvent<playerAttribution> OnAttack;  //�����¼�
    public UnityEvent<Transform> OnTakeDamage;  //�����¼�
    public UnityEvent Ondie;        //�����¼�
    public UnityEvent SuccessDefend;        //�񵶳ɹ�



    [Header("������")]
    public float checkRaduis;
    public Vector2 bottomOffset;
    public LayerMask groundLayer;
    public LayerMask platformLayer;//ƽ̨����
    public bool isGround;
    public bool isPlatform;//�ж��Ƿ���ƽ̨
    public float damageMultiplier = 1;

    public enum STATE { 
        NOTHING,
        DEFEND,
        NORMALATTACK,
        ACCUMULATEATTACK
    }
    public STATE state = STATE.NOTHING;

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
        anim = GetComponent<playerAnimation>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<playerController>();
    }

    public void TakeDamage(Attack attacker, STATE s)
    {

        if (s == STATE.NORMALATTACK)
        {
            TakeDamageHelp(attacker, damageMultiplier);

        }
        else if (s == STATE.ACCUMULATEATTACK && state != STATE.DEFEND)
        {
            TakeDamageHelp(attacker, (float)(damageMultiplier*1.5));
        }

        else if (s == STATE.ACCUMULATEATTACK && state == STATE.DEFEND)
        {
            attacker.pa.currentHealth -= attacker.damage * 2;
            OnTakeDamage?.Invoke(this.transform);
            SuccessDefend?.Invoke();
            attacker.pa.GetComponent<ArmourController>().WeaponsDrops(attacker.pa.transform);
            if (attacker.pa.currentHealth <= 0)
            {
               
                attacker.pa.currentHealth = 0;
               // attacker.pa.Ondie?.Invoke();
                attacker.pa.rb.AddForce(2 * attacker.pa.transform.up * attacker.attackForce, ForceMode2D.Impulse);
                if (attacker.tf.position.x < transform.position.x)
                    attacker.pa.rb.AddForce(-1 * transform.right * attacker.attackForce * 5, ForceMode2D.Impulse);
                else
                    attacker.pa.rb.AddForce(transform.right * attacker.attackForce * 5, ForceMode2D.Impulse);
                attacker.pa.anim.PlayerHurt();
            }
            else
            {
                attacker.pa.rb.AddForce(2 * attacker.pa.transform.up * attacker.attackForce, ForceMode2D.Impulse);
                if (attacker.tf.position.x < transform.position.x)
                    attacker.pa.rb.AddForce(-1 * attacker.pa.transform.right * attacker.attackForce, ForceMode2D.Impulse);
                else
                    attacker.pa.rb.AddForce(attacker.pa.transform.right * attacker.attackForce, ForceMode2D.Impulse);
                attacker.pa.anim.PlayerHurt();
            }
        }

    }

    public void TakeDamageHelp(Attack attacker, float x)
    {
        currentHealth -= attacker.damage * x;
        OnTakeDamage?.Invoke(attacker.transform);

        if (currentHealth <= 0)
        {

            
            currentHealth = 0;
            gameObject.layer = LayerMask.NameToLayer("Default");

            //Ondie?.Invoke();
            rb.AddForce(5 * transform.up * attacker.attackForce * x, ForceMode2D.Impulse);
            anim.PlayerHurt();
            if (attacker.tf.position.x < transform.position.x)
                rb.AddForce(transform.right * attacker.attackForce * 10 * x, ForceMode2D.Impulse);
            else
                rb.AddForce(-1 * transform.right * attacker.attackForce * 10 * x, ForceMode2D.Impulse);

        }
        else
        {
            rb.AddForce(2 * transform.up * attacker.attackForce * x, ForceMode2D.Impulse);
            if (attacker.tf.position.x < transform.position.x)
                rb.AddForce(transform.right * attacker.attackForce * x, ForceMode2D.Impulse);
            else
                rb.AddForce(-1 * transform.right * attacker.attackForce * x, ForceMode2D.Impulse);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("DamageBuff"))
        {
            damageMultiplier +=(float)0.2;
            Destroy(collision.gameObject);

        }
      
        if (collision.gameObject.layer == LayerMask.NameToLayer("BackGround"))
        {
            GetComponent<Transform>().tag = "Untagged";//����ɫ��ǩ��ΪUntagged;
            Debug.Log("disappear");

            gameObject.SetActive(false);
        }


    }

}
