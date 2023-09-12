
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
    [UdonSynced]
    public int lastClickedId;


    void Start()
    {
    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {

    }

    public override void OnPlayerLeft(VRCPlayerApi player)
    {
        
    }

    public void AddNewPlayer()
    {
        Debug.Log("test");
        //if (!Networking.LocalPlayer.isMaster) { return; }
        VRCPlayerApi newPlayer = VRCPlayerApi.GetPlayerById(lastClickedId);
        string output = "";
        int playerIndex = 0;

        playerOrder.Add(name);
        playerPool.GetComponent<PlayerPool>().AddPooledPlayer();
                //Player playerComponent = playerPool.Pool[i].GetComponent<Player>();
                //Debug.Log(playerComponent.playerName);

               // playerComponent.poolIndex = i;

                //Networking.SetOwner(newPlayer, playerPool.Pool[i]);
    }
    public void UpdateList()
    {
        Debug.Log("RACE CONDITION?!");
        string output = "";
         for (int i = 0; i < playerPool.Pool.Length; i++)
            {
             if(playerPool.Pool[i].activeSelf == true)
                {
                    output += $"{playerPool.Pool[i].GetComponent<Player>().LocalPlayer.displayName+i.ToString()}, ";
                    output += $"{playerPool.Pool[i].activeSelf.ToString()}, ";
                    output += $"{playerPool.Pool[i].GetComponent<Player>().poolIndex}, ";
                    output += $"{playerPool.Pool[i].GetComponent<Player>().score}\n";
                } 
            }
        playerListText.GetComponent<Text>().text = output;

    }


}
