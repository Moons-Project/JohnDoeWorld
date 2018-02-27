using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Creature))]
public class SimpleAI : MonoBehaviour {

  public string enemyTag = "ControlPlayer";
  public float viewDistance = 5;
  public float attackDistance = 1;
  public float longAttackDistance = 5;
  public float idleTime = 0;
  public float lockingMaxDistance = 6;
  public float escapeTime = 1.5f;
  public float attackFreezeTime = 0.5f;

  public float patrolMoveMaxTime = 2.0f;

  private Creature thisCreature;
  private GameObject lockingAt;
  
  void Start() {
    thisCreature = GetComponent<Creature>();
  }

  // Update is called once per frame
  void Update () {
    if (lockingAt == null) {

      // 如果没有锁定的目标，则左右探测
      RaycastHit2D[] hit2D;
      // Check left
      if (thisCreature.isFacingRight) {
        hit2D = Physics2D.RaycastAll(transform.position, Vector2.right, viewDistance, LayerMask.GetMask(new string[]{"Creature"}));
      } else {
        hit2D = Physics2D.RaycastAll(transform.position, Vector2.left, viewDistance, LayerMask.GetMask(new string[]{"Creature"}));
      }
      foreach (var h in hit2D) {
        if (h && h.transform.tag == enemyTag) {
          lockingAt = h.transform.gameObject;
          return;
        }
      }

      // 是否巡逻
      if (patrolling) {
        thisCreature.Act(patrolInputInfo);
        return;
      } else {
        if (Random.Range(0, 360) == 0) Patrol();
      }
    } else {
      if (freezing) {
        return;
      }
      if (escaping) {
        Escape();
        return;
      }

      // 如果有锁定的目标
      var distance = Vector2.Distance(transform.position, lockingAt.transform.position);
      if (distance <= attackDistance) {
        // 如果锁定目标在攻击范围内，攻击
        if (thisCreature.cSkillList[0].isCooling) {
          // 如果普通攻击正在冷却，远离目标
          StartCoroutine(Escaping());
          // Escape();
        } else {
          if (facingToIt) {
            // 攻击
            Attack();
          } else {
            Chase();
          }
        }
      } else if (distance <= longAttackDistance) {
        // 如果锁定目标在远距攻击范围内，远程攻击
        if (thisCreature.cSkillList[1].isCooling) {
          // 如果远程攻击正在冷却，接近目标
          Chase();
        } else {
          if (facingToIt) {
            // 远程攻击
            LongAttack();
          } else {
            Chase();
          }
        }
      } else if (distance <= lockingMaxDistance) {
        // 如果目标在攻击范围外、锁定距离内，追赶
        Chase();
      } else {
        // 如果目标脱离锁定范围，解除锁定
        lockingAt = null;
      }
    }
  }

  private bool escaping = false;
  private bool freezing = false;
  private bool patrolling = false;
  private InputInfo patrolInputInfo;

  IEnumerator Escaping() {
    escaping = true;
    yield return new WaitForSeconds(escapeTime);
    escaping = false;
  }

  IEnumerator Freezing() {
    freezing = true;
    yield return new WaitForSeconds(attackFreezeTime);
    freezing = false;
  }

  bool facingToIt {
    get {
      bool right = (lockingAt.transform.position.x - transform.position.x > 0);
      return (right && thisCreature.isFacingRight) || (!right && !thisCreature.isFacingRight);
    }
  }

  void Escape() {
    InputInfo input = new InputInfo();
    bool right = (lockingAt.transform.position.x - transform.position.x > 0);
    if (right) {
      input.horizontalAxis = -0.5f;
    } else {
      input.horizontalAxis = 0.5f;
    }
    thisCreature.Act(input);
  }

  void Chase() {
    InputInfo input = new InputInfo();
    bool right = (lockingAt.transform.position.x - transform.position.x > 0);
    if (right) {
      input.horizontalAxis = 0.5f;
    } else {
      input.horizontalAxis = -0.5f;
    }
    thisCreature.Act(input);
  }

  void Attack() {
    InputInfo input = new InputInfo();
    input.fire0ButtonDown = true;
    thisCreature.Act(input);
  }

  void LongAttack() {
    InputInfo input = new InputInfo();
    input.fire1ButtonDown = true;
    thisCreature.Act(input);
  }

  void Patrol() {
    InputInfo input = new InputInfo();
    bool right = Random.Range(-1f, 1f) > 0;
    if (right) {
      input.horizontalAxis = 0.25f;
    } else {
      input.horizontalAxis = -0.25f;
    }
    StartCoroutine(PatrolCoroutine(input));
  }

  IEnumerator PatrolCoroutine(InputInfo input) {
    patrolling = true;
    patrolInputInfo = input;
    yield return new WaitForSeconds(Random.Range(0, patrolMoveMaxTime));
    patrolling = false;
  }
}
