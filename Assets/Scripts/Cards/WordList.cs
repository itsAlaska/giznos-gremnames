using System;
using System.Linq;
using UdonSharp;
using UnityEngine;
using UnityEngine.Serialization;
using VRC.SDK3.Data;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;

namespace Cards
{
    public class WordList : UdonSharpBehaviour
    {
        public VRCUrl wordListUrl;

        public float reloadDelay = 60;

        public string wordPage;

        [UdonSynced] public string[] words;

        private void Start()
        {
            _DownloadList();
        }

        public void _DownloadList()
        {
            VRCStringDownloader.LoadUrl(wordListUrl, (IUdonEventReceiver)this);
            SendCustomEventDelayedSeconds(nameof(_DownloadList), reloadDelay);
        }

        public override void OnStringLoadSuccess(IVRCStringDownload result)
        {
            wordPage = result.Result;

            words = wordPage.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            Debug.Log(words);
        }

        public override void OnStringLoadError(IVRCStringDownload result)
        {
            Debug.Log(result.Error);
        }
    }
}
