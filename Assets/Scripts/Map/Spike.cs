using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spike : MonoBehaviour
{
    public int force;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("spike");

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            


          if(collision.gameObject.GetComponent<playerController>().faceDirection>0)
          collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * force , ForceMode2D.Impulse);
            else
           collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * force, ForceMode2D.Impulse);


            collision.gameObject.GetComponent<playerAnimation>().PlayerHurt();//调用受击动画
        }
    }
}
