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

  public int[] skillLevel = new int[5] {1, 1, 1, 1, 1 };
  public string[] skillList = new string[5] {"BasicAttack",  "TopDownChop", "ThreeTimesChop", "Stab", "Battojutsu" };
  // public Equipment[] equipmentList = new Equipment[0] {};
  // public Buff[] buffList = new Buff[0] {};

  private class CreatureBuff {
    public Buff buff;
    public float totalTime;

    public float cumulateTime;

    public CreatureBuff(Buff buff, float totalTime) {
      this.buff = buff;
      this.cumulateTime = 0;
      this.totalTime = totalTime;
    }
  }

  private List<CreatureBuff> cBuffList;
  private HashSet<CreatureBuff> cBuffToDel;

  private Rigidbody2D body;
  private Animator animator;
  private Attack attack;

  private bool isFacingRight = true;

  // Use this for initialization
  void Start() {
    attack = transform.GetChild(0).gameObject.GetComponent<Attack>();
    cBuffList = new List<CreatureBuff>();
    cBuffToDel = new HashSet<CreatureBuff>();
  }

  void Awake() {
    body = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    currentInfo = basicInfo;
    currentHP = currentInfo.life;
  }

  // Update is called once per frame
  void Update() {
    animator.SetBool("isGround", CheckIsGround());
    animator.SetFloat("currentHP", currentHP);

    foreach (var cBuff in cBuffList) {
      // Debug.Log(cBuff.buff);
      cBuff.cumulateTime += Time.deltaTime;
      if (cBuff.cumulateTime >= cBuff.totalTime) {
        cBuffToDel.Add(cBuff);
        continue;
      }
      if (cBuff.buff.shouldUpdate) {
        cBuff.buff.BuffEffect(this, cBuff.cumulateTime);
      }
    }

    // Delete the buff that is over
    foreach (var cBuff in cBuffToDel) {
      RemoveCBuff(cBuff);
    }
    cBuffToDel.Clear();
  }

  public void Act(InputInfo inputInfo) {
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
      animator.SetBool("isGround", CheckIsGround());
      animator.SetBool("isClimbing", false);

      body.bodyType = RigidbodyType2D.Dynamic;
      body.velocity = new Vector2(velocityX, velocityY);

      if (inputInfo.fire0ButtonDown) attack.UseSkill(0, this);
      if (inputInfo.fire1ButtonDown) attack.UseSkill(1, this);
      if (inputInfo.fire2ButtonDown) attack.UseSkill(2, this);
      if (inputInfo.fire3ButtonDown) attack.UseSkill(3, this);
      if (inputInfo.fire4ButtonDown) attack.UseSkill(4, this);
      if (inputInfo.fire5ButtonDown) Debug.Log("Compound Kill");
    }
  }

  bool CheckIsGround() {
    // Cast a ray straight down.
    var distance = GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.1f;
    var hit = Physics2D.Raycast(transform.position, Vector2.down, distance, groundMask);
    // If it hits something...
    return hit;
  }

  bool CanJump(bool jumpButtonDown) {
    bool isGround = CheckIsGround();
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

  public void AddEquipment(Equipment equipment) {

    UpdateCurrentInfo(equipment.addition);
  }

  public void RemoveEquipment(Equipment equipment) {
    UpdateCurrentInfo(-equipment.addition);
  }

  private void UpdateCurrentInfo(BasicInfo addition) {
    currentInfo = currentInfo + addition;
  }

  public void Damage(float finalDamage) {
    currentHP -= finalDamage - currentInfo.rigidity;
    Debug.Log(finalDamage);
  }

  public void Dead() {
    Debug.Log("DEAD");
  }

  CreatureBuff FindBuff(Buff buff) {
    foreach (var cBuff in cBuffList) {
      if (cBuff.buff == buff) {
        return cBuff;
      }
    }
    return null;
  }

  public void AddBuff(Buff buff, float totalTime) {
    var cBuff = FindBuff(buff);
    bool hasDuplicate = FindBuff(buff) != null;
    if (!hasDuplicate) {
      // Add to list
      cBuffList.Add(new CreatureBuff(buff, totalTime));
      // Add addition
      UpdateCurrentInfo(buff.addition);
    } else {
      // Exist, refresh time
      cBuff.totalTime = totalTime;
      cBuff.cumulateTime = 0;
    }
  }

  void RemoveCBuff(CreatureBuff cBuff) {
    if (cBuff != null) {
      cBuffList.Remove(cBuff);
      UpdateCurrentInfo(-cBuff.buff.addition);
    }
  }

  public void RemoveBuff(Buff buff) {
    CreatureBuff cBuff = FindBuff(buff);
    RemoveCBuff(cBuff);
  }
}