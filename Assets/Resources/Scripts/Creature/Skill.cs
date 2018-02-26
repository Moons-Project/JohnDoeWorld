using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill {
  public enum DamageType {Sword = 0, Magic = 1};
  [System.Flags]
  public enum AttackType {
    None = 0,
    Weapon = 1,
    Bullet = 2,
  }

  public string idName;
  public string name;
  public string description;
  public DamageType damageType;
  public AttackType attackType;
  public float[] damage;
  public string[] buffList;
  public float cdTime;
  public float[] expList;
}
