// Decompiled with JetBrains decompiler
// Type: CharacterLoot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Text;
using TMPro;
using UnityEngine;

public class CharacterLoot : MonoBehaviour
{
  public ItemCombatIcon item0;
  public ItemCombatIcon item1;
  public ItemCombatIcon item2;
  public ItemCombatIcon item3;
  public ItemCombatIcon item4;
  public SpriteRenderer heroSpr;
  public SpriteRenderer heroSprMask;
  public Transform mark;
  public TMP_Text playerNick;
  public TMP_Text playerNickShadow;
  public Transform buttonCharacterDeck;
  public TMP_Text buttonCharacterDeckText;
  private int heroIndex;
  private Hero hero;

  private void Awake() => this.buttonCharacterDeck.gameObject.SetActive(false);

  public void AssignHero(int _heroIndex)
  {
    if (AtOManager.Instance.GetTeam().Length == 0)
      return;
    this.heroIndex = _heroIndex;
    this.hero = AtOManager.Instance.GetHero(this.heroIndex);
    if ((Object) this.heroSpr == (Object) null || this.hero == null || (Object) this.hero.HeroData == (Object) null)
      return;
    this.heroSpr.sprite = this.heroSprMask.sprite = this.hero.HeroData.HeroSubClass.SpriteBorder;
    this.buttonCharacterDeck.GetComponent<BotonRollover>().auxInt = _heroIndex;
    this.buttonCharacterDeck.gameObject.SetActive(true);
    if (GameManager.Instance.IsMultiplayer())
      this.SetNick(this.hero.Owner);
    this.ShowItems();
  }

  public void ShowItems()
  {
    this.item0.ShowIconExternal("weapon", (Character) this.hero);
    this.item1.ShowIconExternal("armor", (Character) this.hero);
    this.item2.ShowIconExternal("jewelry", (Character) this.hero);
    this.item3.ShowIconExternal("accesory", (Character) this.hero);
    this.item4.ShowIconExternal("pet", (Character) this.hero);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Texts.Instance.GetText("deck"));
    stringBuilder.Append("\n<color=#bbb><size=-.5>");
    stringBuilder.Append(string.Format(Texts.Instance.GetText("cardsNum"), (object) this.hero.Cards.Count));
    this.buttonCharacterDeckText.text = stringBuilder.ToString();
  }

  public void SetNick(string ownerNick)
  {
    this.playerNick.gameObject.SetActive(true);
    this.playerNick.text = this.playerNickShadow.text = "<" + NetworkManager.Instance.GetPlayerNickReal(ownerNick) + ">";
    this.playerNick.color = Functions.HexToColor(NetworkManager.Instance.GetColorFromNick(ownerNick));
  }

  public void Activate(bool state)
  {
    if (state)
    {
      this.heroSprMask.transform.gameObject.SetActive(false);
      this.heroSpr.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
      this.mark.gameObject.SetActive(true);
    }
    else
    {
      this.heroSprMask.transform.gameObject.SetActive(true);
      this.heroSpr.transform.localScale = new Vector3(0.525f, 0.525f, 1f);
      this.mark.gameObject.SetActive(false);
    }
  }

  private void OnMouseEnter()
  {
    if (GameManager.Instance.IsMultiplayer())
      return;
    GameManager.Instance.SetCursorHover();
  }

  private void OnMouseUp()
  {
    if (GameManager.Instance.IsMultiplayer())
      return;
    LootManager.Instance.ChangeCharacter(this.heroIndex);
  }

  private void OnMouseExit() => GameManager.Instance.SetCursorPlain();
}
