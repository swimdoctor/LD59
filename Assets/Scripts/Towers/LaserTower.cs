using UnityEngine;

public class LaserTower : Tower
{
	public override TowerType towerType => TowerType.Laser;
	public override int damage => 3;

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
