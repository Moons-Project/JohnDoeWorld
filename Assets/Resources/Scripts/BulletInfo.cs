using UnityEngine;

[System.Serializable]
public class BulletInfo {  
  public string idName;
  public string name;
  public string description;
  public float force;
  public float angle;
  public RigidbodyPara rigidbodyPara;
  public Vector2 addForce {
    get {
      Vector2 normal = new Vector2(Mathf.Cos(angle / 180 * Mathf.PI), Mathf.Sin(angle / 180 * Mathf.PI));
      return normal * force;
    }
  }
}