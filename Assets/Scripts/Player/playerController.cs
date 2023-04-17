using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private playerAttribution playerAttribution;
    public PlayerInputSystem inputControl;
    public playerAnimation playerAnimation;
    public Vector2 inputDirection;
    
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    public bool isNormalAttack;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAttribution = GetComponent<playerAttribution>();  
        inputControl=new PlayerInputSystem();
        playerAnimation=GetComponent<playerAnimation>();
        inputControl.GamePlay.Jump.started += Jump;

        inputControl.GamePlay.Fire.started += NormalAttack;

    }

   

    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }
    private void Update()
    {
        inputDirection=inputControl.GamePlay.Move.ReadValue<Vector2>(); 
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y); 
        

        //人物翻转
        int faceDirection =(int)transform.localScale.x;
        if(inputDirection.x < 0)
            faceDirection = -1;
        else if(inputDirection.x > 0)
            faceDirection = 1;
        transform.localScale = new Vector3(faceDirection, 1, 1);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if(playerAttribution.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); 
    }


    private void NormalAttack(InputAction.CallbackContext obj)
    {
        isNormalAttack= true;
        playerAnimation.PlayerNormalAttack();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
}
