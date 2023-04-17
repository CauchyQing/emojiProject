/*
 
 »»×°ÏµÍ³
 
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

    private int FaceIndex = 0;
    private int ShoeIndex = 0;

    [HideInInspector]
    public SpriteLibrary spriteLibrary;

    private void Start()
    {
        spriteLibrary = GetComponent<SpriteLibrary>();
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
            }
        }
    }
    public void SetWeapon(string WeaponLabel)
    {
        Weapon.SetCategoryAndLabel(Weapon.GetCategory(), WeaponLabel);
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

    //float t = 0f;
    //private void Update()
    //{
    //    t += Time.deltaTime;
    //    if (t > 2.0f)
    //    {
    //        NextFace();
    //        NextShoe();
    //        t = 0f;
    //    }
    //}

}
