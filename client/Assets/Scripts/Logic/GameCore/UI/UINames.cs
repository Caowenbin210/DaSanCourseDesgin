using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINames : MonoBehaviour
{
    public Text Name;

    private string mName;

    private void Update()
    {
        // 设置ui的朝向为摄像机的朝向
        transform.forward = Camera.main.transform.forward;

        Name.text = mName;
    }

    public void SetName(string name)
    {
        mName = name;
    }
}
