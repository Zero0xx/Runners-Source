using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

// Token: 0x020003B4 RID: 948
public abstract class UIRectItemSlot : MonoBehaviour
{
	// Token: 0x170003FB RID: 1019
	// (get) Token: 0x06001B9D RID: 7069
	protected abstract UIInvGameItem observedItem { get; }

	// Token: 0x06001B9E RID: 7070
	protected abstract UIInvGameItem Replace(UIInvGameItem item);

	// Token: 0x06001B9F RID: 7071 RVA: 0x000A31C8 File Offset: 0x000A13C8
	private void OnTooltip(bool show)
	{
		UIInvGameItem uiinvGameItem = (!show) ? null : this.mItem;
		if (uiinvGameItem != null)
		{
			UIInvBaseItem baseItem = uiinvGameItem.baseItem;
			if (baseItem != null)
			{
				string text = string.Concat(new string[]
				{
					"[",
					NGUITools.EncodeColor(uiinvGameItem.color),
					"]",
					uiinvGameItem.name,
					"[-]\n"
				});
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"[AFAFAF]Level ",
					uiinvGameItem.itemLevel,
					" ",
					baseItem.slot
				});
				List<UIInvStat> list = uiinvGameItem.CalculateStats();
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					UIInvStat uiinvStat = list[i];
					if (uiinvStat.amount != 0)
					{
						if (uiinvStat.amount < 0)
						{
							text = text + "\n[FF0000]" + uiinvStat.amount;
						}
						else
						{
							text = text + "\n[00FF00]+" + uiinvStat.amount;
						}
						if (uiinvStat.modifier == UIInvStat.Modifier.Percent)
						{
							text += "%";
						}
						text = text + " " + uiinvStat.id;
						text += "[-]";
					}
					i++;
				}
				if (!string.IsNullOrEmpty(baseItem.description))
				{
					text = text + "\n[FF9900]" + baseItem.description;
				}
				UITooltip.ShowText(text);
				return;
			}
		}
		UITooltip.ShowText(null);
	}

	// Token: 0x06001BA0 RID: 7072 RVA: 0x000A3368 File Offset: 0x000A1568
	private void OnClick()
	{
		if (UIRectItemSlot.mDraggedItem != null)
		{
			this.OnDrop(null);
		}
		else if (this.mItem != null)
		{
			UIRectItemSlot.mDraggedItem = this.Replace(null);
			if (UIRectItemSlot.mDraggedItem != null)
			{
				NGUITools.PlaySound(this.grabSound);
			}
			this.UpdateCursor();
		}
	}

	// Token: 0x06001BA1 RID: 7073 RVA: 0x000A33C0 File Offset: 0x000A15C0
	private void OnDrag(Vector2 delta)
	{
		if (UIRectItemSlot.mDraggedItem == null && this.mItem != null)
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			UIRectItemSlot.mDraggedItem = this.Replace(null);
			NGUITools.PlaySound(this.grabSound);
			this.UpdateCursor();
		}
	}

	// Token: 0x06001BA2 RID: 7074 RVA: 0x000A340C File Offset: 0x000A160C
	private void OnDrop(GameObject go)
	{
		UIInvGameItem uiinvGameItem = this.Replace(UIRectItemSlot.mDraggedItem);
		if (UIRectItemSlot.mDraggedItem == uiinvGameItem)
		{
			NGUITools.PlaySound(this.errorSound);
		}
		else if (uiinvGameItem != null)
		{
			NGUITools.PlaySound(this.grabSound);
		}
		else
		{
			NGUITools.PlaySound(this.placeSound);
		}
		UIRectItemSlot.mDraggedItem = uiinvGameItem;
		this.UpdateCursor();
	}

	// Token: 0x06001BA3 RID: 7075 RVA: 0x000A3470 File Offset: 0x000A1670
	private void UpdateCursor()
	{
		if (UIRectItemSlot.mDraggedItem != null && UIRectItemSlot.mDraggedItem.baseItem != null)
		{
			UI.UICursor.Set(UIRectItemSlot.mDraggedItem.baseItem.iconAtlas, UIRectItemSlot.mDraggedItem.baseItem.iconName);
		}
		else
		{
			UI.UICursor.Clear();
		}
	}

	// Token: 0x06001BA4 RID: 7076 RVA: 0x000A34C4 File Offset: 0x000A16C4
	private void Update()
	{
		UIInvGameItem observedItem = this.observedItem;
		if (this.mItem != observedItem)
		{
			this.mItem = observedItem;
			UIInvBaseItem uiinvBaseItem = (observedItem == null) ? null : observedItem.baseItem;
			if (this.label != null)
			{
				string text = (observedItem == null) ? null : observedItem.name;
				if (string.IsNullOrEmpty(this.mText))
				{
					this.mText = this.label.text;
				}
				this.label.text = ((text == null) ? this.mText : text);
			}
			if (this.icon != null)
			{
				if (uiinvBaseItem == null || uiinvBaseItem.iconAtlas == null)
				{
					this.icon.enabled = false;
				}
				else
				{
					this.icon.atlas = uiinvBaseItem.iconAtlas;
					this.icon.spriteName = uiinvBaseItem.iconName;
					this.icon.enabled = true;
					this.icon.MakePixelPerfect();
				}
			}
			if (this.background != null)
			{
				this.background.color = ((observedItem == null) ? Color.white : observedItem.color);
			}
		}
	}

	// Token: 0x04001940 RID: 6464
	public UISprite icon;

	// Token: 0x04001941 RID: 6465
	public UIWidget background;

	// Token: 0x04001942 RID: 6466
	public UILabel label;

	// Token: 0x04001943 RID: 6467
	public AudioClip grabSound;

	// Token: 0x04001944 RID: 6468
	public AudioClip placeSound;

	// Token: 0x04001945 RID: 6469
	public AudioClip errorSound;

	// Token: 0x04001946 RID: 6470
	private UIInvGameItem mItem;

	// Token: 0x04001947 RID: 6471
	private string mText = string.Empty;

	// Token: 0x04001948 RID: 6472
	private static UIInvGameItem mDraggedItem;
}
