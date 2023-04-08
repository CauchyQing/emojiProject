using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttribution : MonoBehaviour
{
    [Header("¼ì²â²ÎÊý")]
    public float checkRaduis;
    public Vector2 bottomOffset;
    public LayerMask groundLayer;
    public bool isGround;

    private void Update()
    {
        Check();
    }
    public void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
   
}
