[System.Serializable]
public class CreatureBuff {
  public Buff buff;
  public float totalTime;

  public float cumulateTime;

  public CreatureBuff(Buff buff, float totalTime) {
    this.buff = buff;
    this.cumulateTime = 0;
    this.totalTime = totalTime;
  }
}
