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

        [UdonSynced, FieldChangeCallback(nameof(AllWords))]
        public string[] allWords;

        [UdonSynced, FieldChangeCallback(nameof(ThisRoundWords))]
        public string[] thisRoundWords = new string[16];

        public void PopulateWordList()
        {
            var wordIndices = new int[16];
            var wordsArrayForThisRound = new string[16];
            
            for (var i = 0; i < 16; i++)
            {
                int wordIndex = Random.Range(0, AllWords.Length);

                if (i > 0)
                {
                    for (var j = 0; j < i; j++)
                    {
                        if (wordIndices[j] == wordIndex)
                        {
                            while (wordIndices[j] == wordIndex)
                            {
                                wordIndex = Random.Range(0, AllWords.Length);
                            }
                        }
                    }
                }

                wordIndices[i] = wordIndex;
            }

            for (int g = 0; g < wordIndices.Length; g++)
            {
                wordsArrayForThisRound[g] = AllWords[wordIndices[g]];
            }

            ThisRoundWords = wordsArrayForThisRound;
            RequestSerialization();
        }
        
        public string[] AllWords
        {
            set
            {
                allWords = value;
                RequestSerialization();
            }
            get => allWords;
        }

        public string[] ThisRoundWords
        {
            set
            {
                thisRoundWords = value;
                for (int i = 0; i < wordFields.Length; i++)
                {
                    wordFields[i].text = ThisRoundWords[i];
                }
            }
            get => thisRoundWords;
        }
    }
}
