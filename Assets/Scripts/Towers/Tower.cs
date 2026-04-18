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
	public abstract TowerType towerType { get; }
	public TowerType detection;

	public float cooldown = 0.1f;
	public int damage;
	protected float attackCooldown = 0;

	public virtual void Awake()
	{
		if(towers == null) towers = new List<Tower>();
		if(!towers.Contains(this)) towers.Add(this);

		hitbox = transform.Find("Hitbox").GetComponent<Rigidbody2D>();
		if(starting) activeCooldown = -1;
	}

	public virtual void Update()
	{
		if(Active) Shoot(Time.deltaTime);
		if(activeCooldown > 0)
		{
			activeCooldown -= Time.deltaTime;
			if(activeCooldown < 0) Active = false;
		}
	}

	public abstract void Shoot(float deltaTime);

	public static void DisableTowers()
	{
		foreach(Tower tower in towers) tower.enabled = false;
	}
}
