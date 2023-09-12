
using System.Net;
using System.Runtime.Remoting.Messaging;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Components;
using VRC.SDK3.Data;
using VRC.SDKBase;

namespace Testing
{
    public class ScoreBoardTriggerTest : UdonSharpBehaviour
    {
        // Unity objects
        [SerializeField] private VRCObjectPool rows;
        
        private int _numberOfPlayers = 0;
        private bool _isJoined = false;
        private DataDictionary _players = new DataDictionary();
        private VRCPlayerApi localPlayer;

        // TMP_

        void Start()
        {
            localPlayer = Networking.LocalPlayer;
        }

        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            _numberOfPlayers++;
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            _numberOfPlayers--;
        }

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (player == localPlayer)
            {
                if (_isJoined) return;

                for (var i = 0; i < rows.Pool.Length; i++)
                {
                    if (rows.Pool[i].activeSelf) continue;
                                                    
                    rows.Pool[i].SetActive(true);
                
                    var rank = rows.Pool[i].transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
                    var displayName = rows.Pool[i].transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>();
                    var score = rows.Pool[i].transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>();

                    rank.text = i.ToString();
                    displayName.text = localPlayer.displayName;
                    score.text = "0";
                
                    _isJoined = true;
                                
                    return;
                }
            }
            else
            {
                
            }
        }
    }
}

