using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviour {

  public float maxVelocityX = 10f;
  public float maxVelocityY = 5f;
  public LayerMask groundMask;

  public BasicInfo _basicInfo;

  public BasicInfo basicInfo {
    get {
      return _basicInfo;
    }

    set {
      currentInfo = currentInfo + -_basicInfo + value;
      currentHP = currentInfo.life;
      _basicInfo = value;
    }
  }
  public BasicInfo currentInfo;
  public float currentHP;

  public int[] skillExps = new int[5] { 0, 0, 0, 0, 0 };
  public string[] skillList = new string[5] { "BasicAttack", "TopDownChop", "ThreeTimesChop", "Stab", "Battojutsu" };
  // public Equipment[] equipmentList = new Equipment[0] {};
  // public Buff[] buffList = new Buff[0] {};
  public Equipment equippingItem;

  public CreatureSkill[] cSkillList;

  private List<CreatureBuff> cBuffList;
  private HashSet<CreatureBuff> cBuffToDel;

  private Rigidbody2D body;
  private Animator animator;
  public Attack attack;

  public bool isAttacking = false;

  public bool isFacingRight = true;

  public bool isDead {
    get {
      return currentHP <= 0;
    }
  }

  private bool deadHandled = false;

  // Use this for initialization
  void Start() {
    // attack = transform.GetChild(0).gameObject.GetComponent<Attack>();
    cBuffList = new List<CreatureBuff>();
    cBuffToDel = new HashSet<CreatureBuff>();

    SetSkillList(skillList, skillExps);
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
    if (currentHP <= 0 && !deadHandled) Dead();

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

    // Update CD
    foreach (var cSkill in cSkillList) {
      cSkill.UpdateTime(Time.deltaTime);
    }
  }

  public void Act(InputInfo inputInfo) {
    if (isDead) return;
    // TODO: 日后应使用自身的动画来控制isAttacking
    if (isAttacking) return;
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

      if (inputInfo.fire0ButtonDown) UseSkill(0);
      if (inputInfo.fire1ButtonDown) UseSkill(1);
      if (inputInfo.fire2ButtonDown) UseSkill(2);
      if (inputInfo.fire3ButtonDown) UseSkill(3);
      // if (inputInfo.fire4ButtonDown) UseSkill(4);
      // if (inputInfo.fire5ButtonDown) Debug.Log("Compound Kill");
    }
  }

  /* 
    该方法逻辑较为复杂
    1. 如果该技能使用武器
      1. 根据ProgramInfo更改武器、人物播放动画的速度
      2. 调用attack.UseSkill
    2. 如果该技能使用子弹
      1. 根据子弹信息生成子弹
      2. 将子弹位置放到自身附近
      3. 调用attack.UseSkill
      4. 根据ProgramInfo决定给予子弹的力(包括力的方向) (发射子弹)
   */
  public void UseSkill(int index) {
    if (cSkillList[index].isCooling) return;

    // 播放自身动画
    animator.Play("Attack");
    CreatureSkill skill = cSkillList[index];
    skill.SetCooling();

    if ((skill.skill.attackType & Skill.AttackType.Weapon) != 0) {
      attack.UseSkill(skill, this);
    }
    if ((skill.skill.attackType & Skill.AttackType.Bullet) != 0) {
      Debug.Log("Spawning a bullet");
      // Spawn a bullet
      BulletInfo bulletInfo = BulletDict.instance.itemDict[skill.skill.idName];
      GameObject bullet = Instantiate(GameManager.instance.bulletPrefab);
      Vector3 vec = transform.position;
      // vec.y += 2.0f;
      bullet.transform.position = vec;
      bullet.name = bulletInfo.idName;

      bulletInfo.rigidbodyPara.ApplyToGameObject(bullet);

      // Add force
      Attack attack = bullet.GetComponent<Attack>();
      // attack.destroyGObjOnTriggerEnter = true;
      attack.UseSkill(skill, this);
      Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
      Debug.Log("Force: " + skill.calBulletForce);
      if (isFacingRight) {
        rb.AddForce(skill.calBulletForce);
      } else {
        rb.AddForce(-skill.calBulletForce);
        bullet.transform.localRotation = Quaternion.Euler(0, 180, 0);
      }
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
    // TODO: 现在人物只有一个装备位置，日后需要改动
    attack.GetComponentInChildren<SpriteRenderer>().sprite = JsonManager.instance.spriteDict[equipment.idName];
    equippingItem = equipment;
    UpdateCurrentInfo(equipment.addition);
  }

  public void RemoveEquipment(Equipment equipment) {
    equippingItem = null;

    UpdateCurrentInfo(-equipment.addition);
  }

  private void UpdateCurrentInfo(BasicInfo addition) {
    currentInfo = currentInfo + addition;
  }

  public void Damage(float finalDamage) {
    var calDamage = finalDamage - currentInfo.rigidity;
    if (calDamage < 0) calDamage = 0f;
    currentHP -= calDamage;
    Debug.Log(finalDamage);
  }

  public void Dead() {
    Debug.Log("DEAD");
    deadHandled = true;
    if (GameManager.instance.controllingCreature == this) {
      DialogManager.instance.SystemDialog("<color='red'>YOU DIED</color>");
      GameManager.instance.SwitchScene("main_menu_scene");
      HUDManager.instance.CloseHUD();
    } else {
      StartCoroutine(GoToDestroy());
    }
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

  public void SetSkillList(string[] skills, int[] skillExps) {
    int len = Mathf.Min(skills.Length, skillExps.Length);
    cSkillList = new CreatureSkill[len];
    for (int i = 0; i < len; ++i) {
      cSkillList[i] = new CreatureSkill(JsonManager.instance.skillDict[skills[i]], skillExps[i]);
    }
  }

  IEnumerator GoToDestroy() {
    yield return new WaitForSeconds(1f);
    DestroyMe();
  }

  public void DestroyMe() {
    Destroy(gameObject);
  }
}