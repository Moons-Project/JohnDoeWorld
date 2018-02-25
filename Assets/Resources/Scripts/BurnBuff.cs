using UnityEngine;

public class BurnBuff : Buff {
  private float intervalTime = 0.5f;
  private int count = 0;
  public override void BuffEffect(Creature creature, float cumulateTime) {
    if (cumulateTime >= intervalTime * count) {
      Debug.Log("Burn Damage");
      creature.Damage(1f);
      count++;
    }
  }

  public BurnBuff(Buff other) : base(other) {}
}