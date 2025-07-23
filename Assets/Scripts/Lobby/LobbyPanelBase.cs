using UnityEngine;

public class LobbyPanelBase : MonoBehaviour
{
    [Header("LobbyPanelBase")]
    [SerializeField] LobbyPanelType PanelType;
    [SerializeField] Animator panelAnimator;
    private LobbyUIManeger lobbyUIManager;
    enum LobbyPanelType
    {
        None,
        CreateNicknamePanel,
        MiddleSectionPanel
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void InitPanel(LobbyUIManeger uiManager)
    {
        lobbyUIManager = uiManager;
    }
}
