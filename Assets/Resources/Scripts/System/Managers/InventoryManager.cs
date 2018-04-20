using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

  private InventroyItem[] _inventoryDataset;
  public InventroyItem[] inventoryDataset {
    get {
      return _inventoryDataset;
    }
    set {
      _inventoryDataset = value;
    }
  }
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

    inventoryDataset = new InventroyItem[inventorySize];
  }

  private int FindEmptyDataset() {
    int index = 0;
    for (; index < inventoryDataset.Length; ++index) {
      var invItem = inventoryDataset[index];
      if (invItem == null) return index;
    }
    return -1;
  }

  public bool AddItem(Item item, int count = 1) {
    // 查询是否已经有此物品
    int index = 0;
    for (; index < inventoryDataset.Length; ++index) {
      var invItem = inventoryDataset[index];
      if (invItem != null && invItem.item == item) {
        break;
      }
    }

    if (index >= inventoryDataset.Length) {
      // 当前没有此物品，添加到空位置
      Debug.Log("Add new Item");
      int newIndex = FindEmptyDataset();
      if (newIndex == -1) return false;
      inventoryDataset[newIndex] = new InventroyItem(item, count);
      manager.sysUIManager.UpdateItemGObj(newIndex);
    } else {
      // 有此物品，叠加
      Debug.Log("Add existed Item");
      inventoryDataset[index].count += count;
      manager.sysUIManager.UpdateItemGObj(index);
    }

    return true;
  }

  public void UseItem(int index) {
    var item = inventoryDataset[index];
    if (item.item is Equipment) {
      Equip(item.item as Equipment);
    }
  }

  private void Equip(Equipment item) {
    // TODO: 现在人物只有一个装备位置，日后需要改动
    Debug.Log(manager.controllingCreature.gameObject.name);
    manager.controllingCreature.RemoveEquipment(manager.controllingCreature.equippingItem);
    manager.controllingCreature.AddEquipment(item);
    SaveDataManager.instance.Save(manager.controllingCreature.gameObject);
    
    DialogManager.instance.SystemDialog("已更换装备！");
  }
}
