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

    private long mObjectID;

    // �����
    private GameObject mCameraObj;

    // ��ʼ��
    public void Init()
    {
        mObjectID = 1;
        EnterState(LoadState.Init);
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
                GameObject gameObject = GameObject.Find("Main Camera");
                if(gameObject == null)
                {
                    Debug.LogError("not find main camera");
                    return;
                }

                mCameraObj = gameObject;


                UIManager.Instance.ShowUI(UIDefine.UI_MAIN);

                // �ȴ�����������ɺ󣬼�����ҵ�������
                LoadMainPlayer();

                LoadNpc();
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
        Vector3 mainPlayerPos = new Vector3(63, 22.5f, 43);
        EntityMainPlayer mainPlayer = (EntityMainPlayer)EntityManager.Instance.CreateEntity(eEntityType.PLAYER_MAIN, 10000, mainPlayerPos);
        EntityManager.Instance.MainPlayer = mainPlayer;

        //mainPlayer.PlayAnimator("metarig|Idle");
        mainPlayer.PlayerAnimation("WK_heavy_infantry_05_combat_idle");

        mainPlayer.SetMoveSpeed(8.0f);
        mainPlayer.SetName("ԭ�����");
        mainPlayer.SetHp(200);
        mainPlayer.SetMoney(200);
        mainPlayer.SetMaxHp(500);

        CameraManager.Instance.InitCamera(mCameraObj.transform, mainPlayer.GetTransform());
    }

    // ���س����е�npc
    private void LoadNpc()
    {
        // shop
        Vector3 npcPostion = new Vector3(56.3f, 22.24f, 43.8f);
        EntityNpc npc = (EntityNpc)EntityManager.Instance.CreateEntity(eEntityType.NPC, GeneraterObjectID(), npcPostion);
        npc.mBody.GetComponent<Npc>().NpcID = 1001;
        npc.PlayAnimator("metarig|Idle");
        npc.SetForward(new Vector3(0, 90, 0));
        npc.SetName("��������");
        npc.SetHp(100);

        // talk
        npcPostion = new Vector3(67.2f, 22.24f, 62.0f);
        EntityNpc npc2 = (EntityNpc)EntityManager.Instance.CreateEntity(eEntityType.NPC, GeneraterObjectID(), npcPostion);
        npc2.mBody.GetComponent<Npc>().NpcID = 1002;
        npc2.PlayAnimator("metarig|Idle");
        npc2.SetForward(new Vector3(0, 140, 0));
        npc2.SetName("����");
        npc2.SetHp(100);
    }

    // ���������id
    private long GeneraterObjectID()
    {
        mObjectID++;
        return mObjectID;
    }
}
