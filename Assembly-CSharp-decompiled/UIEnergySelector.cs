// Decompiled with JetBrains decompiler
// Type: UIEnergySelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIEnergySelector : MonoBehaviour
{
  public TMP_Text textAssignEnergy;
  public TMP_Text textInstructions;
  public Transform mask;
  public Canvas canvas;
  public Transform buttonMore;
  public Transform buttonLess;
  public Transform buttonAccept;
  private int maxEnergy = 10;
  private int maxEnergyToBeAssigned = 10;
  private int currentEnergy;
  private string instructions;
  private string instructionsMax;

  private void Awake() => this.canvas.gameObject.SetActive(false);

  private void Start()
  {
    this.instructions = Texts.Instance.GetText("chooseEnergy") + "<line-height=110%><br></line-height><size=3><color=#bbb>" + Texts.Instance.GetText("chooseEnergyAvailable") + " <color=green>%energy%</color></size></color>";
    this.instructionsMax = "\n<size=3><color=#bbb>" + Texts.Instance.GetText("chooseEnergyMax") + " <color=green>%energyMax%</color>";
  }

  public bool IsActive() => this.canvas.gameObject.activeSelf;

  private void TextEnergy() => this.textAssignEnergy.text = this.currentEnergy.ToString();

  public void TurnOn(int energy, int maxToBeAssigned = 0)
  {
    MatchManager.Instance.ShowMask(true);
    MatchManager.Instance.lockHideMask = true;
    if (GameManager.Instance.IsMultiplayer() && !MatchManager.Instance.IsYourTurn())
    {
      this.buttonMore.gameObject.SetActive(false);
      this.buttonLess.gameObject.SetActive(false);
      this.buttonAccept.gameObject.SetActive(false);
    }
    else
    {
      this.buttonMore.gameObject.SetActive(true);
      this.buttonLess.gameObject.SetActive(true);
      this.buttonAccept.gameObject.SetActive(true);
    }
    this.currentEnergy = 0;
    this.maxEnergy = energy;
    if (maxToBeAssigned == 0)
      maxToBeAssigned = 10;
    this.maxEnergyToBeAssigned = maxToBeAssigned;
    string str = this.instructions.Replace("%energy%", energy.ToString());
    if (maxToBeAssigned > 0)
      str += this.instructionsMax.Replace("%energyMax%", maxToBeAssigned.ToString());
    this.textInstructions.text = str;
    this.TextEnergy();
    this.canvas.gameObject.SetActive(true);
  }

  public void TurnOff()
  {
    MatchManager.Instance.lockHideMask = false;
    MatchManager.Instance.ShowMask(false);
    this.canvas.gameObject.SetActive(false);
  }

  public void AssignEnergyZero()
  {
    if ((Object) CardScreenManager.Instance != (Object) null && CardScreenManager.Instance.IsActive() || TomeManager.Instance.IsActive() || !this.buttonAccept.gameObject.activeSelf)
      return;
    this.buttonMore.gameObject.SetActive(false);
    this.buttonLess.gameObject.SetActive(false);
    this.buttonAccept.gameObject.SetActive(false);
    MatchManager.Instance.AssignEnergyAction(0);
  }

  public void AssignEnergyAction()
  {
    if ((Object) CardScreenManager.Instance != (Object) null && CardScreenManager.Instance.IsActive() || TomeManager.Instance.IsActive() || !this.buttonAccept.gameObject.activeSelf)
      return;
    this.buttonMore.gameObject.SetActive(false);
    this.buttonLess.gameObject.SetActive(false);
    this.buttonAccept.gameObject.SetActive(false);
    MatchManager.Instance.AssignEnergyAction(this.currentEnergy);
  }

  public void AssignEnergyFromOutside(int _energy)
  {
    if (_energy > this.maxEnergy)
      _energy = this.maxEnergy;
    if (_energy > this.maxEnergyToBeAssigned)
      _energy = this.maxEnergyToBeAssigned;
    this.currentEnergy = _energy;
    this.TextEnergy();
  }

  public void AssignEnergyMore()
  {
    if ((Object) CardScreenManager.Instance != (Object) null && CardScreenManager.Instance.IsActive() || TomeManager.Instance.IsActive())
      return;
    ++this.currentEnergy;
    if (this.currentEnergy > this.maxEnergy)
      this.currentEnergy = this.maxEnergy;
    if (this.currentEnergy > this.maxEnergyToBeAssigned)
      this.currentEnergy = this.maxEnergyToBeAssigned;
    this.ShareEnergy();
    this.TextEnergy();
  }

  public void AssignEnergyLess()
  {
    if ((Object) CardScreenManager.Instance != (Object) null && CardScreenManager.Instance.IsActive() || TomeManager.Instance.IsActive())
      return;
    --this.currentEnergy;
    if (this.currentEnergy < 0)
      this.currentEnergy = 0;
    this.ShareEnergy();
    this.TextEnergy();
  }

  private void ShareEnergy()
  {
    if (!GameManager.Instance.IsMultiplayer() || !MatchManager.Instance.IsYourTurn())
      return;
    MatchManager.Instance.AssignEnergyMultiplayer(this.currentEnergy);
  }
}
