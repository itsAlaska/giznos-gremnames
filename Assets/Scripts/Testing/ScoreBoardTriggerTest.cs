
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

namespace Testing
{
    public class ScoreBoardTriggerTest : UdonSharpBehaviour
    {
        public VRCPlayerApi Owner;
        
        // Unity objects
        [SerializeField] private Transform[] rows;

        private int _numberOfPlayers = 0;
        private VRCPlayerApi[] _players = new VRCPlayerApi[40];

        void Start()
        {
        }

        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            _numberOfPlayers++;
            _players[_numberOfPlayers - 1] = player;
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            _numberOfPlayers--;
        }

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (Owner == null)
            {
                Owner = player;
                Networking.SetOwner(Owner, gameObject);
            }
            else if (Owner == player)
            {
                
            }
        }
        
        
    }
}

