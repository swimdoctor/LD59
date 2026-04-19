using UnityEngine;
using System.Collections.Generic;


public class EarthquakeTower : Tower
{
	public override TowerType TowerType => TowerType.Earthquake;
	public override int Damage { get; set; } = 20;
	public override float Cooldown => 1f;

	public float maxAlpha;
	public float minAlpha;

	public override void Update()
	{
		base.Update();

		hitbox.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Lerp(maxAlpha, minAlpha, attackCooldown / Cooldown));
	}

	void Start()
	{
		levelToUpgradeCost = new Dictionary<int, int>
        {
            { 1, 120 } // init with level 1 upgrade costing $150
        };
	}

	public override void UpgradeTower()
	{
		if(level == 1)
		{
			level++;
			// FIXME: Add slow
		} 
		else
		{
			print("Already max level");
		}
	}
}
