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

	public static BaseSceneManager instance;

	public static readonly float CHECKING_DELTA = 0.3f;
	public enum Direction {
		Up,
		Down,
		Left,
		Right,
		All
	}

	void Awake() {
		if (instance == null)
			instance = this;
	}

	// Use this for initialization
	void Start() {
		ladderCollider = ladderTilemap.gameObject.GetComponent<TilemapCollider2D>();
		platformCollider = platformTilemap.gameObject.GetComponent<TilemapCollider2D>();

		itemDescriptionRectTransform = itemDescription.transform as RectTransform;

		// Test add inventory
		Equipment equipment = new Equipment();
		equipment.name = "测试";
		equipment.description = "测试描述";
		equipment.swordAddition = 1;
		equipment.magicAddition = -1;
		equipment.lifeAddition = 3;
		equipment.rigidityAddition = 1.5f;

		GameObject item = Instantiate(InventoryItemPrefab);
		item.GetComponent<InventoryItemBehavior>().item = equipment;
		item.transform.parent = Inventory.transform;
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

	private int showDescriptionId = 1;
	private static string templateDescription = "<color>{0}</color>\n" +
		"<color=cyan>{1}</color>\n" +
		"<color=grey>{2}</color>\n";

	public int ShowDescription(Item item) {
		string name = item.name;
		string effect = "";
		string description = item.description;
		if (item is Equipment) {
			Equipment equipment = item as Equipment;
			effect = string.Format("剑术 {0}\n魔力 {1}\n刚性 {2}\n生命 {3}",
				equipment.swordAddition.ToString("+#;-#;0"),
				equipment.magicAddition.ToString("+#;-#;0"),
				equipment.rigidityAddition.ToString("+#;-#;0"),
				equipment.lifeAddition.ToString("+#;-#;0"));
		}

		itemDescription.GetComponentInChildren<Text>().text = string.Format(templateDescription,
			name,
			effect,
			description
		);

		itemDescription.SetActive(true);

		return ++showDescriptionId;
	}

	public void UnshowDescription(int id) {
		if (showDescriptionId == id) itemDescription.SetActive(false);
	}
}