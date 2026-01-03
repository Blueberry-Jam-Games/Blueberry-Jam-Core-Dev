using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BJ.Samples
{
    public class RunDifferentCoroutines : MonoBehaviour
    {
        public void Start()
        {
            StartCoroutine(EfficientEndOfFrame());
            StartCoroutine(EfficientWaitForFixedUpdate());
            StartCoroutine(EfficientWaitForSeconds());
            StartCoroutine(UnsalvagableWaitForSeconds());

            // Quick call functions
            BJ.Coroutines.DoNextFrame(() =>
            {
                Debug.Log("It is the next frame now.");
            });

            BJ.Coroutines.DoAtEndOfFrame(() =>
            {
                Debug.Log("It's the end of the frame");
            });

            BJ.Coroutines.DoAtFixedUpdate(() =>
            {
                Debug.Log("It's a FixedUpdate");
            });

            BJ.Coroutines.DoInSeconds(1.0f, () =>
            {
                Debug.Log("It's 1 second later, and it's efficient");
            });
        }

        public IEnumerator EfficientEndOfFrame()
        {
            yield return BJ.Coroutines.waitForEndOfFrame;
            Debug.Log("End of Frame");
        }

        public IEnumerator EfficientWaitForFixedUpdate()
        {
            yield return BJ.Coroutines.waitForFixedUpdate;
            Debug.Log("FixedUpdate");
        }

        public IEnumerator EfficientWaitForSeconds()
        {
            yield return BJ.Coroutines.WaitforSeconds(0.25f);
            Debug.Log("1/4 seconds later");
        }

        // It is not worth the cost of caching random delay values, as such just use normal new WaitForSeconds.
        // The cost is not the RAM of storing it but rather any resize costs incurred on the storage container.
        public IEnumerator UnsalvagableWaitForSeconds()
        {
            yield return new WaitForSeconds(Random.Range(0f, 1f));
            Debug.Log("A random ammount of time in the future");

            // If you don't know how long the wait will be it's ok to use the caching function in case the time is in the cache.
            yield return BJ.Coroutines.WaitforSeconds(Random.Range(0f, 0.5f));
            Debug.Log("It's even more randomly in the future");
        }
    }
}
