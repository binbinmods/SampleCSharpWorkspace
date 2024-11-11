// Decompiled with JetBrains decompiler
// Type: TMPro.Examples.TextMeshProFloatingText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

namespace TMPro.Examples
{
  public class TextMeshProFloatingText : MonoBehaviour
  {
    public Font TheFont;
    private GameObject m_floatingText;
    private TextMeshPro m_textMeshPro;
    private TextMesh m_textMesh;
    private Transform m_transform;
    private Transform m_floatingText_Transform;
    private Transform m_cameraTransform;
    private Vector3 lastPOS = Vector3.zero;
    private Quaternion lastRotation = Quaternion.identity;
    public int SpawnType;

    private void Awake()
    {
      this.m_transform = this.transform;
      this.m_floatingText = new GameObject(this.name + " floating text");
      this.m_cameraTransform = Camera.main.transform;
    }

    private void Start()
    {
      if (this.SpawnType == 0)
      {
        this.m_textMeshPro = this.m_floatingText.AddComponent<TextMeshPro>();
        this.m_textMeshPro.rectTransform.sizeDelta = new Vector2(3f, 3f);
        this.m_floatingText_Transform = this.m_floatingText.transform;
        this.m_floatingText_Transform.position = this.m_transform.position + new Vector3(0.0f, 15f, 0.0f);
        this.m_textMeshPro.alignment = TextAlignmentOptions.Center;
        this.m_textMeshPro.color = (Color) new Color32((byte) Random.Range(0, (int) byte.MaxValue), (byte) Random.Range(0, (int) byte.MaxValue), (byte) Random.Range(0, (int) byte.MaxValue), byte.MaxValue);
        this.m_textMeshPro.fontSize = 24f;
        this.m_textMeshPro.enableKerning = false;
        this.m_textMeshPro.text = string.Empty;
        this.StartCoroutine(this.DisplayTextMeshProFloatingText());
      }
      else if (this.SpawnType == 1)
      {
        this.m_floatingText_Transform = this.m_floatingText.transform;
        this.m_floatingText_Transform.position = this.m_transform.position + new Vector3(0.0f, 15f, 0.0f);
        this.m_textMesh = this.m_floatingText.AddComponent<TextMesh>();
        this.m_textMesh.font = UnityEngine.Resources.Load<Font>("Fonts/ARIAL");
        this.m_textMesh.GetComponent<Renderer>().sharedMaterial = this.m_textMesh.font.material;
        this.m_textMesh.color = (Color) new Color32((byte) Random.Range(0, (int) byte.MaxValue), (byte) Random.Range(0, (int) byte.MaxValue), (byte) Random.Range(0, (int) byte.MaxValue), byte.MaxValue);
        this.m_textMesh.anchor = TextAnchor.LowerCenter;
        this.m_textMesh.fontSize = 24;
        this.StartCoroutine(this.DisplayTextMeshFloatingText());
      }
      else
      {
        int spawnType = this.SpawnType;
      }
    }

