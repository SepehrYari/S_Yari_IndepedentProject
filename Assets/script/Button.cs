using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    private Button button;
    private SpawnManager gameManager;

    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void SetDifficulty()
    {
        gameManager.GameStart(difficulty);
    }
}
