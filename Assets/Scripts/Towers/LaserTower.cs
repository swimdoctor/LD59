using UnityEngine;

public class LaserTower : Tower
{
	public override TowerType towerType => TowerType.Laser;

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

	public override void Update()
	{
		base.Update();

		RaycastHit2D hit2D;
		LayerMask layerMask = LayerMask.GetMask(new string[]{ "Towers", "Terrain" });

		hit2D = Physics2D.Raycast(transform.position + transform.right * .6f, transform.right, 1000, layerMask);

		float distance = -1;

		if(hit2D.collider == null)
		{
			distance = 100;
		}
		else
		{
			distance = hit2D.distance + .6f;
		}
		distance = ((int)(distance * 16)) / 16f;

		hitbox.GetComponent<SpriteRenderer>().size = new Vector2(distance, 1);
		hitbox.GetComponent<BoxCollider2D>().offset = new Vector2(distance / 2 * 1.01f, 0);
		hitbox.GetComponent<BoxCollider2D>().size = new Vector2(distance * 1.01f, 1);
	}
}
