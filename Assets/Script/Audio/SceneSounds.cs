using System;
using System.Collections;
using System.Collections.Generic;
using VirtualSlot.Core;
using Kauda.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kauda
{
    public class SceneSounds : MonoBehaviour
    {
        [Header("SFXGroup Sound List")]
        [SerializeField] private List<Sound> sfxSounds;
        [Header("MusicGroup Sound List")]
        [SerializeField] private List<Sound> musicSounds;
#if !IS_SERVER
        private void Start()
        {
            if(AudioManager.Instance.ReturnSoundLength() == 0)
            {
                AudioManager.Instance.SetAudioSources(sfxSounds, true);
                AudioManager.Instance.SetAudioSources(musicSounds, false);
            
            }
        }

        private void OnEnable()
        {
            if(SceneManager.GetActiveScene().name != "Main_Menu") return;
            if(AudioManager.Instance.ReturnSoundLength() == 0)
            {
                AudioManager.Instance.SetAudioSources(sfxSounds, true);
                AudioManager.Instance.SetAudioSources(musicSounds, false);
            }
        }

#endif
    }
}
