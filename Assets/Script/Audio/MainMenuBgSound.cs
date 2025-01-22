using System.Collections;
using System.Collections.Generic;
using VirtualSlot.Core;
using UnityEngine;

namespace Kauda
{
    public class MainMenuBgSound : MonoBehaviour
    {
        private void OnEnable()
        {
            if(AudioManager.Instance.ReturnIsCleared())
            {
                Invoke(nameof(PlayBgSound),0.2f);
            }
        }

        private void PlayBgSound()
        {
            AudioManager.Instance.SetIsCleared(false);
            AudioManager.Instance.RandomBgAudio();
        }
    }
}
