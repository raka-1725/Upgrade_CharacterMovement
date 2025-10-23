using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SUIController : MonoBehaviour
{
    SPlayerMovement mPlayerMovement;
    SCameraMovement mCameraMovement;

    [Header("Stats")]
    [SerializeField] TextMeshProUGUI mInput;
    [SerializeField] TextMeshProUGUI mCameraRotation;
    [SerializeField] TextMeshProUGUI mPlayerSpeed;
    [SerializeField] TextMeshProUGUI mPlayerPosition;

    [Header("KeyInput")]
    [SerializeField] Image m_W_Key;
    [SerializeField] Image m_A_key;
    [SerializeField] Image m_S_key;
    [SerializeField] Image m_D_key;
    [SerializeField] Image m_E_key;
    [SerializeField] Image m_Q_key;

    [SerializeField] Sprite m_W_Key_Pressed;
    [SerializeField] Sprite m_A_key_Pressed;
    [SerializeField] Sprite m_S_key_Pressed;
    [SerializeField] Sprite m_D_key_Pressed;
    [SerializeField] Sprite m_E_key_Pressed;
    [SerializeField] Sprite m_Q_key_Pressed;

    private void Awake()
    {
        mPlayerMovement = FindAnyObjectByType<SPlayerMovement>();
        mCameraMovement = FindAnyObjectByType<SCameraMovement>();
    }

    public void UpdateUI() 
    {
        //UI HERE
    }

}
