// Decompiled with JetBrains decompiler
// Type: PopTutorialManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Text;
using TMPro;
using UnityEngine;

public class PopTutorialManager : MonoBehaviour
{
  public Transform content;
  public Transform box;
  public Transform circle;
  public Transform circle2;
  public Transform continueButton;
  public TMP_Text popText;

  private void Awake() => this.ShowContent(false);

  private string FormatText(string title, string body)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<color=#FFF><size=+.6>");
    stringBuilder.Append(title);
    stringBuilder.Append("</color></size><line-height=160%><br></line-height>");
    stringBuilder.Append(body);
    return stringBuilder.ToString();
  }

  public void Show(string type, Vector3 position, Vector3 position2)
  {
    switch (type)
    {
      case "characterUnlock":
        this.box.transform.position = new Vector3(3.1f, 0.0f, this.box.transform.position.z);
        this.popText.text = this.FormatText(Texts.Instance.GetText("tutorialCharactersTitle"), Texts.Instance.GetText("tutorialCharacters"));
        this.MoveCircle(position);
        this.MoveCircle2(new Vector3(3.1f, -1f, 0.0f));
        this.SizeCircle2(new Vector3(1f, 2f, 1f));
        break;
      case "characterPerks":
        this.box.transform.position = new Vector3(3.1f, 0.0f, this.box.transform.position.z);
        this.popText.text = this.FormatText(Texts.Instance.GetText("tutorialPerksTitle"), Texts.Instance.GetText("tutorialPerks"));
        this.MoveCircle(position);
        this.MoveCircle2(new Vector3(3.1f, -1f, 0.0f));
        this.SizeCircle2(new Vector3(1f, 2f, 1f));
        break;
      case "town":
        this.box.transform.position = new Vector3(0.0f, 0.0f, this.box.transform.position.z);
        this.popText.text = this.FormatText(Texts.Instance.GetText("tutorialTownTitle"), Texts.Instance.GetText("tutorialTown"));
        this.MoveCircle(Vector3.zero);
        this.HideCircle2();
        break;
      case "combatSpeed":
        this.box.transform.position = new Vector3(0.0f, 0.0f, this.box.transform.position.z);
        this.popText.text = this.FormatText(Texts.Instance.GetText("tutorialCombatSpeedTitle"), Texts.Instance.GetText("tutorialCombatSpeed"));
        this.MoveCircle(position);
        this.SizeCircle(new Vector3(1.5f, 1.5f, 1f));
        this.HideCircle2();
        break;
      case "firstTurnEnergy":
        this.box.transform.position = new Vector3(Globals.Instance.sizeW * 0.25f, Globals.Instance.sizeH * 0.05f, this.box.transform.position.z);
        this.popText.text = this.FormatText(Texts.Instance.GetText("tutorialFirstTurnEnergyTitle"), Texts.Instance.GetText("tutorialFirstTurnEnergy"));
        this.MoveCircle(position);
        this.SizeCircle(new Vector3(1.5f, 1.5f, 1f));
        this.MoveCircle2(position2);
        this.SizeCircle2(new Vector3(1.5f, 1.5f, 1f));
        break;
      case "cardTarget":
        this.box.transform.position = new Vector3(Globals.Instance.sizeW * 0.25f, Globals.Instance.sizeH * 0.05f, this.box.transform.position.z);
        this.popText.text = this.FormatText(Texts.Instance.GetText("tutorialCombatCardTargetTitle"), Texts.Instance.GetText("tutorialCombatCardTarget"));
        this.MoveCircle(position);
        this.SizeCircle(new Vector3(2.8f, 0.7f, 1f));
        this.HideCircle2();
        break;
      case "combatResists":
        this.box.transform.position = new Vector3(Globals.Instance.sizeW * 0.25f, Globals.Instance.sizeH * 0.05f, this.box.transform.position.z);
        this.popText.text = this.FormatText(Texts.Instance.GetText("tutorialCombatResistsTitle"), Texts.Instance.GetText("tutorialCombatResists"));
        this.MoveCircle(position);
        this.SizeCircle(new Vector3(5f, 2.5f, 1f));
        this.MoveCircle2(position2);
        this.SizeCircle2(new Vector3(3f, 3f, 1f));
        break;
      case "castNPC":
        this.box.transform.position = new Vector3(0.0f, 0.0f, this.box.transform.position.z);
        this.popText.text = this.FormatText(Texts.Instance.GetText("castNPCTitle"), Texts.Instance.GetText("castNPC"));
        this.MoveCircle(position);
        this.SizeCircle(new Vector3(2f, 2f, 1f));
        this.HideCircle2();
        break;
      case "eventRolls":
        this.box.transform.position = new Vector3((float) (-(double) Globals.Instance.sizeW * 0.11999999731779099), Globals.Instance.sizeH * 0.05f, this.box.transform.position.z);
        this.popText.text = this.FormatText(Texts.Instance.GetText("tutorialEventRollsTitle"), Texts.Instance.GetText("tutorialEventRolls"));
        this.MoveCircle(position);
        this.SizeCircle(new Vector3(5f, 1.3f, 1f));
        this.HideCircle2();
        break;
      case "townReward":
        this.box.transform.position = new Vector3(0.0f, 0.0f, this.box.transform.position.z);
        this.popText.text = this.FormatText(Texts.Instance.GetText("tutorialTownRewardsTitle"), Texts.Instance.GetText("tutorialTownRewards"));
        this.MoveCircle(position);
        this.SizeCircle(new Vector3(5f, 1.8f, 1f));
        this.HideCircle2();
        break;
      case "cardsReward":
        this.box.transform.position = new Vector3(0.0f, -1f, this.box.transform.position.z);
        this.popText.text = this.FormatText(Texts.Instance.GetText("tutorialCardsRewardTitle"), Texts.Instance.GetText("tutorialCardsReward"));
        this.MoveCircle(new Vector3(0.0f, -1f, 0.0f));
        this.HideCircle2();
        break;
      case "townItemCraft":
        this.box.transform.position = new Vector3(0.0f, 0.0f, this.box.transform.position.z);
        string body1 = string.Format(Texts.Instance.GetText("tutorialTownCraft"), (object) Globals.Instance.GetCardData("fireball").CardName, (object) "Evelyn");
        this.popText.text = this.FormatText(Texts.Instance.GetText("tutorialTownCraftTitle"), body1);
        this.MoveCircle(Vector3.zero);
        this.HideCircle2();
        break;
      case "townItemUpgrade":
        this.box.transform.position = new Vector3(0.0f, 0.0f, this.box.transform.position.z);
        string body2 = string.Format(Texts.Instance.GetText("tutorialTownUpgrade"), (object) Globals.Instance.GetCardData("faststrike").CardName, (object) "Magnus");
        this.popText.text = this.FormatText(Texts.Instance.GetText("tutorialTownUpgradeTitle"), body2);
        this.MoveCircle(Vector3.zero);
        this.HideCircle2();
        break;
      case "townItemLoot":
        this.box.transform.position = new Vector3(0.0f, 0.0f, this.box.transform.position.z);
        string body3 = string.Format(Texts.Instance.GetText("tutorialTownLoot"), (object) Globals.Instance.GetCardData("spyglass").CardName, (object) "Andrin");
        this.popText.text = this.FormatText(Texts.Instance.GetText("tutorialTownLootTitle"), body3);
        this.MoveCircle(position);
        this.SizeCircle(new Vector3(2.2f, 1.5f, 1f));
        this.HideCircle2();
        break;
    }
    CardScreenManager.Instance.ShowCardScreen(false);
    this.ShowContent();
  }

  private void MoveCircle(Vector3 position) => this.circle.transform.position = position;

  private void MoveCircle2(Vector3 position) => this.circle2.transform.position = position;

  private void SizeCircle(Vector3 scale) => this.circle.transform.localScale = scale;

  private void SizeCircle2(Vector3 scale) => this.circle2.transform.localScale = scale;

  private void HideCircle() => this.circle.gameObject.SetActive(false);

  private void HideCircle2() => this.circle2.gameObject.SetActive(false);

  private void ShowContent(bool state = true) => this.content.gameObject.SetActive(state);
}
