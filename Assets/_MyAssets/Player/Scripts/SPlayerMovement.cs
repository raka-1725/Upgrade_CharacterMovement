using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]
public class SPlayerMovement : MonoBehaviour
{
    [Header("References")]
    private PlayerInputSystem mPlayerInputSystem;
    private CharacterController mCharacterController;

    [Header("Jump Settings")]
    [SerializeField] private float mPlayerJumpForce = 5f;
    [SerializeField] private float mPlayerFallSpeed = 50f;
    private Vector3 mVerticalVelocity;
    
    private void Awake()
    {
        mPlayerInputSystem = new PlayerInputSystem();
        mPlayerInputSystem.Gameplay.Jump.performed += PerformedJump;
        mCharacterController = GetComponent<CharacterController>();
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
        if(mVerticalVelocity.y > -mPlayerFallSpeed)
        {
            mVerticalVelocity.y += Physics.gravity.y * Time.deltaTime;
        }
        mCharacterController.Move(mVerticalVelocity * Time.deltaTime);
    }
}
