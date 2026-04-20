using System.Collections.Generic;
using UnityEngine;

public enum TowerType {
	Fire,
	Laser,
	Wind,
	Earthquake,
	Sound
}

public abstract class Tower : MonoBehaviour
{
	public static List<Tower> towers;

	public bool starting;
	public bool isMoving = false;

	// Level Management vars
	[SerializeField]
	protected int level = 1;
	protected Dictionary<int, int> levelToUpgradeCost;

	// Slow Management Vars. Overridden in EQ tower
	[SerializeField]
	protected float speedMultiplier = 1f;
	[SerializeField]
	protected float slowDuration = 0f;

	[SerializeField] private float activeCooldown;
	public bool Active 
	{
		get => activeCooldown > 0 || activeCooldown == -1;
		set
		{
			activeCooldown = (value ? 1 : 0);
			hitbox.GetComponent<SpriteRenderer>().enabled = Active;
		}
	}
	protected Rigidbody2D hitbox;
	public TowerType detection;

	public abstract TowerType TowerType { get; }
	public abstract int Damage { get; set; }
	public virtual float Cooldown { get => 0.1f; }
	
	protected float attackCooldown = 0;

	public virtual void Awake()
	{
		towers ??= new List<Tower>();
		if(!towers.Contains(this)) towers.Add(this);

		hitbox = transform.Find("Hitbox").GetComponent<Rigidbody2D>();
		if(starting) activeCooldown = -1;
	}

	public virtual void Update()
	{
		if(isMoving) return;
		if(Active)
		{
			attackCooldown += Time.deltaTime;

			while(attackCooldown > Cooldown)
			{
				Shoot();
				attackCooldown -= Cooldown;
			}
		}
		if(activeCooldown > 0)
		{
			activeCooldown -= Time.deltaTime;
			if(activeCooldown < 0) Active = false;
		}
	}

	public virtual void Shoot()
	{
		RaycastHit2D[] targets = new RaycastHit2D[128];
		hitbox.Cast(Vector2.up, targets, 0);

		foreach(RaycastHit2D target in targets)
		{
			if(!target) break;

			Tower tower = target.collider.GetComponent<Tower>();
			EnemyController enemy = target.collider.GetComponent<EnemyController>();
			if(tower != null)
			{
				if(tower == this) continue;

				if(tower.detection == TowerType) tower.Active = true;
			}
			else if(enemy != null)
			{
				enemy.TakeDamage(Damage, speedMultiplier, slowDuration);
			}
		}
	}

	public abstract void UpgradeTower();

	public static void DisableTowers()
	{
		foreach(Tower tower in towers) tower.enabled = false;
	}

	/*
	*  Returns -1 if there is no cost
	*/
	public int getUpgradeCost()
	{
		if (levelToUpgradeCost.TryGetValue(level, out int upgradeCost))
		{
			return upgradeCost;
		}
		else
		{
			return -1;
		}
	}

	public int getLevel()
	{
		return level;
	}

	public void OnDestroy()
	{
		towers.Remove(this);
	}
}
