using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
    public VoidEventSO replayEvent;
    public VoidEventSO developerEvent;

    [Header("广播")]
    public VoidEventSO afterSceneLoadedEvent;

    [Header("场景")]
    public GameSceneSO firstLoadScene;
    public GameSceneSO menuScene;
    public GameSceneSO chooseCharacterScene;
    public GameSceneSO chooseMapScene;
    public GameSceneSO developerScene;
    public GameSceneSO endScene;
    private GameSceneSO currentLoadScene;
    private GameSceneSO sceneToLoad;
    private bool isLoading;

    public Canvas canvas;
    public float hintDuration;
    public GameObject generatePlayer;
    public GameObject weapon;
    private GameObject[] players;
    private GameObject[] endPlayers;
    private GameObject endPlayer;

    private void Start()
    {
        OnLoadRequestEvent(menuScene);
    }

    private void Update()
    {
        if (currentLoadScene != null && currentLoadScene.sceneType == SceneType.Map)
        {
            endPlayers = GameObject.FindGameObjectsWithTag("Player");
            if (endPlayers.Length == 1)
            {
                endPlayers[0].transform.SetParent(GameObject.Find("Character").transform);

                endPlayers[0].GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
                endPlayers[0].transform.position = new Vector2(1.65f, -0.33f);
                OnLoadRequestEvent(endScene);

            }
        }
    }

    private void OnEnable()
    {
        loadEventSO.loadRequestEvent += OnLoadRequestEvent;
        chooseMapEvent.OnEventRaised += ChooseMap;
        chooseCharacterEvent.OnEventRaised += ChooseCharacter;
        newGameEvent.OnEventRaised += NewGame;
        afterSceneLoadedEvent.OnEventRaised += EndEvent;
        replayEvent.OnEventRaised += ReStart;
        developerEvent.OnEventRaised += OpenDeveloperScene;
    }

    private void OnDisable()
    {
        loadEventSO.loadRequestEvent -= OnLoadRequestEvent;
        newGameEvent.OnEventRaised -= ChooseMap;
        chooseCharacterEvent.OnEventRaised -= ChooseCharacter;
        newGameEvent.OnEventRaised -= NewGame;
        afterSceneLoadedEvent.OnEventRaised -= EndEvent;
        replayEvent.OnEventRaised -= ReStart;
        developerEvent.OnEventRaised -= OpenDeveloperScene;

    }

    private void OpenDeveloperScene()
    {
        OnLoadRequestEvent(developerScene);
    }

    private void ChooseCharacter()
    {

        sceneToLoad = chooseCharacterScene;
        if (firstLoadScene != null)
        {
            OnLoadRequestEvent(sceneToLoad);
            generatePlayer.SetActive(true);
        }
        else
            StartCoroutine(FadeHint());
    }

    private IEnumerator FadeHint()
    {
        canvas.gameObject.SetActive(true);
        //等待一定秒数
        yield return new WaitForSeconds(hintDuration);
        canvas.gameObject.SetActive(false);
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
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerInput>().SwitchCurrentActionMap("GamePlay");
        }
        generatePlayer.SetActive(false);
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

        isLoading = false;

        //场景加载完成后事件
        afterSceneLoadedEvent.RaiseEvent();
    }

    private void EndEvent()
    {
        if (currentLoadScene.sceneType == SceneType.Map)
        {
            weapon.SetActive(true);
        }
        else
        {
            weapon.SetActive(false);
        }
    }

    private void ReStart()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Debug.Log("123");
            Destroy(player);
        }
        OnLoadRequestEvent(menuScene);
    }
}
