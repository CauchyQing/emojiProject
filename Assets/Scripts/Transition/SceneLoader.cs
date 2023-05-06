using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("�¼�����")]
    public SceneLoadEventSO loadEventSO;

    public GameSceneSO firstLoadScene;

    private GameSceneSO currentLoadScene;
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;

    public float fadeDuration;

    private void Awake()
    {
        Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        currentLoadScene = firstLoadScene;
    }

    private void OnEnable()
    {
        loadEventSO.loadRequestEvent += OnLoadRequestEvent;
    }

    private void OnDisable()
    {
        loadEventSO.loadRequestEvent -= OnLoadRequestEvent;

    }

    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        sceneToLoad = locationToLoad;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;


    }

    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //TODO:ʵ�ֽ��뽥��
        }

        //�ȴ�һ������
        yield return new WaitForSeconds(fadeDuration);

        if (currentLoadScene != null)
            currentLoadScene.sceneReference.UnLoadScene();
    }

    //private void Load
}
