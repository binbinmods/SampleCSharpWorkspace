// Decompiled with JetBrains decompiler
// Type: EffectsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
  public GameObject effectsPrefab;
  private List<GameObject> listEffects;
  public GameObject slashPrefab;
  private Dictionary<string, float> EffectPlayedTimePassed = new Dictionary<string, float>();

  public static EffectsManager Instance { get; private set; }

  private void Awake()
  {
    if ((Object) EffectsManager.Instance == (Object) null)
      EffectsManager.Instance = this;
    else if ((Object) EffectsManager.Instance != (Object) this)
      Object.Destroy((Object) this.gameObject);
    this.CreateEffects(60, this.effectsPrefab);
  }

  public void CreateEffects(int size, GameObject prefab)
  {
    this.listEffects = new List<GameObject>();
    for (int index = 0; index < size; ++index)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(prefab, this.transform);
      this.listEffects.Add(gameObject);
      gameObject.SetActive(false);
    }
  }

  public void PlayEffect(
    CardData card,
    bool isCaster,
    bool isHero,
    Transform theTransform,
    float delay = 0.0f)
  {
    if (!GameManager.Instance.ConfigShowEffects)
      return;
    this.StartCoroutine(this.PlayEffectCo(card, isCaster, isHero, theTransform));
  }

  private IEnumerator PlayEffectCo(
    CardData card,
    bool isCaster,
    bool isHero,
    Transform theTransform,
    float delay = 0.0f)
  {
    GameObject effectGO = this.GetEffect();
    Effects effects = effectGO.GetComponent<Effects>();
    if (!effectGO.gameObject.activeSelf)
      effectGO.gameObject.SetActive(true);
    string effect = "";
    bool flip = false;
    if (card.CardClass == Enums.CardClass.Monster)
      flip = true;
    effect = !isCaster ? card.EffectTarget : card.EffectCaster;
    string key = effect + theTransform.name;
    float num1 = 0.0f;
    float num2 = 0.2f;
    if (this.EffectPlayedTimePassed.TryGetValue(key, out num1))
    {
      if ((double) Time.time < (double) this.EffectPlayedTimePassed[key] + (double) num2)
        yield break;
      else
        this.EffectPlayedTimePassed[key] = Time.time;
    }
    else
      this.EffectPlayedTimePassed.Add(key, Time.time);
    bool castInCenter = false;
    if (isCaster && card.EffectCastCenter)
      castInCenter = true;
    if (isHero)
      effects.Play(effect, theTransform, true, flip, castInCenter);
    else
      effects.Play(effect, theTransform, false, flip, castInCenter);
    for (int _exhaust = 0; !effects.HasStopped() && _exhaust < 50; ++_exhaust)
      yield return (object) Globals.Instance.WaitForSeconds((float) ((double) Time.deltaTime * 60.0 * 0.10000000149011612));
    effects.Stop(effect);
    this.DestroyEffect(effectGO);
    yield return (object) null;
  }

  public void PlayEffectAC(
    string effect,
    bool isHero,
    Transform theTransform,
    bool flip,
    float delay = 0.0f)
  {
    if (!GameManager.Instance.ConfigShowEffects)
      return;
    this.StartCoroutine(this.PlayEffectACCo(effect, isHero, theTransform, flip, delay));
  }

  private IEnumerator PlayEffectACCo(
    string effect,
    bool isHero,
    Transform theTransform,
    bool flip,
    float delay)
  {
    yield return (object) Globals.Instance.WaitForSeconds(Time.deltaTime * 60f * delay);
    if (!((Object) theTransform == (Object) null))
    {
      string key = effect + theTransform.name;
      float num1 = 0.0f;
      float num2 = 1f;
      if (this.EffectPlayedTimePassed.TryGetValue(key, out num1))
      {
        if ((double) Time.time < (double) this.EffectPlayedTimePassed[key] + (double) num2)
          yield break;
        else
          this.EffectPlayedTimePassed[key] = Time.time;
      }
      else
        this.EffectPlayedTimePassed.Add(key, Time.time);
      GameObject effectGO = this.GetEffect();
      if ((Object) effectGO != (Object) null)
      {
        Effects effects = effectGO.GetComponent<Effects>();
        effectGO.gameObject.SetActive(true);
        effects.Play(effect, theTransform, isHero, flip, false);
        for (int _exhaust = 0; !effects.HasStopped() && _exhaust < 50; ++_exhaust)
          yield return (object) Globals.Instance.WaitForSeconds((float) ((double) Time.deltaTime * 60.0 * 0.10000000149011612));
        effects.Stop(effect);
        this.DestroyEffect(effectGO);
        effects = (Effects) null;
      }
    }
  }

  public void PlayEffectTrail(
    CardData card,
    bool isHero,
    Transform from,
    Transform to,
    int distance)
  {
    if (!GameManager.Instance.ConfigShowEffects || !GameManager.Instance.IsMultiplayer() && GameManager.Instance.configGameSpeed == Enums.ConfigSpeed.Ultrafast)
      MatchManager.Instance.waitingTrail = false;
    else
      this.StartCoroutine(this.PlayEffectTrailCo(card, isHero, from, to, distance));
  }

  private IEnumerator PlayEffectTrailCo(
    CardData card,
    bool isHero,
    Transform from,
    Transform to,
    int distance)
  {
    if (!GameManager.Instance.IsMultiplayer() && GameManager.Instance.configGameSpeed == Enums.ConfigSpeed.Ultrafast)
    {
      MatchManager.Instance.waitingTrail = false;
    }
    else
    {
      GameObject effectGO = this.GetEffect();
      if (!((Object) effectGO == (Object) null))
      {
        Effects effects = effectGO.GetComponent<Effects>();
        if ((Object) effects == (Object) null)
        {
          Debug.LogError((object) "Effects is null");
        }
        else
        {
          if (!effectGO.gameObject.activeSelf)
            effectGO.gameObject.SetActive(true);
          if (isHero)
            effects.Play(card.EffectTrail, from, true, false, false);
          else
            effects.Play(card.EffectTrail, from, false, true, false);
          Vector3 vector3_1 = new Vector3(to.position.x, 1.4f, to.position.z);
          Transform effectT = effectGO.transform;
          effectT.position = new Vector3(from.position.x, 1.4f, from.position.z);
          if (card.EffectTrailAngle == Enums.EffectTrailAngle.Parabolic || card.EffectTrailAngle == Enums.EffectTrailAngle.Horizontal)
          {
            if ((double) to.position.x > (double) from.position.x)
              effectT.position += new Vector3(0.6f, 0.0f, 0.0f);
            else
              effectT.position -= new Vector3(0.6f, 0.0f, 0.0f);
          }
          if (card.CardClass == Enums.CardClass.Monster)
            effectT.localScale = new Vector3(effectT.localScale.x * -1f, effectT.localScale.y * -1f, effectT.localScale.z);
          bool goingRight;
          float iterationDelay;
          float speed;
          Vector3 targetPos1;
          float stepY;
          if (card.EffectTrailAngle == Enums.EffectTrailAngle.Diagonal || card.EffectTrailAngle == Enums.EffectTrailAngle.Vertical)
          {
            float num1 = (float) Screen.height * 0.005f;
            float num2 = (float) Screen.width * 0.0015f;
            goingRight = true;
            float num3 = (float) (-(double) effectT.localPosition.y + 1.7999999523162842);
            if (card.EffectTrailAngle == Enums.EffectTrailAngle.Vertical)
            {
              iterationDelay = 0.01f;
              speed = 6f * card.EffectTrailSpeed;
              if (GameManager.Instance.IsMultiplayer() || GameManager.Instance.configGameSpeed == Enums.ConfigSpeed.Fast || GameManager.Instance.configGameSpeed == Enums.ConfigSpeed.Ultrafast)
                speed *= 1.5f;
              else
                speed *= 1.2f;
              effectT.position = new Vector3(to.position.x, num1 + 2f, to.position.z);
              targetPos1 = new Vector3(to.position.x, 1.4f, to.position.z);
              bool finished = false;
              effectT.rotation = Quaternion.Euler(0.0f, 0.0f, 90f);
              while (!finished)
              {
                Vector3 vector3_2 = Vector3.MoveTowards(effectT.position, targetPos1, speed * Time.deltaTime);
                Debug.Log((object) vector3_2.y);
                effectT.position = vector3_2;
                if ((double) Mathf.Abs(vector3_2.y - targetPos1.y) < 0.10000000149011612)
                  finished = true;
                yield return (object) Globals.Instance.WaitForSeconds(Time.deltaTime * 60f * iterationDelay);
              }
              targetPos1 = new Vector3();
            }
            else
            {
              float num4 = num3 - 0.2f;
              if ((double) to.position.x > (double) from.position.x)
              {
                effectT.localRotation = Quaternion.Euler(0.0f, 0.0f, -45f);
                effectT.position = to.position + new Vector3(-num2 - num4, num1 + 2f, 0.0f);
              }
              else
              {
                goingRight = false;
                effectT.localRotation = Quaternion.Euler(0.0f, 0.0f, 45f);
                effectT.position = to.position + new Vector3(num2 + num4, num1 + 2f, 0.0f);
              }
              speed = 40f;
              iterationDelay = num2 / speed;
              stepY = num1 / speed;
              effectT.position += new Vector3(0.0f, stepY, 0.0f);
              for (int i = 0; (double) i < (double) speed; ++i)
              {
                if (card.EffectTrailAngle == Enums.EffectTrailAngle.Vertical)
                  effectT.position += new Vector3(0.0f, -stepY, 0.0f);
                else if (goingRight)
                  effectT.position += new Vector3(iterationDelay, -stepY, 0.0f);
                else
                  effectT.position += new Vector3(-iterationDelay, -stepY, 0.0f);
                yield return (object) Globals.Instance.WaitForSeconds((float) ((double) Time.deltaTime * 60.0 * 0.0099999997764825821));
              }
            }
          }
          else
          {
            float num5 = Globals.Instance.sizeW * 0.8f;
            float num6 = Globals.Instance.sizeW * 0.2f;
            stepY = 0.01f;
            iterationDelay = 14f * card.EffectTrailSpeed;
            if (GameManager.Instance.IsMultiplayer() || GameManager.Instance.configGameSpeed == Enums.ConfigSpeed.Fast || GameManager.Instance.configGameSpeed == Enums.ConfigSpeed.Ultrafast)
              iterationDelay *= 1.85f;
            else
              iterationDelay *= 1.7f;
            float x1 = 0.4f;
            targetPos1 = new Vector3(from.position.x, 1.4f, from.position.z);
            Vector3 targetPos2 = new Vector3(to.position.x - to.localPosition.x, 1.4f, to.position.z);
            if ((double) targetPos1.x < (double) targetPos2.x)
            {
              targetPos1 += new Vector3(x1, 0.0f, 0.0f);
              targetPos2 -= new Vector3(x1, 0.0f, 0.0f);
            }
            else
            {
              targetPos1 -= new Vector3(x1, 0.0f, 0.0f);
              targetPos2 += new Vector3(x1, 0.0f, 0.0f);
            }
            float num7 = Mathf.Abs(targetPos1.x - targetPos2.x);
            effectT.position = targetPos1;
            if (card.EffectTrailAngle == Enums.EffectTrailAngle.Parabolic)
            {
              goingRight = false;
              float min = 0.1f;
              float max = 4f;
              speed = Mathf.Clamp(min + (float) (((double) num7 - (double) num6) / ((double) num5 - (double) num6) * ((double) max - (double) min)), min, max);
              while (!goingRight)
              {
                float x2 = targetPos1.x;
                float x3 = targetPos2.x;
                float num8 = x3 - x2;
                float x4 = Mathf.MoveTowards(effectT.position.x, x3, iterationDelay * Time.deltaTime);
                float num9 = Mathf.Lerp(targetPos1.y, targetPos2.y, (x4 - x2) / num8);
                float num10 = (float) ((double) speed * ((double) x4 - (double) x2) * ((double) x4 - (double) x3) / (-0.25 * (double) num8 * (double) num8));
                Vector3 vector3_3 = new Vector3(x4, num9 + num10, effectT.position.z);
                effectT.rotation = this.LookAt2D((Vector2) (vector3_3 - effectT.position));
                effectT.position = vector3_3;
                if ((double) Mathf.Abs(vector3_3.x - targetPos2.x) < 0.10000000149011612)
                  goingRight = true;
                yield return (object) Globals.Instance.WaitForSeconds(Time.deltaTime * 60f * stepY);
              }
            }
            else
            {
              goingRight = false;
              while (!goingRight)
              {
                Vector3 vector3_4 = Vector3.MoveTowards(effectT.position, targetPos2, iterationDelay * Time.deltaTime);
                effectT.rotation = this.LookAt2D((Vector2) (vector3_4 - effectT.position));
                effectT.position = vector3_4;
                if ((double) Mathf.Abs(vector3_4.x - targetPos2.x) < 0.10000000149011612)
                  goingRight = true;
                yield return (object) Globals.Instance.WaitForSeconds(Time.deltaTime * 60f * stepY);
              }
            }
            targetPos1 = new Vector3();
            targetPos2 = new Vector3();
          }
          effectT.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
          MatchManager.Instance.waitingTrail = false;
          effects.Stop(card.EffectTrail);
          this.DestroyEffect(effectGO);
        }
      }
    }
  }

  private Quaternion LookAt2D(Vector2 forward) => Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(forward.y, forward.x) * 57.29578f);

  public GameObject GetEffect()
  {
    if (this.listEffects.Count <= 0)
      return (GameObject) null;
    GameObject listEffect = this.listEffects[0];
    if ((double) listEffect.transform.localScale.x < 0.0)
      listEffect.transform.localScale = new Vector3(Mathf.Abs(listEffect.transform.localScale.x), Mathf.Abs(listEffect.transform.localScale.y), listEffect.transform.localScale.z);
    this.listEffects.RemoveAt(0);
    return listEffect;
  }

  public void DestroyEffect(GameObject obj)
  {
    if (obj.gameObject.activeSelf)
      obj.gameObject.SetActive(false);
    this.listEffects.Add(obj);
  }

  public void ClearEffects()
  {
    this.listEffects.Clear();
    this.listEffects = (List<GameObject>) null;
  }
}
