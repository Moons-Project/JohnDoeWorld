using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

  public float maxVelocityX = 10f;
  public float maxVelocityY = 5f;
  public LayerMask groundMask;

  public BasicInfo basicInfo;
  public BasicInfo currentInfo;
  public float currentHP;

  public int[] skillLevel = new int[4] { 1, 1, 1, 1 };
  public int[] skillList = new int[4] { 1, 1, 1, 1 };
  // public Equipment[] equipmentList = new Equipment[0] {};
  // public Buff[] buffList = new Buff[0] {};

  private Rigidbody2D body;
  private Animator animator;
  private Attack attack;

  private bool isFacingRight = true;

  // Use this for initialization
  void Start() {
    attack = transform.GetChild(0).gameObject.GetComponent<Attack>();
  }

  void Awake() {
    body = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    currentInfo = basicInfo;
    currentHP = currentInfo.life;
  }

  // Update is called once per frame
  void Update() {
    animator.SetBool("isGround", checkIsGround());
  }

  public void act(InputInfo inputInfo) {
    // test can climb
    bool canClimb = inputInfo.verticalAxis != 0 && GameManager.instance.tilemapManager.FindLadderPosition(gameObject, 
      inputInfo.verticalAxis > 0 ? TilemapManager.Direction.Up : TilemapManager.Direction.Down).Count > 0;
    if (canClimb) {
      body.bodyType = RigidbodyType2D.Kinematic;
      body.velocity = new Vector2(0.0f, 0.0f);
      animator.SetBool("isClimbing", true);
      transform.position = transform.position + new Vector3(0, inputInfo.verticalAxis * 0.05f, 0);
    } else {
      UpdateFacing(inputInfo.horizontalAxis);

      float velocityX = inputInfo.horizontalAxis * maxVelocityX;
      // Debug.Log(body);
      float velocityY = CanJump(inputInfo.jumpButtonDown) ? maxVelocityY : body.velocity.y;
      animator.SetFloat("velocityX", Mathf.Abs(body.velocity.x));
      animator.SetBool("isGround", checkIsGround());
      animator.SetBool("isClimbing", false);

      body.bodyType = RigidbodyType2D.Dynamic;
      body.velocity = new Vector2(velocityX, velocityY);

      if (inputInfo.fire0ButtonDown) attack.UseSkill(1, this);
      if (inputInfo.fire1ButtonDown) attack.UseSkill(1, this);
      if (inputInfo.fire2ButtonDown) attack.UseSkill(2, this);
      if (inputInfo.fire3ButtonDown) attack.UseSkill(3, this);
      if (inputInfo.fire4ButtonDown) attack.UseSkill(4, this);
      if (inputInfo.fire5ButtonDown) Debug.Log("Compound Kill");
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
    transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
  }

  public void addEquement(Equipment equipment) {

    updateCurrentInfo(equipment.addition);
  }

  public void removeEquement(Equipment equipment) {
    updateCurrentInfo(-equipment.addition);
  }

  private void updateCurrentInfo(BasicInfo addition) {
    currentInfo = currentInfo + addition;
  }

  public void skillResult(float finalDamage) {
    currentHP -= finalDamage - currentInfo.rigidity;
    Debug.Log(currentHP);
  }
}