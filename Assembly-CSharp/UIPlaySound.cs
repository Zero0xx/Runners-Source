using System;
using UnityEngine;

// Token: 0x0200008F RID: 143
[AddComponentMenu("NGUI/Interaction/Play Sound")]
public class UIPlaySound : MonoBehaviour
{
	// Token: 0x060003AA RID: 938 RVA: 0x00011DC0 File Offset: 0x0000FFC0
	private void OnHover(bool isOver)
	{
		if (base.enabled && ((isOver && this.trigger == UIPlaySound.Trigger.OnMouseOver) || (!isOver && this.trigger == UIPlaySound.Trigger.OnMouseOut)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00011E14 File Offset: 0x00010014
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIPlaySound.Trigger.OnPress) || (!isPressed && this.trigger == UIPlaySound.Trigger.OnRelease)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00011E68 File Offset: 0x00010068
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIPlaySound.Trigger.OnClick)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x0400027C RID: 636
	public AudioClip audioClip;

	// Token: 0x0400027D RID: 637
	public UIPlaySound.Trigger trigger;

	// Token: 0x0400027E RID: 638
	[Range(0f, 1f)]
	public float volume = 1f;

	// Token: 0x0400027F RID: 639
	[Range(0f, 2f)]
	public float pitch = 1f;

	// Token: 0x02000090 RID: 144
	public enum Trigger
	{
		// Token: 0x04000281 RID: 641
		OnClick,
		// Token: 0x04000282 RID: 642
		OnMouseOver,
		// Token: 0x04000283 RID: 643
		OnMouseOut,
		// Token: 0x04000284 RID: 644
		OnPress,
		// Token: 0x04000285 RID: 645
		OnRelease
	}
}
