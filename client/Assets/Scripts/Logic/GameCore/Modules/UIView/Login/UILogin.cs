using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILogin : MonoBehaviour
{
    public void OnLogin()
    {
        LoginManager.Instance.OnLogin();
    }
}
