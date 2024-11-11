// Decompiled with JetBrains decompiler
// Type: CombatText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CombatText : MonoBehaviour
{
  public GameObject CTI_Prefab;
  private CombatTextInstance CTI;
  private Queue<Action> ctQueue = new Queue<Action>();
  private Queue<Action> ctQueueDamage = new Queue<Action>();
  private int indexQueue;
  private int indexQueueDamage;
  private bool isWaitingQueueDamage;
  private bool isWaitingQueue;
  private CharacterItem characterItem;
  private Dictionary<string, float> TextShowed = new Dictionary<string, float>();
  private float timePassed = 0.7f;

  private void Awake() => this.characterItem = this.transform.parent.transform.parent.transform.parent.GetComponent<CharacterItem>();

  private void Start()
  {
  }

  private void SetTimer() => this.StartCoroutine(this.TimerCo());

  private IEnumerator TimerCo()
  {
    if (this.isWaitingQueue)
    {
      ++this.indexQueue;
      if (this.indexQueue > 7)
      {
        this.isWaitingQueue = false;
        if (this.ctQueue.Count > 0)
          this.ctQueue.Dequeue()();
      }
    }
    if (this.isWaitingQueueDamage)
    {
      ++this.indexQueueDamage;
      if (this.indexQueueDamage > 4)
      {
        this.isWaitingQueueDamage = false;
        if (this.ctQueueDamage.Count > 0)
          this.ctQueueDamage.Dequeue()();
      }
    }
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    if (this.isWaitingQueue || this.isWaitingQueueDamage)
      this.SetTimer();
  }

  private void LaunchInstance(CastResolutionForCombatText _cast)
  {
    if (!((UnityEngine.Object) this != (UnityEngine.Object) null) || !((UnityEngine.Object) this.transform != (UnityEngine.Object) null) || !((UnityEngine.Object) this.characterItem != (UnityEngine.Object) null))
      return;
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.CTI_Prefab, new Vector3(this.transform.position.x, 1.5f, 0.0f), Quaternion.identity, MatchManager.Instance.combattextTransform);
    CombatTextInstance component = gameObject.GetComponent<CombatTextInstance>();
    gameObject.transform.position = new Vector3(this.characterItem.transform.position.x, 1.2f, 0.0f);
    component.ShowDamage(this, this.characterItem, _cast);
  }

  private void LaunchInstanceText(string text, Enums.CombatScrollEffectType type)
  {
    if (!((UnityEngine.Object) this != (UnityEngine.Object) null) || !((UnityEngine.Object) this.transform != (UnityEngine.Object) null) || !((UnityEngine.Object) this.characterItem != (UnityEngine.Object) null))
      return;
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.CTI_Prefab, new Vector3(this.transform.position.x, 1.5f, 0.0f), Quaternion.identity, MatchManager.Instance.combattextTransform);
    CombatTextInstance component = gameObject.GetComponent<CombatTextInstance>();
    gameObject.transform.position = new Vector3(this.characterItem.transform.position.x, 1.2f, 0.0f);
    component.ShowText(this, this.characterItem, text, type);
  }

  public void SetDamageNew(CastResolutionForCombatText _cast)
  {
    if (!this.isWaitingQueueDamage)
    {
      this.isWaitingQueueDamage = true;
      this.LaunchInstance(_cast);
      this.indexQueueDamage = 0;
    }
    else
      this.ctQueueDamage.Enqueue((Action) (() => this.SetDamageNew(_cast)));
    this.SetTimer();
  }

  public void SetText(string text, Enums.CombatScrollEffectType type, bool forceIt = false)
  {
    bool flag = true;
    StringBuilder stringBuilder = new StringBuilder();
    if (!forceIt && type != Enums.CombatScrollEffectType.Damage && type != Enums.CombatScrollEffectType.Heal)
    {
      stringBuilder.Append(text);
      stringBuilder.Append("_");
      stringBuilder.Append(Enum.GetName(typeof (Enums.CombatScrollEffectType), (object) type));
      if (this.TextShowed.TryGetValue(stringBuilder.ToString(), out float _) && (double) Time.time < (double) this.TextShowed[stringBuilder.ToString()] + (double) this.timePassed)
      {
        flag = false;
        this.TextShowed[stringBuilder.ToString()] = Time.time;
      }
    }
    if (!flag)
      return;
    if (!this.isWaitingQueue)
    {
      this.isWaitingQueue = true;
      this.LaunchInstanceText(text, type);
      this.indexQueue = 0;
    }
    else
      this.ctQueue.Enqueue((Action) (() => this.SetText(text, type, true)));
    this.SetTimer();
    if (!(stringBuilder.ToString() != ""))
      return;
    if (this.TextShowed.ContainsKey(stringBuilder.ToString()))
      this.TextShowed[stringBuilder.ToString()] = Time.time;
    else
      this.TextShowed.Add(stringBuilder.ToString(), Time.time);
  }

  public bool IsPlaying() => this.isWaitingQueue || this.ctQueue.Count > 0;
}
