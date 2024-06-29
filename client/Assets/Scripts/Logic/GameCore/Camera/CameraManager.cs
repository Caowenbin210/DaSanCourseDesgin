using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 摄像机管理器
public class CameraManager : BaseMgr<CameraManager>
{
    internal static readonly float CAMERA_ERROR_VALUE = -1000;
    internal static readonly float CAMERA_ANGLE_ERROR = 0.000f;


    // 摄像机的旋转角度
    private float mTargetYaw;
    // 摄像机的俯仰角
    private float mTargetPitch;


    // 跟随的摄像机
    private Transform mFollowCamera;
    // 摄像机跟随的人物
    public Transform mFollowPlayer;
    // 镜头的角度
    private Vector3 mFollowAngle = new Vector3(0, 0, 0);


    // 摄像机默认俯仰角度
    public float defaultPitch = 25;
    // 摄像机默认的偏航角
    public float defaultYaw = 20;
    // 摄像机与观察物体之间默认的距离
    public float defaultDistance = 7;

    private static float yawMultiplier = 2.0f;
    private static float yawLerpSpeed = 10;

    private static float pitchMultiplier = 2.0f;
    private static float pitchMinValue = -20;
    private static float pitchMaxValue = 40;
    private static float pitchLerpSpeed = 2.0f;

    // 摄像机当前俯仰角度
    private float mCurrentPitch;
    // 摄像机当前的偏航角
    private float mCurrenYaw;

    public void Init()
    {
        InputManager.Instance.InitEvent(InputEvent);

        mTargetYaw = defaultYaw;
        mTargetPitch = defaultPitch;
    }

    // 初始化摄像机观察目标
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

        // 计算摄像机的朝向
        Vector3 cameraForward = Quaternion.Euler(mCurrentPitch, mCurrenYaw + mFollowAngle.y, 0) * Vector3.forward;
        mFollowCamera.forward = cameraForward;

        // 计算摄像机的位置
        Vector3 cameraPosition = mFollowPlayer.position - cameraForward * defaultDistance;
        mFollowCamera.position = cameraPosition;
    }

    public void InputEvent(Vector2 deltaPosition)
    {
        Rotate(deltaPosition.x, deltaPosition.y);
    }

    // 旋转摄像机，根据鼠标移动差值，计算摄像机的偏航角度，和俯仰角度
    private void Rotate(float dx,float dy)
    {
        mTargetYaw = YawValueAdd(mTargetYaw, dx, dy);
        mTargetPitch = PitchValueAdd(mTargetPitch, dx, dy);
    }

    private float YawValueAdd(float cur, float dx,float dy)
    {
        // 默认鼠标从屏幕左侧滑动到右侧镜头旋转360度，通过乘数控制滑动幅度
        return cur + dx * (360.0f / Screen.width) * yawMultiplier;
    }

    // 偏航角度线性插值计算
    private float YawValueLerp(float cur, float tar, float deltaTime)
    {
        // 角度差低于一定阈值之后直接切换到目标值
        if (Mathf.Abs(tar - cur) <= CAMERA_ERROR_VALUE) return tar;

        // 如果速度为负值，则直接切换到目标
        if (yawLerpSpeed <= 0) return tar;

        // 从当前角度平滑过渡到目标角度
        return Mathf.LerpAngle(cur,tar, deltaTime * yawLerpSpeed);
    }

    private float PitchValueAdd(float cur, float dx,float dy)
    {
        return cur - dy * ((float)(pitchMaxValue - pitchMinValue) /Screen.height) * pitchMultiplier;
    }

    // 俯仰角度线性插值计算
    private float PitchValueLerp(float cur, float tar, float deltaTime)
    {
        if (Mathf.Abs(tar - cur) <= CAMERA_ANGLE_ERROR) return tar;

        if(pitchLerpSpeed <= 0) return tar;

        return Mathf.LerpAngle(cur, tar, deltaTime * pitchLerpSpeed);

    }
}
