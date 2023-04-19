using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
    public float speed;//移动速度  
    public float waitTime;//等待时间
    public Transform[] movePos;//

    private int dir;
    private GameObject player;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        dir = 1;
        player = GameObject.Find("Emoji-Character");
        playerTransform = player.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
   
        transform.position = Vector2.MoveTowards(transform.position, movePos[dir].position, speed*Time.deltaTime);
        if (Vector2.Distance(transform.position, movePos[dir].position)<0.001f)
        {
            if (waitTime <0.0f)
            {
                if (dir == 1)
                    dir =0;
                else
                    dir = 1;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -=Time.deltaTime;
            }
        }



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player.GetComponent<playerAttribution>().isGround)
        {

            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        collision.gameObject.transform.parent = playerTransform;


    }
}