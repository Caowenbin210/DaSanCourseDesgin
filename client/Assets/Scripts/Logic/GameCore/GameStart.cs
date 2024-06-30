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
            // ��ʼ��ui����
            UIManager.Instance.Init();

            // ���������ʼ��
            WorldManager.Instance.Init();

            // ʵ������ʼ��
            EntityManager.Instance.Init();

            // �������������ʼ��
            CameraManager.Instance.Init();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        // ��ʼ��¼
        LoginManager.Instance.StartLogin();
    }

    // ��Ϸѭ��
    private void Update()
    {
        try
        {
            UIManager.Instance.Update();

            ResManager.Instance.Update();

            WorldManager.Instance.Update();

            EntityManager.Instance.Update();

            InputManager.Instance.Update();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    // ��update �����
    private void LateUpdate()
    {
        try
        { 
            EntityManager.Instance.LateUpdate();

            CameraManager.Instance .LateUpdate();

            InputManager.Instance.LateUpdate();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    // �Թ̶�Ƶ�ʸ���
    private void FixedUpdate()
    {
        try
        {

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
            EntityManager.Instance.Exit();

            UIManager.Instance.Exit();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}
