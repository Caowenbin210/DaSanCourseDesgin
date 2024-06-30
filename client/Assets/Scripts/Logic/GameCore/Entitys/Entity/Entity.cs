using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ʵ��Ļ��࣬��Ϸ�����е����嶼�̳д���
public class Entity
{
    // ʵ�������
    protected eEntityType mEntityType;

    // ʵ���ΨһId
    private long mId;

    private int mHp;
    private int mMaxHp;
    private string mName;

    // ʵ��Ŀɼ�ģ��
    public GameObject mBody;

    // ʵ���ƶ����
    // �ƶ��ķ���
    private Vector3 mMoveDir = new Vector3();
    // �ƶ����ٶ�
    private float mMoveSpeed;
    // ��ʼ�ƶ���ʱ��
    private float mStartMoveTime;
    // ��ʼ�ƶ��������
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

    // ����ʵ�������
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

    // ����ʵ���λ������
    public void SetPosition(Vector3 position)
    {
        mBody.transform.position = position;
    }

    public Vector3 GetPosition()
    {
        return mBody.transform.position;
    }

    // ����ʵ��ĳ���
    public void SetForward(Vector3 forward)
    {
        mBody.transform.eulerAngles = forward;
    }

    // �����ƶ��ٶ�
    public void SetMoveSpeed(float speed)
    {
        mMoveSpeed = speed;
    }

    // �����ƶ�����
    public void SetMoveDir(Vector3 dir)
    {
        mMoveDir = dir;
    }

    public Vector3 GetMoveDir()
    {
        return mMoveDir;
    }

    // ���ÿ�ʼ�ƶ��ĵ�
    public void SetStartPosition(Vector3 pos)
    {
        mStartPosition = pos;
    }

    // ���ÿ�ʼ�ƶ���ʱ��
    public void SetStartMoveTime(float time)
    {
        mStartMoveTime = time;
    }

    // ����ʵ��Ķ��� 
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

    // ��ʵ�崴�����������
    public virtual void OnCreate()
    {

    }

    //������һ��ʵ��ʱ����
    public virtual void OnDestory()
    {

    }

    // ʵ������
    public virtual void OnEnable()
    {

    }

    // ����ʵ��
    public virtual void OnDisable()
    {

    }

    // ʵ�����
    public virtual void OnUpdate(float deltaTime)
    {
        if (mMoveDir == Vector3.zero)
        {
            return;
        }
        OnMoveUpdate();
        OnRotateUpdate(deltaTime);
    }

    // ��ʱ����
    public virtual void OnLateUpdate(float deltaTime)
    {
        
    }

    public Transform GetTransform()
    {
        return mBody.transform;
    }

    // �ƶ�
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
