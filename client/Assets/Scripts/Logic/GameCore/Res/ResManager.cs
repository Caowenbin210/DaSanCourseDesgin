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
                // 通过回调的方式告诉我们 场景加载完成
                SceneManager.sceneLoaded += SceneManager_sceneLoaded;

                // 通过异步方式加载场景
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

    private void LoadPlayer()
    {

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

    // 加载资源
    public Object LoadResource(string resPath)
    {
#if UNITY_EDITOR
        // 只能在unity 的 editor 下的资源加载方式，只是从磁盘加载到内存
        Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(resPath);
        return obj;
#else
        // 
        其他的加载方式
#endif
    }
    
    // 实例化显示一个资源
    public GameObject InstantiateGameObject(string resPath)
    {
        GameObject obj = LoadResource(resPath) as GameObject;
        if (obj != null)
        {
            // 实例化资源
            GameObject go = GameObject.Instantiate<GameObject>(obj);
            if (go == null)
            {
                Debug.LogError("game instactiate failed " + resPath);
                return null;
            }

            // 显示资源
            go.SetActive(true);
            return go;
        }
        else
        {
            return null;
        }
    }

    // 加载图片
    public Sprite LoadImage(string resPath)
    {
        Sprite obj = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(resPath);
        return obj;
    }
}
