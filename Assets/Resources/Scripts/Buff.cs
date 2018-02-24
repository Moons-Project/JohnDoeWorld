using UnityEngine;

// public delegate void DelegateWithOneCreature(Creature creature);

[System.Serializable]
public class Buff {
  public int id;
  public string idName;
  public string name;
  public string description;
  public BasicInfo addition;
  public bool shouldUpdate;

  public Buff() {
    
  }

  public Buff(Buff other) {
    this.id = other.id;
    this.idName = other.idName;
    this.name = other.name;
    this.description = other.description;
    this.addition = other.addition;
    this.shouldUpdate = other.shouldUpdate;
  }

  public static Buff FromJson(string json) {
    return JsonUtility.FromJson<Buff>(json);
  }

  public virtual void BuffEffect(Creature creature, float cumulateTime) {}
}