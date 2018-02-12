using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

  [HideInInspector]
  public List<InventroyItem> inventoryDataset;
  public int inventorySize = 20;

  public static InventoryManager instance;

  private GameManager manager;

  void Awake() {
    if (instance == null) {
      instance = this;
    }
  }

  // Use this for initialization
  void Start () {
    manager = GameManager.instance;
    manager.sysUIManager.GenerateInventoryUI(inventorySize);

    for (int i = 0; i < inventorySize; ++i) {
      inventoryDataset.Add(null);
    }
  }

  private int FindEmptyDataset() {
    int index = 0;
    for (; index < inventoryDataset.Count; ++index) {
      var invItem = inventoryDataset[index];
      if (invItem == null) return index;
    }
    return -1;
  }

  public bool AddItem(Item item, int count = 1) {
    // 查询是否已经有此物品
    int index = 0;
    for (; index < inventoryDataset.Count; ++index) {
      var invItem = inventoryDataset[index];
      if (invItem != null && invItem.item.id == item.id) {
        break;
      }
    }

    if (index >= inventoryDataset.Count) {
      // 当前没有此物品，添加到空位置
      int newIndex = FindEmptyDataset();
      if (newIndex == -1) return false;
      inventoryDataset[newIndex] = new InventroyItem(item, count);
      manager.sysUIManager.UpdateItemGObj(newIndex);
    } else {
      // 有此物品，叠加
      inventoryDataset[index].count += count;
      manager.sysUIManager.UpdateItemGObj(index);
    }

    return true;
  }
}
