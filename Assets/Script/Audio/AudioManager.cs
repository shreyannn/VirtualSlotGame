using System;
using System.Collections;
using System.Collections.Generic;
using Kauda.Utils;
using UnityEngine;
using UnityEngine.Audio;

namespace VirtualSlot.Core
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("Instance is Null");
                return _instance;
            }
        }
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            SetPreferencesValue();
        }

        [SerializeField] private List<AudioClip> bgAudioClips;
        [SerializeField] private List<AudioSource> _audioSource;
        
        private List<Sound> sounds = new List<Sound>();
        public AudioMixer audioMixer;
        private float multiplier = 20f;
        private int selectedIndex;
        private bool isUserSelected;
        private int userSelectedIndex;
        private int index;
        [SerializeField] private bool isCleared = true;

        public AudioMixerGroup sfxMixerGroup;
        public AudioMixerGroup musicMixerGroup;
        
        #region MainMenuBackgroundTheme
        public void RandomBgAudio()
        {
            StopAllCoroutines();
            StartCoroutine(BgMusicRoutine());
        }
        
        IEnumerator BgMusicRoutine()
        {
            while (true)
            {
                int temp = bgAudioClips.Count;
                if (isUserSelected)
                    index = userSelectedIndex;
                else
                {
                    selectedIndex = PlayerPrefs.GetInt("BackgroundAudioIndex", 0);
                    while (true)
                    {
                        yield return null;
                        index = UnityEngine.Random.Range(0, temp);
                        if (index != selectedIndex)
                        {
                            break;
                        }
                    }
                }
                PlayerPrefs.SetInt("BackgroundAudioIndex", index);
                string name = "MusicTheme";
                int var = ReturnId(name);
                if (var < 0)
                {
                    break;
                }
                Sound s = sounds[var];

                s.audioSource.clip = bgAudioClips[index];
                s.audioSource.Play();
                yield return new WaitForSeconds(s.audioSource.clip.length + 1f);
            }
        }
        #endregion
        

        #region Audio Source Scene SetUp
        public void ClearAudioComponent()
        {
            StopAllCoroutines();
            foreach (Sound s in sounds)
            {
                Destroy(s.audioSource);
            }
            _audioSource.Clear();
            sounds.Clear();
        }
        
        public void SetIsCleared(bool status)
        {
            isCleared = status;
        }
        
        public bool ReturnIsCleared()
        {
            return isCleared;
        }
        
        public void SetAudioSources(List<Sound> sounds, bool isSfx)
        {
            var mixerGroup = isSfx ? sfxMixerGroup : musicMixerGroup;
            foreach (var sound in sounds)
            {
                this.sounds.Add(sound);
            }
            foreach (var s in sounds)
            {
                s.audioSource = gameObject.AddComponent<AudioSource>();
                _audioSource.Add(s.audioSource);
                s.audioSource.clip = s.clip;
                s.audioSource.volume = s.volume;
                s.audioSource.pitch = s.pitch;
                s.audioSource.loop = s.loop;
                s.audioSource.outputAudioMixerGroup = mixerGroup;
            }
        }
        #endregion
        
        #region Player Preferences
        private void SetPreferencesValue()
        {
            audioMixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat("Master", 1f)) * multiplier);
            audioMixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("Music", 1f)) * multiplier);
            audioMixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("SFX", 1f)) * multiplier);
            isUserSelected = IntToBool(PlayerPrefs.GetInt("UserSelectedBool", 0));
            userSelectedIndex = PlayerPrefs.GetInt("BackgroundAudioIndex", 0);
        }
        
        public void SetUserSelectedIndex(int value)
        {
            isUserSelected = true;
            PlayerPrefs.SetInt("UserSelectedBool", BoolToInt(true));
            userSelectedIndex = value;
        }

        public void SetUserSelectedBool(bool status)
        {
            isUserSelected = status;
            PlayerPrefs.SetInt("UserSelectedBool", BoolToInt(status));
        }
        
        public void SFXControl()
        {
            audioMixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("SFX", 1f)) * multiplier);
        }
        public void MasterControl()
        {
            audioMixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat("Master", 1f)) * multiplier);
        }

        #endregion
        

        #region play and stop audioSource

        public void Play(string name)
        {
            int index = ReturnId(name);
            if(index < 0)
            {
                return;
            }
            Sound s = sounds[index];
            if (s == null)
            {
                return;
            }
            s.audioSource.Play();
        }
        public void Stop(string name)
        {
            int index = ReturnId(name);
            if (index < 0)
            {
                return;
            }
            Sound s = sounds[index];
            if (s == null)
            {
                return;
            }
            s.audioSource.Stop();
        }

        #endregion
        
        public int ReturnSoundLength()
        {
            return sounds.Count;
        }
        
        public AudioMixerGroup GetAudioMixer()
        {
            return sfxMixerGroup;
        }

        private bool IntToBool(int value)
        {
            if (value == 0)
                return false;
            return true;
        }
        
        private int BoolToInt(bool status)
        {
            if (status)
                return 1;
            return 0;
        }
        
        private int ReturnId(string name)
        {
            foreach(Sound s in sounds)
            {
                if(s.name == name)
                {
                    return sounds.IndexOf(s);
                }
            }
            return -1;
        }
        
        public String ReturnClipName()
        {
            return bgAudioClips[PlayerPrefs.GetInt("BackgroundAudioIndex", 0)].name;
        }

        public String ReturnClipName(int index)
        {
            return bgAudioClips[index].name;
        }
        
        public AudioMixer ReturnAudioMixer()
        {
            return audioMixer;
        }
    }
}
