using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNicknamePanel : LobbyPanelBase
{
    [Header("CreateNicknamePanel")]
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button createNicknameBtn;
    private const int MAX_CHAR_FOR_NICKNAME = 2;

    private void Start()
    {
        createNicknameBtn.interactable = false;
        createNicknameBtn.onClick.AddListener(OnClickCreateNickname);
        inputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    void OnInputValueChanged(string val)
    {
        createNicknameBtn.interactable = val.Length >= MAX_CHAR_FOR_NICKNAME;
    }

    void OnClickCreateNickname()
    {
        string nickname = inputField.text;

        if (nickname.Length >= MAX_CHAR_FOR_NICKNAME)
        {
            
        }
    }
}
