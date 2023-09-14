using Cards;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace Testing
{
    
    public class ToggleBox : UdonSharpBehaviour
    {
        [SerializeField] private WordList wordList;
        [SerializeField] private CardMatrix cardMatrix;
        
        private void Interact()
        {
            var currentCardMatrixOwner = Networking.GetOwner(cardMatrix.gameObject);
            var localPlayer = Networking.LocalPlayer;
            
            if(localPlayer != currentCardMatrixOwner) Networking.SetOwner(localPlayer, cardMatrix.gameObject);
            
            if (cardMatrix.AllWords.Length > 0)
            {
                cardMatrix.PopulateWordList();
            }
            else
            {
                wordList._DownloadList();
            }
            
        }
    }
}
