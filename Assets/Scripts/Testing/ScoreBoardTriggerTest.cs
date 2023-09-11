
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

namespace Testing
{
    public class ScoreBoardTriggerTest : UdonSharpBehaviour
    {
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
            Debug.Log(rows[0]);
            rows[0].GetChild(0).GetChild(2).GetComponent<Text>().text = "1";
            rows[0].GetChild(1).GetChild(2).GetComponent<Text>().text = player.displayName;
            rows[0].GetChild(2).GetChild(2).GetComponent<Text>().text = "0";
        }
    }
}

