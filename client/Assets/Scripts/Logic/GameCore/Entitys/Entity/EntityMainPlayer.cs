using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ʵ�壬�̳�ʵ�����
public class EntityMainPlayer : Entity
{
    public EntityMainPlayer() :base()
    {

    }

    public override void OnCreate()
    {
        base.OnCreate();

        string pathPrefab = "Assets/Res/Role/Male/Male B.prefab";
        mBody = ResManager.Instance.InstantiateGameObject(pathPrefab);

        OnEnable();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        InputManager.Instance.InitDragCallback(EventOnDragStart,EventOnDrag,EventOnDragEnd);
    }

    // ��ʼ�ƶ��Ļص�����
    private void EventOnDragStart()
    {
        StartMove();
    }

    private void EventOnDrag(float dx, float dz)
    {
        SetMoveDir(new Vector3(dx, 0, dz));
        SetStartPosition(GetPosition());
        SetStartMoveTime(Time.realtimeSinceStartup);
    }

    // �����ƶ��Ļص�
    private void EventOnDragEnd()
    {
        StopMove();
    }

    private void StartMove()
    {
        SetStartPosition(GetPosition());
        SetStartMoveTime(Time.realtimeSinceStartup);
        PlayAnimator("Male_Sword_Walk");
    }

    private void StopMove()
    {
        SetMoveDir(new Vector3(0, 0, 0));
        PlayAnimator("Male Idle");
    }
}
