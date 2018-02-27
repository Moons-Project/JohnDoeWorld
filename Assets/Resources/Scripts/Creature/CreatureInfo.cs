using System.Collections;
using System.Collections.Generic;
using System.IO;
// using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class CreatureInfo {
  public string idName = "Test";
  public BasicInfo info = new BasicInfo(1f, 2f, 3f, 4f, 5f);
  public Equipment equement;
  public string[] skillList = new string[5] { "BasicAttack", "TopDownChop", "ThreeTimesChop", "Stab", "Battojutsu" };
  public Color color = Color.white;

  public CreatureInfo() {}

  public CreatureInfo(GameObject creatureObj) {
    var creature = creatureObj.GetComponent<Creature>();
    var sprite = creatureObj.GetComponent<SpriteRenderer>();
    info = creature.basicInfo;
    equement = creature.equippingItem;
    skillList = creature.skillList;
    color  = sprite.color;
  }

  public GameObject ApplyTo(GameObject creatureObj) {
    var creature = creatureObj.GetComponent<Creature>();
    var sprite = creatureObj.GetComponent<SpriteRenderer>();
    creature.basicInfo = info;
    creature.equippingItem = equement;
    creature.skillList = skillList;
    sprite.color = color;
    return creatureObj;
  }
}