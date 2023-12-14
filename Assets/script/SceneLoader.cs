using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName; // Variable to hold the scene name to load

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(sceneName); // Load the scene with the specified name
    }
}
