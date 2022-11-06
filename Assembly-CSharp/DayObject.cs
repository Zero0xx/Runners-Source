using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000402 RID: 1026
public class DayObject
{
	// Token: 0x06001E8B RID: 7819 RVA: 0x000B51F4 File Offset: 0x000B33F4
	public DayObject(GameObject obj, Color color, int day)
	{
		this.m_clearGameObject = obj;
		this.m_effect = GameObjectUtil.FindChildGameObject(this.m_clearGameObject, "eff_4");
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_clearGameObject, "img_day_num");
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_clearGameObject, "img_frame_color");
		this.m_imgItem = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_clearGameObject, "img_daily_item");
		this.m_imgChara = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_clearGameObject, "img_chara");
		this.m_imgChao = GameObjectUtil.FindChildGameObjectComponent<UITexture>(this.m_clearGameObject, "img_chao");
		this.m_lblCount = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_clearGameObject, "Lbl_count");
		this.m_imgHidden = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_clearGameObject, "img_hidden");
		this.m_imgCheck = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_clearGameObject, "img_check");
		if (this.m_effect != null)
		{
			this.m_effect.SetActive(false);
		}
		if (uisprite2 != null)
		{
			uisprite2.color = color;
		}
		if (uisprite != null)
		{
			uisprite.spriteName = "ui_daily_num_" + day;
		}
		if (this.m_lblCount != null)
		{
			this.m_lblCount.text = "0";
		}
		if (this.m_imgItem != null)
		{
			this.m_imgItem.spriteName = string.Empty;
		}
		if (this.m_imgChara != null)
		{
			this.m_imgChara.spriteName = string.Empty;
			this.m_imgChara.alpha = 0f;
		}
		if (this.m_imgChao != null)
		{
			this.m_imgChao.mainTexture = null;
			this.m_imgChao.alpha = 0f;
		}
		if (this.m_imgHidden != null)
		{
			this.m_imgHidden.enabled = false;
		}
		if (this.m_imgCheck != null)
		{
			this.m_imgCheck.enabled = false;
		}
		this.m_clearAnimation = obj.GetComponentInChildren<Animation>();
	}

	// Token: 0x06001E8C RID: 7820 RVA: 0x000B5404 File Offset: 0x000B3604
	public void SetAlready(bool already)
	{
		if (this.m_imgHidden != null)
		{
			this.m_imgHidden.enabled = already;
		}
		if (this.m_imgCheck != null)
		{
			this.m_imgCheck.enabled = already;
		}
	}

	// Token: 0x06001E8D RID: 7821 RVA: 0x000B544C File Offset: 0x000B364C
	public void PlayGetAnimation()
	{
		if (this.m_imgCheck != null)
		{
			this.m_imgCheck.enabled = true;
		}
		if (this.m_clearAnimation != null)
		{
			if (this.m_effect != null)
			{
				this.m_effect.SetActive(true);
			}
			ActiveAnimation.Play(this.m_clearAnimation, Direction.Forward);
		}
	}

	// Token: 0x06001E8E RID: 7822 RVA: 0x000B54B4 File Offset: 0x000B36B4
	public bool SetItem(int id)
	{
		if (this.m_imgItem != null && this.m_imgChara != null && this.m_imgChao != null)
		{
			if (id >= 0)
			{
				int num = Mathf.FloorToInt((float)id / 100000f);
				int num2 = num;
				if (num2 != 3)
				{
					if (num2 != 4)
					{
						this.m_imgItem.alpha = 1f;
						this.m_imgChara.alpha = 0f;
						this.m_imgChao.alpha = 0f;
						this.m_imgItem.spriteName = "ui_cmn_icon_item_" + id;
					}
					else
					{
						this.m_imgItem.alpha = 0f;
						this.m_imgChara.alpha = 0f;
						this.m_imgChao.alpha = 1f;
						ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(this.m_imgChao, null, true);
						ChaoTextureManager.Instance.GetTexture(id - 400000, info);
					}
				}
				else
				{
					this.m_imgItem.alpha = 0f;
					this.m_imgChara.alpha = 1f;
					this.m_imgChao.alpha = 0f;
					UISprite imgChara = this.m_imgChara;
					string str = "ui_tex_player_";
					ServerItem serverItem = new ServerItem((ServerItem.Id)id);
					imgChara.spriteName = str + CharaTypeUtil.GetCharaSpriteNameSuffix(serverItem.charaType);
				}
			}
			else
			{
				this.m_imgItem.spriteName = "ui_cmn_icon_rsring_L";
			}
			return true;
		}
		return false;
	}

	// Token: 0x17000457 RID: 1111
	// (get) Token: 0x06001E8F RID: 7823 RVA: 0x000B563C File Offset: 0x000B383C
	// (set) Token: 0x06001E90 RID: 7824 RVA: 0x000B5644 File Offset: 0x000B3844
	public int count
	{
		get
		{
			return this.m_count;
		}
		set
		{
			if (this.m_count != value)
			{
				this.m_count = value;
				if (this.m_lblCount != null)
				{
					this.m_lblCount.text = HudUtility.GetFormatNumString<int>(this.m_count);
				}
			}
		}
	}

	// Token: 0x04001BEF RID: 7151
	public GameObject m_clearGameObject;

	// Token: 0x04001BF0 RID: 7152
	private GameObject m_effect;

	// Token: 0x04001BF1 RID: 7153
	private Animation m_clearAnimation;

	// Token: 0x04001BF2 RID: 7154
	private UISprite m_imgCheck;

	// Token: 0x04001BF3 RID: 7155
	private UISprite m_imgItem;

	// Token: 0x04001BF4 RID: 7156
	private UISprite m_imgChara;

	// Token: 0x04001BF5 RID: 7157
	private UITexture m_imgChao;

	// Token: 0x04001BF6 RID: 7158
	private UISprite m_imgHidden;

	// Token: 0x04001BF7 RID: 7159
	private UILabel m_lblCount;

	// Token: 0x04001BF8 RID: 7160
	private int m_count;
}
