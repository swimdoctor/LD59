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

	public bool active;
	public Rigidbody2D hitbox;
	public abstract TowerType towerType { get; }
	public TowerType detection;

	public float cooldown = 0.05f;
	public float damage;
	protected float attackCooldown = 0;

	public virtual void Awake()
	{
		if(towers == null) towers = new List<Tower>();
		if(!towers.Contains(this)) towers.Add(this);

		hitbox = transform.Find("Hitbox").GetComponent<Rigidbody2D>();
	}

	public void Update()
	{
		if(active) Shoot(Time.deltaTime);
	}

	public abstract void Shoot(float deltaTime);

	public static void DisableTowers()
	{
		foreach(Tower tower in towers) tower.enabled = false;
	}
}
