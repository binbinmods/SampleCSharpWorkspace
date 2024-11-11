// Decompiled with JetBrains decompiler
// Type: UICombatDeath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICombatDeath : MonoBehaviour
{
  public TMP_Text textCharDeath;
  public TMP_Text textInstructions;
  public Transform mask;
  public Image imageChar;
  public Canvas canvas;
  public Transform button;
  private List<GameObject> cardsGO;
  private Hero theHero;

  private void Awake()
  {
    this.canvas.gameObject.SetActive(false);
    this.cardsGO = new List<GameObject>();
  }

  public bool IsActive() => this.canvas.gameObject.activeSelf;

  public void TurnOn(bool showButton = true)
  {
    MatchManager.Instance.lockHideMask = true;
    this.canvas.gameObject.SetActive(true);
    if (showButton)
      this.button.gameObject.SetActive(true);
    else
      this.button.gameObject.SetActive(false);
  }

  public void SetCharacter(Hero _hero)
  {
    string str1 = string.Format(Texts.Instance.GetText("deathScreenTitle"), (object) _hero.SourceName);
    string str2 = string.Format(Texts.Instance.GetText("deathScreenBody"), (object) 70.ToString());
    this.textCharDeath.text = str1;
    this.textInstructions.text = str2;
    if ((Object) _hero.BorderSprite != (Object) null)
    {
      this.imageChar.gameObject.SetActive(true);
      this.imageChar.sprite = _hero.BorderSprite;
    }
    else
      this.imageChar.gameObject.SetActive(false);
  }

  public void SetCard(string theCard)
  {
    int num = 0;
    string cardInDictionary = MatchManager.Instance.CreateCardInDictionary(theCard);
    GameObject gameObject = Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.transform);
    this.cardsGO.Add(gameObject);
    CardItem component = gameObject.GetComponent<CardItem>();
    gameObject.name = "TMP_" + cardInDictionary;
    component.SetCard(cardInDictionary, false);
    component.TopLayeringOrder("UI", 32100 + 40 * num);
    component.SetLocalScale(new Vector3(1.2f, 1.2f, 1f));
    component.SetLocalPosition(new Vector3(4.9f, 1.9f, 0.0f));
    component.DisableTrail();
    component.HideRarityParticles();
    component.HideCardIconParticles();
    component.cardfordisplay = true;
    component.DrawBorder("red");
  }

  public void TurnOffFromButton()
  {
    this.button.gameObject.SetActive(false);
    MatchManager.Instance.DeathScreenOff();
  }

  public void TurnOff()
  {
    MatchManager.Instance.lockHideMask = false;
    this.canvas.gameObject.SetActive(false);
    for (int index = 0; index < this.cardsGO.Count; ++index)
      Object.Destroy((Object) this.cardsGO[index]);
    this.cardsGO.Clear();
    MatchManager.Instance.waitingDeathScreen = false;
  }
}
