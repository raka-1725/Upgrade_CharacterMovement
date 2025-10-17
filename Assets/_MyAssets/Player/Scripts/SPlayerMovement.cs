using UnityEngine;
using UnityEngine.InputSystem;
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
        if(mCharacterController.isGrounded)
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
        mHorizontalVelocity += mMoveInput.x * Camera.main.transform.right * mPlayerMoveSpeed * Time.deltaTime;
        mHorizontalVelocity += mMoveInput.y * Camera.main.transform.forward * mPlayerMoveSpeed * Time.deltaTime;
        mHorizontalVelocity = Vector3.ClampMagnitude(mHorizontalVelocity, mPlayerMoveSpeed);

        Vector3 FinalMov = mHorizontalVelocity + mVerticalVelocity;
        mCharacterController.Move(FinalMov * Time.deltaTime);
    }
}
