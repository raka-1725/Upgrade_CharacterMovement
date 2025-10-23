using UnityEngine;

public class SCameraMovement : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private GameObject mMainCamera;
    [SerializeField] private GameObject mCameraTarget;

    [Header("Settings")]
    [SerializeField] private float mRotateSpeed;
    [SerializeField] private float mCurrentAngle = 0;
    [SerializeField] private float mOffsetToPlayer = 10;

    [Header("Input")]
    [SerializeField] private float mRotationInput;

    public float cameraRotation => mCurrentAngle;
    public float rotationInput => mRotationInput;

    private void Awake()
    {
        if (mMainCamera == null || mCameraTarget == null) 
        {
            Debug.LogWarning("Objects are not assigned!!");
            return;
        }
    }
    public void RotateCamera(float input) 
    {
        mRotationInput = input;

        mCurrentAngle += -(mRotationInput * mRotateSpeed * Time.deltaTime);
        Quaternion rotation = Quaternion.Euler(0, mCurrentAngle - 45, 0);

        Vector3 offset = rotation * new Vector3(-10, 2, -mOffsetToPlayer);
        mMainCamera.transform.position = mCameraTarget.transform.position + offset;

        mMainCamera.transform.LookAt(mCameraTarget.transform.position);
    }

    public void SetRotationSpeed(float input) 
    {
        mRotateSpeed = input;
    }
}
