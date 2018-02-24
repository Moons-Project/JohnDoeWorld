using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act : MonoBehaviour {

  public float maxVelocityX= 10f;
  public float maxVelocityY = 5f;
  public LayerMask groundMask;


  private Rigidbody2D body;
  private Animator animator;
  private Attack attack;
  private Info info;
  // private int groundLayerMask;


  private bool isFacingRight = true;

  // Use this for initialization
  void Start() {
    attack = transform.GetChild(0).gameObject.GetComponent<Attack>();
    info = transform.GetChild(1).gameObject.GetComponent<Info>();
  }
  void Awake() {
    body = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update() {
    animator.SetBool("isGround", checkIsGround());
  }

  public void act(InputInfo inputInfo) {
    // test can climb
    bool canClimb = false && GameManager.instance.tilemapManager.FindLadderPosition(gameObject, inputInfo.horizontalAxis > 0 ? TilemapManager.Direction.Up : TilemapManager.Direction.Down).Count > 0;
    if (canClimb) {
      animator.SetBool("isClimbing", true);
    } else {
      UpdateFacing(inputInfo.horizontalAxis);

      float velocityX = inputInfo.horizontalAxis * maxVelocityX;
      // Debug.Log(body);
      float velocityY = CanJump(inputInfo.jumpButtonDown) ? maxVelocityY : body.velocity.y;
      animator.SetFloat("velocityX", Mathf.Abs(body.velocity.x));
      animator.SetBool("isGround", checkIsGround());
      animator.SetBool("isClimbing", false);
      body.velocity = new Vector2(velocityX, velocityY);

      if (inputInfo.fire1ButtonDown) attack.UseSkill(1, info);
    }
  }

  bool checkIsGround() {
    // Cast a ray straight down.
     var distance = GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.1f;
     var hit = Physics2D.Raycast(transform.position, Vector2.down, distance, groundMask);
     // If it hits something...
     return hit;
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