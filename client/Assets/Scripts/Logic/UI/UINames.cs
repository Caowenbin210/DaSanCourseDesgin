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
        // ����ui�ĳ���Ϊ������ĳ���
        transform.forward = Camera.main.transform.forward;

        Name.text = mName;
    }

    public void SetName(string name)
    {
        mName = name;
    }
}
