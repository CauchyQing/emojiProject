/*
 
 换装系统
 
 */



using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.InputSystem;
public class ArmourController : MonoBehaviour
{
    //public List<SpriteResolver> spriteResolvers = new List<SpriteResolver>();
    public List<string> FaceLabels;
    public List<string> ShoeLabels;

    private SpriteResolver Weapon;
    private SpriteResolver Face;
    private SpriteResolver RShoe;
    private SpriteResolver LShoe;
    private SpriteResolver RHand;
    private SpriteResolver LHand;
    private int FaceIndex = 0;
    private int ShoeIndex = 0;

    [HideInInspector]
    public SpriteLibrary spriteLibrary;

    public string PlayerWeapon = "weapon";//角色当前手持的武器

    private playerController playerController;

    public Animator animator;
    public AnimatorOverrideController GuitarController;
    public AnimatorOverrideController EmptyController;
    public RuntimeAnimatorController originalContorller;

    private void Awake()
    {
        spriteLibrary = GetComponent<SpriteLibrary>();
        playerController = GetComponent<playerController>();
        foreach (var resolver in GetComponentsInChildren<SpriteResolver>())
        {
            var category = resolver.GetCategory();
            switch (category)
            {
                case "Weapon":
                    Weapon = resolver;
                    break;
                case "Face":
                    Face = resolver;
                    break;
                case "RShoe":
                    RShoe = resolver;
                    break;
                case "LShoe":
                    LShoe = resolver;
                    break;
                case "RHand":
                    RHand = resolver;
                    break;
                case "LHand":
                    LHand = resolver;
                    break;
            }
        }
    }
    private void Start()
    {

    }
    public void SetWeapon(string WeaponLabel)
    {
        if (Weapon != null)
        {
            Weapon.SetCategoryAndLabel(Weapon.GetCategory(), WeaponLabel);
            PlayerWeapon = WeaponLabel;
            UpdateAnimtorContoller();
        }
    }

    public void NextFace(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            FaceIndex += 1;
            if (FaceIndex >= FaceLabels.Count)
            {
                FaceIndex = 0;
            }
            Face.SetCategoryAndLabel(Face.GetCategory(), FaceLabels[FaceIndex]);
        }
    }

    public void NextShoe(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            ShoeIndex += 1;
            if (ShoeIndex >= ShoeLabels.Count)
            {
                ShoeIndex = 0;
            }
            LShoe.SetCategoryAndLabel(LShoe.GetCategory(), ShoeLabels[ShoeIndex]);
            RShoe.SetCategoryAndLabel(RShoe.GetCategory(), ShoeLabels[ShoeIndex]);
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{

    //    if (other.gameObject.CompareTag("Weapon") && PlayerWeapon == "empty")
    //    {
    //        SetWeapon(other.GetComponent<WeaponAttri>().GetWeaponName());
    //        Destroy(other.gameObject);
    //    }
    //}

    private List<Collider2D> otherWeapons = new List<Collider2D>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            otherWeapons.Add(other);
        }
        //if (other.gameObject.CompareTag("Weapon") && )
        //{
        //    SetWeapon(other.GetComponent<WeaponAttri>().GetWeaponName());
        //    Destroy(other.gameObject);
        //}
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            otherWeapons.Remove(other);
        }
    }
    public void ChageWeapon(InputAction.CallbackContext obj)
    {
        if (obj.performed && (otherWeapons.Count != 0))
        {
            if (PlayerWeapon != "empty")
            {
                WeaponsDrops(transform);
            }
            var coll = otherWeapons[0];
            SetWeapon(coll.GetComponent<WeaponAttri>().GetWeaponName());
            Destroy(coll.gameObject);
            otherWeapons.Remove(coll);
        }
    }

    private void Update()
    {
        if (PlayerWeapon == "empty" && playerController.isNormalAttack)
        {
            //originalContorller = animator.runtimeAnimatorController;
            animator.runtimeAnimatorController = EmptyController;
            RHand.SetCategoryAndLabel(RHand.GetCategory(), "riot");
            LHand.SetCategoryAndLabel(LHand.GetCategory(), "riot");
        }
        else if (RHand.GetLabel() != "hand")
        {
            UpdateAnimtorContoller();
            RHand.SetCategoryAndLabel(RHand.GetCategory(), "hand");
            LHand.SetCategoryAndLabel(LHand.GetCategory(), "hand");
        }

    }

    /*武器掉落，需传入掉落武器的Transform*/
    public void WeaponsDrops(Transform transform)
    {
        WeaponManager.Instance.InstantiateDropWeapon(PlayerWeapon, transform);
        SetWeapon("empty");
    }

    private void UpdateAnimtorContoller()
    {
        Debug.Log("Plyer Weapon----> " + PlayerWeapon);
        if (PlayerWeapon == "guitar")
        {
            Debug.Log("Update Controller----> " + PlayerWeapon);
            Debug.Log(animator.runtimeAnimatorController);

            animator.runtimeAnimatorController = GuitarController;
        }
        else
        {
            Debug.Log("Update Controller----> " + PlayerWeapon);

            animator.runtimeAnimatorController = originalContorller;
        }
    }
}
