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

    // 键盘事件
    public delegate void OnJoystickDragStart();
    public delegate void OnJoystickDrag(float dx, float dy);
    public delegate void OnJoystickDragEnd();

    // 拖拽回调
    private OnJoystickDragStart mOnDragStart;
    private OnJoystickDrag mOnDrag;
    private OnJoystickDragEnd mOnDragEnd;

    // 当前键盘是否被按下
    private bool mKeyboardPressing = false;

    // 注册鼠标事件
    public void InitEvent(Action<Vector2> csFunc)
    {
        mCSFunc = csFunc;
    }

    public void InitDragCallback(OnJoystickDragStart onDragStart,OnJoystickDrag onDrag,OnJoystickDragEnd onDragEnd)
    {
        mOnDragStart = onDragStart;
        mOnDrag = onDrag;
        mOnDragEnd = onDragEnd;
    }


    public void Update()
    {
        
    }

    public void LateUpdate()
    {
        UpdateKeyboard();
        UpdateMouse();
    }

    private void UpdateMouse()
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

    private void UpdateKeyboard()
    {
        if (mKeyboardPressing)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.A)) OnDrag(Vector2.up + Vector2.left);
                if (Input.GetKey(KeyCode.D)) OnDrag(Vector2.up + Vector2.right);
                else OnDrag(Vector2.up);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.A)) OnDrag(Vector2.down + Vector2.left);
                if (Input.GetKey(KeyCode.D)) OnDrag(Vector2.down + Vector2.right);
                else OnDrag(Vector2.down);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                OnDrag(Vector2.left);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                OnDrag(Vector2.right);
            }
            else
            {
                mKeyboardPressing = false;
                OnPress(false);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.W)) mKeyboardPressing = true;
            if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.W)) mKeyboardPressing = true;
            if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.W)) mKeyboardPressing = true;
            if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.W)) mKeyboardPressing = true;
            if (mKeyboardPressing) OnPress(true);
        }

    }
    private void OnDrag(Vector2 delta)
    {
        Vector3 moveDir = new Vector3(delta.x, 0, delta.y);
        moveDir.Normalize();
        mOnDrag(moveDir.x, moveDir.z);
    }

    // 处理键盘按下
    private void OnPress(bool press)
    {
        if (press)
        {
            mOnDragStart();
        }
        else
        {
            mOnDragEnd();
        }
    }
}
