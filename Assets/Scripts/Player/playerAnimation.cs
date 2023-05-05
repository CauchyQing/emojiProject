using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;

    public Rigidbody2D rb;

    public playerAttribution attribution;

    public playerController playerController;
   
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
        anim.SetFloat("velocityX", Mathf.Abs(playerController.inputDirection.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround",attribution.isGround);
        anim.SetBool("isNormalAttack", playerController.isNormalAttack);
        anim.SetBool("isAccumulateAttack", playerController.isAccumulate);
        anim.SetBool("isDefend", playerController.isDefend);
    }

    public void PlayerNormalAttack()
    {
        anim.SetTrigger("normalAttack");
    }
    
    public void PlayerAccumulate()
    {
        anim.SetBool ("accumulate",true);
    }
    public void PlayerDefend()
    {
        anim.SetTrigger("defend");
    }


    public void PlayerHurt()
    {
        anim.SetTrigger("hurt");
    }

}
