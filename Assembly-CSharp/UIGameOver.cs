using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000322 RID: 802
[AddComponentMenu("Scripts/Runners/GameMode/Stage")]
public class UIGameOver : MonoBehaviour
{
	// Token: 0x060017BB RID: 6075 RVA: 0x00087F08 File Offset: 0x00086108
	private void Start()
	{
		float left = ((float)Screen.width - 250f) * 0.5f;
		float top = ((float)Screen.height - 400f) * 0.5f;
		this.m_windowRect = new Rect(left, top, 250f, 400f);
		base.StartCoroutine("waitForDrawDistance", 2f);
	}

	// Token: 0x060017BC RID: 6076 RVA: 0x00087F68 File Offset: 0x00086168
	private void Update()
	{
		float num = this.m_totalDistance - this.m_drawDistance;
		float num2 = (float)((num <= 100f) ? 40 : 400);
		this.m_drawDistance += Time.deltaTime * num2;
		if (this.m_drawDistance >= this.m_totalDistance)
		{
			this.m_drawDistance = this.m_totalDistance;
			this.m_onButton = true;
		}
	}

	// Token: 0x060017BD RID: 6077 RVA: 0x00087FD4 File Offset: 0x000861D4
	public void SetSkin(GUISkin skin)
	{
		this.m_skin = skin;
		this.m_skin.window.fontSize = 20;
		this.m_skin.window.normal.textColor = Color.white;
		this.m_skin.button.fontSize = 20;
		this.m_skin.button.normal.textColor = Color.white;
		this.m_skin.label.fontSize = 20;
		this.m_skin.label.normal.textColor = Color.white;
	}

	// Token: 0x060017BE RID: 6078 RVA: 0x0008806C File Offset: 0x0008626C
	public void OnGUI()
	{
		GUI.backgroundColor = Color.green;
		this.m_windowRect = GUI.Window(1, this.m_windowRect, new GUI.WindowFunction(this.DoMyWindow), "Game Over", this.m_skin.window);
		if (!this.m_onButton)
		{
			return;
		}
	}

	// Token: 0x060017BF RID: 6079 RVA: 0x000880C0 File Offset: 0x000862C0
	private void DoMyWindow(int windowID)
	{
		string text = "Total Distance:" + Mathf.FloorToInt(this.m_drawDistance).ToString();
		GUIContent guicontent = new GUIContent();
		guicontent.text = text;
		Rect position = new Rect(5f, 40f, 180f, 40f);
		GUI.Label(position, guicontent, this.m_skin.label);
		float left = 10f;
		float num = 100f;
		if (GUI.Button(new Rect(left, num, 230f, 40f), "Retry", this.m_skin.button) && !this.m_buttonPressed)
		{
			Application.LoadLevel("s_w01StageTestmap");
			this.m_buttonPressed = true;
		}
		num += 180f;
		if (GUI.Button(new Rect(left, num, 230f, 40f), "Go Title", this.m_skin.button) && !this.m_buttonPressed)
		{
			Application.LoadLevel("s_title1st");
			this.m_buttonPressed = true;
		}
	}

	// Token: 0x060017C0 RID: 6080 RVA: 0x000881D0 File Offset: 0x000863D0
	private IEnumerator waitForDrawDistance(float time)
	{
		yield return new WaitForSeconds(time);
		yield break;
	}

	// Token: 0x0400153D RID: 5437
	private const float windowWidth = 250f;

	// Token: 0x0400153E RID: 5438
	private const float windowHeight = 400f;

	// Token: 0x0400153F RID: 5439
	private Rect m_windowRect;

	// Token: 0x04001540 RID: 5440
	public float m_totalDistance;

	// Token: 0x04001541 RID: 5441
	private float m_drawDistance;

	// Token: 0x04001542 RID: 5442
	private bool m_onButton;

	// Token: 0x04001543 RID: 5443
	private bool m_buttonPressed;

	// Token: 0x04001544 RID: 5444
	private GUISkin m_skin;
}
