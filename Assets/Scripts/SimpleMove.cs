using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SimpleMove : MonoBehaviour {

	private GameObject controlObject;
	private TestSceneManager manager;
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
		manager = TestSceneManager.instance;
		thisRigidbody = gameObject.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update() {
		Vector2 prevPos = controlObject.transform.position;
		if (Input.GetKey(KeyCode.W)) {
			var ret = manager.HasLadder(gameObject, TestSceneManager.Direction.Up);
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
			var ret = manager.HasLadder(gameObject, TestSceneManager.Direction.Down);
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

	}

	void OnMouseDown() {
		Debug.Log("hhh");
	}
}