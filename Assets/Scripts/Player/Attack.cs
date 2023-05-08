using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;  //Ôö¼Ó»÷·É¸ÅÂÊ
    public float attackForce;

    [HideInInspector]
    public Transform tf;
    public GameObject parent;
    public playerAttribution pa;
    public playerController controller;

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    Debug.Log("pengzhuang");
    //    other.GetComponent<playerAttribution>().TakeDamage(this,pa.state);
    //}
    void Start()
    {
        // pa = GetComponentInParent<playerAttribution>();
        pa = this.transform.parent.GetComponent<playerAttribution>();

        tf = GetComponent<Transform>();
        // controller = GetComponentInParent<playerController>();
        controller = this.transform.parent.GetComponent<playerController>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("pengzhuang");
         other.GetComponent<playerAttribution>().TakeDamage(this, pa.state);
        //other.GetComponent<playerAttribution>().TakeDamage(this, GetComponent<playerAttribution>().state);

    }
    // Start is called before the first frame update
   

    // Update is called once per frame
    
}
