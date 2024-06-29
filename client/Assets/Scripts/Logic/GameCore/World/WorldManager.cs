using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : BaseMgr<WorldManager>
{
    // 场景状态机
    enum LoadState
    {
        // 初始化状态
        Init,
        // 加载场景
        LoadScene,
        // 更新状态
        Update,
        // 等待状态
        Wait,
    }
    private LoadState mState;
    private string mLoadSceneName;

    private long mObjectID;

    // 摄像机
    private GameObject mCameraObj;

    // 初始化
    public void Init()
    {
        mObjectID = 1;
        EnterState(LoadState.Init);
    }

    // 世界更新
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
                GameObject gameObject = GameObject.Find("Main Camera");
                if(gameObject == null)
                {
                    Debug.LogError("not find main camera");
                    return;
                }

                mCameraObj = gameObject;

                // 等待场景加载完成后，加载玩家到场景中
                LoadMainPlayer();

                LoadNpc();
            });
        }
    }

    // 世界管理中的加载场景
    public void LoadScene(string name)
    {
        mLoadSceneName = name;

        EnterState(LoadState.LoadScene);
    }

    // 改变当前状态机
    private void EnterState(LoadState state)
    {
        this.mState = state;
    }

    private void LoadMainPlayer()
    {
        Vector3 mainPlayerPos = new Vector3(63, 22.5f, 43);
        EntityMainPlayer mainPlayer = (EntityMainPlayer)EntityManager.Instance.CreateEntity(eEntityType.PLAYER_MAIN, 10000, mainPlayerPos);
        //mainPlayer.PlayAnimation("metarig|Idle");
        mainPlayer.PlayerAnimation("WK_heavy_infantry_05_combat_idle");
        CameraManager.Instance.InitCamera(mCameraObj.transform, mainPlayer.GetTransform());

    }

    // 加载场景中的npc
    private void LoadNpc()
    {
        Vector3 npcPostion = new Vector3(56.3f, 22.23f, 43.8f);
        EntityNpc npc = (EntityNpc)EntityManager.Instance.CreateEntity(eEntityType.NPC, GeneraterObjectID(), npcPostion);
        npc.PlayAnimation("metarig|Idle");
        npc.SetForward(new Vector3(90, 0, 0));
        npc.SetName("神秘商人");
    }

    // 生成物体得id
    private long GeneraterObjectID()
    {
        mObjectID++;
        return mObjectID;
    }
}
