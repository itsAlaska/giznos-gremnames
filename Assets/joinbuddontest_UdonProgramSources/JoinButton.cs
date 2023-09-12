
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class JoinButton : UdonSharpBehaviour
{
    private bool interactable = true;
    [SerializeField]
    private GameObject textComponent;
    [SerializeField]
    private GamemasterGizno gameManager;
    

    void Interact()
    {
        //if (interactable == true)
        //{
            interactable = false;
            gameManager.lastClickedId = Networking.LocalPlayer.playerId;
            gameManager.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "AddNewPlayer");
            
            //gameManager.SendCustomEvent(nameof(gameManager.AddNewPlayer));
        //}

    }
}
