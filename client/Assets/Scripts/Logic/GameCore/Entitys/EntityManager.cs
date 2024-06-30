using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 实体管理
public class EntityManager:BaseMgr<EntityManager>
{
    // 存储了所有已经创建出来的实体
    private Dictionary<long, Entity> mEntitys= new Dictionary<long, Entity>();

    public EntityMainPlayer MainPlayer {  get; set; }

    // 初始化
    public void Init()
    {

    }

    // 逻辑更新
    public void Update()
    {
        float deltaTime = Time.deltaTime;
        foreach (var entity in mEntitys)
        {
            entity.Value.OnUpdate(deltaTime);
        }
    }

    // 延后更新
    public void LateUpdate()
    {

    }

    // 退出
    public void Exit()
    {

    }

    // 创建实体
    public Entity CreateEntity(eEntityType entityType,long entityID,Vector3 pos)
    {
        // 判断id是否存在，如果存在什么也不做
        if (ExistEntity(entityID))
        {
            Debug.LogError("Create entity error, entity id " + entityID + " exist");
            return null;
        }

        Entity entity = EntityFactory.Instance.CreateEntity(entityType, entityID);
        AddEntity(entity);

        entity.SetPosition(pos);

        return entity;
    }

    private void AddEntity(Entity entity)
    {
        entity.OnCreate();
        mEntitys.Add(entity.GetID(), entity);
    }

    // 判断一个id的实体是否存在
    private bool ExistEntity(long entityID)
    {
        if (mEntitys.ContainsKey(entityID))
        {
            return true;
        }
        return false;
    }
}
