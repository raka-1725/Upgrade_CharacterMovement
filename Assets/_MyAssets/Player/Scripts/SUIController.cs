using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SUIController : MonoBehaviour
{
    SPlayerMovement mPlayerMovement;
    SCameraMovement mCameraMovement;

    [Header("Stats")]
    [SerializeField] TextMeshProUGUI mInputText;
    [SerializeField] TextMeshProUGUI mCameraRotationText;
    [SerializeField] TextMeshProUGUI mPlayerSpeedText;
    [SerializeField] TextMeshProUGUI mPlayerPositionText;
    [SerializeField] TextMeshProUGUI mCameraRotationSpeedText;

    private Vector2 mMoveInput;
    private float mCameraRotation;
    private float mCameraRotationInput;
    private float mPlayerSpeed;
    private Transform mPlayerTransform;
    private float mCameraRotationSpeed;

    [Header("KeyInput")]
    [SerializeField] Image m_W_Key;
    [SerializeField] Image m_A_key;
    [SerializeField] Image m_S_key;
    [SerializeField] Image m_D_key;
    [SerializeField] Image m_E_key;
    [SerializeField] Image m_Q_key;
    [SerializeField] Image m_Space_key;

    [Header("KeySprite")]
    [SerializeField] Sprite m_W_Key_Sprite;
    [SerializeField] Sprite m_A_key_Sprite;
    [SerializeField] Sprite m_S_key_Sprite;
    [SerializeField] Sprite m_D_key_Sprite;
    [SerializeField] Sprite m_E_key_Sprite;
    [SerializeField] Sprite m_Q_key_Sprite;
    [SerializeField] Sprite m_Space_key_Sprite;

    [SerializeField] Sprite m_W_Key_Pressed;
    [SerializeField] Sprite m_A_key_Pressed;
    [SerializeField] Sprite m_S_key_Pressed;
    [SerializeField] Sprite m_D_key_Pressed;
    [SerializeField] Sprite m_E_key_Pressed;
    [SerializeField] Sprite m_Q_key_Pressed;
    [SerializeField] Sprite m_Space_key_Pressed;

    [Header("Slider")]
    [SerializeField] Slider mCameraRotationSpeedSlider;
    private void Awake()
    {
        mPlayerMovement = FindAnyObjectByType<SPlayerMovement>();
        mCameraMovement = FindAnyObjectByType<SCameraMovement>();
        mCameraRotationSpeed = mCameraMovement.rotationSpeed;
        mCameraRotationSpeedText.text = mCameraRotationSpeed.ToString("F2");
        mCameraRotationSpeedSlider.onValueChanged.AddListener(UpdateCameraRotationSpeed);
    }
    private void Update()
    {
        UpdatePlayerInput();
        UpdatePlayerStats();
    }
    private void UpdateCameraRotationSpeed(float speed)
    {
        mCameraRotationSpeed = speed;
        mCameraRotationSpeedText.text = mCameraRotationSpeed.ToString("F2");
        mCameraMovement.SetRotationSpeed(mCameraRotationSpeed);
    }
    private void UpdatePlayerInput() 
    {
        mMoveInput = mPlayerMovement.currentInput;
        mInputText.text = mMoveInput.ToString();

        if (mPlayerMovement)
        {
            m_W_Key.sprite = m_W_Key_Sprite;
            m_A_key.sprite = m_A_key_Sprite;
            m_S_key.sprite = m_S_key_Sprite;
            m_D_key.sprite = m_D_key_Sprite;
            m_Space_key.sprite = m_Space_key_Sprite;

            if (mMoveInput.y < -0.1)
            {
                m_S_key.sprite = m_S_key_Pressed;
            }
            if (mMoveInput.y > 0.1) 
            {
                m_W_Key.sprite = m_W_Key_Pressed;
            }
            if (mMoveInput.x < -0.1) 
            {
                m_A_key.sprite = m_A_key_Pressed;
            }
            if (mMoveInput.x > 0.1) 
            {
                m_D_key.sprite = m_D_key_Pressed;
            }
            if (mPlayerMovement.bJump) 
            {
                m_Space_key.sprite = m_Space_key_Pressed;
            }
        }
        mCameraRotation = mCameraMovement.cameraRotation;
        mCameraRotationInput = mCameraMovement.rotationInput;
        mCameraRotationText.text = mCameraRotation.ToString("F3");
        if (mCameraMovement) 
        {
            m_Q_key.sprite = m_Q_key_Sprite;
            m_E_key.sprite = m_E_key_Sprite;

            if (mCameraRotationInput < -0.1) 
            {
                m_Q_key.sprite = m_Q_key_Pressed;
            }
            if (mCameraRotationInput > 0.1) 
            {
                m_E_key.sprite = m_E_key_Pressed;
            }
        }
    }
    private void UpdatePlayerStats() 
    {
        mPlayerSpeed = mPlayerMovement.playerSpeed;
        mPlayerSpeedText.SetText(mPlayerSpeed.ToString("F2"));

        mPlayerTransform = mPlayerMovement.transform;
        mPlayerPositionText.SetText(mPlayerTransform.position.ToString("F2"));
    }
}
