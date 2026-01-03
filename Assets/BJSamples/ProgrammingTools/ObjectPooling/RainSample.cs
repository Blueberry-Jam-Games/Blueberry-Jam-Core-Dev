using System.Collections;
using System.Collections.Generic;
using BJ;
using UnityEngine;

namespace BJ.Samples
{
    public class RainSample : MonoBehaviour
    {
        public ObjectPool rainPool;

        public List<GameObject> raindrops;

        public int maxDelay = 10;
        private int delay;

        public GameObject raindropPrefab;

        [Header("Demo Configuration")]
        [Tooltip("Allows enabling or disabling the object pool. If it is disabled, Instantiate and Destroy will be used instead")]
        public bool poolObjects = true;

        private void Awake()
        {
            raindrops = new List<GameObject>();
        }

        private void FixedUpdate()
        {
            if (delay == 0)
            {
                AddRaindrop();
                CheckRaindropEnd();
                delay = maxDelay;
            }
            else
            {
                delay--;
            }
        }

        private void AddRaindrop()
        {
            GameObject raindrop;
            if (poolObjects)
            {
                raindrop = rainPool.DePool();
            }
            else
            {
                raindrop = GameObject.Instantiate(raindropPrefab);
            }
            raindrop.transform.position = new Vector3(Random.Range(-5f, 5f), transform.position.y, transform.position.z);
            raindrops.Add(raindrop);
        }

        private void CheckRaindropEnd()
        {
            for (int i = 0; i < raindrops.Count; i++)
            {
                GameObject raindrop = raindrops[i];
                if (raindrop.transform.position.y < -3f)
                {
                    raindrops.RemoveAt(i);
                    i--;
                    if (poolObjects)
                    {
                        rainPool.RePool(raindrop);
                    }
                    else
                    {
                        GameObject.Destroy(raindrop);
                    }
                }
            }
        }
    }
}
