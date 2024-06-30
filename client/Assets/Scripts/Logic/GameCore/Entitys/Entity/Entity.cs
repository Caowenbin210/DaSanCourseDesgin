using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 所有实体的基类，游戏中所有的物体都继承此类
public class Entity
{
    // 实体的类型
    protected eEntityType mEntityType;

    // 实体的唯一Id
    private long mId;

    private int mHp;
    private int mMaxHp;
    private string mName;

    // 实体的可见模型
    public GameObject mBody;

    // 实体移动相关
    // 移动的方向
    private Vector3 mMoveDir = new Vector3();
    // 移动的速度
    private float mMoveSpeed;
    // 开始移动的时间
    private float mStartMoveTime;
    // 开始移动的坐标点
    private Vector3 mStartPosition;


    public void SetID(long id)
    {
        mId = id;
    }

    public long GetID()
    {
        return mId;
    }

    public void SetHp(int hp)
    {
        mHp = hp;
    }

    public int GetHp()
    {
        return (mHp);
    }

    public string GetName()
    {
        return mName;
    }

    public void SetMaxHp(int maxHp)
    {
        mMaxHp = maxHp;
    }

    public int GetMaxHp()
    {
        return mMaxHp;
    }

    // 设置实体的名字
    public void SetName(string name)
    {
        GameObject nameBar = mBody.transform.Find("NameBar").gameObject;
        if (nameBar == null)
        {
            return;
        }

        UINames uiName = nameBar.GetComponent<UINames>();
        if (uiName == null)
        {
            return;
        }
        uiName.SetName(name);

        mName = name;
    }

    // 设置实体的位置坐标
    public void SetPosition(Vector3 position)
    {
        mBody.transform.position = position;
    }

    public Vector3 GetPosition()
    {
        return mBody.transform.position;
    }

    // 设置实体的朝向
    public void SetForward(Vector3 forward)
    {
        mBody.transform.eulerAngles = forward;
    }

    // 设置移动速度
    public void SetMoveSpeed(float speed)
    {
        mMoveSpeed = speed;
    }

    // 设置移动方向
    public void SetMoveDir(Vector3 dir)
    {
        mMoveDir = dir;
    }

    public Vector3 GetMoveDir()
    {
        return mMoveDir;
    }

    // 设置开始移动的点
    public void SetStartPosition(Vector3 pos)
    {
        mStartPosition = pos;
    }

    // 设置开始移动的时间
    public void SetStartMoveTime(float time)
    {
        mStartMoveTime = time;
    }

    // 播放实体的动作 
    public void PlayAnimator(string state)
    {
        Animator animator = mBody.GetComponent<Animator>();
        if (animator == null)
        {
            return;
        }

        animator.Play(state);
    }

    public void PlayerAnimation(string state)
    {
        Animation animation = mBody.GetComponent<Animation>();
        if (animation == null)
        {
            return;
        }
        animation.CrossFade(state);

    }

    // 当实体创建出来后调用
    public virtual void OnCreate()
    {

    }

    //当销毁一个实体时调用
    public virtual void OnDestory()
    {

    }

    // 实体启用
    public virtual void OnEnable()
    {

    }

    // 禁用实体
    public virtual void OnDisable()
    {

    }

    // 实体更新
    public virtual void OnUpdate(float deltaTime)
    {
        if (mMoveDir == Vector3.zero)
        {
            return;
        }
        OnMoveUpdate();
        OnRotateUpdate(deltaTime);
    }

    // 延时更新
    public virtual void OnLateUpdate(float deltaTime)
    {
        
    }

    public Transform GetTransform()
    {
        return mBody.transform;
    }

    // 移动
    private void OnMoveUpdate()
    {
        float fTimeSpan = Time.realtimeSinceStartup - mStartMoveTime;
        Vector3 distance = mMoveDir * mMoveSpeed * fTimeSpan;
        Vector3 endPos = mStartPosition + distance;
        endPos.y = mStartPosition.y;
        SetPosition(endPos);
    }

    public void OnRotateUpdate(float deltaTime)
    {
        Vector3 pathForward = GetMoveDir();
        Vector3 beforeFoward = mBody.transform.forward;

        float angle = Vector3.Angle(beforeFoward, pathForward);
        if (angle < 0.1f)
        {
            return ;
        }

        float pathTurnSpeed = 15;
        beforeFoward.y = 0;
        Vector3 afterFoward = Vector3.Lerp(beforeFoward, pathForward, pathTurnSpeed * deltaTime);
        mBody.transform.forward = afterFoward;
    }
}
