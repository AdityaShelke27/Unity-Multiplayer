using Fusion;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiddleSectionPanel : LobbyPanelBase
{
    [Header("MiddleSectionPanel")]
    [SerializeField] private Button joinRandomRoomBtn;
    [SerializeField] private Button joinRoomByArgBtn;
    [SerializeField] private Button createRoomBtn;

    [SerializeField] private TMP_InputField joinRoomByArgInputField;
    [SerializeField] private TMP_InputField createRoomInputField;
    private NetworkRunnerController networkRunnerController;

    public override void InitPanel(LobbyUIManager uiManager)
    {
        base.InitPanel(uiManager);

        joinRandomRoomBtn.onClick.AddListener(JoinRandomRoom);
        joinRoomByArgBtn.onClick.AddListener(() => CreateOrJoinRoom(GameMode.Client, joinRoomByArgInputField.text));
        createRoomBtn.onClick.AddListener(() => CreateOrJoinRoom(GameMode.Host, createRoomInputField.text));
        networkRunnerController = GlobalManagers.Instance.NetworkRunnerController;
    }

    private void CreateOrJoinRoom(GameMode gameMode, string field)
    {
        if(field.Length >= 2)
        {
            Debug.Log($"-----{gameMode}-----");
            networkRunnerController.StartGame(gameMode, field);
        }
        
    }

    private void JoinRandomRoom()
    {
        Debug.Log($"-----AutoHostOrClient-----");
        networkRunnerController.StartGame(GameMode.AutoHostOrClient, "");
    }
}
