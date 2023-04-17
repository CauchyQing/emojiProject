using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GetCameraElement : MonoBehaviour
{
    private CinemachineTargetGroup cinemachineTargetGroup;
    private GameObject[] players;

    private void Start()
    {
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();

        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            cinemachineTargetGroup.AddMember(player.transform, 1, 1);
        }
    }
}
