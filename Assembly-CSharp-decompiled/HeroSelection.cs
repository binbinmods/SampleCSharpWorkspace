// Decompiled with JetBrains decompiler
// Type: HeroSelection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;

public class HeroSelection : MonoBehaviour
{
  public bool blocked;
  private Vector3 destination = Vector3.zero;
  private float distance;
  private GameObject oldBox;
  private SubClassData subClassData;
  public Transform sprite;
  public Transform spriteBackground;
  public TMP_Text nameTM;
  public TMP_Text rankTM;
  public int rankTMHidden;
  public TMP_Text nameShadow;
  public TMP_Text nameOver;
  public TMP_Text rankOver;
  private string id;
  public ParticleSystem particleFlash;
  public Transform borderTransform;
  public Transform botPopup;
  public Transform botPopupPerks;
  public TMP_Text perkPoints;
  public Transform perkPointsT;
  public Transform botPerks;
  public Transform lockIcon;
  private Transform parent;
  private Transform nameT;
  private SpriteRenderer spriteSR;
  private SpriteRenderer spriteBackgroundSR;
  private Vector3 startMousePos;
  private Vector3 startPos;
  private Vector3 namePos;
  private bool controllerClicked;
  private bool moveThis;
  private bool isInOriPosition = true;
  private bool multiplayerBlocked;
  public bool selected;
  private bool dlcBlocked;
  public Transform whiteSquare;

  public string Id
  {
    get => this.id;
    set => this.id = value;
  }

  private void Awake()
  {
    this.spriteSR = this.sprite.GetComponent<SpriteRenderer>();
    this.spriteBackgroundSR = this.spriteBackground.GetComponent<SpriteRenderer>();
    this.nameT = this.nameTM.transform;
    this.namePos = this.nameT.position;
  }

  private void Start()
  {
    this.destination = this.sprite.position;
    this.nameOver.gameObject.SetActive(false);
    this.rankOver.gameObject.SetActive(false);
    this.spriteSR.enabled = false;
    this.parent = this.gameObject.transform.parent;
    if (GameManager.Instance.IsObeliskChallenge())
      this.botPerks.gameObject.SetActive(false);
    if (this.blocked || this.multiplayerBlocked || this.dlcBlocked)
      this.rankTM.gameObject.SetActive(false);
    this.showWhiteSquare(false);
  }

  private void Update()
  {
    if (this.moveThis)
      this.sprite.position = this.destination;
    if (!((Object) HeroSelectionManager.Instance.controllerCurrentHS == (Object) this))
      return;
    this.Dragging();
  }

  public void SetMultiplayerBlocked(bool state)
  {
    if (state)
    {
      this.multiplayerBlocked = true;
      this.nameTM.color = Functions.HexToColor(Globals.Instance.ClassColor["warrior"]);
    }
    else
    {
      this.multiplayerBlocked = false;
      this.nameTM.color = Functions.HexToColor(Globals.Instance.ClassColor["scout"]);
    }
  }

  public void SetSubclass(SubClassData _subclassdata)
  {
    this.subClassData = _subclassdata;
    this.id = _subclassdata.Id;
    this.SetDlcBlock();
  }

  public void SetDlcBlock()
  {
    if (this.subClassData.Sku != "" && !SteamManager.Instance.PlayerHaveDLC(this.subClassData.Sku))
      this.dlcBlocked = true;
    else
      this.dlcBlocked = false;
  }

  public string GetSubclassName() => (Object) this.subClassData != (Object) null ? this.subClassData.SubClassName : "";

