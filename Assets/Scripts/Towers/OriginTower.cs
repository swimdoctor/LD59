using UnityEngine;
using System.Collections.Generic;

public class OriginTower : Tower
{
	public override TowerType TowerType => TowerType.Origin;
	public override int Damage { get; set; } = 0;


	void Start()
	{

	}

	public override void Shoot()
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
                tower.Active = true;
                print("please work");
			}
			else if(enemy != null)
			{
				enemy.TakeDamage(Damage, speedMultiplier, slowDuration);
			}
		}
	}


	public override void UpgradeTower()
	{
		if(level == 1)
		{
			print("Already max level");

		}

	}
}
