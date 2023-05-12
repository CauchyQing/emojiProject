using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //public Transform playerTrans;
    /*    public Vector3 firstPosition;
        public Vector3 selectPosition;*/

    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;
    public VoidEventSO newGameEvent;
    public VoidEventSO chooseCharacterEvent;
    public VoidEventSO chooseMapEvent;

    [Header("广播")]
    public VoidEventSO afterSceneLoadedEvent;

    [Header("场景")]
    public GameSceneSO firstLoadScene;
    public GameSceneSO menuScene;
    public GameSceneSO chooseCharacterScene;
    public GameSceneSO chooseMapScene;
    private GameSceneSO currentLoadScene;
    private GameSceneSO sceneToLoad;
    private bool isLoading;

    private void Start()
    {
        OnLoadRequestEvent(menuScene);
    }

    private void OnEnable()
    {
        loadEventSO.loadRequestEvent += OnLoadRequestEvent;
        chooseMapEvent.OnEventRaised += ChooseMap;
        chooseCharacterEvent.OnEventRaised += ChooseCharacter;
        newGameEvent.OnEventRaised += NewGame;
    }

    private void OnDisable()
    {
        loadEventSO.loadRequestEvent -= OnLoadRequestEvent;
        newGameEvent.OnEventRaised -= ChooseMap;
        chooseCharacterEvent.OnEventRaised -= ChooseCharacter;
        newGameEvent.OnEventRaised -= NewGame;
    }

    private void ChooseCharacter()
    {
        sceneToLoad = chooseCharacterScene;
        OnLoadRequestEvent(sceneToLoad);
    }

    private void ChooseMap()
    {
        sceneToLoad = chooseMapScene;
        OnLoadRequestEvent(sceneToLoad);
    }

    private void NewGame()
    {
        sceneToLoad = firstLoadScene;
        OnLoadRequestEvent(sceneToLoad);
    }

    /// <summary>
    /// 场景加载事件请求
    /// </summary>
    /// <param name="locationToLoad"></param>
    private void OnLoadRequestEvent(GameSceneSO locationToLoad)
    {
        if (isLoading)
            return;
        isLoading = true;
        sceneToLoad = locationToLoad;
        if (currentLoadScene != null)
            StartCoroutine(UnLoadPreviousScene());
        else
            LoadNewScene();
    }

    private IEnumerator UnLoadPreviousScene()
    {
        yield return currentLoadScene.sceneReference.UnLoadScene();

        //playerTrans.gameObject.SetActive(false);

        LoadNewScene();
    }

    private void LoadNewScene()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
    }

    /// <summary>
    /// 场景加载完成后
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoadCompleted(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadScene = sceneToLoad;

        /*        playerTrans.position = positionToGo;

                playerTrans.gameObject.SetActive(true);

                if (fadeScreen)
                {
                    //TODO:
                }*/

        isLoading = false;

        //场景加载完成后事件
        afterSceneLoadedEvent.RaiseEvent();
    }
}
