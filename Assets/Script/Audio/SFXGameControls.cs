using UnityEngine;
using UnityEngine.UI;
using VirtualSlot.Core;

namespace Kauda
{
    public class SFXGameControls : MonoBehaviour
    {
        public static SFXGameControls Instance { get; private set; }
        
        [SerializeField] private Sprite MuteImage;
        [SerializeField] private Sprite NormalImage;
        [SerializeField] private Image targetImage;
        [SerializeField] private bool hasAudioChangeText = false;
        [SerializeField] private AudioChangeText audioChangeText;
        [SerializeField] private bool changeSpriteAtStart = true;
        [SerializeField] private bool changeLoginSoundSprite;
        private bool soundOn;
        private float minValue = 0.0001f;
        private float maxValue;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
        }

        private void Start()
        {
            if(changeSpriteAtStart)
                ChangeSprite();

            if (changeLoginSoundSprite)
                SetLoginSoundSprite();
        }
        public void SetTargetImage(Image img)
        {
            targetImage = img;
        }
        
        private void SetLoginSoundSprite()
        {
            maxValue = PlayerPrefs.GetFloat("Master", 1f);
            if (maxValue == 0.0001f)
            {
                maxValue = 1f;
            }
            if (minValue == PlayerPrefs.GetFloat("Master", 0f))
            {
                soundOn = false;
                targetImage.sprite = MuteImage;
                PlayerPrefs.SetFloat("Master", minValue);
                AudioManager.Instance.MasterControl();
                return;
            }
            soundOn = true;
            targetImage.sprite = NormalImage;
            PlayerPrefs.SetFloat("Master",maxValue);
            AudioManager.Instance.MasterControl();
        }

        public void ChangeSprite()
        {
            maxValue = PlayerPrefs.GetFloat("SFX", 1f);
            if(maxValue == 0.0001f)
            {
                maxValue = 1f;
            }
            if (minValue == PlayerPrefs.GetFloat("SFX", 0f))
            {
                soundOn = false;
                targetImage.sprite = MuteImage;
                PlayerPrefs.SetFloat("SFX", minValue);
                AudioManager.Instance.SFXControl();
                if (hasAudioChangeText)
                    audioChangeText.SetOffText();
            }
            else
            {
                soundOn = true;
                targetImage.sprite = NormalImage;
                PlayerPrefs.SetFloat("SFX", maxValue);
                AudioManager.Instance.SFXControl();
                if (hasAudioChangeText)
                    audioChangeText.SetOnText();
            }
        }

        public void Sound()
        {
            if (soundOn)
            {
                soundOn = false;
                targetImage.sprite = MuteImage;
                PlayerPrefs.SetFloat("SFX", minValue);
                AudioManager.Instance.SFXControl();
                if (hasAudioChangeText)
                    audioChangeText.SetOffText();
            }
            else
            {
                soundOn = true;
                targetImage.sprite = NormalImage;
                PlayerPrefs.SetFloat("SFX", maxValue);
                AudioManager.Instance.SFXControl();
                if (hasAudioChangeText)
                    audioChangeText.SetOnText();
            }
            
        }
    
        
        

        
    }
}
