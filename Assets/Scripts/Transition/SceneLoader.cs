using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Transform playerTrans;
    public Vector3 firstPosition;
    public Vector3 selectPosition;

    [Header("�¼�����")]
    public SceneLoadEventSO loadEventSO;
    public VoidEventSO newGameEvent;

    [Header("�㲥")]
    public VoidEventSO afterSceneLoadedEvent;

    [Header("����")]
    public GameSceneSO firstLoadScene;
    public GameSceneSO menuScene;
    private GameSceneSO currentLoadScene;
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;
    private bool isLoading;
    public float fadeDuration;

    //TODO:
    private void Start()
    {
        //NewGame();position��Ҫ��
        OnLoadRequestEvent(menuScene, selectPosition, true);

    }

    private void OnEnable()
    {
        loadEventSO.loadRequestEvent += OnLoadRequestEvent;
        newGameEvent.OnEventRaised += NewGame;
    }

    private void OnDisable()
    {
        loadEventSO.loadRequestEvent -= OnLoadRequestEvent;
        newGameEvent.OnEventRaised -= NewGame;
    }

    private void NewGame()
    {
        sceneToLoad = firstLoadScene;
        OnLoadRequestEvent(sceneToLoad, firstPosition, true);
    }

    /// <summary>
    /// ���������¼�����
    /// </summary>
    /// <param name="locationToLoad"></param>
    /// <param name="posToGo"></param>
    /// <param name="fadeScreen"></param>
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        if (isLoading)
            return;
        isLoading = true;
        sceneToLoad = locationToLoad;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;
        if (currentLoadScene != null)
            StartCoroutine(UnLoadPreviousScene());
        else
            LoadNewScene();
    }

    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //TODO:ʵ�ֽ��뽥��
        }

        //�ȴ�һ������
        yield return new WaitForSeconds(fadeDuration);


        yield return currentLoadScene.sceneReference.UnLoadScene();

        playerTrans.gameObject.SetActive(false);

        LoadNewScene();
    }

    private void LoadNewScene()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
    }

    /// <summary>
    /// ����������ɺ�
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoadCompleted(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadScene = sceneToLoad;

        playerTrans.position = positionToGo;

        playerTrans.gameObject.SetActive(true);

        if (fadeScreen)
        {
            //TODO:
        }

        isLoading = false;

        //����������ɺ��¼�
        afterSceneLoadedEvent.RaiseEvent();
    }
}