  public void SetSprite(Sprite _sprite = null, Sprite _spriteBorder = null, Sprite _spriteLocked = null)
  {
    if ((Object) _sprite != (Object) null)
      this.spriteSR.sprite = _sprite;
    if (!this.blocked && !this.dlcBlocked)
    {
      if (!((Object) _spriteBorder != (Object) null))
        return;
      this.spriteBackgroundSR.sprite = _spriteBorder;
      this.spriteBackground.GetComponent<SpriteMask>().sprite = _spriteBorder;
    }
    else
    {
      if (!((Object) _spriteLocked != (Object) null))
        return;
      this.spriteBackgroundSR.sprite = _spriteLocked;
      this.spriteBackground.GetComponent<SpriteMask>().sprite = _spriteLocked;
    }
  }

  public void SetSpriteSilueta(Sprite _spriteBorder = null)
  {
    if (!((Object) _spriteBorder != (Object) null))
      return;
    this.spriteBackgroundSR.sprite = _spriteBorder;
    this.spriteBackground.GetComponent<SpriteMask>().sprite = _spriteBorder;
  }

  public void SetName(string _name)
  {
    this.nameTM.text = this.nameShadow.text = this.nameOver.text = _name;
    this.SetRank();
  }

  public void SetRank()
  {
    this.rankTM.text = this.rankOver.text = string.Format(Texts.Instance.GetText("rankProgress"), (object) PlayerManager.Instance.GetPerkRank(this.subClassData.Id));
    this.rankTMHidden = PlayerManager.Instance.GetPerkRank(this.subClassData.Id);
    if (!GameManager.Instance.IsObeliskChallenge())
      return;
    this.rankTM.gameObject.SetActive(false);
    this.rankOver.gameObject.SetActive(false);
  }

  public void SetRankBox(int _rank)
  {
    this.rankOver.text = string.Format(Texts.Instance.GetText("rankProgress"), (object) _rank);
    this.rankTMHidden = _rank;
    if (!GameManager.Instance.IsObeliskChallenge())
      return;
    this.rankTM.gameObject.SetActive(false);
    this.rankOver.gameObject.SetActive(false);
  }

  private void ResetSkin() => this.SetSkin(PlayerManager.Instance.GetActiveSkin(this.subClassData.Id));

  public void SetSkin(string _skinId)
  {
    if (_skinId == "")
      _skinId = Globals.Instance.GetSkinBaseIdBySubclass(this.subClassData.Id);
    SkinData skinData = Globals.Instance.GetSkinData(_skinId);
    if (!((Object) skinData != (Object) null))
      return;
    this.spriteSR.sprite = skinData.SpritePortrait;
  }

  public void Init()
  {
    this.perkPointsT.gameObject.SetActive(false);
    if (!this.blocked && !this.dlcBlocked)
    {
      this.botPopup.gameObject.SetActive(true);
      this.botPopup.GetComponent<BotHeroChar>().SetSubClassData(this.subClassData);
      this.botPopupPerks.gameObject.SetActive(true);
      this.botPopupPerks.GetComponent<BotHeroChar>().SetSubClassData(this.subClassData);
      this.lockIcon.gameObject.SetActive(false);
      this.SetPerkPoints();
    }
    else
    {
      this.botPopup.gameObject.SetActive(false);
      this.botPopupPerks.gameObject.SetActive(false);
      this.lockIcon.gameObject.SetActive(true);
      if (this.dlcBlocked)
      {
        this.lockIcon.GetComponent<PopupText>().id = "";
        this.lockIcon.GetComponent<PopupText>().text = string.Format(Texts.Instance.GetText("requiredDLCandQuest"), (object) SteamManager.Instance.GetDLCName(this.subClassData.Sku));
      }
      this.nameDisabled();
    }
  }

  public void ShowComingSoon()
  {
    this.nameTM.text = this.nameShadow.text = Texts.Instance.GetText("comingSoon");
    this.nameTM.color = Functions.HexToColor("#572424");
    this.lockIcon.gameObject.SetActive(false);
  }

