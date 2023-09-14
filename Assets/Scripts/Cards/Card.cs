using TMPro;
using UdonSharp;
using UnityEngine;

namespace Cards
{
    public class Card : UdonSharpBehaviour
    {
        public string[] words;

        // [UdonSynced, FieldChangeCallback(nameof(Word))] public string word;
        public string word;
        
        [SerializeField] private TextMeshProUGUI wordField;

        public void PickWord()
        {
            int wordListIndex = Random.Range(0, words.Length + 1);
            
            Debug.Log($"The wordListIndex value is {wordListIndex}");
            word = words[wordListIndex];
            wordField.text = word;
        }

        public void UpdateCardDisplay(string currentWord)
        {
            wordField.text = currentWord;
        }

        // public string Word
        // {
        //     set
        //     {
        //         word = value;
        //         UpdateCardDisplay(word);
        //     }
        //     get => word;
        // }
    }
}
