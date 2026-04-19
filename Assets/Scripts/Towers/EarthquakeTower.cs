using UnityEngine;

public class EarthquakeTower : Tower
{
	public override TowerType TowerType => TowerType.Earthquake;
	public override int Damage => 20;
	public override float Cooldown => 1f;

	public float maxAlpha;
	public float minAlpha;

	public override void Update()
	{
		base.Update();

		hitbox.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Lerp(maxAlpha, minAlpha, attackCooldown / Cooldown));
	}
}
