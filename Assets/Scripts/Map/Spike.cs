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

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {

            collision.gameObject.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);



            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * force, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(gameObject.GetComponent<Transform>().localScale.x * (-1), 1) * force, ForceMode2D.Impulse);

        }
    }
}
