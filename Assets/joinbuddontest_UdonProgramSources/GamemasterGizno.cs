
using System.Collections.Generic;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Components;
using VRC.SDK3.Data;
using VRC.SDKBase;
using VRC.Udon;
using GremnamesPlayer;

public class GamemasterGizno : UdonSharpBehaviour
{

    //private DataDictionary userListData = new DataDictionary() {};
    //private DataDictionary localPlayerData = new DataDictionary() { {"status", "spectating"} };


    public DataList playerOrder = new DataList();
    [SerializeField]
    private Text playerListText;
    [SerializeField]
    public VRCObjectPool playerPool;
    [SerializeField]
    public JoinButton joinButton;
    [UdonSynced, FieldChangeCallback(nameof(BoardTextUpdated))]
    public string boardText = "PLAYERS";

    public bool initialSync = true;


    public override void OnDeserialization()
    {
        if(initialSync == true)
        {
            initialSync = false;
            playerListText.GetComponent<Text>().text = boardText;

        }
    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {

    }

    public override void OnPlayerLeft(VRCPlayerApi player)
    {
        
    }

    public void AddNewPlayer()
    {
        //TODO: Maybe make incoming ids add to a queue and process them in update?
        Debug.Log(joinButton.lastClickedId);
        if (!Networking.LocalPlayer.isMaster) { return; }
        VRCPlayerApi newPlayer = VRCPlayerApi.GetPlayerById(joinButton.lastClickedId);
        string output = "";
        int playerIndex = 0;

        playerOrder.Add(name);
        playerPool.GetComponent<PlayerPool>().AddPooledPlayer();
    }

    public void RemovePlayer()
    {
        
        playerPool.GetComponent<PlayerPool>().RemovePooledPlayer();
    }

    public void UpdateList()
    {
        if (Networking.LocalPlayer.isMaster == true) {

            string output = "";
            for (int i = 0; i < playerPool.Pool.Length; i++)
                {
                 if(playerPool.Pool[i].activeSelf == true)
                    {
                        output += $"{playerPool.Pool[i].GetComponent<Player>().LocalPlayer.displayName+i.ToString()}, ";
                        output += $"{playerPool.Pool[i].GetComponent<Player>().LocalPlayer.playerId.ToString()}, ";
                        output += $"{playerPool.Pool[i].activeSelf.ToString()}, ";
                        output += $"{playerPool.Pool[i].GetComponent<Player>().poolIndex}, ";
                        output += $"{playerPool.Pool[i].GetComponent<Player>().score}\n";
                    } 
                }
            SetProgramVariable("boardText", output);
            RequestSerialization();
        }
    }

    public string BoardTextUpdated
    {
        set
        {
            boardText = value;
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "UpdateBoardText");
        }

        get => boardText;
    }

    public void UpdateBoardText()
    {
        playerListText.GetComponent<Text>().text = boardText;
    }


}
