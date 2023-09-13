
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
    [SerializeField]
    public JoinButton joinButton;

    void Start()
    {
        playerPool = GetComponent<VRCObjectPool>(); 
    }

    public void AddPooledPlayer()
    {
        if (!Networking.LocalPlayer.isMaster) { return; }
        VRCPlayerApi newPlayer = VRCPlayerApi.GetPlayerById(joinButton.lastClickedId);

        for (int i = 0; i < playerPool.Pool.Length; i++)
        {
         if(playerPool.Pool[i].activeSelf == false)
            {
                Debug.Log($"Found slot at index{i}");
                playerPool.Pool[i].SetActive(true);
                playerPool.Pool[i].GetComponent<Player>().LocalPlayer = newPlayer;
                playerPool.Pool[i].GetComponent<Player>().poolIndex = i;
                playerPool.Pool[i].GetComponent<Player>().playerName = newPlayer.displayName;
                Networking.SetOwner(newPlayer, playerPool.Pool[i]);
                RequestSerialization();
                break;
            } 
        }
        //GamemasterGizno.SendCustomEvent(nameof(GamemasterGizno.UpdateList));
        GamemasterGizno.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "UpdateList");
    }
}
