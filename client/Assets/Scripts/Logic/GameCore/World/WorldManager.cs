using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : BaseMgr<WorldManager>
{
    // ����״̬��
    enum LoadState
    {
        // ��ʼ��״̬
        Init,
        // ���س���
        LoadScene,
        // ����״̬
        Update,
        // �ȴ�״̬
        Wait,
    }
    private LoadState mState;
    private string mLoadSceneName;

    // ��ʼ��
    public void Init()
    {

    }

    // �������
    public void Update()
    {
        if(mState == LoadState.Init)
        {

        }

        if(mState == LoadState.LoadScene)
        {
            EnterState(LoadState.Wait);
            ResManager.Instance.LoadSceneAsync(mLoadSceneName, ()=>
            {
                // �ȴ�����������ɺ󣬼�����ҵ�������
                LoadMainPlayer();
            });
        }
    }

    // ��������еļ��س���
    public void LoadScene(string name)
    {
        mLoadSceneName = name;

        EnterState(LoadState.LoadScene);
    }

    // �ı䵱ǰ״̬��
    private void EnterState(LoadState state)
    {
        this.mState = state;
    }

    private void LoadMainPlayer()
    {
        GameObject mainPlayr = ResManager.Instance.InstantiateGameObject("Assets/Res/Role/Peasant Nolant Blue(Free Version).prefab");
        if (mainPlayr == null)
        {

        }

        mainPlayr.transform.position = new Vector3(63, 22.5f, 43);
    }
}
