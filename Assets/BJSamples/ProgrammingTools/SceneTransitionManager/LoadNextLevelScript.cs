using System;
using UnityEngine;

public class LoadNextLevelScript : MonoBehaviour
{
    [SerializeField] private string NextScene;
    void Update()
    {
#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            BJ.SceneTransitionManager.LoadNewScene(NextScene);
        }
#endif
    }
}
