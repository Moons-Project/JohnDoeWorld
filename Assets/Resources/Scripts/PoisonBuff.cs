using UnityEngine;

public class PoisonBuff : Buff {
  private float intervalTime = 0.5f;
  private int count = 0;
  public override void BuffEffect(Creature creature, float cumulateTime) {
    if (cumulateTime >= intervalTime * count) {
      Debug.Log("Poison Damage");
      creature.Damage(1f);
      count++;
    }
  }

  public PoisonBuff(Buff other) : base(other) {}
}