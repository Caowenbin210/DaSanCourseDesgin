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

    // ʵ��Ŀɼ�ģ��
    public GameObject mBody;

    public void SetID(long id)
    {
        mId = id;
    }

    public long GetID()
    {
        return mId;
    }

    // ����ʵ�������
    public void SetName(string name)
    {
        GameObject nameBar = mBody.transform.Find("NameBar").gameObject;
        if (nameBar == null)
        {
            return;
        }

        UIName uiName = nameBar.GetComponent<UIName>();
        if (uiName == null)
        {
            return;
        }
        uiName.SetName(name);
    }

    // ����ʵ���λ������
    public void SetPosition(Vector3 position)
    {
        mBody.transform.position = position;
    }

    // ����ʵ��ĳ���
    public void SetForward(Vector3 forward)
    {
        mBody.transform.forward = forward;
    }

    // ����ʵ��Ķ��� 
    public void PlayAnimation(string state)
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

    }

    // ��ʱ����
    public virtual void OnLateUpdate(float deltaTime)
    {

    }
}
