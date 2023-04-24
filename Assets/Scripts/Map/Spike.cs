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

        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(collision.gameObject.GetComponent<Transform>().up*force, ForceMode2D.Impulse);
    }
}
