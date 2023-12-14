using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject obsPrefab;
    public Vector3 spawnPos;
    private FirstPersonController Playercontroller;
    private float SpawnRate = 2.0f;
    public List<GameObject> prefabs;

    public GameObject enemyPrefab;
    private float spawnRange = 8.5f;
    private int enemyCount;

    private int waveNumber = 1;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI gameOverText;
    public RawImage GameoverBanner;
    //public Button RestartButton;
    private int score = 0;
    public bool GameActive = false;
    //public GameObject titleScreen;

    public void GameOver()
    {
        GameActive = false;
        GameoverBanner.gameObject.SetActive(true);
        //gameOverText.gameObject.SetActive(true);
        //RestartButton.gameObject.SetActive(true);
    }

   

    public void UpdateScore(int scoreData)
    {
        score += scoreData;
        if (score < 0)
        {
            score = 0;
        }
        ScoreText.text = "Score: " + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    void Start()
    {
        SpawnWave(waveNumber);
        Playercontroller = GameObject.Find("Player").GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        //Debug.Log(FindObjectsOfType<Enemy>().Length);
        Debug.Log(enemyCount);

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnWave(waveNumber);
        }
    }

    void SpawnWave(int enemyNum)
    {
        for (int i = 0; i < enemyNum; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    IEnumerator SpawnTarget()
    {
        while (GameActive)
        {
            yield return new WaitForSeconds(SpawnRate);
            Instantiate(prefabs[Random.Range(0, prefabs.Count)]);

        }
    }

    public void GameStart(int difficulty)
    {
        GameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        SpawnRate = SpawnRate / difficulty;
        Debug.Log("Game spawn rate = " + SpawnRate);

    }

    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRange, spawnRange);
        float zPos = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(xPos, enemyPrefab.transform.position.y, zPos);
        return spawnPos;
    }
}
