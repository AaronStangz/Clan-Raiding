using UnityEngine;
using Steamworks;
using NUnit.Framework;
using Unity.VisualScripting;
using System.Collections.Generic;

public class LobbiesListManager : MonoBehaviour
{
    public static LobbiesListManager instance;

    public GameObject LobbiesMenu;
    public GameObject LobbyDataItemPrefab;
    public GameObject LobbiesListContent;

    public List<GameObject> listOfLobbies = new List<GameObject>();

    private void Awake()
    {
        if(instance == null) { instance = this; }
    }

    public void GetListOfLobbies()
    {
        LobbiesMenu.SetActive(true);

        SteamLobby.instance.GetLobbiesList();
    }

    public void DisplayLobbies(List<CSteamID> lobbyIDs, LobbyDataUpdate_t result)
    {
        for(int i = 0; i < lobbyIDs.Count; i++) 
        {
            GameObject createdItem = Instantiate(LobbyDataItemPrefab);

            createdItem.GetComponent<LobbyDataEntry>().lobbyID = (CSteamID)lobbyIDs[i].m_SteamID;

            createdItem.GetComponent<LobbyDataEntry>().lobbyName =
                SteamMatchmaking.GetLobbyData((CSteamID)lobbyIDs[i].m_SteamID, "name");

            createdItem.GetComponent<LobbyDataEntry>().SetLobbyData();

            createdItem.transform.SetParent(LobbiesListContent.transform);
            createdItem.transform.localScale = Vector3.one;

            listOfLobbies.Add(createdItem);
        }
    }

    public void DestroyLobbies()
    {
        foreach(GameObject LobbyItem in listOfLobbies)
        {
            Destroy(LobbyItem);
        }
        listOfLobbies.Clear();
    }
}
