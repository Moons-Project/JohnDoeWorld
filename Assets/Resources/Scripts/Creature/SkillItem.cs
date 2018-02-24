using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillItem {
  public enum DamageType {Sword = 0, Magic = 1};

  public int id;
  public string idName;
  public string name;
  public string description;
  public DamageType damageType;
  public float[] damage;
  public string[] buffList;
}
