using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWhichMap : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public GameSceneSO redGate;
    public GameSceneSO castle;

    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO redGateEvent;
    public VoidEventSO castleEvent;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        redGateEvent.OnEventRaised += ChooseRedGate;
        castleEvent.OnEventRaised += ChooseCastle;
    }

    private void OnDisable()
    {
        redGateEvent.OnEventRaised -= ChooseRedGate;
        castleEvent.OnEventRaised -= ChooseCastle;
    }

    public void ChooseRedGate()
    {
        sceneLoader.firstLoadScene = redGate;
    }

    public void ChooseCastle()
    {
        sceneLoader.firstLoadScene = castle;
    }
}
