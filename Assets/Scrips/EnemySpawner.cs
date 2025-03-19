using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private float timeBetweenSpawns = 2f;

    [SerializeField]
    private GameObject[] powerups;

    [SerializeField]
    private List<int> defaultDropBuckets = new List<int> { 3, 4, 5 };
    private List<int> powerUpDropBuckets;
    private int killCount = 0;
    private int nextPowerUpDrop = 2;

    void Start()
    {
        powerUpDropBuckets = new List<int>(defaultDropBuckets);
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyObject = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            Enemy enemyScript = enemyObject.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.SetSpawner(this);
            }
        }
    }

    public void EnemyDie(Vector2 position)
    {
        killCount++;
        if (killCount >= nextPowerUpDrop)
        {
            UpdateNextPowerUpDrop();
            GameObject powerUp = powerups[Random.Range(0, powerups.Length)];
            Instantiate(powerUp, position, Quaternion.identity);
        }
    }

    private void UpdateNextPowerUpDrop()
    {
        powerUpDropBuckets.RemoveAt(0);
        if (powerUpDropBuckets.Count == 0)
        {
            powerUpDropBuckets = new List<int>(defaultDropBuckets);
            ShufflePowerUpBuckets();
        }
        nextPowerUpDrop += powerUpDropBuckets[0];
    }

    private void ShufflePowerUpBuckets()
    {
        for (int i = powerUpDropBuckets.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (powerUpDropBuckets[i], powerUpDropBuckets[j]) = (
                powerUpDropBuckets[j],
                powerUpDropBuckets[i]
            );
        }
    }
}
