using System;
using UdonSharp;
using UnityEngine.Serialization;
using VRC.SDKBase;

namespace Player
{
    public class Player : UdonSharpBehaviour
    {
        // Public synced variables
        [UdonSynced] public int score;
        
        // Public variables
        // This variable will store the player's info that it is being assigned to.
        public VRCPlayerApi LocalPlayer;
        private void OnEnable()
        {
            LocalPlayer = Networking.LocalPlayer;
        }

        private void OnDisable()
        {
            LocalPlayer = null;
        }
    }
}
