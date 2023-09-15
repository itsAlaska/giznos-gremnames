using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;

namespace Cards
{
    public class CardMatrix : UdonSharpBehaviour
    {
        // [SerializeField] private GameObject[] cards;
        [SerializeField] private TextMeshProUGUI[] wordFields = new TextMeshProUGUI[16];

        
        public string[] allWords;

        [UdonSynced, FieldChangeCallback(nameof(ThisRoundWords))]
        public string[] thisRoundWords = new string[16];

        public override void OnDeserialization()
        {
            UpdateCards();
        }

        public void PopulateWordList()
        {
            var wordIndices = new int[16];
            var wordsArrayForThisRound = new string[16];
            
            for (var i = 0; i < 16; i++)
            {
                int wordIndex = Random.Range(0, allWords.Length);

                if (i > 0)
                {
                    for (var j = 0; j < i; j++)
                    {
                        if (wordIndices[j] == wordIndex)
                        {
                            while (wordIndices[j] == wordIndex)
                            {
                                wordIndex = Random.Range(0, allWords.Length);
                            }
                        }
                    }
                }

                wordIndices[i] = wordIndex;
            }

            for (int g = 0; g < wordIndices.Length; g++)
            {
                wordsArrayForThisRound[g] = allWords[wordIndices[g]];
            }

            ThisRoundWords = wordsArrayForThisRound;
            RequestSerialization();
        }

        public void UpdateCards()
        {
            for (var i = 0; i < wordFields.Length; i++)
            {
                wordFields[i].text = ThisRoundWords[i];
            }
        }
        
        // public string[] AllWords
        // {
        //     set
        //     {
        //         allWords = value;
        //         RequestSerialization();
        //     }
        //     get => allWords;
        // }

        public string[] ThisRoundWords
        {
            set
            {
                thisRoundWords = value;
                UpdateCards();
            }
            get => thisRoundWords;
        }
    }
}
