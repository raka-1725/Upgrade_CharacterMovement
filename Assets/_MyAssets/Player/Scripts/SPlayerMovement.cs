using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
[RequireComponent(typeof(CharacterController))]
public class SPlayerMovement : MonoBehaviour
{
    [Header("References")]
    private PlayerInputSystem mPlayerInputSystem;
    private CharacterController mCharacterController;
    private SCameraMovement mCameraMovement;

    [Header("Player Jump Settings")]
    [SerializeField] private float mPlayerJumpForce = 5f;
    [SerializeField] private float mPlayerFallSpeed = 50f;
    private Vector3 mVerticalVelocity;

    [Header("Player Movement Settings")]
    [SerializeField] private float mPlayerMoveSpeed = 5;
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
        mCharacterController = GetComponent<CharacterController>();
        mCameraMovement = GetComponent<SCameraMovement>();
    }
    private void PlayerMovement(InputAction.CallbackContext context)
    {
        mMoveInput = context.ReadValue<Vector2>();
        Debug.Log($"Player is Moving");
        Debug.Log($"Player input{mMoveInput}");
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
 
        if (mCharacterController.isGrounded)
        {
            Debug.Log($"Player is Jumping");
            mVerticalVelocity.y = mPlayerJumpForce;
        }
    }
    private void Update()
    {
        mCameraMovement.RotateCamera(mCameraRotation);


        if(mVerticalVelocity.y > -mPlayerFallSpeed)
        {
            mVerticalVelocity.y += Physics.gravity.y * Time.deltaTime;
        }
        //mHorizontalVelocity += mMoveInput.x * Camera.main.transform.right * mPlayerMoveSpeed * Time.deltaTime;
        //mHorizontalVelocity += mMoveInput.y * Camera.main.transform.forward * mPlayerMoveSpeed * Time.deltaTime;
        mHorizontalVelocity = (Camera.main.transform.right * mMoveInput.x + Camera.main.transform.forward * mMoveInput.y).normalized;
        mHorizontalVelocity = Vector3.ClampMagnitude(mHorizontalVelocity, mPlayerMoveSpeed);

        if (mHorizontalVelocity.sqrMagnitude > 0) 
        {
            mHorizontalVelocity -= mHorizontalVelocity.normalized * 2.5f *Time.deltaTime;
            if (mHorizontalVelocity.sqrMagnitude < 0.1)
            {
                mHorizontalVelocity = Vector3.zero;
            }
        }


        Vector3 FinalMov = (mPlayerMoveSpeed * mHorizontalVelocity) + mVerticalVelocity;
        mCharacterController.Move(FinalMov * Time.deltaTime);
        mCurrentSpeed = mCharacterController.velocity.magnitude;
    }
}
