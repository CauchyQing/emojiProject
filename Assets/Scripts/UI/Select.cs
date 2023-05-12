using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    public ArmourController armourController;

    public void Awake()
    {
        armourController = GetComponent<ArmourController>();
    }

    private void Update()
    {
        
    }
}
