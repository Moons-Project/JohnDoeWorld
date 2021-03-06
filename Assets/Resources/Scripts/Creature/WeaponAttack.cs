using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : Attack {

  public GameObject weaponEffectPrefab;

  public override void UseSkill(CreatureSkill skill, Creature attackSource) {
    base.UseSkill(skill, attackSource);
    Animator animator = GetComponent<Animator>();
    animator.speed = skill.calActionSpeedMultiplier * animator.speed;
    animator.Play(skill.skill.idName);
  }

  public override void startSkill() {
    base.startSkill();
    GetComponent<Collider2D>().enabled = true;
    attackSource.isAttacking = true;
  }

  public override void endSkill() {
    base.endSkill();
    GetComponent<Collider2D>().enabled = false;
    GetComponent<Animator>().speed = 1;
    attackSource.isAttacking = false;
  }

  protected override void HitCollider(Collider2D other) {
    base.HitCollider(other);
    var weaponEffect = Instantiate(weaponEffectPrefab, other.transform.position, Quaternion.identity);
    var main =weaponEffect.GetComponent<ParticleSystem>().main;
    main.startColor = Color.yellow;
  }
}