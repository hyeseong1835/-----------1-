using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnInterval = 2f;
    float timer;
    public Pool enemyPool;

    void Update()
    {
        this.timer += Time.deltaTime;
        if (this.timer >= this.spawnInterval)
        {
            SpawnEnemy();
            this.timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Enemy enemy = this.enemyPool.Get().GetComponent<Enemy>();
        enemy.pool = this.enemyPool;
        enemy.transform.position = this.transform.position;
        enemy.Init();
    }
}