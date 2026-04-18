using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    public int spawnInterval = 2;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer -= 2;
        }
    }

void SpawnEnemy()
{
    GameObject enemy = Instantiate(enemyPrefab);
    enemy.transform.position = transform.position;
    enemy.GetComponent<EnemyController>().player = player;
}
}