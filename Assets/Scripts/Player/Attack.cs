using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;  //���ӻ��ɸ���
    public float attackRange;
    public float attackRate; //����Ƶ��


    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<playerAttribution>().TakeDamage(this);
    }







    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}