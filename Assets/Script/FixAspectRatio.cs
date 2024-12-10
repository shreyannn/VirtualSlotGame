using UnityEngine;

namespace Kauda.Core
{
    public class FixAspectRatio : MonoBehaviour
    {
        private float _aspectScreenWidth=16.0f;
        private float _aspectScreenHeight=9.0f;
        [SerializeField] private bool isLandscape=true;
        [SerializeField] private bool hasAnimation;
        [SerializeField] private bool HasObject;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject blackOverlay;
        private float _screenWidth;
        private float _screenHeight;

        private void OnEnable()
        {
            if (isLandscape)
            {
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                _aspectScreenWidth = 16f;
                _aspectScreenHeight = 9f;
                ChangeAspect();
                return;
            }
            Screen.orientation = ScreenOrientation.Portrait;
            _aspectScreenWidth = 9f;
            _aspectScreenHeight = 16f;
            ChangeAspect();
        }

        private void ChangeAspect()
        {
            _screenHeight= Screen.height;
            _screenWidth= Screen.width;
            if (_screenWidth < _screenHeight && isLandscape)
            {
                (_screenWidth, _screenHeight) = (_screenHeight, _screenWidth);
                Screen.orientation = ScreenOrientation.LandscapeLeft;
            }
            
            float targetaspect = _aspectScreenWidth / _aspectScreenHeight;

            // determine the game window's current aspect ratio
            float windowaspect;
            if (_aspectScreenWidth / _aspectScreenHeight > 1)
                windowaspect = _screenWidth / _screenHeight;
            else
                windowaspect = _screenHeight / _screenWidth;

            // current viewport height should be scaled by this amount
            float scaleheight = windowaspect / targetaspect;//

            // obtain camera component so we can modify its viewport
            Camera camera = GetComponent<Camera>();

            // if scaled height is less than current height, add letterbox
            if (scaleheight < 1.0f)
            {
                Rect rect = camera.rect;

                rect.width = 1.0f;
                rect.height = scaleheight;
                rect.x = 0;
                rect.y = (1.0f - scaleheight) / 2.0f;

                camera.rect = rect;
            }
            else // add pillarbox
            {
                float scalewidth = 1.0f / scaleheight;

                Rect rect = camera.rect;

                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scalewidth) / 2.0f;
                rect.y = 0;

                camera.rect = rect;
            }
            if(HasObject)
                Invoke(nameof(DisableObject),0.2f);
            
            if (hasAnimation)
            {
                Invoke(nameof(EnableAnimator),0.2f);
                Invoke(nameof(DisableBlack),0.5f);
            }
        }

        private void DisableObject()
        {
            blackOverlay.SetActive(false);
        }
        
        private void EnableAnimator()
        {
            animator.enabled = true;
        }

        private void DisableBlack()
        {
            animator.gameObject.SetActive(false);
        }
    }
}
