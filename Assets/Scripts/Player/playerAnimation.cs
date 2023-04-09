using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;

    private Rigidbody2D rb;

    private playerAttribution attribution;

    private playerController playerController;
   
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attribution = GetComponent<playerAttribution>();
        playerController = GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
    }
    //绑定Animation中的属性
    public void SetAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround",attribution.isGround);
    }


}
