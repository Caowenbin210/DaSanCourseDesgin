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
    private Dictionary<int, GameObject> mShowUI = new Dictionary<int, GameObject>();


    // ��ʼ������ȡui����������Ϊ�����٣��л�һֱ��ʾ
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

    // ��ʾui
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

    // �ر�ui
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
