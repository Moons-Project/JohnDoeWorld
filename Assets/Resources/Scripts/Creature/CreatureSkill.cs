using UnityEngine;

[System.Serializable]
public class CreatureSkill {
  public CreatureSkill() {
    programInfo = new ProgramInfo();
    cumulateTime = float.MaxValue;
  }
  public CreatureSkill(Skill skill, int skillExp) {
    programInfo = new ProgramInfo();
    cumulateTime = float.MaxValue;
    this.skill = skill;
    this.exp = skillExp;
  }

  public bool isCooling {
    get {
      return cumulateTime < calCDTime;
    }
  }

  public Skill skill;
  public int level {
    get {
      for (int i = 0; i < skill.expList.Length; ++i) {
        if (exp < skill.expList[i]) {
          return i + 1;
        }
      }
      return skill.expList.Length;
    }
  }
  public Buff buff {
    get {
      if (level >= skill.buffList.Length) return null;
      return BuffDict.instance.itemDict[skill.buffList[level - 1]];
    }
  }
  public float cumulateTime;
  public float exp;
  public float expToNextLevel {
    get {
      if (level >= skill.damage.Length) return -1f;
      return skill.expList[level - 1] - exp;
    }
  }

  [System.Serializable]
  public class ProgramInfo {
    public ProgramInfo() { }

    public ProgramInfo(ProgramInfo other) {
      damageDelta = other.damageDelta;
      cdTimeDelta = other.cdTimeDelta;
      bulletForceNormDelta = other.bulletForceNormDelta;
      bulletAngle = other.bulletAngle;
      actionSpeedDelta = other.actionSpeedDelta;
    }

    public int damageDelta;
    public int cdTimeDelta;
    public int bulletForceNormDelta;
    public float bulletAngle;
    public int actionSpeedDelta;

    public static readonly float damageDeltaPercent = 0.1f;
    public static readonly float cdTimeDeltaPercent = 0.1f;
    public static readonly float bulletForceNormDeltaPercent = 0.1f;
    public static readonly float actionSpeedDeltaPercent = 0.1f;

    public bool ValidateSelf() {
      var tempCDTimeDelta = -cdTimeDelta;
      return damageDelta + tempCDTimeDelta + bulletForceNormDelta + actionSpeedDelta == 0;
    }

    public int balanceValue {
      get {
        var tempCDTimeDelta = -cdTimeDelta;
        return damageDelta + tempCDTimeDelta + bulletForceNormDelta + actionSpeedDelta;        
      }
    }
  }
  public ProgramInfo programInfo;

  public float originDamage {
    get {
      return skill.damage[level - 1];
    }
  }
  public float calDamage {
    get {
      if (level > skill.damage.Length) return 0f;
      return skill.damage[level - 1] * (1 + programInfo.damageDelta * ProgramInfo.damageDeltaPercent);
    }
  }

  public float originCDTime {
    get {
      return skill.cdTime;
    }
  }
  public float calCDTime {
    get {
      return skill.cdTime * (1 + programInfo.cdTimeDelta * ProgramInfo.cdTimeDeltaPercent);
    }
  }

  public float originBulletForceNorm {
    get {
      if (!BulletDict.instance.itemDict.ContainsKey(skill.idName)) return -1f;
      var bulletInfo = BulletDict.instance.itemDict[skill.idName];
      return bulletInfo.force;
    }
  }
  public float originBulletAngle {
    get {
      if (!BulletDict.instance.itemDict.ContainsKey(skill.idName)) return -1f;
      var bulletInfo = BulletDict.instance.itemDict[skill.idName];
      return bulletInfo.angle;
    }
  }
  public Vector2 calBulletForce {
    get {
      if (!BulletDict.instance.itemDict.ContainsKey(skill.idName)) return new Vector2();
      var bulletInfo = BulletDict.instance.itemDict[skill.idName];
      Debug.Log(bulletInfo.force * (1 + programInfo.bulletForceNormDelta * ProgramInfo.bulletForceNormDeltaPercent));
      return new Vector2(Mathf.Cos(programInfo.bulletAngle / 180 * Mathf.PI), Mathf.Sin(programInfo.bulletAngle / 180 * Mathf.PI)) * bulletInfo.force * (1 + programInfo.bulletForceNormDelta * ProgramInfo.bulletForceNormDeltaPercent);
    }
  }

  public float originActionSpeedMultiplier {
    get {
      if ((skill.attackType & Skill.AttackType.Bullet) != 0) return -1f;
      return 1f;
    }
  }
  public float calActionSpeedMultiplier {
    get {
      return 1 + programInfo.actionSpeedDelta * ProgramInfo.actionSpeedDeltaPercent;
    }
  }

  public void UpdateTime(float deltaTime) {
    if (isCooling)
      cumulateTime += deltaTime;
  }

  public void SetCooling() {
    cumulateTime = 0f;
  }
}