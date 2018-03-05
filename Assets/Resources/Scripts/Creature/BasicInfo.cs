using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

[System.Serializable]
public class BasicInfo {
  public float sword = 100f;
  public float magic = 100f;
  public float rigidity = 100f; //刚性
  public float life = 100f;
  public float talent = 100f;

  public BasicInfo() {
    this.sword = 0;
    this.magic = 0;
    this.rigidity = 0;
    this.life = 0;
    this.talent = 0;
  }

  public BasicInfo(float sword, float magic, float rigidity, float life, float talent) {
    this.sword = sword;
    this.magic = magic;
    this.rigidity = rigidity;
    this.life = life;
    this.talent = talent;
  }

  public static BasicInfo operator -(BasicInfo info) {
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

  public override string ToString() {
    Type type = this.GetType();
    FieldInfo[] fields = type.GetFields();
    PropertyInfo[] properties = type.GetProperties();

    var sb = new StringBuilder();
    // Array.ForEach(fields, (field) => {
    //   sb.AppendLine(field.Name + ": " + (field.GetValue(this) ?? "(null)").ToString());
    // });
    // Array.ForEach(properties, (property) => {
    //   if (property.CanRead)
    //     sb.AppendLine(property.Name + ": " + (property.GetValue(this, null) ?? "(null)").ToString());
    // });
    return sb.ToString();
  }
}