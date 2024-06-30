using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : BaseMgr<LoginManager>
{
    public void StartLogin()
    {
        UIManager.Instance.ShowUI(UIDefine.UI_LOGIN);
    }

    public void StopLogin()
    {
        UIManager.Instance.CloseUI(UIDefine.UI_LOGIN);
    }

    public void OnLogin()
    {
        WorldManager.Instance.LoadScene("rpgpp_lt_scene_1.0");
        StopLogin();
    }
}
