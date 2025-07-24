using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private LobbyPanelBase[] lobbyPanels;
    [SerializeField] private LoadingCanvasController loadingCanvasControllerPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (LobbyPanelBase lobby in lobbyPanels)
        {
            lobby.InitPanel(this);
        }

        Instantiate(loadingCanvasControllerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPanel(LobbyPanelBase.LobbyPanelType type)
    {
        foreach(LobbyPanelBase lobby in lobbyPanels)
        {
            if(lobby.PanelType == type)
            {
                lobby.ShowPanel();
                
                break;
            }
        }
    }
}
