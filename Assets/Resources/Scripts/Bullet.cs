using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour {

  public Vector2 force;

  // Use this for initialization
  void Start () {
  }
  
  // Update is called once per frame
  void Update () {
    if (Input.GetKeyDown(KeyCode.V)) {
      gameObject.GetComponent<Rigidbody2D>().AddForce(force);
    }
  }
}
