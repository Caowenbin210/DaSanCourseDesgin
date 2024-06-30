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
                // ͨ���ص��ķ�ʽ�������� �����������
                SceneManager.sceneLoaded += SceneManager_sceneLoaded;

                // ͨ���첽��ʽ���س���
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

    private void LoadPlayer()
    {

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

    // ������Դ
    public Object LoadResource(string resPath)
    {
#if UNITY_EDITOR
        // ֻ����unity �� editor �µ���Դ���ط�ʽ��ֻ�ǴӴ��̼��ص��ڴ�
        Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(resPath);
        return obj;
#else
        // 
        �����ļ��ط�ʽ
#endif
    }
    
    // ʵ������ʾһ����Դ
    public GameObject InstantiateGameObject(string resPath)
    {
        GameObject obj = LoadResource(resPath) as GameObject;
        if (obj != null)
        {
            // ʵ������Դ
            GameObject go = GameObject.Instantiate<GameObject>(obj);
            if (go == null)
            {
                Debug.LogError("game instactiate failed " + resPath);
                return null;
            }

            // ��ʾ��Դ
            go.SetActive(true);
            return go;
        }
        else
        {
            return null;
        }
    }

    // ����ͼƬ
    public Sprite LoadImage(string resPath)
    {
        Sprite obj = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(resPath);
        return obj;
    }
}
