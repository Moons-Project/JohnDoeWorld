using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SysUIManager : MonoBehaviour {
  public Canvas UICanvas;
  public Camera UICamera;

  public GameObject tabPanel;

  public GameObject itemDescription;
  private RectTransform itemDescriptionRectTransform;
  public GameObject inventory;
  public GameObject inventoryItemPrefab;
  private List<GameObject> inventorySlots;
  public GameObject inventoryPanel;

  public GameObject cheatPanel;
  public InputField cheatInput;

  public GameObject skillPanel;
  public GameObject skillDetailPanel;
  public Text leftPartText;
  public Text rightPartText;
  public Image[] skillIcons;

  private ItemManager itemManager;
  private GameManager manager;

  public static SysUIManager instance;

  void Awake() {
    if (instance == null) {
      instance = this;
    }
  }

  // Use this for initialization
  void Start() {
    itemDescriptionRectTransform = itemDescription.transform as RectTransform;
    manager = GameManager.instance;
    itemManager = manager.itemManager;
  }

  public void GenerateInventoryUI(int inventorySize) {
    // Init Inventory
    inventorySlots = new List<GameObject>();
    for (int i = 0; i < inventorySize; ++i) {
      GameObject prefab = Instantiate(inventoryItemPrefab, inventory.transform);
      prefab.GetComponent<InventoryItemBehavior>().inventoryItemIndex = i;
      inventorySlots.Add(prefab);
    }
  }

  void OnGUI() {
    if (itemDescription.activeSelf) {
      // Set Position
      Vector2 pos;
      RectTransformUtility.ScreenPointToLocalPointInRectangle(UICanvas.transform as RectTransform, Input.mousePosition, UICamera, out pos);
      pos.x += itemDescriptionRectTransform.sizeDelta.x / 2;
      pos.y -= itemDescriptionRectTransform.sizeDelta.y / 2;

      itemDescriptionRectTransform.anchoredPosition = pos;
    }
  }

  #region Cheat

  private bool isCheatOpen = false;

  public void OpenCheat() {
    // Close other panel
    CloseAllTabs();

    isCheatOpen = true;
    cheatPanel.SetActive(true);
  }

  public void CloseCheat() {
    isCheatOpen = false;
    cheatPanel.SetActive(false);
  }

  public void ToggleCheat() {
    isCheatOpen = !isCheatOpen;

    // Close other panel
    if (isCheatOpen) CloseAllTabs();

    cheatPanel.SetActive(isCheatOpen);
  }

  public void ExecuteCheat() {
    string cheatString = cheatInput.text;
    CloseCheat();
    string[] splits = cheatString.Split(' ');
    if (splits.Length >= 2) {
      if (splits[0] == "goto") {
        manager.SwitchScene(splits[1]);
      }
    }
  }

  #endregion

  #region Inventory

  private bool isInventoryOpen = false;

  public void OpenInventory() {
    // Close other panel
    CloseAllTabs();

    isInventoryOpen = true;
    inventoryPanel.SetActive(true);
  }

  public void CloseInventory() {
    isInventoryOpen = false;
    inventoryPanel.SetActive(false);
  }

  public void ToggleInventory() {
    isInventoryOpen = !isInventoryOpen;

    // Close other panel
    if (isCheatOpen) CloseAllTabs();

    inventoryPanel.SetActive(isInventoryOpen);
  }

  private int showDescriptionId = 1;
  private static string templateDescription = "<color>{0}</color>\n" +
    "<color=cyan>{1}</color>\n" +
    "<color=grey>{2}</color>\n";

  public int ShowDescription(int index) {
    // Debug.Log(index);
    var invItem = manager.inventoryManager.inventoryDataset[index];

    if (invItem == null || invItem.item == null) return -1;

    Item item = invItem.item;

    string effect = "";
    if (item is Equipment) {
      Equipment equipment = item as Equipment;
      effect = string.Format("剑术 {0}\n魔力 {1}\n刚性 {2}\n生命 {3}",
        equipment.addition.sword.ToString("+#;-#;0"),
        equipment.addition.magic.ToString("+#;-#;0"),
        equipment.addition.rigidity.ToString("+#;-#;0"),
        equipment.addition.life.ToString("+#;-#;0"));
    }

    itemDescription.GetComponentInChildren<Text>().text = string.Format(templateDescription,
      item.name,
      effect,
      item.description
    );

    itemDescription.SetActive(true);

    return ++showDescriptionId;
  }

  public void UnshowDescription(int id) {
    if (showDescriptionId == id) itemDescription.SetActive(false);
  }

  public void UpdateItemGObj(int index) {
    inventorySlots[index].GetComponent<InventoryItemBehavior>().UpdateData();
  }

  #endregion

  #region Skill

  private bool isSkillOpen = false;

  private string leftPartTextTemplate = "<color='black'>{0}</color>\n" +
    "<color='white'>{1}</color>\n" +
    "<color='black'>伤害类型: {2}</color>\n" +
    "<color='black'>附加BUFF: {3}</color>\n" +
    "<color='black'>当前等级: {4}</color>\n" +
    "<color='black'>当前经验: {5}</color>\n" +
    "<color='black'>距离下一等级经验: {6}</color>";

  private string rightPartTextTemplate = "<color='black'>伤害系数: {0}</color><color='white'> {1}%</color>\n" +
    "<color='black'>冷却时间: {2}</color><color='white'> {3}%</color>\n" +
    "<color='black'>子弹力度: {4}</color><color='white'> {5}%</color>\n" +
    "<color='black'>子弹角度: {6}°</color>\n" +
    "<color='black'>动作速度: {7}</color><color='white'> {8}%</color>\n" +
    "<color='black'>平衡值: {9} (此值为0才可确认)</color>";

  private Creature.CreatureSkill edittingSkill;
  private Creature.CreatureSkill.ProgramInfo edittingProgramInfo;

  public void SetSkill(Creature.CreatureSkill cSkill) {
    edittingSkill = cSkill;
    edittingProgramInfo = new Creature.CreatureSkill.ProgramInfo(cSkill.programInfo);
    
    string leftPartTextText = string.Format(
      leftPartTextTemplate,
      cSkill.skill.name,
      cSkill.skill.description,
      cSkill.skill.damageType.ToString(),
      cSkill.buff != null ? cSkill.buff.name : "/",
      cSkill.level,
      cSkill.exp.ToString("f1"),
      cSkill.expToNextLevel.ToString("f1")
    );

    string rightPartTextText = string.Format(
      rightPartTextTemplate,
      cSkill.originDamage.ToString("f1"),
      (cSkill.programInfo.damageDelta * Creature.CreatureSkill.ProgramInfo.damageDeltaPercent  * 100f).ToString("+#;-#;0"),
      cSkill.originCDTime.ToString("f1"),
      (cSkill.programInfo.cdTimeDelta * Creature.CreatureSkill.ProgramInfo.cdTimeDeltaPercent  * 100f).ToString("+#;-#;0"),
      cSkill.originBulletForceNorm.ToString("f1"),
      (cSkill.programInfo.bulletForceNormDelta * Creature.CreatureSkill.ProgramInfo.bulletForceNormDeltaPercent  * 100f).ToString("+#;-#;0"),
      cSkill.originBulletAngle.ToString("f1"),
      cSkill.originActionSpeedMultiplier.ToString("f1"),
      (cSkill.programInfo.actionSpeedDelta * Creature.CreatureSkill.ProgramInfo.actionSpeedDeltaPercent  * 100f).ToString("+#;-#;0"),
      cSkill.programInfo.balanceValue
    );

    leftPartText.text = leftPartTextText;
    rightPartText.text = rightPartTextText;
  }

  public void OpenSkill() {
    CloseAllTabs();

    Creature creature = manager.inputManager.player.GetComponent<Creature>();
    for (int i = 0; i < skillIcons.Length; ++i) {
      skillIcons[i].sprite = itemManager.spriteDict[creature.skillList[i + 1]];
    }

    isSkillOpen = true;
    skillPanel.SetActive(true);
  }

  public void CloseSkill() {
    isSkillOpen = false;
    skillPanel.SetActive(false);
    CloseSkillDetail();
  }

  public void ToggleSkill() {
    isSkillOpen = !isSkillOpen;

    if (isSkillOpen) {
      CloseAllTabs();
      OpenSkill();
    }
  }

  public void OpenSkillDetail() {
    skillDetailPanel.SetActive(true);
  }

  public void CloseSkillDetail() {
    skillDetailPanel.SetActive(false);
  }

  public void OnSkillClick(int index) {
    Creature creature = manager.inputManager.player.GetComponent<Creature>();
    SetSkill(creature.cSkillList[index]);
    OpenSkillDetail();
  }

  public void UpdateRightPartWithProgramInfo(Creature.CreatureSkill.ProgramInfo info) {
    string rightPartTextText = string.Format(
      rightPartTextTemplate,
      edittingSkill.originDamage.ToString("f1"),
      (info.damageDelta * Creature.CreatureSkill.ProgramInfo.damageDeltaPercent  * 100f).ToString("+#;-#;0"),
      edittingSkill.originCDTime.ToString("f1"),
      (info.cdTimeDelta * Creature.CreatureSkill.ProgramInfo.cdTimeDeltaPercent  * 100f).ToString("+#;-#;0"),
      edittingSkill.originBulletForceNorm.ToString("f1"),
      (info.bulletForceNormDelta * Creature.CreatureSkill.ProgramInfo.bulletForceNormDeltaPercent  * 100f).ToString("+#;-#;0"),
      edittingSkill.originBulletAngle.ToString("f1"),
      edittingSkill.originActionSpeedMultiplier.ToString("f1"),
      (info.actionSpeedDelta * Creature.CreatureSkill.ProgramInfo.actionSpeedDeltaPercent  * 100f).ToString("+#;-#;0"),
      info.balanceValue
    );

    rightPartText.text = rightPartTextText;
  }

  public void ModifyDamageDelta(bool up) {
    var delta = edittingProgramInfo.damageDelta + (up ? 1 : -1);
    var rate = delta * Creature.CreatureSkill.ProgramInfo.damageDeltaPercent;
    if (rate < -1f) return;

    edittingProgramInfo.damageDelta = delta;
    // Update UI
    UpdateRightPartWithProgramInfo(edittingProgramInfo);
  }

  public void ModifyCDDelta(bool up) {
    var delta = edittingProgramInfo.cdTimeDelta + (up ? 1 : -1);
    var rate = delta * Creature.CreatureSkill.ProgramInfo.cdTimeDeltaPercent;
    if (rate < -1f) return;

    edittingProgramInfo.cdTimeDelta = delta;
    // Update UI
    UpdateRightPartWithProgramInfo(edittingProgramInfo);
  }

  public void ModifyBulletForceNormDelta(bool up) {
    var delta = edittingProgramInfo.bulletForceNormDelta + (up ? 1 : -1);
    var rate = delta * Creature.CreatureSkill.ProgramInfo.bulletForceNormDeltaPercent;
    if (rate < -1f) return;

    edittingProgramInfo.bulletForceNormDelta = delta;
    // Update UI
    UpdateRightPartWithProgramInfo(edittingProgramInfo);
  }

  public void ModifyActionSpeedDelta(bool up) {
    var delta = edittingProgramInfo.actionSpeedDelta + (up ? 1 : -1);
    var rate = delta * Creature.CreatureSkill.ProgramInfo.actionSpeedDeltaPercent;
    if (rate < -1f) return;

    edittingProgramInfo.actionSpeedDelta = delta;
    // Update UI
    UpdateRightPartWithProgramInfo(edittingProgramInfo);
  }

  public void ConfirmAndCloseSkillDetail() {
    // Validate
    if (!edittingProgramInfo.ValidateSelf()) {
      manager.dialogManager.SystemDialog("必须使平衡值为0");
      return;
    }

    edittingSkill.programInfo = edittingProgramInfo;
    CloseSkillDetail();
  }

  #endregion

  public void CloseAllTabs() {
    CloseCheat();
    CloseInventory();
    CloseSkill();
  }

  private bool isTabOpen = false;

  public void ToggleTab() {
    isTabOpen = !isTabOpen;
    tabPanel.SetActive(isTabOpen);
  }

  public void OpenTab() {
    isTabOpen = true;
    tabPanel.SetActive(true);
  }

  public void CloseTabTab() {
    isTabOpen = false;
    tabPanel.SetActive(false);
  }
}