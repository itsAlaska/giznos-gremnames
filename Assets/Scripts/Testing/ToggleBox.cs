using Cards;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace Testing
{
    
    public class ToggleBox : UdonSharpBehaviour
    {
        [SerializeField] private WordList wordList;
        [SerializeField] private Card card;
        
        private void Interact()
        {
            var currentCardOwner = Networking.GetOwner(card.gameObject);
            var localPlayer = Networking.LocalPlayer;
            
            if(localPlayer != currentCardOwner) Networking.SetOwner(localPlayer, card.gameObject);
            
            if (card.words.Length > 0)
            {
                card.PickWord();
            }
            else
            {
                wordList._DownloadList();
            }
            
        }
    }
}