  public void SetPerkPoints()
  {
    if (GameManager.Instance.IsLoadingGame())
    {
      this.perkPointsT.gameObject.SetActive(false);
      this.botPerks.gameObject.SetActive(false);
    }
    else
    {
      int perkPointsAvailable = PlayerManager.Instance.GetPerkPointsAvailable(this.Id);
      if (perkPointsAvailable > 0)
      {
        this.perkPoints.text = perkPointsAvailable.ToString();
        this.perkPointsT.gameObject.SetActive(true);
      }
      else
        this.perkPointsT.gameObject.SetActive(false);
      if (!GameManager.Instance.IsObeliskChallenge())
        return;
      this.perkPointsT.gameObject.SetActive(false);
    }
  }

  public void AssignHeroToBox(GameObject _box) => this.StartCoroutine(this.AssignCo(_box));

  private IEnumerator AssignCo(GameObject _box)
  {
    HeroSelection _heroSelection = this;
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    _heroSelection.isInOriPosition = false;
    _heroSelection.transform.parent = GameObject.Find("/BoxCharacters").transform;
    _heroSelection.sprite.position = _box.transform.position;
    _heroSelection.sprite.transform.localScale = new Vector3(1f, 1f, 1f);
    _heroSelection.spriteBackgroundSR.color = new Color(0.3f, 0.3f, 0.3f, 1f);
    _heroSelection.spriteSR.sortingLayerName = "Characters";
    _heroSelection.spriteSR.sortingOrder = 0;
    _heroSelection.spriteSR.enabled = true;
    _heroSelection.spriteSR.color = new Color(1f, 1f, 1f, 1f);
    _heroSelection.nameOver.gameObject.SetActive(true);
    _heroSelection.nameOver.GetComponent<Renderer>().sortingOrder = 1;
    _heroSelection.rankOver.gameObject.SetActive(true);
    _heroSelection.rankOver.GetComponent<Renderer>().sortingOrder = 1;
    _heroSelection.nameDisabled();
    HeroSelectionManager.Instance.FillBox(_box, _heroSelection, true);
    if (GameManager.Instance.IsObeliskChallenge())
    {
      _heroSelection.rankTM.gameObject.SetActive(false);
      _heroSelection.rankOver.gameObject.SetActive(false);
    }
  }

  public void MoveToBox(GameObject _box, GameObject _oldBox, bool animation = true)
  {
    this.selected = false;
    this.oldBox = _oldBox;
    this.spriteSR.enabled = true;
    this.isInOriPosition = false;
    this.destination = _box.transform.position;
    this.sprite.position = this.destination;
    this.nameOver.gameObject.SetActive(true);
    this.rankOver.gameObject.SetActive(true);
    HeroSelectionManager.Instance.FillBox(_box, this, true);
    if (!GameManager.Instance.IsObeliskChallenge())
      return;
    this.rankTM.gameObject.SetActive(false);
    this.rankOver.gameObject.SetActive(false);
  }

  public void GoBackToOri()
  {
    this.selected = false;
    this.oldBox = (GameObject) null;
    this.spriteSR.enabled = false;
    this.spriteBackgroundSR.color = new Color(1f, 1f, 1f, 1f);
    this.nameRegular();
    this.nameOver.gameObject.SetActive(false);
    this.rankOver.gameObject.SetActive(false);
    this.transform.parent = this.parent;
    this.sprite.position = this.spriteBackground.position;
    this.isInOriPosition = true;
    this.transform.localPosition = Vector3.zero;
    if (!this.controllerClicked)
      return;
    this.ResetClickedController();
  }

  private void nameMouseEnter()
  {
    this.nameTM.color = new Color(1f, 0.7f, 0.0f, 1f);
    this.spriteBackgroundSR.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
  }

  private void nameDisabled()
  {
    this.nameTM.color = new Color(0.3f, 0.3f, 0.3f, 1f);
    this.spriteBackgroundSR.transform.localScale = new Vector3(0.95f, 0.95f, 1f);
  }

