using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.Serialization;
using VRC.SDK3.Components;
using VRC.SDKBase;

namespace GremnamesPlayer
{
    public class Player : UdonSharpBehaviour
    {
        // Public synced variables
        [UdonSynced] public int score;
        [UdonSynced] public int poolIndex;
        [UdonSynced] public string playerName;
        VRCObjectPool objectPool;
        
        // Public variables
        // This variable will store the player's info that it is being assigned to.
        public VRCPlayerApi LocalPlayer;
        //private override void OnOwnershipTransferred()
        //{
        //Debug.Log("AHM WAID AWEK");
        //if (Networking.LocalPlayer.playerId != GetComponentInParent<GamemasterGizno>().lastClickedId)
        // {
        //    return;
        //}
        //LocalPlayer = Networking.LocalPlayer;
        //Debug.Log(LocalPlayer.displayName);
        //playerName = LocalPlayer.displayName;
        //score = 0;
        //SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(GamemasterGizno.UpdateList));
        //}
        public override void OnOwnershipTransferred(VRCPlayerApi player)
        {
            if(Networking.LocalPlayer != player){return;}

        }

        private void OnDisable()
        {
            LocalPlayer = null;
        }

    }
}
