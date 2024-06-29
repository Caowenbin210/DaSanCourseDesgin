using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����豸���������������ͼ���
public class InputManager : BaseMgr<InputManager>
{
    // ö�����İ���
    public enum MouseButton
    {
        Left = 0,
        Right = 1,
        Middle = 2,
    }

    // �����һ֡��λ��
    private Vector2 oldMousePosition;
    // ��굱ǰ֡��λ��
    private Vector2 mousePosition;

    private Vector2 delataPosition;

    private Action<Vector2> mCSFunc;

    // �����¼�
    public delegate void OnJoystickDragStart();
    public delegate void OnJoystickDrag(float dx, float dy);
    public delegate void OnJoystickDragEnd();

    // ��ק�ص�
    private OnJoystickDragStart mOnDragStart;
    private OnJoystickDrag mOnDrag;
    private OnJoystickDragEnd mOnDragEnd;

    // ��ǰ�����Ƿ񱻰���
    private bool mKeyboardPressing = false;

    // ע������¼�
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

    // ������̰���
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