  private void nameRegular()
  {
    this.nameTM.color = new Color(1f, 1f, 1f, 1f);
    this.spriteBackgroundSR.transform.localScale = new Vector3(0.95f, 0.95f, 1f);
    if (!GameManager.Instance.IsObeliskChallenge() && !this.blocked && !this.multiplayerBlocked && !this.dlcBlocked)
      return;
    this.rankTM.gameObject.SetActive(false);
    this.rankOver.gameObject.SetActive(false);
  }

  public void OnMouseOver()
  {
    if (GameManager.Instance.GetDeveloperMode() && (this.blocked || this.dlcBlocked) && Input.GetMouseButtonUp(1))
      PlayerManager.Instance.HeroUnlock(this.id, achievement: false);
    if (GameManager.Instance.IsWeeklyChallenge() && !GameManager.Instance.GetDeveloperMode() || AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || GameManager.Instance.IsMultiplayer() && GameManager.Instance.IsLoadingGame() || HeroSelectionManager.Instance.charPopup.MoveThis || this.blocked || this.multiplayerBlocked || this.dlcBlocked || HeroSelectionManager.Instance.dragging)
      return;
    GameObject overBox = HeroSelectionManager.Instance.GetOverBox();
    if ((Object) overBox != (Object) null && HeroSelectionManager.Instance.IsYourBox(overBox.name))
      HeroSelectionManager.Instance.ShowDrag(true, this.transform.position);
    if (!Input.GetMouseButtonUp(1))
      return;
    this.RightClick();
  }

  public void RightClick()
  {
    if ((Object) HeroSelectionManager.Instance.controllerCurrentHS != (Object) null)
    {
      HeroSelectionManager.Instance.controllerCurrentHS.ResetClickedController();
      HeroSelectionManager.Instance.ResetController();
    }
    HeroSelectionManager.Instance.charPopup.Init(this.subClassData);
    HeroSelectionManager.Instance.charPopup.Show();
    HeroSelectionManager.Instance.MouseOverBox((GameObject) null);
  }

  public void OnMouseDown()
  {
    this.showWhiteSquare(false);
    if (GameManager.Instance.IsWeeklyChallenge() && !GameManager.Instance.GetDeveloperMode() || AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || GameManager.Instance.IsMultiplayer() && GameManager.Instance.IsLoadingGame() || HeroSelectionManager.Instance.charPopup.MoveThis)
      return;
    if (!this.blocked && !this.dlcBlocked)
    {
      GameObject overBox = HeroSelectionManager.Instance.GetOverBox();
      if ((Object) overBox != (Object) null && !HeroSelectionManager.Instance.IsYourBox(overBox.name))
      {
        this.multiplayerBlocked = true;
        return;
      }
      this.multiplayerBlocked = false;
      this.PickHero();
    }
    if (this.blocked || this.multiplayerBlocked || this.dlcBlocked)
      return;
    this.Dragging();
  }

  public void OnMouseDrag()
  {
    if (GameManager.Instance.IsWeeklyChallenge() && !GameManager.Instance.GetDeveloperMode() || !this.selected || AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || GameManager.Instance.IsMultiplayer() && GameManager.Instance.IsLoadingGame() || this.blocked || this.multiplayerBlocked || this.dlcBlocked)
      return;
    this.Dragging();
  }

  public void OnMouseUp()
  {
    if (GameManager.Instance.IsWeeklyChallenge() && !GameManager.Instance.GetDeveloperMode() || AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || GameManager.Instance.IsMultiplayer() && GameManager.Instance.IsLoadingGame())
      return;
    if (HeroSelectionManager.Instance.charPopup.MoveThis)
    {
      this.DraggingStop();
    }
    else
    {
      if (this.blocked || this.dlcBlocked)
        return;
      this.DraggingStop();
    }
  }

