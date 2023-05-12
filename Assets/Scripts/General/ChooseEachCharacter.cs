using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseEachCharacter : MonoBehaviour
{
    private GameObject[] players;
    public Vector2 position;
    private float x;

    public void ChooseCharacter()
    {
        x = -5.5f;
        position = new Vector2(x, -1.1f);
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.transform.position = position;
            x = x + 3.72f;
            position = new Vector2(x, -1.1f);
        }
    }
}
