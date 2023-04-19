using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos : MonoBehaviour
{
    public GameObject MovingPlatform;
 
    public float distance;
    
    void Start()
    {
        transform.position  = new Vector2(MovingPlatform.GetComponent<Transform>().position.x+distance, MovingPlatform.GetComponent<Transform>().position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
