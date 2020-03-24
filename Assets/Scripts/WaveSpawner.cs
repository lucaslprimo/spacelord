using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    public Wave[] waves;

    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    private Transform player;
    private int currentWaveIndex;
    private bool spawnFinished;
    public Boss boss;

    public GameObject bossMusic;
    public GameObject ambientMusic;

    private bool finalRound;

    private float winTime;
    public float secondsForWinScreen;
    public SceneTransictions sceneTransictions;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(0));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        if (currentWaveIndex < waves.Length)
        {
            spawnFinished = false;
            currentWave = waves[index];

            for (int i = 0; i < currentWave.count; i++)
            {

                if (player == null)
                {
                    yield break;
                }

                Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
                Transform spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)];

                Instantiate(randomEnemy, spawnPosition.position, spawnPosition.rotation);
                yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
            }

            spawnFinished = true;
            currentWaveIndex++;
        }
    }

    private void Update()
    {
        if (player != null && GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && spawnFinished && !finalRound)
        {
            if (currentWaveIndex == waves.Length)
            {
                StartCoroutine(StartBoss());
            }
            else
            {
                spawnFinished = false;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
        }


        if(finalRound && GameObject.FindGameObjectsWithTag("Boss").Length == 0)
        {
            if (winTime == 0)
            {
                winTime = Time.time + secondsForWinScreen;
            }
            else
            {
                if (Time.time >= winTime)
                {
                    sceneTransictions.LoadScene("Win");
                }
            }
        }
    }

    IEnumerator StartBoss()
    {
        Destroy(ambientMusic);
        bossMusic.SetActive(true);
        yield return new WaitForSeconds(2);
        boss.gameObject.SetActive(true);
        finalRound = true;
    }
}
