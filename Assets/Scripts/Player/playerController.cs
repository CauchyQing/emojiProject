using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private playerAttribution playerAttribution;

    public playerAnimation playerAnimation;
    public Vector2 inputDirection;

    [Header("基本参数")]
    public float speed;
    public float jumpForce;

    public bool isNormalAttack;
    public bool isAccumulate;
    public bool isDefend;
    public float recoveryTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAttribution = GetComponent<playerAttribution>();
        playerAnimation = GetComponent<playerAnimation>();
    }



    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    private void Update()
    {
        transform.Translate(new Vector2(inputDirection.x, inputDirection.y) * speed * Time.deltaTime);
    }




    public void Move(InputAction.CallbackContext ctx)
    {

        inputDirection = ctx.ReadValue<Vector2>();
        Debug.Log("123");
        //人物翻转
        int faceDirection = (int)transform.localScale.x;
        if (inputDirection.x < 0)
        {

            faceDirection = -1;
        }

        else if (inputDirection.x > 0)
        {
            faceDirection = 1;
        }

        transform.localScale = new Vector3(faceDirection, 1, 1);
    }

    public void Jump(InputAction.CallbackContext obj)
    {
        if (playerAttribution.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            GetComponent<AudioDefination>()?.PlayAudioClip();
        }
    }
    public void Down(InputAction.CallbackContext obj)
    {

        if (playerAttribution.isPlatform)
        {
            gameObject.layer = LayerMask.NameToLayer("Platform");
            Invoke("Recovery", recoveryTime);
        }
    }
    public void Recovery()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");

    }


    public void NormalAttack(InputAction.CallbackContext obj)
    {
        if (obj.performed) {
            isNormalAttack = true;
            playerAnimation.PlayerNormalAttack();
            playerAttribution.OnAttack?.Invoke(playerAttribution);
        }
        
    }

    public void AccumulateAttack(InputAction.CallbackContext obj)
    {
        if (obj.performed) { isAccumulate = true; }
        if(obj.canceled)
        {
            playerAnimation.PlayerAccumulate();
            playerAttribution.OnAttack?.Invoke(playerAttribution);
        }
        // playerAnimation.PlayerAccumulate();

    }


    public void Defend(InputAction.CallbackContext obj)
    {
        if (obj.performed) {
            isDefend = true;
            playerAnimation.PlayerDefend();
        }



        // Start is called before the first frame update
        void Start()
        {

        }
    }
    // Update is called once per frame


