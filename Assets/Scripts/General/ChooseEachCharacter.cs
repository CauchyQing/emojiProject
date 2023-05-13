using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseEachCharacter : MonoBehaviour
{
    private GameObject[] players;
    public Vector2 position;
    public Vector3 rgb;
    private float x;
    private Color[] RGB;

    private void Start()
    {
        //RGB ={ Color.white,Color.green  };
    }

    public void ChooseCharacter()
    {
        x = -5.5f;
        position = new Vector2(x, -1.1f);
        players = GameObject.FindGameObjectsWithTag("Player");
        Color newColor = new Color(0.3f, 0.4f, 0.6f, 1f);
        foreach (GameObject player in players)
        {
            player.transform.position = position;
            player.transform.GetChild(7).GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = newColor;
            x = x + 3.72f;
            position = new Vector2(x, -1.1f);
        }
    }
}
