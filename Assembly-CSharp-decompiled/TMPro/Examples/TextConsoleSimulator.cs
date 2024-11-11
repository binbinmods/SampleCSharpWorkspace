// Decompiled with JetBrains decompiler
// Type: TMPro.Examples.TextConsoleSimulator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

namespace TMPro.Examples
{
  public class TextConsoleSimulator : MonoBehaviour
  {
    private TMP_Text m_TextComponent;
    private bool hasTextChanged;

    private void Awake() => this.m_TextComponent = this.gameObject.GetComponent<TMP_Text>();

    private void Start() => this.StartCoroutine(this.RevealCharacters(this.m_TextComponent));

    private void OnEnable() => TMPro_EventManager.TEXT_CHANGED_EVENT.Add(new Action<UnityEngine.Object>(this.ON_TEXT_CHANGED));

    private void OnDisable() => TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(new Action<UnityEngine.Object>(this.ON_TEXT_CHANGED));

    private void ON_TEXT_CHANGED(UnityEngine.Object obj) => this.hasTextChanged = true;

    private IEnumerator RevealCharacters(TMP_Text textComponent)
    {
      textComponent.ForceMeshUpdate();
      TMP_TextInfo textInfo = textComponent.textInfo;
      int totalVisibleCharacters = textInfo.characterCount;
      int visibleCount = 0;
      while (true)
      {
        if (this.hasTextChanged)
        {
          totalVisibleCharacters = textInfo.characterCount;
          this.hasTextChanged = false;
        }
        if (visibleCount > totalVisibleCharacters)
        {
          yield return (object) new WaitForSeconds(1f);
          visibleCount = 0;
        }
        textComponent.maxVisibleCharacters = visibleCount;
        ++visibleCount;
        yield return (object) null;
      }
    }

    private IEnumerator RevealWords(TMP_Text textComponent)
    {
      textComponent.ForceMeshUpdate();
      int totalWordCount = textComponent.textInfo.wordCount;
      int totalVisibleCharacters = textComponent.textInfo.characterCount;
      int counter = 0;
      int visibleCount = 0;
      while (true)
      {
        int num = counter % (totalWordCount + 1);
        if (num == 0)
          visibleCount = 0;
        else if (num < totalWordCount)
          visibleCount = textComponent.textInfo.wordInfo[num - 1].lastCharacterIndex + 1;
        else if (num == totalWordCount)
          visibleCount = totalVisibleCharacters;
        textComponent.maxVisibleCharacters = visibleCount;
        if (visibleCount >= totalVisibleCharacters)
          yield return (object) new WaitForSeconds(1f);
        ++counter;
        yield return (object) new WaitForSeconds(0.1f);
      }
    }
  }
}
