using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BaseSceneManager : MonoBehaviour {

	public Tilemap ladderTilemap;
	public Tilemap platformTilemap;

	public Canvas UICanvas;
	public GameObject itemDescription;
	public Camera UICamera;
	public GameObject Inventory;

	public GameObject InventoryItemPrefab;

	private RectTransform itemDescriptionRectTransform;

	private TilemapCollider2D ladderCollider;
	private TilemapCollider2D platformCollider;

	[HideInInspector]
	public List<InventroyItem> inventoryDataset;
	private List<GameObject> inventorySlots;
	public int inventorySize = 20;

	public static BaseSceneManager instance;
	public ItemManager itemManager;

	public static readonly float CHECKING_DELTA = 0.3f;
	public enum Direction {
		Up,
		Down,
		Left,
		Right,
		All
	}

	void Awake() {
		if (instance == null) {
			instance = this;
			itemManager = ItemManager.instance;
		}
	}

	// Use this for initialization
	void Start() {
		ladderCollider = ladderTilemap.gameObject.GetComponent<TilemapCollider2D>();
		platformCollider = platformTilemap.gameObject.GetComponent<TilemapCollider2D>();

		itemDescriptionRectTransform = itemDescription.transform as RectTransform;

		// Init Inventory
		inventoryDataset = new List<InventroyItem>();
		inventorySlots = new List<GameObject>();
		for (int i = 0; i < inventorySize; ++i) {
			inventoryDataset.Add(null);
			GameObject inventoryItemPrefab = Instantiate(InventoryItemPrefab);
			inventoryItemPrefab.GetComponent<InventoryItemBehavior>().inventoryItemIndex = i;
			inventoryItemPrefab.transform.SetParent(Inventory.transform);
			inventorySlots.Add(inventoryItemPrefab);
		}
	}

	// Update is called once per frame
	void Update() {

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

	public List<Vector3> HasLadder(GameObject gObj, Direction direction) {
		List<Vector3> posList = new List<Vector3>();

		Collider2D collider = gObj.GetComponent<Collider2D>();

		if (collider == null) return posList;
		Bounds bounds = collider.bounds;
		Vector3 centerPoint = collider.bounds.center;

		if (direction == Direction.Up) {
			/* 
			检测上方梯子
			直接使用gObj的中心点，检测其是否在梯子中
			 */
			Vector3 vec3 = centerPoint;
			Debug.Log(vec3);
			Vector3Int cellPos = ladderTilemap.WorldToCell(vec3);
			bool collision = ladderTilemap.GetTile(cellPos) != null;
			if (collision) {
				Vector3 temp = ladderTilemap.CellToWorld(cellPos);
				posList.Add(temp);
			}
		} else if (direction == Direction.Down) {
			/* 
			检测下方梯子
			使用gObj的下边界中心，检测其是否在梯子中
			 */
			Vector3 vec3 = centerPoint;
			vec3.y -= bounds.extents.y + CHECKING_DELTA;
			Debug.Log(vec3);
			Vector3Int cellPos = ladderTilemap.WorldToCell(vec3);
			bool collision = ladderTilemap.GetTile(cellPos) != null;
			if (collision) {
				Vector3 temp = ladderTilemap.CellToWorld(cellPos);
				posList.Add(temp);
			}
		} else {
			return posList;
		}

		return posList;
	}

	public Vector3Int GetCellPos(GameObject gObj) {
		return ladderTilemap.WorldToCell(gObj.transform.position);
	}

	private bool isInventoryOpen = false;

	public void OpenInventory() {
		isInventoryOpen = true;
		UICamera.gameObject.SetActive(true);
	}

	public void CloseInventory() {
		isInventoryOpen = false;
		UICamera.gameObject.SetActive(false);
	}

	public void ToggleInventory() {
		isInventoryOpen = !isInventoryOpen;
		UICamera.gameObject.SetActive(isInventoryOpen);
	}

	private int showDescriptionId = 1;
	private static string templateDescription = "<color>{0}</color>\n" +
		"<color=cyan>{1}</color>\n" +
		"<color=grey>{2}</color>\n";

	public int ShowDescription(int index) {
		// Debug.Log(index);
		var invItem = inventoryDataset[index];

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
			UpdateItemGObj(newIndex);
		} else {
			// 有此物品，叠加
			inventoryDataset[index].count += count;
			UpdateItemGObj(index);
		}

		return true;
	}

	public void UpdateItemGObj(int index) {
		inventorySlots[index].GetComponent<InventoryItemBehavior>().UpdateData();
	}
}