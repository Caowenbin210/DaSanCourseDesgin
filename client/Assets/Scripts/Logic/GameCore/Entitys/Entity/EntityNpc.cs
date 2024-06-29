using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// npc ʵ���࣬�̳�ʵ�����
public class EntityNpc : Entity
{
    public EntityNpc() : base()
    {

    }
    public override void OnCreate()
    {
        base.OnCreate();

        string pathPrefab = "Assets/Res/Npc/Peasant Nolant Blue.prefab";
        mBody = ResManager.Instance.InstantiateGameObject(pathPrefab);

        OnEnable();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        //mBody.transform.Rotate(new Vector3(0f, 1, 0f));
    }
}
