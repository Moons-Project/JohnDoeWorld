using UnityEngine;

[System.Serializable]
public class RigidbodyPara {
  public float mass;
  public float gravityScale;

  public void ApplyToGameObject(GameObject gobj) {
    Rigidbody2D rb = gobj.GetComponent<Rigidbody2D>();
    rb.mass = mass;
    rb.gravityScale = gravityScale;
  }
}