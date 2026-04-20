using UnityEngine;
using System.Collections.Generic;

public class FireTower : Tower
{
	public override TowerType TowerType => TowerType.Fire;
	public override int Damage => 4;

	public override void Shoot()
	{
		RaycastHit2D[] targets = new RaycastHit2D[128];
		hitbox.Cast(Vector2.up, targets, 0);

		var enemies = new System.Collections.Generic.List<EnemyController>();
		foreach (RaycastHit2D target in targets)
		{
			if (!target) break;
			Tower tower = target.collider.GetComponent<Tower>();
			EnemyController enemy = target.collider.GetComponent<EnemyController>();
			if (tower != null)
			{
				if (tower == this) continue;

				if (tower.detection == TowerType) tower.Active = true;
			}
			if (enemy != null)
			{
				enemies.Add(enemy);
			}
		}

		int count = enemies.Count;
		if (count == 0) return;

		int baseDamage = Mathf.Max(1, Mathf.CeilToInt((float)Damage / count));
		for (int i = 0; i < count; i++)
		{
			enemies[i].TakeDamage(baseDamage);
		}
	}
}
