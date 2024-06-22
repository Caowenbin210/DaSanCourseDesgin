using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

// 资源管理类，单例模式实现
public class ResManager : BaseMgr<ResManager>
{
    public enum LoadState
    {
        // 空闲状态
        Idle,
        // 加载状态
        LoadScene,
        // 进度加载
        TickLoadSceneProgress,
    }

    private LoadState mCurrentLoadState = LoadState.Idle;
    private string mCurrentSceneName = null;
    //当前加载进度
    private AsyncOperation mCurrentSceneAsyncOperation;

    public delegate void OnLoadCallBack();
    private OnLoadCallBack SceneLoadedCallback;

    public void Update()
    {
        switch (mCurrentLoadState)
        {
            case LoadState.Idle:
                break;
            case LoadState.LoadScene:
                SceneManager.sceneLoaded += SceneManager_sceneLoaded;
                mCurrentSceneAsyncOperation = SceneManager.LoadSceneAsync(mCurrentSceneName, LoadSceneMode.Single);
                if (mCurrentSceneAsyncOperation == null)
                {
                    Debug.LogError("Failed to load scene,mCurrentSceneAsyncOperation is null");
                    mCurrentLoadState = LoadState.Idle;
                    return;
                }
                mCurrentLoadState = LoadState.TickLoadSceneProgress;
                break;
            case LoadState.TickLoadSceneProgress:
                Debug.Log("Loading scene " + mCurrentSceneName + " progress " + mCurrentSceneAsyncOperation.progress);
                break;
        }
    }

    // 异步加载场景
    public void LoadSceneAsync(string name, OnLoadCallBack callback)
    {
        // 判断当前是否正在加载场景
        if (mCurrentLoadState != LoadState.Idle)
        {
            Debug.LogError("One scene is Loading,scene name " + name);
            return;
        }

        mCurrentLoadState = LoadState.LoadScene;
        mCurrentSceneName = name;
        SceneLoadedCallback = callback;
    }

    //untiy 回调给我们的加载完成
    public void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        mCurrentLoadState = LoadState.Idle;

        if (SceneLoadedCallback != null)
        {
            SceneLoadedCallback();
        }
    }
}
