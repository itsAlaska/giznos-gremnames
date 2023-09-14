using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using VRC.Udon.Common.Interfaces;

namespace Cards
{
    public class Card : UdonSharpBehaviour
    {
        [UdonSynced, FieldChangeCallback(nameof(Words))] public string[] words;

        [UdonSynced, FieldChangeCallback(nameof(Word))] public string word;
        
        public TextMeshProUGUI wordField;

        public void PickWord()
        {
            
            int wordListIndex = Random.Range(0, words.Length + 1);
            Word = Words[wordListIndex];
            RequestSerialization();
        }

        // Field Change Callbacks
        public string[] Words
        {
            set
            {
                words = value;
                RequestSerialization();
            }
            get => words;
            
        }
        
        public string Word
        {
            set
            {
                word = value;
                wordField.text = Word;
            }
            get => word;
        }
    }
}
