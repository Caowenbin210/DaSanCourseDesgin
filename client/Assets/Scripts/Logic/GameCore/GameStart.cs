using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    // ��Ϸ�����ڼ�ʼ�ձ�����GameObject
    private GameObject mGo;

    // ��Ϸ�������п�ʼ�ĵط�
    private void Start()
    {   
        Debug.Log("Game Start");
        mGo = gameObject;

        // �л���������ʱ������
        DontDestroyOnLoad(mGo);

        try
        {

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        ResManager.Instance.LoadSceneAsync("rpgpp_lt_scene_1.0", null);
    }

    // ��Ϸѭ��
    private void Update()
    {
        try
        {
            ResManager.Instance.Update();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    // ��Ϸ�˳�
    // �����ǣ��˳���Ϸʱ������Դ
    private void OnApplicationQuit()
    {
        Debug.Log("Game Quit");

        try
        {

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}
