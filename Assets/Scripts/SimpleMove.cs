using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SimpleMove : MonoBehaviour {

  private GameObject controlObject;
  private BaseSceneManager manager;
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
    manager = BaseSceneManager.instance;
    thisRigidbody = gameObject.GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update() {
    Vector2 prevPos = controlObject.transform.position;
    if (Input.GetKey(KeyCode.W)) {
      var ret = manager.HasLadder(gameObject, BaseSceneManager.Direction.Up);
      if (state != MovingObjectState.Climbing) {
        if (ret.Count != 0) {
          Debug.Log(ret[0]);
          state = MovingObjectState.Climbing;
          thisRigidbody.bodyType = RigidbodyType2D.Kinematic;
          // prevPos.x = ret[0].x;
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
      var ret = manager.HasLadder(gameObject, BaseSceneManager.Direction.Down);
      if (state != MovingObjectState.Climbing) {
        if (ret.Count != 0) {
          Debug.Log(ret[0]);
          state = MovingObjectState.Climbing;
          thisRigidbody.bodyType = RigidbodyType2D.Kinematic;
          // prevPos.x = ret[0].x;
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
      Debug.Log(manager.GetCellPos(gameObject));
    }
    controlObject.transform.position = prevPos;

    if (Input.GetKeyDown(KeyCode.O)) {

      // Test add inventory
      Equipment equipment = new Equipment(1);
      equipment.swordAddition = 1;
      equipment.magicAddition = -1;
      equipment.lifeAddition = 3;
      equipment.rigidityAddition = 1.5f;
      manager.AddItem(equipment);

      DontDestroyOnLoad(gameObject);
    }

    if (Input.GetKeyDown(KeyCode.I)) {
      manager.ToggleInventory();
    }

  }

  void OnMouseDown() {
    Debug.Log("hhh");
  }
}