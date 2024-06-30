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
    private Dictionary<int, GameObject> mShowUI = new Dictionary<int, GameObject>();


    // 初始化，获取ui画布，设置为不销毁，切换一直显示
    public void Init()
    {
        UIDefine.Init();
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
    public void ShowUI(int ui)
    {
        if (mShowUI.ContainsKey(ui))
        {
            Debug.LogError("ui " + ui.ToString()+"is show");
            return;
        }

        string prefab = UIDefine.GetUI(ui);
        GameObject uiObj = ResManager.Instance.InstantiateGameObject(prefab);
        if (mShowUI == null)
        {
            Debug.LogError("show " + prefab + "error");
            return;
        }

        uiObj.transform.SetParent(mUIRoot, false);
        uiObj.transform.SetAsLastSibling();

        mShowUI.Add(ui, uiObj);
    }

    // 关闭ui
    public void CloseUI(int ui)
    {
        if (!mShowUI.ContainsKey(ui))
        {
            Debug.LogError("not find ui "+ui.ToString());
            return;
        }
        GameObject uiObj = mShowUI[ui];
        if (mShowUI != null)
        {
            Object.Destroy(uiObj);
        }

        mShowUI.Remove(ui);
    }
}
