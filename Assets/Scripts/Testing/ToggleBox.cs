using Cards;
using UdonSharp;
using UnityEngine;

namespace Testing
{
    
    public class ToggleBox : UdonSharpBehaviour
    {
        // [SerializeField] private WordList wordList;
        [SerializeField] private Card card;
        
        private void Interact()
        {
            if (card.words.Length > 0)
            {
                card.PickWord();
            }
            else
            {
                // wordList._DownloadList();
            }
            
        }
    }
}
