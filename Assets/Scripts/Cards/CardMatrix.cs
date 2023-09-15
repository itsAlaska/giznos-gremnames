using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;

namespace Cards
{
    public class CardMatrix : UdonSharpBehaviour
    {
        [SerializeField] private GameObject[] outlines = new GameObject[16];
        [SerializeField] private TextMeshProUGUI[] wordFields = new TextMeshProUGUI[16];
        
        public string[] allWords;

        [UdonSynced, FieldChangeCallback(nameof(ThisRoundWords))]
        public string[] thisRoundWords = new string[16];

        public override void OnDeserialization()
        {
            ResetOutlines();
            UpdateCards();
        }

        public void PopulateWordList()
        {
            ResetOutlines();
            
            if (Networking.IsOwner(Networking.LocalPlayer, gameObject))
            {
                var outlineIndices = new int[6];
                for (var i = 0; i < 6; i++)
                {
                    int outlineIndex = Random.Range(0, 16);

                    if (i > 0)
                    {
                        for (var j = 0; j < i; j++)
                        {
                            if (outlineIndices[j] == outlineIndex)
                            {
                                while (outlineIndices[j] == outlineIndex)
                                {
                                    outlineIndex = Random.Range(0, 16);
                                }
                            }
                        }
                    }

                    outlineIndices[i] = outlineIndex;
                }
                for (int g = 0; g < 6; g++)
                {
                    outlines[outlineIndices[g]].SetActive(true);
                }
            }
            
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

        public void ResetOutlines()
        {
            foreach (var outline in outlines) outline.SetActive(false);
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
