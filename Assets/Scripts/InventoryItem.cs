[System.Serializable]
public class InventroyItem {
  public Item item;
  public int count;

  public InventroyItem(Item item, int count) {
    this.item = item;
    this.count = count;
  }
}