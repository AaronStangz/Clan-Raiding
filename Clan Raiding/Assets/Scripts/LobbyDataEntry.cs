using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using TMPro;

public class LobbyDataEntry : MonoBehaviour
{
    public CSteamID lobbyID;
    public string lobbyName;
    public TMP_Text lobbyNameText;

    public void SetLobbyData()
    {
        if(lobbyName == "")
        {
            lobbyNameText.text = "Clear";
        }
        else 
        {
            lobbyNameText.text = lobbyName;
        }
    }

    public void JoinLobby()
    {
        SteamLobby.instance.JoinLobby(lobbyID);
    }
}
