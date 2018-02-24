[System.Serializable]
public class Equipment : Item {
  public Equipment(int id) : base(id) { }
  public BasicInfo addition = new BasicInfo(0f, 0f, 0f, 0f, 0f);
}