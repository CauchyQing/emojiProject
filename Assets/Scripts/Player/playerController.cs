using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private playerAttribution playerAttribution;
    private ArmourController armourController;

    public playerAnimation playerAnimation;
    public Vector2 inputDirection;
    public int faceDirection;
 [Header("基本参数")]
    public float speed;
    public float jumpForce;

    public bool isNormalAttack;
    public bool isAccumulate;
    public bool isDefend;
    public float recoveryTime;

    public UnityEvent DefendEvent;        //振刀事件

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAttribution = GetComponent<playerAttribution>();
        playerAnimation = GetComponent<playerAnimation>();
        armourController = GetComponent<ArmourController>();
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
        if(!playerAttribution.isHurt) { 
        inputDirection = ctx.ReadValue<Vector2>();

        //人物翻转
         faceDirection = (int)transform.localScale.x;
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
    }
    public void Jump(InputAction.CallbackContext obj)
    {
        Debug.Log("jump");
        if (playerAttribution.isGround && obj.performed&& !playerAttribution.isHurt)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            GetComponent<AudioDefination>()?.PlayAudioClip();
        }
    }
    public void Down(InputAction.CallbackContext obj)
    {

        if (playerAttribution.isPlatform && obj.performed&& !playerAttribution.isHurt)
        {
            gameObject.layer = LayerMask.NameToLayer("Platform");
            Invoke("Recovery", recoveryTime);
        }
    }
    public void Recovery()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");

    }


    public void NormalAttack(InputAction.CallbackContext obj)
    {
        if (obj.performed&& !playerAttribution.isHurt)
        {
            isNormalAttack = true;
            playerAnimation.PlayerNormalAttack();
            playerAttribution.OnAttack?.Invoke(playerAttribution);
        }

    }

    public void AccumulateAttack(InputAction.CallbackContext obj)
    {
        if (obj.performed&&armourController.PlayerWeapon!= "empty"&& !playerAttribution.isHurt) { isAccumulate = true; }
        if (obj.canceled && armourController.PlayerWeapon != "empty" && !playerAttribution.isHurt)
        {
            playerAnimation.PlayerAccumulate();
            playerAttribution.OnAttack?.Invoke(playerAttribution);
        }
        // playerAnimation.PlayerAccumulate();

    }


    public void Defend(InputAction.CallbackContext obj)
    {
        if (obj.performed && armourController.PlayerWeapon != "empty" && !playerAttribution.isHurt)
        {
            isDefend = true;
            DefendEvent?.Invoke();
            playerAnimation.PlayerDefend();
        }
    }
}
// Update is called once per frame


