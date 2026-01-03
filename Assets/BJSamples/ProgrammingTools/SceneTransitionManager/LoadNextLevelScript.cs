using System;
using UnityEngine;

public class LoadNextLevelScript : MonoBehaviour
{
    [SerializeField] private string NextScene;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            BJ.SceneTransitionManager.LoadNewScene(NextScene);
        }
    }
}
