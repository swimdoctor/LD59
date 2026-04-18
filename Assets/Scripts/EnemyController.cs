using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int moveSpeed = 2;
    public int maxHealth = 100;
    public int currentHealth;

    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (Vector3)player.transform.position - (Vector3)transform.position;
        direction.Normalize();
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }
}
