using System;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;

namespace Cards
{
    public class WordList : UdonSharpBehaviour
    {
        public VRCUrl wordListUrl;
        public float reloadDelay = 60;
        public CardMatrix cardMatrix;
        
        public void _DownloadList()
        {
            VRCStringDownloader.LoadUrl(wordListUrl, (IUdonEventReceiver)this);
            SendCustomEventDelayedSeconds(nameof(_DownloadList), reloadDelay);
        }

        public override void OnStringLoadSuccess(IVRCStringDownload result)
        {
            if (cardMatrix.AllWords.Length > 0) return;
            cardMatrix.AllWords = result.Result.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            cardMatrix.PopulateWordList();
        }

        public override void OnStringLoadError(IVRCStringDownload result)
        {
            Debug.Log(result.Error);   
        }
    }
}
