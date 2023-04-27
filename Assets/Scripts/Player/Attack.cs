using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;  //Ôö¼Ó»÷·É¸ÅÂÊ
    public float attackForce;
    public Transform tf;
    public playerAttribution pa;
    public playerController controller;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<playerAttribution>().TakeDamage(this,pa.state);
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
