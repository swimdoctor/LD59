using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;

public class LaserTower : Tower
{
	public override TowerType TowerType => TowerType.Laser;
	public override int Damage { get; set; } = 2;

	public int pierce = 5;

	void Start()
	{
		levelToUpgradeCost = new Dictionary<int, int>
        {
            { 1, 150 } // init with level 1 upgrade costing $150
        };
	}

	float distance;
	bool doDamage = true;

	public override void Update()
	{
		RaycastHit2D hit2D;
		LayerMask layerMask = LayerMask.GetMask(new string[]{ "Towers", "Terrain" });

		hit2D = Physics2D.Raycast(transform.position + transform.right * .6f, transform.right, 1000, layerMask);

		if(hit2D.collider == null)
		{
			distance = 100;
		}
		else
		{
			distance = hit2D.distance + .6f;
		}

		base.Update();
		doDamage = false;
		if(!isMoving) Shoot();
		doDamage = true;

		distance = ((int)(distance * 16)) / 16f;

		hitbox.GetComponent<SpriteRenderer>().size = new Vector2(distance, 1);
		hitbox.GetComponent<BoxCollider2D>().offset = new Vector2(1000 / 2 * 1.01f, 0);
		hitbox.GetComponent<BoxCollider2D>().size = new Vector2(1000 * 1.01f, 1);
	}

	public override void Shoot()
	{
		RaycastHit2D[] targets = new RaycastHit2D[128];
		hitbox.Cast(Vector2.up, targets, 0);

		List<EnemyController> enemies = new List<EnemyController>();
		Tower t = null;

		foreach(RaycastHit2D target in targets)
		{
			if(!target) break;

			Tower tower = target.collider.GetComponent<Tower>();
			EnemyController enemy = target.collider.GetComponent<EnemyController>();
			if(tower != null)
			{
				if(tower == this) continue;

				if(tower.detection == TowerType)
				{
					if(t == null || Vector3.Distance(tower.transform.position, transform.position) < Vector3.Distance(transform.position, t.transform.position)) t = tower;
				}
			}
			else if(enemy != null)
			{
				enemies.Add(enemy);
			}
		}

		enemies.Sort((e1, e2) => (int)Mathf.Sign(Vector3.Distance(e1.transform.position, transform.position) - Vector3.Distance(e2.transform.position, transform.position)));

		for(int i = 0; i < Mathf.Min(enemies.Count, pierce); i++)
		{
			if(doDamage) enemies[i].TakeDamage(Damage);
		}

		if(enemies.Count >= pierce)
		{
			distance = Vector3.Distance(enemies[pierce - 1].transform.position, transform.position);
			print(Vector3.Distance(enemies[pierce - 1].transform.position, transform.position));
		}
		else if(t) t.Active = true;
	}

	public override void UpgradeTower()
	{
		if(level == 1)
		{
			level++;
			// FIXME: Add +5 pierce
			pierce = 10;
		} 
		else
		{
			print("Already max level");
		}
	}
}
