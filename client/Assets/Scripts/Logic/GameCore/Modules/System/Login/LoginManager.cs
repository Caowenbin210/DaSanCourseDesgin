using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : BaseMgr<LoginManager>
{
    private const string UIPrefab = "Assets/Res/UI/Prefab/UI_Login.prefab";

    public void StartLogin()
    {
        UIManager.Instance.ShowUI(UIPrefab);
    }

    public void StopLogin()
    {
        UIManager.Instance.CloseUI();
    }

    public void OnLogin()
    {
        WorldManager.Instance.LoadScene("rpgpp_lt_scene_1.0");
        StopLogin();
    }
}
