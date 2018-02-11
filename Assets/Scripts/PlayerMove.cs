using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

  public float speed = 3.0f;

  // Use this for initialization
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    float delateX = Input.GetAxis("Horizontal") * speed;
    float delateY = Input.GetAxis("Vertical") * speed;
    var movement = new Vector3 (delateX, delateY, 0);
		transform.position = transform.position + movement;
  }
}