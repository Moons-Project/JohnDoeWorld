using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SysUIManager : MonoBehaviour {
  public Canvas UICanvas;
  public Camera UICamera;

  public GameObject itemDescription;
  private RectTransform itemDescriptionRectTransform;
  public GameObject inventory;
  public GameObject inventoryItemPrefab;
  private List<GameObject> inventorySlots;
  public GameObject inventoryPanel;  

  public GameObject cheatPanel;
  public InputField cheatInput;

  private ItemManager itemManager;
  private GameManager manager;

  public static SysUIManager instance;

  void Awake() {
    if (instance == null) {
      instance = this;
    }
  }

  // Use this for initialization
  void Start () {
    itemDescriptionRectTransform = itemDescription.transform as RectTransform;
    manager = GameManager.instance;
    itemManager = manager.itemManager;
  }

  public void GenerateInventoryUI(int inventorySize) {
    // Init Inventory
    inventorySlots = new List<GameObject>();
    for (int i = 0; i < inventorySize; ++i) {
      GameObject prefab = Instantiate(inventoryItemPrefab);
      prefab.GetComponent<InventoryItemBehavior>().inventoryItemIndex = i;
      prefab.transform.SetParent(inventory.transform);
      inventorySlots.Add(prefab);
    }
  }

  void OnGUI() {
    if (itemDescription.activeSelf) {
      // Set Position
      Vector2 pos;
      RectTransformUtility.ScreenPointToLocalPointInRectangle(UICanvas.transform as RectTransform, Input.mousePosition, UICamera, out pos);
      pos.x += itemDescriptionRectTransform.sizeDelta.x / 2;
      pos.y -= itemDescriptionRectTransform.sizeDelta.y / 2;

      itemDescriptionRectTransform.anchoredPosition = pos;
    }
  }

  private bool isCheatOpen = false;

  public void OpenCheat() {
    // Close other panel
    CloseInventory();
    
    isCheatOpen = true;
    cheatPanel.SetActive(true);
  }

  public void CloseCheat() {
    isCheatOpen = false;
    cheatPanel.SetActive(false);
  }

  public void ToggleCheat() {
    isCheatOpen = !isCheatOpen;

    // Close other panel
    if (isCheatOpen) CloseInventory();

    cheatPanel.SetActive(isCheatOpen);
  }

  public void ExecuteCheat() {
    string cheatString = cheatInput.text;
    CloseCheat();
    string[] splits = cheatString.Split(' ');
    if (splits.Length >= 2) {
      if (splits[0] == "goto") {
        manager.SwitchScene(splits[1]);
      }
    }
  }


  private bool isInventoryOpen = false;

  public void OpenInventory() {
    // Close other panel
    CloseCheat();

    isInventoryOpen = true;
    inventoryPanel.SetActive(true);
  }

  public void CloseInventory() {
    isInventoryOpen = false;
    inventoryPanel.SetActive(false);
  }

  public void ToggleInventory() {
    isInventoryOpen = !isInventoryOpen;

    // Close other panel
    if (isCheatOpen) CloseCheat();

    inventoryPanel.SetActive(isInventoryOpen);
  }


  private int showDescriptionId = 1;
  private static string templateDescription = "<color>{0}</color>\n" +
    "<color=cyan>{1}</color>\n" +
    "<color=grey>{2}</color>\n";

  public int ShowDescription(int index) {
    // Debug.Log(index);
    var invItem = manager.inventoryManager.inventoryDataset[index];

    // Debug.Log(inventoryDataset.Count);

    if (invItem == null) return -1;

    Item item = invItem.item;

    ItemManager.ItemInfo info;
    itemManager.itemDict.TryGetValue(item.id, out info);
    string effect = "";
    if (item is Equipment) {
      Equipment equipment = item as Equipment;
      effect = string.Format("剑术 {0}\n魔力 {1}\n刚性 {2}\n生命 {3}",
        equipment.swordAddition.ToString("+#;-#;0"),
        equipment.magicAddition.ToString("+#;-#;0"),
        equipment.rigidityAddition.ToString("+#;-#;0"),
        equipment.lifeAddition.ToString("+#;-#;0"));
    }

    itemDescription.GetComponentInChildren<Text>().text = string.Format(templateDescription,
      info.name,
      effect,
      info.description
    );

    itemDescription.SetActive(true);

    return ++showDescriptionId;
  }

  public void UnshowDescription(int id) {
    if (showDescriptionId == id) itemDescription.SetActive(false);
  }

  public void UpdateItemGObj(int index) {
    inventorySlots[index].GetComponent<InventoryItemBehavior>().UpdateData();
  }
}
