using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseMgr<UIManager>
{
    //ui 画布的根节点
    private Transform mUIRoot;
    //ui 的事件
    private Transform mEventSystem;

    // 当前显示的ui
    private GameObject mShowUI;


    // 初始化，获取ui画布，设置为不销毁，切换一直显示
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

    // 显示ui
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

    // 关闭ui
    public void CloseUI()
    {
        if (mShowUI != null)
        {
            Object.Destroy(mShowUI.gameObject);
        }
    }
}
