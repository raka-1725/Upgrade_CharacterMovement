using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class SInteractability : MonoBehaviour
{
    [Header("References")]
    private PlayerInputSystem mPlayerInputSystem;
    private SUIController mUI;

    private void Awake()
    {
        mPlayerInputSystem = new PlayerInputSystem();

        mPlayerInputSystem.Gameplay.Interaction.performed += PlayerInteraction;
        mPlayerInputSystem.Gameplay.Interaction.canceled += PlayerInteractionIdle;

        mUI = FindAnyObjectByType<SUIController>();
    }

    private void PlayerInteractionIdle(InputAction.CallbackContext context)
    {
        mUI.UpdateInteractablityUI(false);
    }
    #region Enable/disable
    private void OnEnable()
    {
        mPlayerInputSystem.Enable();
    }
    private void OnDisable()
    {
        mPlayerInputSystem.Disable();
    }
    #endregion //ignore this 
    private void PlayerInteraction(InputAction.CallbackContext context)
    {
        //use the text in the line below in the parenthesis to print anything you write out when you press the F key!!
        Debug.Log($"Put Anything here to print it!!");

        mUI.UpdateInteractablityUI(true);
    }

}
