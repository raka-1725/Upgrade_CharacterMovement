using System.Timers;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]
public class SPlayerMovement : MonoBehaviour
{
    [Header("References")]
    private PlayerInputSystem mPlayerInputSystem;
    private CharacterController mCharacterController;
    private SCameraMovement mCameraMovement;
    private Animator mAnimator;

    [Header("Player Jump Settings")]
    [SerializeField] private float mPlayerJumpForce = 5f;
    [SerializeField] private float mPlayerFallSpeed = 50f;
    private Vector3 mVerticalVelocity;

    [Header("Player Movement Settings")]
    [SerializeField] private float mPlayerMoveSpeed = 5;
    [SerializeField] private float mPlayerAirMoveSpeed = 2f;
    private Vector3 mHorizontalVelocity;
    private Vector2 mMoveInput;
    private float mCameraRotation;

    [Header("Stats")]
    private float mCurrentSpeed;
    public Vector2 currentInput => mMoveInput;
    public float playerSpeed => mCurrentSpeed;
    public bool bJump => !mCharacterController.isGrounded;
    private void Awake()
    {
        mPlayerInputSystem = new PlayerInputSystem();

        mPlayerInputSystem.Gameplay.Jump.performed += PerformedJump;
        mPlayerInputSystem.Gameplay.Move.performed += PlayerMovement;
        mPlayerInputSystem.Gameplay.Move.canceled += PlayerMovement;
        mPlayerInputSystem.Gameplay.RotateCamera.performed += CameraRotation;
        mPlayerInputSystem.Gameplay.RotateCamera.canceled += CameraRotation;

        mAnimator = GetComponentInChildren<Animator>();

        mCharacterController = GetComponent<CharacterController>();
        mCameraMovement = GetComponent<SCameraMovement>();
    }
    private void PlayerMovement(InputAction.CallbackContext context)
    {
        mMoveInput = context.ReadValue<Vector2>();
    }
    private void CameraRotation(InputAction.CallbackContext context) 
    {
        mCameraRotation = context.ReadValue<float>();
    }
    private void OnEnable()
    {
        mPlayerInputSystem.Enable();
    }
    private void OnDisable()
    {
        mPlayerInputSystem.Disable();
    }
    private void PerformedJump(InputAction.CallbackContext context)
    {
        if (mCharacterController.isGrounded && mVerticalVelocity.y <= 0f)
        {
            mVerticalVelocity.y = mPlayerJumpForce;
        }
    }
    private void Update()
    {
        mCameraMovement.RotateCamera(mCameraRotation);

        if (mCharacterController.isGrounded)
        {
            if (mVerticalVelocity.y < 0) 
            {
                mVerticalVelocity.y = -1;
            }
            mHorizontalVelocity = (Camera.main.transform.right * mMoveInput.x + Camera.main.transform.forward * mMoveInput.y).normalized;
            mHorizontalVelocity.y = 0f;
        }
        else
        {
            mVerticalVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        if (mHorizontalVelocity.sqrMagnitude > 0 && mCharacterController.isGrounded) 
        {
            mHorizontalVelocity -= mHorizontalVelocity.normalized * 2.5f *Time.deltaTime;
            if (mHorizontalVelocity.sqrMagnitude < 0.1)
            {
                mHorizontalVelocity = Vector3.zero;
            }
        }
        float speed = mCharacterController.isGrounded ? mPlayerMoveSpeed : mPlayerAirMoveSpeed;
        Vector3 FinalMov = (speed * mHorizontalVelocity) + mVerticalVelocity;
        mCharacterController.Move(FinalMov * Time.deltaTime);
        mCurrentSpeed = mCharacterController.velocity.magnitude;


        mAnimator.SetFloat("Speed", Mathf.Abs(mMoveInput.x) + Mathf.Abs(mMoveInput.y));
    }
}
