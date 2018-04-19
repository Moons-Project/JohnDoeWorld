using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LeverTriggerBehavior : MonoBehaviour {

  public Tilemap LeftLadderTilemap, RightLadderTilemap;
  public string SoundEffect;
  public Sprite spriteToChange;

  // Use this for initialization
  void Start () {
    
  }
  
  // Update is called once per frame
  void Update () {
    
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (collider.CompareTag("ControlPlayer")) {
      if (SoundEffect != null) GameManager.instance.musicManager.PlaySE(SoundEffect);
      LeftLadderTilemap.gameObject.SetActive(false);
      RightLadderTilemap.gameObject.SetActive(true);
      GameManager.instance.tilemapManager.ladderTilemap = RightLadderTilemap;
      GetComponent<SpriteRenderer>().sprite = spriteToChange;
    }
  }
}
