using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 输入设备管理器，处理鼠标和键盘
public class InputManager : BaseMgr<InputManager>
{
    // 枚举鼠标的按键
    public enum MouseButton
    {
        Left = 0,
        Right = 1,
        Middle = 2,
    }

    // 鼠标上一帧的位置
    private Vector2 oldMousePosition;
    // 鼠标当前帧的位置
    private Vector2 mousePosition;

    private Vector2 delataPosition;

    private Action<Vector2> mCSFunc;

    // 注册鼠标事件
    public void InitEvent(Action<Vector2> csFunc)
    {
        mCSFunc = csFunc;
    }


    public void Update()
    {
        OnMouseMove();
    }

    private void OnMouseMove()
    {
        mousePosition = Input.mousePosition;

        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            if (Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                oldMousePosition = mousePosition;
                delataPosition = Vector2.zero;
                return;
            }

            delataPosition = mousePosition - oldMousePosition;
            oldMousePosition = mousePosition;

            OnEvent();
        }
    }

    private void OnEvent()
    {
        mCSFunc(delataPosition);
    }
}
