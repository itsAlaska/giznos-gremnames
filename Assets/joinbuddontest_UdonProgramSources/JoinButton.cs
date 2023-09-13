
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
    [UdonSynced]
    public int lastClickedId = 0;

    public double buttonDelay = 3;
    private double _buttonCounter = 0;
    

    public override void Interact()
    {
        if (interactable == true)
        {
            interactable = false;
            DisableInteractive = true; ;
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            lastClickedId = Networking.LocalPlayer.playerId;
            RequestSerialization();
            gameManager.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "AddNewPlayer");
        }
    }

    public void Update()
    {
        if(interactable == false)
        {
            if(_buttonCounter >= buttonDelay)
            {
                interactable = true;
                DisableInteractive = false;
                _buttonCounter = 0;
            }
            else
            {
                _buttonCounter += Time.deltaTime;
            }
        }
    }
}

