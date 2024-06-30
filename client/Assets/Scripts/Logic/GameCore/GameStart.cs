using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    // 游戏运行期间始终保留的GameObject
    private GameObject mGo;

    // 游戏启动运行开始的地方
    private void Start()
    {   
        Debug.Log("Game Start");
        mGo = gameObject;

        // 切换场景加载时不销毁
        DontDestroyOnLoad(mGo);

        try
        {
            // 初始化ui管理
            UIManager.Instance.Init();

            // 场景世界初始化
            WorldManager.Instance.Init();

            // 实体管理初始化
            EntityManager.Instance.Init();

            // 摄像机管理器初始化
            CameraManager.Instance.Init();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        // 开始登录
        LoginManager.Instance.StartLogin();
    }

    // 游戏循环
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

    // 在update 后更新
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

    // 以固定频率更新
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

    // 游戏退出
    // 作用是，退出游戏时销毁资源
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
