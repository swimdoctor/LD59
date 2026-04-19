using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int moveSpeed = 2;
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;
    public GameObject player;
    public PlayerController guy;

    public EnemyType enemyType;
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
        Vector3 direction = (Vector3)player.transform.position - (Vector3)transform.position;
        direction.Normalize();
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		if(currentHealth <= 0)
		{
            Destroy(gameObject);
		}
	}

    void OnDestroy()
{
    guy.AddMoney(10);
}

    public enum EnemyType
    {
        TestMonster,
        TestMonster2,
        EyeMonster
    }
}
