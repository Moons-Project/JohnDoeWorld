using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

  public float maxVelocityX= 10f;
  public float maxVelocityY = 5f;



  private Rigidbody2D body;
  private Animator animator;


  private bool isFacingRight = true;

  // Use this for initialization
  void Start() {
    body = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update() {
  }

  void FixedUpdate() {
    float horizontalAxis = Input.GetAxis("Horizontal");
    bool jumpButtonDown = Input.GetButtonDown("Jump");

    UpdateFacing(horizontalAxis);

    float velocityX = horizontalAxis * maxVelocityX;
    float velocityY = CanJump(jumpButtonDown) ? maxVelocityY : body.velocity.y;
    animator.SetFloat("velocityX", Mathf.Abs(body.velocity.x));
    animator.SetBool("isGround", checkIsGround());
    body.velocity = new Vector2(velocityX, velocityY);
  }

  bool checkIsGround() {
    return true;
  }
  bool CanJump(bool jumpButtonDown) {
    bool isGround = checkIsGround();
    bool canJump = jumpButtonDown && isGround;
    return canJump;
  }

  void UpdateFacing(float horizontalAxis) {
    if (horizontalAxis > 0 && !isFacingRight)
      Flip();
    else if (horizontalAxis < 0 && isFacingRight)
      Flip();
  }

  void Flip() {
    // 改变朝向
    isFacingRight = !isFacingRight;
    // 改变x方向上的scale进行翻转
    transform.localScale =  Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
  }
}