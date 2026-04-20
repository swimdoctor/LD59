using UnityEngine;
using System.Collections.Generic;


public class EarthquakeTower : Tower
{
	public override TowerType TowerType => TowerType.Earthquake;
	public override int Damage { get; set; } = 20;
	public override float Cooldown => 1f;

	public float maxAlpha;
	public float minAlpha;

	private float speedMultiplierOverride = 0.75f;
	private float slowDurationOverride = 2f;

	public override void Update()
	{
		base.Update();

		hitbox.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Lerp(maxAlpha, minAlpha, attackCooldown / Cooldown));
	}

	void Start()
	{
		// Set slows
		speedMultiplier = speedMultiplierOverride;
		slowDuration = slowDurationOverride;
		
		// Set dict
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
			speedMultiplier = 0.5f;
		} 
		else
		{
			print("Already max level");
		}
	}
}
