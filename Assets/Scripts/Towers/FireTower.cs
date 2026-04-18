using UnityEngine;

public class FireTower : Tower
{
	public override TowerType towerType => TowerType.Fire;
	public override int damage => 4;
}
