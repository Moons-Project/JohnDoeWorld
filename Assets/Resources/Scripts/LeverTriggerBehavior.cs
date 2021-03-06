﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LeverTriggerBehavior : MonoBehaviour {

  bool triggered = false;

  public Tilemap LeftLadderTilemap, RightLadderTilemap;
  public string SoundEffect;
  public Sprite spriteToChange;

  void OnTriggerEnter2D(Collider2D collider) {
    if (triggered) return;
    Debug.Log(collider.gameObject.name);
    if (collider.CompareTag("ControlPlayer")) {
      triggered = true;
      if (SoundEffect != null) GameManager.instance.musicManager.PlaySE(SoundEffect);
      LeftLadderTilemap.gameObject.SetActive(false);
      RightLadderTilemap.gameObject.SetActive(true);
      GameManager.instance.tilemapManager.ladderTilemap = RightLadderTilemap;
      GetComponent<SpriteRenderer>().sprite = spriteToChange;
    }
  }
}
