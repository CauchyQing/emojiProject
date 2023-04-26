/*
 
 换装系统
 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

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
    public AnimatorOverrideController overrideController;
    public RuntimeAnimatorController originalContorller;

    private void Start()
    {
        spriteLibrary = GetComponent<SpriteLibrary>();
        playerController = GetComponent<playerController>();
        foreach (var resolver in FindObjectsOfType<SpriteResolver>())
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
    public void SetWeapon(string WeaponLabel)
    {
        Weapon.SetCategoryAndLabel(Weapon.GetCategory(), WeaponLabel);
        PlayerWeapon = WeaponLabel;
    }

    public void NextFace()
    {
        FaceIndex += 1;
        if (FaceIndex >= FaceLabels.Count)
        {
            FaceIndex = 0;
        }
        Face.SetCategoryAndLabel(Face.GetCategory(), FaceLabels[FaceIndex]);
    }

    public void NextShoe()
    {
        ShoeIndex += 1;
        if (ShoeIndex >= ShoeLabels.Count)
        {
            ShoeIndex = 0;
        }
        LShoe.SetCategoryAndLabel(LShoe.GetCategory(), ShoeLabels[ShoeIndex]);
        RShoe.SetCategoryAndLabel(RShoe.GetCategory(), ShoeLabels[ShoeIndex]);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon") && PlayerWeapon == "empty")
        {
            SetWeapon(other.GetComponent<WeaponAttri>().GetWeaponName());
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (PlayerWeapon == "empty" && playerController.isNormalAttack)
        {
            //originalContorller = animator.runtimeAnimatorController;
            animator.runtimeAnimatorController = overrideController;
            RHand.SetCategoryAndLabel(RHand.GetCategory(), "riot");
            LHand.SetCategoryAndLabel(LHand.GetCategory(), "riot");
        }
        else
        {
            animator.runtimeAnimatorController = originalContorller;
            RHand.SetCategoryAndLabel(RHand.GetCategory(), "hand");
            LHand.SetCategoryAndLabel(LHand.GetCategory(), "hand");
        }
    }

    /*武器掉落，需传入掉落武器的Transform*/
    public void WeaponsDrops(Transform transform)
    {
        SetWeapon("empty");
        WeaponManager.Instance.InstantiateWeapon(PlayerWeapon, transform);
    }
}
