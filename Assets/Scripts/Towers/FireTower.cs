using UnityEngine;
using System.Collections.Generic;

public class FireTower : Tower
{
	public override TowerType TowerType => TowerType.Fire;
	public override int Damage { get; set; } = 4;
	void Start()
	{
		levelToUpgradeCost = new Dictionary<int, int>
        {
            { 1, 100 } // init with level 1 upgrade costing $150
        };
	}

	public override void UpgradeTower()
	{
		if(level == 1)
		{
			level++;
			// FIXME: Add damage properly
			Damage *= 2;
		} 
		else
		{
			print("Already max level");
		}
	}
}