  public void OnMouseExit()
  {
    if (GameManager.Instance.IsWeeklyChallenge() && !GameManager.Instance.GetDeveloperMode() || AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || GameManager.Instance.IsMultiplayer() && GameManager.Instance.IsLoadingGame())
      return;
    this.showWhiteSquare(false);
    if (!this.blocked && !this.dlcBlocked && this.isInOriPosition && !HeroSelectionManager.Instance.dragging)
    {
      this.particleFlash.gameObject.SetActive(false);
      this.nameRegular();
    }
    if ((Object) HeroSelectionManager.Instance.charPopupGO != (Object) null)
    {
      int num = HeroSelectionManager.Instance.charPopupGO.activeSelf ? 1 : 0;
    }
    if (this.blocked || this.dlcBlocked)
      return;
    HeroSelectionManager.Instance.ShowDrag(false, Vector3.zero);
  }

  public void OnMouseEnter()
  {
    if (GameManager.Instance.IsWeeklyChallenge() && !GameManager.Instance.GetDeveloperMode() || GameManager.Instance.IsMultiplayer() && GameManager.Instance.IsLoadingGame() || this.blocked || this.dlcBlocked || !this.isInOriPosition || HeroSelectionManager.Instance.dragging)
      return;
    this.particleFlash.gameObject.SetActive(true);
    GameManager.Instance.PlayLibraryAudio("castnpccardfast");
    this.nameMouseEnter();
    this.showWhiteSquare(true);
  }

  private void showWhiteSquare(bool _state)
  {
    if (this.whiteSquare.gameObject.activeSelf == _state)
      return;
    this.whiteSquare.gameObject.SetActive(_state);
  }

  public void OnClickedController()
  {
    HeroSelectionManager.Instance.dragging = false;
    if (this.blocked || this.multiplayerBlocked || this.dlcBlocked || GameManager.Instance.IsLoadingGame())
      return;
    GameObject overBox = HeroSelectionManager.Instance.GetOverBox();
    if ((Object) overBox == (Object) null || (Object) HeroSelectionManager.Instance.controllerCurrentHS == (Object) null)
    {
      HeroSelectionManager.Instance.controllerCurrentHS = this;
      this.controllerClicked = true;
      this.OnMouseDown();
      if (!((Object) overBox == (Object) null))
        return;
      HeroSelectionManager.Instance.MoveAbsoluteToCharactersAfterClick();
    }
    else
    {
      if (!((Object) overBox != (Object) null))
        return;
      if ((Object) HeroSelectionManager.Instance.controllerCurrentHS != (Object) null)
        this.PickStop();
      else
        this.PickHero();
    }
  }

  public void ResetClickedController()
  {
    HeroSelectionManager.Instance.controllerCurrentHS = (HeroSelection) null;
    this.controllerClicked = false;
    HeroSelectionManager.Instance.dragging = false;
    this.PickStop(_removeHeroAbsolute: true);
  }

  private void Dragging()
  {
    if (this.blocked || this.multiplayerBlocked || this.dlcBlocked)
      return;
    if (HeroSelectionManager.Instance.IsHeroSelected(this.Id))
    {
      this.Reset();
    }
    else
    {
      Vector3 worldPoint = GameManager.Instance.cameraMain.ScreenToWorldPoint(Input.mousePosition) with
      {
        z = -5f
      };
      if (this.controllerClicked)
        worldPoint += new Vector3(-0.3f, 0.3f, 0.0f);
      this.sprite.position = worldPoint;
      if ((Object) HeroSelectionManager.Instance.GetOverBox() != (Object) null)
        this.spriteSR.color = new Color(1f, 1f, 1f, 0.8f);
      else if ((double) this.spriteSR.color.a < 1.0)
        this.spriteSR.color = new Color(1f, 1f, 1f, 1f);
      this.nameDisabled();
    }
  }

  private void DraggingStop()
  {
    if (!HeroSelectionManager.Instance.dragging)
      return;
    this.PickStop();
  }

  public void Reset() => this.GoBackToOri();

