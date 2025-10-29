using UnityEngine;

public class SCameraMovement : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private GameObject mMainCamera;
    [SerializeField] private GameObject mCameraTarget;

    [Header("Settings")]
    [SerializeField] private float mRotateSpeed = 90;
    [SerializeField] private float mCurrentAngle = 0;
    [SerializeField] private float mOffsetToPlayer = 10;

    [Header("Input")]
    [SerializeField] private float mRotationInput;
    public float cameraRotation => mCurrentAngle;
    public float rotationInput => mRotationInput;

    public float rotationSpeed => mRotateSpeed;
    private void Awake()
    {
        if (mMainCamera == null || mCameraTarget == null) 
        {
            Debug.LogWarning("Objects are not assigned!!");
            return;
        }
    }
    private void Start()
    {
        mRotateSpeed = 90;
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
