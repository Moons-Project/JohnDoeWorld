using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : Attack {

  public void startSkill() {
    Debug.Log("WeaponAttack startSkill");
    GetComponent<Collider2D>().enabled = true;
  }

  public void endSkill() {
    Debug.Log("WeaponAttack endSkill");
    GetComponent<Collider2D>().enabled = false;
  }
}