    public IEnumerator DisplayTextMeshProFloatingText()
    {
      TextMeshProFloatingText meshProFloatingText = this;
      float CountDuration = 2f;
      float starting_Count = Random.Range(5f, 20f);
      float current_Count = starting_Count;
      Vector3 start_pos = meshProFloatingText.m_floatingText_Transform.position;
      Color32 start_color = (Color32) meshProFloatingText.m_textMeshPro.color;
      float alpha = (float) byte.MaxValue;
      float fadeDuration = 3f / starting_Count * CountDuration;
      while ((double) current_Count > 0.0)
      {
        current_Count -= Time.deltaTime / CountDuration * starting_Count;
        if ((double) current_Count <= 3.0)
          alpha = Mathf.Clamp(alpha - (float) ((double) Time.deltaTime / (double) fadeDuration * (double) byte.MaxValue), 0.0f, (float) byte.MaxValue);
        int num = (int) current_Count;
        meshProFloatingText.m_textMeshPro.text = num.ToString();
        meshProFloatingText.m_textMeshPro.color = (Color) new Color32(start_color.r, start_color.g, start_color.b, (byte) alpha);
        meshProFloatingText.m_floatingText_Transform.position += new Vector3(0.0f, starting_Count * Time.deltaTime, 0.0f);
        if (!meshProFloatingText.lastPOS.Compare(meshProFloatingText.m_cameraTransform.position, 1000) || !meshProFloatingText.lastRotation.Compare(meshProFloatingText.m_cameraTransform.rotation, 1000))
        {
          meshProFloatingText.lastPOS = meshProFloatingText.m_cameraTransform.position;
          meshProFloatingText.lastRotation = meshProFloatingText.m_cameraTransform.rotation;
          meshProFloatingText.m_floatingText_Transform.rotation = meshProFloatingText.lastRotation;
          Vector3 vector3 = meshProFloatingText.m_transform.position - meshProFloatingText.lastPOS;
          meshProFloatingText.m_transform.forward = new Vector3(vector3.x, 0.0f, vector3.z);
        }
        yield return (object) new WaitForEndOfFrame();
      }
      yield return (object) new WaitForSeconds(Random.Range(0.1f, 1f));
      meshProFloatingText.m_floatingText_Transform.position = start_pos;
      meshProFloatingText.StartCoroutine(meshProFloatingText.DisplayTextMeshProFloatingText());
    }

    public IEnumerator DisplayTextMeshFloatingText()
    {
      TextMeshProFloatingText meshProFloatingText = this;
      float CountDuration = 2f;
      float starting_Count = Random.Range(5f, 20f);
      float current_Count = starting_Count;
      Vector3 start_pos = meshProFloatingText.m_floatingText_Transform.position;
      Color32 start_color = (Color32) meshProFloatingText.m_textMesh.color;
      float alpha = (float) byte.MaxValue;
      float fadeDuration = 3f / starting_Count * CountDuration;
      while ((double) current_Count > 0.0)
      {
        current_Count -= Time.deltaTime / CountDuration * starting_Count;
        if ((double) current_Count <= 3.0)
          alpha = Mathf.Clamp(alpha - (float) ((double) Time.deltaTime / (double) fadeDuration * (double) byte.MaxValue), 0.0f, (float) byte.MaxValue);
        int num = (int) current_Count;
        meshProFloatingText.m_textMesh.text = num.ToString();
        meshProFloatingText.m_textMesh.color = (Color) new Color32(start_color.r, start_color.g, start_color.b, (byte) alpha);
        meshProFloatingText.m_floatingText_Transform.position += new Vector3(0.0f, starting_Count * Time.deltaTime, 0.0f);
        if (!meshProFloatingText.lastPOS.Compare(meshProFloatingText.m_cameraTransform.position, 1000) || !meshProFloatingText.lastRotation.Compare(meshProFloatingText.m_cameraTransform.rotation, 1000))
        {
          meshProFloatingText.lastPOS = meshProFloatingText.m_cameraTransform.position;
          meshProFloatingText.lastRotation = meshProFloatingText.m_cameraTransform.rotation;
          meshProFloatingText.m_floatingText_Transform.rotation = meshProFloatingText.lastRotation;
          Vector3 vector3 = meshProFloatingText.m_transform.position - meshProFloatingText.lastPOS;
          meshProFloatingText.m_transform.forward = new Vector3(vector3.x, 0.0f, vector3.z);
        }
        yield return (object) new WaitForEndOfFrame();
      }
      yield return (object) new WaitForSeconds(Random.Range(0.1f, 1f));
      meshProFloatingText.m_floatingText_Transform.position = start_pos;
      meshProFloatingText.StartCoroutine(meshProFloatingText.DisplayTextMeshFloatingText());
    }
  }
}
