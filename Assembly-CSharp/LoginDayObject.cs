using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x0200042F RID: 1071
public class LoginDayObject
{
	// Token: 0x0600208C RID: 8332 RVA: 0x000C3488 File Offset: 0x000C1688
	public LoginDayObject(GameObject obj, int day)
	{
		this.m_clearGameObject = obj;
		this.m_effect = GameObjectUtil.FindChildGameObject(this.m_clearGameObject, "eff_4");
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_clearGameObject, "img_day_num");
		this.m_imgItem = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_clearGameObject, "img_login_item");
		this.m_imgChara = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_clearGameObject, "img_chara");
		this.m_imgChao = GameObjectUtil.FindChildGameObjectComponent<UITexture>(this.m_clearGameObject, "img_chao");
		this.m_lblCount = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_clearGameObject, "Lbl_count");
		this.m_imgHidden = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_clearGameObject, "img_hidden");
		this.m_imgCheck = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_clearGameObject, "img_check");
		if (this.m_effect != null)
		{
			this.m_effect.SetActive(false);
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
		this.m_clearAnimation = obj.GetComponent<Animation>();
	}

	// Token: 0x0600208D RID: 8333 RVA: 0x000C3674 File Offset: 0x000C1874
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

	// Token: 0x0600208E RID: 8334 RVA: 0x000C36BC File Offset: 0x000C18BC
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
			SoundManager.SePlay("sys_dailychallenge", "SE");
			ActiveAnimation.Play(this.m_clearAnimation, Direction.Forward);
		}
	}

	// Token: 0x0600208F RID: 8335 RVA: 0x000C3734 File Offset: 0x000C1934
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
						ServerItem serverItem = new ServerItem((ServerItem.Id)id);
						int rewardType = (int)serverItem.rewardType;
						this.m_imgItem.alpha = 1f;
						this.m_imgChara.alpha = 0f;
						this.m_imgChao.alpha = 0f;
						this.m_imgItem.spriteName = "ui_cmn_icon_item_" + rewardType;
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
					string str = "ui_tex_player_set_";
					ServerItem serverItem2 = new ServerItem((ServerItem.Id)id);
					imgChara.spriteName = str + CharaTypeUtil.GetCharaSpriteNameSuffix(serverItem2.charaType);
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

	// Token: 0x1700047E RID: 1150
	// (get) Token: 0x06002090 RID: 8336 RVA: 0x000C38CC File Offset: 0x000C1ACC
	// (set) Token: 0x06002091 RID: 8337 RVA: 0x000C38D4 File Offset: 0x000C1AD4
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

	// Token: 0x04001D1C RID: 7452
	public GameObject m_clearGameObject;

	// Token: 0x04001D1D RID: 7453
	private GameObject m_effect;

	// Token: 0x04001D1E RID: 7454
	private Animation m_clearAnimation;

	// Token: 0x04001D1F RID: 7455
	private UISprite m_imgCheck;

	// Token: 0x04001D20 RID: 7456
	private UISprite m_imgItem;

	// Token: 0x04001D21 RID: 7457
	private UISprite m_imgChara;

	// Token: 0x04001D22 RID: 7458
	private UITexture m_imgChao;

	// Token: 0x04001D23 RID: 7459
	private UISprite m_imgHidden;

	// Token: 0x04001D24 RID: 7460
	private UILabel m_lblCount;

	// Token: 0x04001D25 RID: 7461
	private int m_count;
}
