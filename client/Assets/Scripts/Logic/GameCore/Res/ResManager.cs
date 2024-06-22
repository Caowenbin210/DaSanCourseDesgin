using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

// ��Դ�����࣬����ģʽʵ��
public class ResManager : BaseMgr<ResManager>
{
    public enum LoadState
    {
        // ����״̬
        Idle,
        // ����״̬
        LoadScene,
        // ���ȼ���
        TickLoadSceneProgress,
    }

    private LoadState mCurrentLoadState = LoadState.Idle;
    private string mCurrentSceneName = null;
    //��ǰ���ؽ���
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

    // �첽���س���
    public void LoadSceneAsync(string name, OnLoadCallBack callback)
    {
        // �жϵ�ǰ�Ƿ����ڼ��س���
        if (mCurrentLoadState != LoadState.Idle)
        {
            Debug.LogError("One scene is Loading,scene name " + name);
            return;
        }

        mCurrentLoadState = LoadState.LoadScene;
        mCurrentSceneName = name;
        SceneLoadedCallback = callback;
    }

    //untiy �ص������ǵļ������
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
