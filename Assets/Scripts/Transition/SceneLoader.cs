using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameSceneSO firstLoadScene;

    private void Awake()
    {
        Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
    }
}
