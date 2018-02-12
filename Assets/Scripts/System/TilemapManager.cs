using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour {

  public Tilemap ladderTilemap;
  public Tilemap platformTilemap;

  public static TilemapManager instance;

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
  
}
