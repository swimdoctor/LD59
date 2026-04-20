using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;

public class LaserTower : Tower
{
	public override TowerType TowerType => TowerType.Laser;
	public override int Damage { get; set; } = 3;

	void Start()
	{
		levelToUpgradeCost = new Dictionary<int, int>
        {
            { 1, 150 } // init with level 1 upgrade costing $150
        };
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

	public override void UpgradeTower()
	{
		if(level == 1)
		{
			level++;
			// FIXME: Add +5 pierce
		} 
		else
		{
			print("Already max level");
		}
	}
}
