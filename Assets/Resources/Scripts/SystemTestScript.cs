using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SystemTestScript : MonoBehaviour {

  private GameObject controlObject;
  private GameManager manager;
  private Rigidbody2D thisRigidbody;
  public float speed = 0.1f;

  public enum MovingObjectState {
    Idling,
    Moving,
    Climbing,
    };

    private MovingObjectState state;

    // Use this for initialization
    void Start() {
    controlObject = this.gameObject;
    manager = GameManager.instance;
    thisRigidbody = gameObject.GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update() {
    Vector2 prevPos = controlObject.transform.position;
    if (Input.GetKey(KeyCode.W)) {
      var ret = manager.tilemapManager.FindLadderPosition(gameObject, TilemapManager.Direction.Up);
      if (state != MovingObjectState.Climbing) {
        if (ret.Count != 0) {
          Debug.Log(ret[0]);
          state = MovingObjectState.Climbing;
          thisRigidbody.bodyType = RigidbodyType2D.Kinematic;
        } else {
          Debug.Log("No ladders.");
        }
      } else {
        prevPos.y += speed;
        if (ret.Count == 0) {
          state = MovingObjectState.Idling;
          thisRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
      }
    }
    if (Input.GetKey(KeyCode.S)) {
      var ret = manager.tilemapManager.FindLadderPosition(gameObject, TilemapManager.Direction.Down);
      if (state != MovingObjectState.Climbing) {
        if (ret.Count != 0) {
          Debug.Log(ret[0]);
          state = MovingObjectState.Climbing;
          thisRigidbody.bodyType = RigidbodyType2D.Kinematic;
        } else {
          Debug.Log("No ladders.");
        }
      } else {
        prevPos.y -= speed;
        if (ret.Count == 0) {
          state = MovingObjectState.Idling;
          thisRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
      }
    }
    if (Input.GetKey(KeyCode.A)) {
      prevPos.x -= speed;
    }
    if (Input.GetKey(KeyCode.D)) {
      prevPos.x += speed;
    }
    if (Input.GetKey(KeyCode.Space)) {
      Debug.Log(manager.tilemapManager.GetCellPos(gameObject));
    }
    controlObject.transform.position = prevPos;

    if (Input.GetKeyDown(KeyCode.O)) {

      // Test add inventory
      Equipment equipment = new Equipment(1);
      equipment.addition.sword = 1;
      equipment.addition.magic = -1;
      equipment.addition.life = 3;
      equipment.addition.rigidity = 1.5f;
      manager.inventoryManager.AddItem(equipment);
    }

    if (Input.GetKeyDown(KeyCode.I)) {
      manager.sysUIManager.ToggleInventory();
    }

    if (Input.GetKeyDown(KeyCode.B)) {
      manager.globalEffectManager.Flash();
    }

    if (Input.GetKeyDown(KeyCode.V)) {
      manager.globalEffectManager.SwitchScene();
    }
  }

  void OnMouseDown() {
    Debug.Log("hhh");
  }
}