using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using System;


public class SceneLoader : MonoBehaviour
{
    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;

    private GameSceneSO sceneToLoad;
    private GameSceneSO currentLoadedScene;

    private Vector3 positionToGo;

    private bool fadeScreen;

    public float fadeDuration;
    private void Awake()
    {
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        currentLoadedScene = firstLoadScene;
        currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);

    }

    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
    }

    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
    }

    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        sceneToLoad = locationToLoad;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;

        if (currentLoadedScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
            //Debug.Log(sceneToLoad.sceneReference.SubObjectName);
        }
    }


    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //TODO;实现渐入渐出
        }

        yield return new WaitForSeconds(fadeDuration);



        yield return currentLoadedScene.sceneReference.UnLoadScene();



        LoadNewScene();
    }

    private void LoadNewScene()
    {
        sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);


    }


}