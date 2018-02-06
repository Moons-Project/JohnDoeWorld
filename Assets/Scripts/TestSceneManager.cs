using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestSceneManager : MonoBehaviour {

	public Tilemap ladderTilemap;
	public Tilemap platformTilemap;

	private TilemapCollider2D ladderCollider;
	private TilemapCollider2D platformCollider;
	
	public static TestSceneManager instance;


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
	}

	// Update is called once per frame
	void Update() {

	}

	public List<Vector3Int> HasLadder(GameObject gObj, Direction direction) {
		List<Vector3Int> posList = new List<Vector3Int>();

		Collider2D collider = gObj.GetComponent<Collider2D>();

		if (collider == null) return posList;
		Bounds bounds = ladderCollider.bounds;
		Vector3 centerPoint = collider.bounds.center;

		if (direction == Direction.Up) {
			/* 
			检测上方梯子
			直接使用gObj的中心点，检测其是否在梯子中
			 */
			Vector3 vec3 = centerPoint;
			Vector3Int cellPos = ladderTilemap.WorldToCell(vec3);
			bool collision = ladderTilemap.GetTile(cellPos) != null;
			if (collision) {
				Vector3Int temp = ladderTilemap.WorldToCell(vec3);
				posList.Add(temp);
			}
		} else if (direction == Direction.Down) {
			/* 
			检测下方梯子
			使用gObj的下边界中心，检测其是否在梯子中
			 */
			Vector3 vec3 = centerPoint;
			vec3.y -= bounds.extents.y + CHECKING_DELTA;
			Vector3Int cellPos = ladderTilemap.WorldToCell(vec3);
			bool collision = ladderTilemap.GetTile(cellPos) != null;
			if (collision) {
				Vector3Int temp = ladderTilemap.WorldToCell(vec3);
				posList.Add(temp);
			}
		} else {
			return posList;
		}

		return posList;
	}
}