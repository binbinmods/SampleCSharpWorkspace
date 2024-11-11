// Decompiled with JetBrains decompiler
// Type: Buff
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class Buff : MonoBehaviour
{
  public string buffId = "";
  public AuraCurseData acData;
  public SpriteRenderer spriteSR;
  public SpriteRenderer spriteSRShadow;
  public TMP_Text chargesTM;
  private MeshRenderer chargesTMMesh;
  public Transform spriteAnim;
  private Animator spriteAnimator;
  private TMP_Text chargesAnimTM;
  private int charges;
  public string buffStatus = "";
  public SpriteRenderer bgIconSprite;
  private bool auraImmunity;
  private Color colorAura = new Color(0.0f, 0.26f, 1f, 1f);
  private Color colorCurse = new Color(0.81f, 0.04f, 0.72f, 1f);
  private Color colorOver = new Color(0.6f, 0.6f, 0.6f, 1f);
  private Coroutine AmplifyCo;
  private string charId = "";

  private void Awake()
  {
    this.chargesAnimTM = this.spriteAnim.GetChild(0).transform.GetComponent<TMP_Text>();
    this.chargesTMMesh = this.chargesTM.GetComponent<MeshRenderer>();
    this.spriteAnimator = this.spriteAnim.GetComponent<Animator>();
    this.spriteAnimator.enabled = false;
  }

  public void DisplayBecauseCard(bool _status)
  {
    if (!this.gameObject.activeSelf)
      return;
    if (_status)
    {
      this.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
      this.bgIconSprite.color = new Color(1f, 0.69f, 0.0f, 1f);
    }
    else
      this.transform.localScale = new Vector3(1f, 1f, 1f);
  }

  public void RestoreBecauseCard()
  {
    this.transform.localScale = new Vector3(1f, 1f, 1f);
    this.RestoreColor();
  }

  public void CleanBuff()
  {
    if (!((Object) this.gameObject != (Object) null))
      return;
    this.buffStatus = "";
    this.gameObject.name = "";
    if (!this.gameObject.activeSelf)
      return;
    this.gameObject.SetActive(false);
  }

  private void SetBuffStatus(string _buffId, int _buffCharges)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(_buffId);
    stringBuilder.Append("_");
    stringBuilder.Append(_buffCharges);
    this.buffStatus = stringBuilder.ToString();
  }

  public void SetBuffInStats(bool _auraImmunity = false)
  {
    if (!this.gameObject.activeSelf)
      this.gameObject.SetActive(true);
    this.auraImmunity = _auraImmunity;
    this.spriteSR.sortingOrder = 1001;
    this.spriteSR.sortingLayerName = "UI";
    if (this.spriteSRShadow.transform.gameObject.activeSelf)
      this.spriteSRShadow.gameObject.SetActive(false);
    if (this.auraImmunity)
    {
      if (this.chargesTM.gameObject.activeSelf)
        this.chargesTM.gameObject.SetActive(false);
    }
    else
    {
      if (!this.chargesTM.gameObject.activeSelf)
        this.chargesTM.gameObject.SetActive(true);
      this.chargesTMMesh.sortingOrder = 1005;
      this.chargesTMMesh.sortingLayerName = "UI";
    }
    this.transform.localScale = new Vector3(1.7f, 1.7f, 1f);
    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -5f);
  }

  public void SetBuffInStatsCharges()
  {
    if (!this.gameObject.activeSelf)
      this.gameObject.SetActive(true);
    this.spriteSR.sortingOrder = 1601;
    this.spriteSR.sortingLayerName = "UI";
    if (this.spriteSRShadow.gameObject.activeSelf)
      this.spriteSRShadow.gameObject.SetActive(false);
    if (!this.chargesTM.gameObject.activeSelf)
      this.chargesTM.gameObject.SetActive(true);
    this.chargesTMMesh.sortingOrder = 1605;
    this.chargesTMMesh.sortingLayerName = "UI";
    this.chargesTM.transform.localPosition = new Vector3(0.0f, this.chargesTM.transform.localPosition.y, this.chargesTM.transform.localPosition.z);
    this.transform.localScale = new Vector3(1.7f, 1.7f, 1f);
    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -5f);
  }

  public void SetBuff(AuraCurseData _acData, int _charges, string _chargesStr = "", string _charId = "")
  {
    this.charId = _charId;
    if ((Object) this.spriteSR == (Object) null)
    {
      if (!this.gameObject.activeSelf)
        return;
      this.gameObject.SetActive(false);
    }
    else if ((Object) _acData != (Object) null)
    {
      this.acData = _acData;
      this.spriteSR.sprite = this.spriteSRShadow.sprite = _acData.Sprite;
      this.buffId = _acData.Id.ToLower();
      if (_charges == 0 && _chargesStr == "")
      {
        this.buffId = "";
        if (!this.gameObject.activeSelf)
          return;
        this.gameObject.SetActive(false);
      }
      else
      {
        if (!this.gameObject.activeSelf)
          this.gameObject.SetActive(true);
        this.RestoreColor();
        this.chargesTM.text = !(_chargesStr == "") ? _chargesStr : _charges.ToString();
        this.charges = _charges;
        if (!this.spriteSR.transform.gameObject.activeSelf)
          this.spriteSR.transform.gameObject.SetActive(true);
        if (this.chargesTM.transform.gameObject.activeSelf)
          return;
        this.chargesTM.transform.gameObject.SetActive(true);
      }
    }
    else
    {
      this.buffId = "";
      if (!this.gameObject.activeSelf)
        return;
      this.gameObject.SetActive(false);
    }
  }

  public void SetTauntSize()
  {
    if (!((Object) this.spriteSR != (Object) null))
      return;
    this.spriteAnim.localScale = new Vector3(1.4f, 1.4f, 1f);
    this.chargesAnimTM.GetComponent<MeshRenderer>().sortingOrder = 32021;
    if (this.spriteSR.transform.gameObject.activeSelf)
      this.spriteSR.transform.gameObject.SetActive(false);
    if (this.chargesTM.transform.gameObject.activeSelf)
      this.chargesTM.transform.gameObject.SetActive(false);
    this.spriteSR.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
    this.spriteSR.sortingOrder = 32010;
    this.chargesTM.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
    this.chargesTM.transform.localPosition = new Vector3(-0.1f, -0.13f, 0.0f);
    this.chargesTMMesh.sortingOrder = 32011;
  }

  public void Amplify(int charges)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("+");
    stringBuilder.Append(charges);
    this.chargesAnimTM.text = stringBuilder.ToString();
    if (!this.spriteAnim.transform.gameObject.activeSelf)
      this.spriteAnim.gameObject.SetActive(true);
    this.spriteAnimator.enabled = true;
    if (this.AmplifyCo != null)
      this.StopCoroutine(this.AmplifyCo);
    this.AmplifyCo = this.StartCoroutine(this.AmplifyHiddenCo());
  }

  private IEnumerator AmplifyHiddenCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(1f);
    if (this.spriteAnim.transform.gameObject.activeSelf)
      this.spriteAnim.gameObject.SetActive(false);
    this.spriteAnimator.enabled = false;
  }

  private void OnMouseEnter()
  {
    if (!this.auraImmunity)
      PopupManager.Instance.SetAuraCurse(this.transform, this.acData, this.chargesTM.text, true, this.charId);
    else
      PopupManager.Instance.ShowKeyNote(this.transform, this.acData.Id, "center", true);
    this.bgIconSprite.color = this.colorOver;
  }

  private void OnMouseExit()
  {
    PopupManager.Instance.ClosePopup();
    this.RestoreColor();
  }

  private void RestoreColor()
  {
    if ((Object) this.acData == (Object) null)
      return;
    this.bgIconSprite.color = !this.acData.IsAura ? this.colorCurse : this.colorAura;
    this.bgIconSprite.color = this.bgIconSprite.color with
    {
      a = !GameManager.Instance.ConfigACBackgrounds ? 0.0f : 1f
    };
  }
}
