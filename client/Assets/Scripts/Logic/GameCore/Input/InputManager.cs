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

    // ע������¼�
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
