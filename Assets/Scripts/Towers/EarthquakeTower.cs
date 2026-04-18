using UnityEngine;

public class EarthquakeTower : Tower
{
	public override TowerType towerType => TowerType.Earthquake;
	public override int damage => 20;
	public override float cooldown => 1f;

	public float maxAlpha;
	public float minAlpha;

	public override void Update()
	{
		base.Update();

		hitbox.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Lerp(maxAlpha, minAlpha, attackCooldown / cooldown));
	}
}
