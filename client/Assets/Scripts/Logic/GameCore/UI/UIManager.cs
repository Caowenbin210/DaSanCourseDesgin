using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseMgr<UIManager>
{
    //ui �����ĸ��ڵ�
    private Transform mUIRoot;
    //ui ���¼�
    private Transform mEventSystem;

    // ��ǰ��ʾ��ui
    private GameObject mShowUI;


    // ��ʼ������ȡui����������Ϊ�����٣��л�һֱ��ʾ
    public void Init()
    {
        if (mUIRoot == null)
        {
            mUIRoot = GameObject.Find("Canvas").transform;
            mEventSystem = GameObject.Find("EventSystem").transform;

            if (Application.isPlaying)
            {
                Object.DontDestroyOnLoad(mUIRoot.gameObject);
                Object.DontDestroyOnLoad(mEventSystem.gameObject);
            }
        }
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }

    // ��ʾui
    public void ShowUI(string uiPrefab)
    {
        mShowUI = ResManager.Instance.InstantiateGameObject(uiPrefab);
        if (mShowUI == null)
        {
            Debug.LogError("show " + uiPrefab + "error");
            return;
        }

        mShowUI.transform.SetParent(mUIRoot, false);
        mShowUI.transform.SetAsLastSibling();
    }

    // �ر�ui
    public void CloseUI()
    {
        if (mShowUI != null)
        {
            Object.Destroy(mShowUI.gameObject);
        }
    }
}
