using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����������
public class CameraManager : BaseMgr<CameraManager>
{
    internal static readonly float CAMERA_ERROR_VALUE = -1000;
    internal static readonly float CAMERA_ANGLE_ERROR = 0.000f;


    // ���������ת�Ƕ�
    private float mTargetYaw;
    // ������ĸ�����
    private float mTargetPitch;


    // ����������
    private Transform mFollowCamera;
    // ��������������
    public Transform mFollowPlayer;
    // ��ͷ�ĽǶ�
    private Vector3 mFollowAngle = new Vector3(0, 0, 0);


    // �����Ĭ�ϸ����Ƕ�
    public float defaultPitch = 25;
    // �����Ĭ�ϵ�ƫ����
    public float defaultYaw = 20;
    // �������۲�����֮��Ĭ�ϵľ���
    public float defaultDistance = 7;

    private static float yawMultiplier = 2.0f;
    private static float yawLerpSpeed = 10;

    private static float pitchMultiplier = 2.0f;
    private static float pitchMinValue = -20;
    private static float pitchMaxValue = 40;
    private static float pitchLerpSpeed = 2.0f;

    // �������ǰ�����Ƕ�
    private float mCurrentPitch;
    // �������ǰ��ƫ����
    private float mCurrenYaw;

    public void Init()
    {
        InputManager.Instance.InitEvent(InputEvent);

        mTargetYaw = defaultYaw;
        mTargetPitch = defaultPitch;
    }

    // ��ʼ��������۲�Ŀ��
    public void InitCamera(Transform camera,Transform target)
    {
        mFollowCamera = camera;
        mFollowPlayer = target;

        mFollowAngle = target.eulerAngles;
    }

    public void LateUpdate()
    {
        if(mFollowCamera == null)
        {
            return;
        }

        mCurrentPitch = PitchValueLerp(mCurrentPitch, mTargetPitch, Time.smoothDeltaTime);
        mCurrenYaw = YawValueLerp(mCurrenYaw, mTargetYaw, Time.smoothDeltaTime);

        // ����������ĳ���
        Vector3 cameraForward = Quaternion.Euler(mCurrentPitch, mCurrenYaw + mFollowAngle.y, 0) * Vector3.forward;
        mFollowCamera.forward = cameraForward;

        // �����������λ��
        Vector3 cameraPosition = mFollowPlayer.position - cameraForward * defaultDistance;
        mFollowCamera.position = cameraPosition;
    }

    public void InputEvent(Vector2 deltaPosition)
    {
        Rotate(deltaPosition.x, deltaPosition.y);
    }

    // ��ת���������������ƶ���ֵ�������������ƫ���Ƕȣ��͸����Ƕ�
    private void Rotate(float dx,float dy)
    {
        mTargetYaw = YawValueAdd(mTargetYaw, dx, dy);
        mTargetPitch = PitchValueAdd(mTargetPitch, dx, dy);
    }

    private float YawValueAdd(float cur, float dx,float dy)
    {
        // Ĭ��������Ļ��໬�����Ҳྵͷ��ת360�ȣ�ͨ���������ƻ�������
        return cur + dx * (360.0f / Screen.width) * yawMultiplier;
    }

    // ƫ���Ƕ����Բ�ֵ����
    private float YawValueLerp(float cur, float tar, float deltaTime)
    {
        // �ǶȲ����һ����ֵ֮��ֱ���л���Ŀ��ֵ
        if (Mathf.Abs(tar - cur) <= CAMERA_ERROR_VALUE) return tar;

        // ����ٶ�Ϊ��ֵ����ֱ���л���Ŀ��
        if (yawLerpSpeed <= 0) return tar;

        // �ӵ�ǰ�Ƕ�ƽ�����ɵ�Ŀ��Ƕ�
        return Mathf.LerpAngle(cur,tar, deltaTime * yawLerpSpeed);
    }

    private float PitchValueAdd(float cur, float dx,float dy)
    {
        return cur - dy * ((float)(pitchMaxValue - pitchMinValue) /Screen.height) * pitchMultiplier;
    }

    // �����Ƕ����Բ�ֵ����
    private float PitchValueLerp(float cur, float tar, float deltaTime)
    {
        if (Mathf.Abs(tar - cur) <= CAMERA_ANGLE_ERROR) return tar;

        if(pitchLerpSpeed <= 0) return tar;

        return Mathf.LerpAngle(cur, tar, deltaTime * pitchLerpSpeed);

    }
}