  public void PickHero(bool _comingFromRandom = false)
  {
    this.selected = true;
    HeroSelectionManager.Instance.charPopup.Close();
    this.moveThis = false;
    this.spriteSR.enabled = true;
    this.nameOver.gameObject.SetActive(false);
    this.rankOver.gameObject.SetActive(false);
    this.startPos = this.sprite.position;
    this.startMousePos = Input.mousePosition;
    this.transform.parent = HeroSelectionManager.Instance.boxCharacters;
    this.sprite.transform.localScale = new Vector3(1f, 1f, 1f);
    this.spriteSR.sortingLayerName = "UI";
    this.spriteSR.sortingOrder = 100;
    this.spriteBackgroundSR.color = new Color(0.3f, 0.3f, 0.3f, 1f);
    this.nameOver.GetComponent<Renderer>().sortingOrder = 101;
    this.rankOver.GetComponent<Renderer>().sortingOrder = 101;
    HeroSelectionManager.Instance.dragging = true;
    GameObject _box = HeroSelectionManager.Instance.GetOverBox();
    if (_comingFromRandom)
      _box = (GameObject) null;
    if ((Object) _box != (Object) null)
    {
      this.oldBox = _box;
      HeroSelectionManager.Instance.FillBox(_box, (HeroSelection) null, false);
    }
    else
    {
      this.oldBox = (GameObject) null;
      if (GameManager.Instance.IsMultiplayer())
        this.ResetSkin();
    }
    HeroSelectionManager.Instance.ShowDrag(false, Vector3.zero);
    this.borderTransform.gameObject.SetActive(false);
  }

  public void PickStop(int _box = -1, bool _removeHeroAbsolute = false)
  {
    GameObject gameObject = (GameObject) null;
    if (!_removeHeroAbsolute)
      gameObject = _box == -1 ? HeroSelectionManager.Instance.GetOverBox() : HeroSelectionManager.Instance.boxGO[_box];
    HeroSelectionManager.Instance.dragging = false;
    if ((Object) HeroSelectionManager.Instance.controllerCurrentHS != (Object) null)
    {
      HeroSelectionManager.Instance.controllerCurrentHS = (HeroSelection) null;
      this.controllerClicked = false;
    }
    if (GameManager.Instance.IsMultiplayer() && HeroSelectionManager.Instance.IsHeroSelected(this.Id))
      return;
    this.spriteSR.sortingLayerName = "Characters";
    this.spriteSR.sortingOrder = 0;
    this.nameOver.GetComponent<Renderer>().sortingOrder = 1;
    this.rankOver.GetComponent<Renderer>().sortingOrder = 1;
    this.spriteSR.color = new Color(1f, 1f, 1f, 1f);
    if ((Object) gameObject == (Object) null || !HeroSelectionManager.Instance.IsYourBox(gameObject.name))
    {
      if (GameManager.Instance.IsWeeklyChallenge())
      {
        this.MoveToBox(this.oldBox, this.oldBox);
      }
      else
      {
        this.GoBackToOri();
        if (!GameManager.Instance.IsMultiplayer())
          return;
        HeroSelectionManager.Instance.ResetHero(this.Id);
      }
    }
    else
    {
      if (HeroSelectionManager.Instance.IsBoxFilled(gameObject))
      {
        if ((Object) this.oldBox != (Object) null)
        {
          HeroSelection boxHero = HeroSelectionManager.Instance.GetBoxHero(gameObject);
          if ((Object) boxHero != (Object) null)
            boxHero.MoveToBox(this.oldBox, gameObject);
        }
        else
          HeroSelectionManager.Instance.GetBoxHero(gameObject).GoBackToOri();
      }
      this.MoveToBox(gameObject, this.oldBox);
    }
  }

  public bool RandomAvailable() => !this.selected && !this.blocked && !this.multiplayerBlocked && !this.dlcBlocked;
}
