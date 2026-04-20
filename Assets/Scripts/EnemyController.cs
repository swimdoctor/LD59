using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class EnemyController : MonoBehaviour
{
    public int moveSpeed = 2;
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;
    public GameObject player;
    public PlayerController guy;

    public EnemyType enemyType;

    // Temp var
    private float speedMultiplier = 1f;
    private float slowDuration = 0f;

    // Pathing
    // Path vectors is initialized by the WaveSpawner instantiation
    private List<Vector2> pathVectors;
    private int pathNodeIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        if(guy==null){
            guy=player.GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSpeed();
        MoveEnemy();
    }

    public void SetPathVectors(List<Vector2> vectors)
    {
        pathVectors = vectors;
    }

    void CalculateSpeed()
    {
        slowDuration -= Time.deltaTime;
        if(slowDuration <= 0)
        {
            speedMultiplier = 1f;
        }
    }

    void MoveEnemy()
    {
        // Ensure we don't go OOB
        if (pathNodeIndex < pathVectors.Count)
        {
            Vector2 targetVec = pathVectors[pathNodeIndex];

            transform.position = Vector2.MoveTowards(
                transform.position,
                targetVec,
                moveSpeed * speedMultiplier * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, targetVec) < 0.1f)
            {
                pathNodeIndex++;
            }
        }
    }

	public void TakeDamage(int amount, float speedMultiplier = 1, float slowDuration = 0)
	{
        // Adjust speed
        this.slowDuration = Mathf.Max(slowDuration, this.slowDuration);
        this.speedMultiplier = Mathf.Min(speedMultiplier, this.speedMultiplier);

        // Adjust health
		currentHealth -= amount;
		if(currentHealth <= 0)
		{
            Destroy(gameObject);
		}
	}

    void OnDestroy()
{
    guy.ChangeMoney(10);
}

    public enum EnemyType
    {
        TestEnemy1,
        TestEnemy2,
        EyeMonster
    }
}
