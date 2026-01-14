using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace BJ.Samples
{
    public class DoTransition : MonoBehaviour
    {
        // Lets us reuse the same level without double registering transtions.
        static bool loadedOnce = false;
        // Save the current dropdown for better usability.
        static int transitionSelectValuePrevious = 0;

        [Tooltip("This is visible in editor for deubgging, setting it manually can and will break things")]
        public bool transitionPlaying = false;

        [Header("Scene Refernces")]
        [SerializeField]
        private Button goButton;
        [SerializeField]
        private TMP_Dropdown transitionSelect;
        [SerializeField]
        private LevelTransitionEffect cookieCrusadeTransition;

        // I don't have a creative way to map this right now so it matches the options I know are in the level loader.
        private readonly List<string> transitionOptions = new List<string> { "None", "FadeBlackLoadbar", "FadeGreenNoBar", "CookieCrusade" };

        private void Start()
        {
            transitionPlaying = false;
            goButton.onClick.AddListener(TransitionPressed);

            if (!loadedOnce)
            {
                loadedOnce = true;
                BJ.LevelTransitionEffect.Templates.FadeTransition("FadeBlackLoadbar", 1.0f, Color.black, true, Color.red, Color.green);
                BJ.LevelTransitionEffect.Templates.FadeTransition("FadeGreenNoBar", 1.0f, Color.green, false, Color.black, Color.black);
                BJ.LevelLoader.Instance.RegisterTransition("CookieCrusade", cookieCrusadeTransition);
            }

            transitionSelect.AddOptions(transitionOptions);
            transitionSelect.value = transitionSelectValuePrevious;
        }

        private void TransitionPressed()
        {
            string transition = transitionOptions[transitionSelect.value];
            transitionSelectValuePrevious = transitionSelect.value;
            // This is never set to false, a new instance of this component is later created with it as false already.
            transitionPlaying = true;
            BJ.LevelLoader.Instance.LoadLevel("StarterLevel", transition);
        }
    }
}
