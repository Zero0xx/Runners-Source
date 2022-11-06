using System;
using UnityEngine;

// Token: 0x0200008B RID: 139
[AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton : MonoBehaviour
{
	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06000394 RID: 916 RVA: 0x00011658 File Offset: 0x0000F858
	// (set) Token: 0x06000395 RID: 917 RVA: 0x00011680 File Offset: 0x0000F880
	public bool isEnabled
	{
		get
		{
			Collider collider = base.collider;
			return collider && collider.enabled;
		}
		set
		{
			Collider collider = base.collider;
			if (!collider)
			{
				return;
			}
			if (collider.enabled != value)
			{
				collider.enabled = value;
				this.UpdateImage();
			}
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x000116BC File Offset: 0x0000F8BC
	private void OnEnable()
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<UISprite>();
		}
		this.UpdateImage();
	}

	// Token: 0x06000397 RID: 919 RVA: 0x000116E4 File Offset: 0x0000F8E4
	private void UpdateImage()
	{
		if (this.target != null)
		{
			if (this.isEnabled)
			{
				this.target.spriteName = ((!UICamera.IsHighlighted(base.gameObject)) ? this.normalSprite : this.hoverSprite);
			}
			else
			{
				this.target.spriteName = this.disabledSprite;
			}
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x06000398 RID: 920 RVA: 0x0001175C File Offset: 0x0000F95C
	private void OnHover(bool isOver)
	{
		if (this.isEnabled && this.target != null)
		{
			this.target.spriteName = ((!isOver) ? this.normalSprite : this.hoverSprite);
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x06000399 RID: 921 RVA: 0x000117B4 File Offset: 0x0000F9B4
	private void OnPress(bool pressed)
	{
		if (pressed)
		{
			this.target.spriteName = this.pressedSprite;
			this.target.MakePixelPerfect();
		}
		else
		{
			this.UpdateImage();
		}
	}

	// Token: 0x04000261 RID: 609
	public UISprite target;

	// Token: 0x04000262 RID: 610
	public string normalSprite;

	// Token: 0x04000263 RID: 611
	public string hoverSprite;

	// Token: 0x04000264 RID: 612
	public string pressedSprite;

	// Token: 0x04000265 RID: 613
	public string disabledSprite;
}
