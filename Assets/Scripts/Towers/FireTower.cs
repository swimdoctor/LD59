using UnityEngine;

public class FireTower : Tower
{
	public override TowerType TowerType => TowerType.Fire;
	public override int Damage => 4;
}
