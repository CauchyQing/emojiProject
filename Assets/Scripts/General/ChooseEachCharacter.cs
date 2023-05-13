using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseEachCharacter : MonoBehaviour
{
    private GameObject[] players;
    public Vector2 position;
    public Vector3 rgb;
    private float x;

    private Color[] RGB = new Color[4];

    // private Color newColor;

    private void Start()
    {

        RGB[0] = new Color(0.0f, 0.0f, 0.0f);//ºÚ
        RGB[1] = new Color(1.0f, 1.0f, 1.0f);//°×
        RGB[2] = new Color(0.0f, 1.0f, 0.0f);//ÂÌ
        RGB[3] = new Color(1, 0.92f, 0.016f);//»Æ
    }


    public void ChooseCharacter()
    {
        int t = 0;
        x = -5.5f;
        position = new Vector2(x, -1.1f);
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            Debug.Log(t);
            player.transform.position = position;
            player.transform.GetChild(7).GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = RGB[t % 3];
            x = x + 3.72f;
            position = new Vector2(x, -1.1f);
            t++;

        }
    }
}
