using UnityEngine;

public class FireTower : Tower
{
	public override TowerType towerType => TowerType.Fire;

	public override void Shoot(float deltaTime)
	{
		attackCooldown += deltaTime;

		while(attackCooldown > cooldown)
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
					
					if(tower.detection == towerType) tower.Active = true;
				}
				else if(enemy != null)
				{
					print("AAAAAAAAAAAAAAAAAA");
					enemy.TakeDamage(damage);
				}
			}

			attackCooldown -= cooldown;
		}
	}

	
}
