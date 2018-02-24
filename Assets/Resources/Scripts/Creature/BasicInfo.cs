using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicInfo {
  public float sword = 100f;
  public float magic = 100f;
  public float rigidity = 100f; //刚性
  public float life = 100f;
  public float talent = 100f;

  public BasicInfo(float sword, float magic, float rigidity, float life, float talent) {
    this.sword = sword;
    this.magic = magic;
    this.rigidity = rigidity;
    this.life = life;
    this.talent = talent;
  }


  public static BasicInfo operator -( BasicInfo info){
    return new BasicInfo(-info.sword, -info.magic, -info.rigidity, -info.life, -info.talent);
  }

  public static BasicInfo operator +(BasicInfo left, BasicInfo right) {
    return new BasicInfo(
      left.sword + right.sword,
      left.magic + right.magic,
      left.rigidity + right.rigidity,
      left.life + right.life,
      left.talent + right.talent
    );
  }
}