// Decompiled with JetBrains decompiler
// Type: ProgressManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
  private int totalCardsUnlocked = -1;
  private int totalNPCsKilled = -1;
  private int totalBossKilled = -1;
  public Transform cardProgress;
  public TMP_Text cardProgressTxt;
  public Animator cardProgressAnim;
  public Transform equipmentProgress;
  public TMP_Text equipmentProgressTxt;
  public Animator equipmentProgressAnim;
  public Transform npcProgress;
  public TMP_Text npcProgressTxt;
  public Animator npcProgressAnim;
  public Transform bossProgress;
  public TMP_Text bossProgressTxt;
  public Animator bossProgressAnim;
  private float totalTimeout;
  private float waitTimeout = 4f;

  public static ProgressManager Instance { get; private set; }

  private void Awake()
  {
    if ((Object) ProgressManager.Instance == (Object) null)
      ProgressManager.Instance = this;
    else if ((Object) ProgressManager.Instance != (Object) this)
      Object.Destroy((Object) this.gameObject);
    Object.DontDestroyOnLoad((Object) this.gameObject);
  }

  private void Start() => this.HideAll();

  private void Init()
  {
    this.totalCardsUnlocked = AtOManager.Instance.unlockedCards.Count;
    this.totalNPCsKilled = PlayerManager.Instance.MonstersKilled;
    this.totalBossKilled = PlayerManager.Instance.BossesKilled;
  }

  public void CheckProgress()
  {
    this.totalTimeout = 0.0f;
    if (this.totalCardsUnlocked == -1)
      this.Init();
    int count = AtOManager.Instance.unlockedCards.Count;
    if (count > this.totalCardsUnlocked)
    {
      int num = count - this.totalCardsUnlocked;
      int _items = 0;
      for (int index = 0; index < num; ++index)
      {
        if (Globals.Instance.GetCardData(AtOManager.Instance.unlockedCards[AtOManager.Instance.unlockedCards.Count - 1 - index]).CardClass == Enums.CardClass.Item)
          ++_items;
      }
      int _cards = num - _items;
      if (_cards > 0)
        this.DoCardProgress(_cards);
      if (_items > 0)
        this.DoItemProgress(_items);
    }
    this.totalCardsUnlocked = AtOManager.Instance.unlockedCards.Count;
    int monstersKilled = PlayerManager.Instance.MonstersKilled;
    if (monstersKilled > this.totalNPCsKilled)
      this.DoNPCProgress(monstersKilled - this.totalNPCsKilled);
    this.totalNPCsKilled = PlayerManager.Instance.MonstersKilled;
    int bossesKilled = PlayerManager.Instance.BossesKilled;
    if (bossesKilled > this.totalBossKilled)
      this.DoBossProgress(bossesKilled - this.totalBossKilled);
    this.totalBossKilled = PlayerManager.Instance.BossesKilled;
  }

  private void DoItemProgress(int _items)
  {
    if (AtOManager.Instance.currentMapNode == "tutorial_0" || AtOManager.Instance.currentMapNode == "tutorial_1" || AtOManager.Instance.currentMapNode == "tutorial_2")
      return;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Texts.Instance.GetText("equipmentDiscoveredTome"));
    stringBuilder.Append(" <size=+.1><color=#FFF>+");
    stringBuilder.Append(_items);
    this.equipmentProgressTxt.text = stringBuilder.ToString();
    this.equipmentProgress.gameObject.SetActive(true);
    this.StartCoroutine(this.ShowElement("equipment"));
  }

  private void DoCardProgress(int _cards)
  {
    if (AtOManager.Instance.currentMapNode == "tutorial_0" || AtOManager.Instance.currentMapNode == "tutorial_1" || AtOManager.Instance.currentMapNode == "tutorial_2")
      return;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Texts.Instance.GetText("cardsUnlockedTome"));
    stringBuilder.Append(" <size=+.1><color=#FFF>+");
    stringBuilder.Append(_cards);
    this.cardProgressTxt.text = stringBuilder.ToString();
    this.cardProgress.gameObject.SetActive(true);
    this.StartCoroutine(this.ShowElement("cards"));
  }

  private void DoBossProgress(int _bosses)
  {
    if (AtOManager.Instance.currentMapNode == "tutorial_0" || AtOManager.Instance.currentMapNode == "tutorial_1" || AtOManager.Instance.currentMapNode == "tutorial_2")
      return;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Texts.Instance.GetText("bossesKilledTome"));
    stringBuilder.Append(" <size=+.1><color=#FFF>+");
    stringBuilder.Append(_bosses);
    this.bossProgressTxt.text = stringBuilder.ToString();
    this.bossProgress.gameObject.SetActive(true);
    this.StartCoroutine(this.ShowElement("boss"));
  }

  private void DoNPCProgress(int _npcs)
  {
    if (AtOManager.Instance.currentMapNode == "tutorial_0" || AtOManager.Instance.currentMapNode == "tutorial_1" || AtOManager.Instance.currentMapNode == "tutorial_2")
      return;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Texts.Instance.GetText("monstersKilledTome"));
    stringBuilder.Append(" <size=+.1><color=#FFF>+");
    stringBuilder.Append(_npcs);
    this.npcProgressTxt.text = stringBuilder.ToString();
    this.npcProgress.gameObject.SetActive(true);
    this.StartCoroutine(this.ShowElement("npcs"));
  }

  public void HideAll()
  {
    this.cardProgress.gameObject.SetActive(false);
    this.equipmentProgress.gameObject.SetActive(false);
    this.bossProgress.gameObject.SetActive(false);
    this.npcProgress.gameObject.SetActive(false);
  }

  private IEnumerator ShowElement(string item)
  {
    this.totalTimeout += 0.25f;
    yield return (object) Globals.Instance.WaitForSeconds(this.totalTimeout);
    if (item == "cards")
    {
      this.cardProgressAnim.ResetTrigger("show");
      this.cardProgressAnim.SetTrigger("show");
      yield return (object) Globals.Instance.WaitForSeconds(this.waitTimeout);
      this.cardProgressAnim.ResetTrigger("hide");
      this.cardProgressAnim.SetTrigger("hide");
      yield return (object) Globals.Instance.WaitForSeconds(2f);
      this.cardProgress.gameObject.SetActive(false);
    }
    if (item == "equipment")
    {
      this.equipmentProgressAnim.ResetTrigger("show");
      this.equipmentProgressAnim.SetTrigger("show");
      yield return (object) Globals.Instance.WaitForSeconds(this.waitTimeout);
      this.equipmentProgressAnim.ResetTrigger("hide");
      this.equipmentProgressAnim.SetTrigger("hide");
      yield return (object) Globals.Instance.WaitForSeconds(2f);
      this.equipmentProgress.gameObject.SetActive(false);
    }
    else if (item == "npcs")
    {
      this.npcProgressAnim.ResetTrigger("show");
      this.npcProgressAnim.SetTrigger("show");
      yield return (object) Globals.Instance.WaitForSeconds(this.waitTimeout);
      this.npcProgressAnim.ResetTrigger("hide");
      this.npcProgressAnim.SetTrigger("hide");
      yield return (object) Globals.Instance.WaitForSeconds(2f);
      this.npcProgress.gameObject.SetActive(false);
    }
    else if (item == "boss")
    {
      this.bossProgressAnim.ResetTrigger("show");
      this.bossProgressAnim.SetTrigger("show");
      yield return (object) Globals.Instance.WaitForSeconds(this.waitTimeout);
      this.bossProgressAnim.ResetTrigger("hide");
      this.bossProgressAnim.SetTrigger("hide");
      yield return (object) Globals.Instance.WaitForSeconds(2f);
      this.bossProgress.gameObject.SetActive(false);
    }
  }
}
