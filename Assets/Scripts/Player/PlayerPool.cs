
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using GremnamesPlayer;

public class PlayerPool : UdonSharpBehaviour
{
    public GamemasterGizno GamemasterGizno;
    public VRCObjectPool playerPool;

    void Start()
    {
        playerPool = GetComponent<VRCObjectPool>(); 
    }

    public void AddPooledPlayer()
    {
        Debug.Log("Add player called");
        VRCPlayerApi newPlayer = VRCPlayerApi.GetPlayerById(GamemasterGizno.lastClickedId);

        for (int i = 0; i < playerPool.Pool.Length; i++)
        {
         if(playerPool.Pool[i].activeSelf == false)
            {
                Debug.Log($"Found slot at index{i}");
                playerPool.Pool[i].SetActive(true);
                Networking.SetOwner(newPlayer, playerPool.Pool[i]);
                Debug.Log($"Setting {newPlayer.displayName} as owner");
                playerPool.Pool[i].GetComponent<Player>().LocalPlayer = newPlayer;
                Debug.Log($"Set {playerPool.Pool[i].GetComponent<Player>().LocalPlayer} as player.localplayer");

                break;
            } 
        }
        Debug.Log("Rendering list");
        GamemasterGizno.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "UpdateList");
    }
}
