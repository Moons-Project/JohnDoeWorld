using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

  public Camera HUDCamera;
  public Canvas HUDCanvas;

  public Slider hpSlider;
  public static readonly string hpTextTemplate = "{0}/{1}";
  public Text hpText;

  public Image[] skillIcons;
  public Image[] cdHint;

  private GameManager manager;

  public static HUDManager instance;

  void Awake() {
    if (instance == null) {
      instance = this;
    }
  }

  // Use this for initialization
  void Start () {
    manager = GameManager.instance;
  }
  
  void OnGUI() {
    Creature controllingCreature = manager.controllingCreature;
    if (manager.controllingCreature == null) return;

    // 更新生命值
    hpText.text = string.Format(hpTextTemplate, controllingCreature.currentHP.ToString("f0"), controllingCreature.currentInfo.life.ToString("f0"));
    hpSlider.maxValue = controllingCreature.currentInfo.life;
    hpSlider.value = controllingCreature.currentHP;

    // 更新技能图标、冷却
    for (int i = 0; i < skillIcons.Length; ++i) {
      var cSkill = controllingCreature.cSkillList[i];
      var sprite = JsonManager.instance.spriteDict[cSkill.skill.idName];
      if (skillIcons[i].sprite != sprite) {
        skillIcons[i].sprite = sprite;
      }

      if (cSkill.isCooling) {
        cdHint[i].gameObject.SetActive(true);
        cdHint[i].fillAmount = cSkill.cumulateTime / cSkill.calCDTime;
        cdHint[i].GetComponentInChildren<Text>().text = (cSkill.calCDTime - cSkill.cumulateTime).ToString("f1");
      } else {
        cdHint[i].gameObject.SetActive(false);
      }
    }
  }

  public void OpenHUD() {
    HUDCanvas.gameObject.SetActive(true);
  }

  public void CloseHUD() {
    HUDCanvas.gameObject.SetActive(false);
  }
}
