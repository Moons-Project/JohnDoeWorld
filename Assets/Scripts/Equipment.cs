[System.Serializable]
public class Equipment : Item {
  public Equipment(int id) : base(id) {}

  public float swordAddition = 0f;
  public float magicAddition = 0f;
  public float rigidityAddition = 0f;
  public float lifeAddition = 0f;
}