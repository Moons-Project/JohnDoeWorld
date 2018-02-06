using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SimpleMove : MonoBehaviour {

	private GameObject controlObject;
	private TestSceneManager manager;
	public float speed = 0.1f;

	// Use this for initialization
	void Start() {
		controlObject = this.gameObject;
		manager = TestSceneManager.instance;
	}

	// Update is called once per frame
	void Update() {
		Vector2 prevPos = controlObject.transform.position;
		if (Input.GetKey(KeyCode.W)) {
			var ret = manager.HasLadder(gameObject, TestSceneManager.Direction.Up);
			if (ret.Count != 0)
				Debug.Log(ret[0]);
			else
				Debug.Log("No ladders.");
		}
		if (Input.GetKey(KeyCode.S)) {
			var ret = manager.HasLadder(gameObject, TestSceneManager.Direction.Down);
			if (ret.Count != 0)
				Debug.Log(ret[0]);
			else
				Debug.Log("No ladders.");
		}
		if (Input.GetKey(KeyCode.A)) {
			prevPos.x -= speed;
		}
		if (Input.GetKey(KeyCode.D)) {
			prevPos.x += speed;
		}
		controlObject.transform.position = prevPos;

	}

	void OnMouseDown() {
		Debug.Log("hhh");
	}
}