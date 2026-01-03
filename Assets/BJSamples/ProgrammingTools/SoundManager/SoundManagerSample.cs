using System.Collections;
using System.Collections.Generic;
using BJ;
using UnityEngine;

public class SoundManagerSample : MonoBehaviour
{
    private SoundManager sounds;

    private void Start()
    {
        sounds = GetComponent<SoundManager>();
    }

    private void Update()
    {
#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.GetMouseButtonDown(0))
        {
            // left click
            sounds.PlaySound("wand1");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            // right click
            sounds.PlaySound("wand2");
        }
#endif
    }
}
