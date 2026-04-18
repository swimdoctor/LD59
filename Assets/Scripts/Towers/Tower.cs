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

	[SerializeField] private bool active;
	public bool Active 
	{
		get => active;
		set
		{
			active = value;
			hitbox.GetComponent<SpriteRenderer>().enabled = value;
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
		Active = starting || active;
	}

	public void Update()
	{
		if(Active) Shoot(Time.deltaTime);
	}

	public abstract void Shoot(float deltaTime);

	public static void DisableTowers()
	{
		foreach(Tower tower in towers) tower.enabled = false;
	}
}
