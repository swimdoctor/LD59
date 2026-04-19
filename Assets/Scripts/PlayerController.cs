using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Money text;
    public int money = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        text.SetText("Money: " + money);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeMoney(int amount)
    {
        money += amount;
        text.SetText("Money: " + money);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        print("Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            print("u r a bum");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        TakeDamage(enemy.damage);

        if (enemy != null)
        {
            Destroy(collision.gameObject);
        }
    }

    public float GetHealthPercent()
    {
        return (float)currentHealth / maxHealth;
    }

}
