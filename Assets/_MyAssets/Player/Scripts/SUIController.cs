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

    [Header("InputUI")]
    [SerializeField] GameObject mKeyboardInput;
    [SerializeField] GameObject mGameControllerInput;
    [SerializeField] Button mSwitchInputButton;
    
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
    [Header("GameControllerSprite")]
    [SerializeField] Image m_LeftStick;
    [SerializeField] Image m_RightStick;
    [SerializeField] Image m_Dkey;
    [SerializeField] Image m_RightTrigger;
    [SerializeField] Image m_LeftTrigger;
    //[SerializeField] Image mXButton;
    //[SerializeField] Image mYButton;
    [SerializeField] Image mAButton;
    //[SerializeField] Image mBButton;

    [SerializeField] Sprite[] m_LeftStickSprites; //0-default, 1-up, 2-down, 3-left, 4-right
    [SerializeField] Sprite[] m_RightSticksSprites; 
    [SerializeField] Sprite[] mRTSprites;
    [SerializeField] Sprite[] mLTSprites;
    [SerializeField] Sprite[] mAButtonSprites;
    [SerializeField] Sprite[] mDkeySprites;





    [Header("Slider")]
    [SerializeField] Slider mCameraRotationSpeedSlider;
    
    
    public bool bGameController;

    private void Awake()
    {
        mPlayerMovement = FindAnyObjectByType<SPlayerMovement>();
        mCameraMovement = FindAnyObjectByType<SCameraMovement>();
        mCameraRotationSpeed = mCameraMovement.rotationSpeed;
        mCameraRotationSpeedText.text = mCameraRotationSpeed.ToString("F2");
        mCameraRotationSpeedSlider.onValueChanged.AddListener(UpdateCameraRotationSpeed);
        mSwitchInputButton.onClick.AddListener(SwitchInputUI);

        mGameControllerInput.SetActive(bGameController); 
        mKeyboardInput.SetActive(!bGameController);
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
        mCameraRotation = mCameraMovement.cameraRotation;
        mCameraRotationInput = mCameraMovement.rotationInput;
        mCameraRotationText.text = mCameraRotation.ToString("F3");

        if (mPlayerMovement)
        {
            UpdateKeyInputUI();
        }
        if (mCameraMovement)
        {
            UpdateCameraRotationInputUI();
        }
    }

    private void SwitchInputUI() 
    {
        bGameController = !bGameController;
        mGameControllerInput.SetActive(bGameController);
        mKeyboardInput.SetActive(!bGameController);
    }

    private void UpdateCameraRotationInputUI()
    {
        if (!bGameController)
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
        else 
        {
            m_RightStick.sprite = m_RightSticksSprites[0];
            m_LeftTrigger.sprite = mLTSprites[0];
            m_RightTrigger.sprite = mRTSprites[0];


            if (mCameraRotationInput < -0.1)
            {
                m_RightStick.sprite = m_RightSticksSprites[3];
                m_LeftTrigger.sprite = mLTSprites[1];
            }
            if (mCameraRotationInput > 0.1)
            {
                m_RightStick.sprite = m_RightSticksSprites[4];
                m_RightTrigger.sprite = mRTSprites[1];
            }
        }
    }

    private void UpdateKeyInputUI()
    {
        if (!bGameController)
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
        else
        {
            m_LeftStick.sprite = m_LeftStickSprites[0];
            m_Dkey.sprite = mDkeySprites[0];
            mAButton.sprite = mAButtonSprites[0];

            if (mMoveInput.y < -0.1)
            {
                m_LeftStick.sprite = m_LeftStickSprites[2];
                m_Dkey.sprite = mDkeySprites[2];
            }
            if (mMoveInput.y > 0.1)
            {
                m_LeftStick.sprite = m_LeftStickSprites[1];
                m_Dkey.sprite = mDkeySprites[1];
            }
            if (mMoveInput.x < -0.1)
            {
                m_LeftStick.sprite = m_LeftStickSprites[3];
                m_Dkey.sprite = mDkeySprites[3];
            }
            if (mMoveInput.x > 0.1)
            {
                m_LeftStick.sprite = m_LeftStickSprites[4];
                m_Dkey.sprite = mDkeySprites[4];
            }
            if (mPlayerMovement.bJump)
            {
                mAButton.sprite = mAButtonSprites[1];
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
