using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BJ.Samples
{
    public class CookieCrusadeTransition : LevelTransitionEffect
    {
        [Header("Prefab Components")]
        [SerializeField]
        private CanvasGroup fadeGroup;
        [SerializeField]
        private Slider gignerbreadMove;
        [SerializeField]
        private Image gingerbreadSprite;

        [Header("Animation Sequence")]
        [SerializeField]
        private List<Sprite> animationSequence;

        private int animationFrame = 0;
        // 8 frames per second
        private float secondsPerFrame = 1f / 8f;
        private float timeLastAnimation = 0f;

        private float animationTime = 0.5f;
        private float walkTime = 2.0f;

        private void Start()
        {
            animationFrame = 0;
            gingerbreadSprite.sprite = animationSequence[animationFrame];
            timeLastAnimation = Time.time;
        }

        private void Update()
        {
            if (Time.time - timeLastAnimation >= secondsPerFrame)
            {
                animationFrame++;
                if (animationFrame >= animationSequence.Count)
                {
                    animationFrame = 0;
                }

                gingerbreadSprite.sprite = animationSequence[animationFrame];
                timeLastAnimation = Time.time;
            }
        }

        public override IEnumerator CurtainDown()
        {
            float time = 0f;

            while (time < animationTime)
            {
                time += Time.deltaTime;
                float newAlpha = Mathf.Clamp01(time / animationTime);
                fadeGroup.alpha = newAlpha;

                yield return null;
            }

            fadeGroup.alpha = 1.0f;
        }

        public override IEnumerator CurtainUp()
        {
            float time = 0f;

            while (time < animationTime)
            {
                time += Time.deltaTime;
                float newAlpha = 1.0f - Mathf.Clamp01(time / animationTime);
                fadeGroup.alpha = newAlpha;

                yield return null;
            }

            fadeGroup.alpha = 0.0f;
        }

        public override void JumpToCurtainDown()
        {
            fadeGroup.alpha = 1.0f;
            gignerbreadMove.value = 0f;
        }

        public override void JumpToCurtainUp()
        {
            fadeGroup.alpha = 1.0f;
            gignerbreadMove.value = 0f;
        }

        // The Gingerbread Man will take a total of 2 seconds to walk accross the screen
        // Spread out by the increments of progress updates
        public override IEnumerator UpdateProgress(double progress)
        {
            float fProgress = (float)progress;
            float current = gignerbreadMove.value;
            float delta = fProgress - current;

            float time = 0;
            float totalTime = delta * walkTime;

            while (time < totalTime)
            {
                time += Time.deltaTime;
                gignerbreadMove.value = Mathf.Lerp(current, fProgress, time / totalTime);

                yield return null;
            }
        }
    }
}
