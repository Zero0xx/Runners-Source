using System;
using Text;
using UnityEngine;

// Token: 0x0200022E RID: 558
public class ui_damage_reward_scroll : MonoBehaviour
{
	// Token: 0x06000F01 RID: 3841 RVA: 0x00057B74 File Offset: 0x00055D74
	public void Start()
	{
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x00057B78 File Offset: 0x00055D78
	private void LoadImage()
	{
		if (this.m_user != null && !this.m_isImgLoad && (this.m_myCell || this.m_user.isFriend))
		{
			this.m_faceTexture.mainTexture = RankingUtil.GetProfilePictureTexture(this.m_user.id, delegate(Texture2D _faceTexture)
			{
				this.m_faceTexture.mainTexture = _faceTexture;
				this.m_faceTexture.alpha = 1f;
			});
			if (this.m_faceTexture.mainTexture != null)
			{
				this.m_faceTexture.alpha = 1f;
			}
			this.m_isImgLoad = true;
		}
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x00057C0C File Offset: 0x00055E0C
	public void UpdateView(RaidBossUser user, RaidBossData bossData)
	{
		if (user == null)
		{
			return;
		}
		this.m_myCell = false;
		this.m_isImgLoad = false;
		this.m_bossData = bossData;
		this.m_user = user;
		if (this.m_lvLabel == null)
		{
			this.m_lvLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_lv");
		}
		this.SetChaoTexture(this.m_mainChaoIcon, this.m_user.mainChaoId);
		this.SetChaoTexture(this.m_subChaoIcon, this.m_user.subChaoId);
		if (this.m_user.isFriend)
		{
			this.LoadImage();
		}
		else
		{
			this.m_faceTexture.mainTexture = PlayerImageManager.GetPlayerDefaultImage();
		}
		this.m_scoreRankIconSprite.enabled = this.m_user.isRankTop;
		if (this.m_friendIconSprite != null)
		{
			this.m_friendIconSprite.spriteName = RankingUtil.GetFriendIconSpriteName(this.m_user);
		}
		if (this.m_user.charaType != CharaType.UNKNOWN)
		{
			if (AtlasManager.Instance != null)
			{
				AtlasManager.Instance.ReplacePlayerAtlasForRaidResult(this.m_charaSprite.atlas);
			}
			this.m_charaSprite.spriteName = "ui_tex_player_set_" + CharaTypeUtil.GetCharaSpriteNameSuffix(this.m_user.charaType);
		}
		if (this.m_lvLabel != null)
		{
			string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_LevelNumber").text;
			this.m_lvLabel.text = text.Replace("{PARAM}", user.charaLevel.ToString());
		}
		if (this.m_chao1BgSprite != null)
		{
			if (this.m_user.mainChaoId == -1)
			{
				this.m_chao1BgSprite.gameObject.SetActive(false);
			}
			else
			{
				this.m_chao1BgSprite.gameObject.SetActive(true);
				this.m_chao1BgSprite.spriteName = "ui_ranking_scroll_char_bg_" + this.m_user.mainChaoRarity;
			}
		}
		if (this.m_chao2BgSprite != null)
		{
			if (this.m_user.subChaoId == -1)
			{
				this.m_chao2BgSprite.gameObject.SetActive(false);
			}
			else
			{
				this.m_chao2BgSprite.gameObject.SetActive(true);
				this.m_chao2BgSprite.spriteName = "ui_ranking_scroll_char_bg_S" + this.m_user.subChaoRarity;
			}
		}
		this.m_nameLabel.text = this.m_user.userName;
		if (this.m_damage != null && this.m_damageLabel != null && this.m_damageRateLabel != null)
		{
			this.m_damageLabel.text = HudUtility.GetFormatNumString<long>(this.m_user.damage);
			float num = (float)this.m_user.damage / (float)this.m_bossData.hpMax;
			int num2;
			if (num < 0f)
			{
				num = 0f;
				num2 = 0;
			}
			else if (num > 1f)
			{
				num = 1f;
				num2 = 100;
			}
			else
			{
				int num3 = Mathf.FloorToInt(num * 10000f);
				if (num3 > 1)
				{
					num3--;
				}
				num = (float)num3 / 10000f;
				num2 = Mathf.CeilToInt(100f * num);
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			else if (num2 >= 100)
			{
				num2 = 100;
				if (this.m_user.damage < this.m_bossData.hpMax)
				{
					num2 = 99;
				}
			}
			string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "odds").text;
			this.m_damageRateLabel.text = TextUtility.Replace(text2, "{ODDS}", string.Empty + num2);
			this.m_damage.value = num;
			this.m_damage.ForceUpdate();
		}
		if (this.m_destroyCountLabel != null)
		{
			this.m_destroyCountLabel.text = HudUtility.GetFormatNumString<long>(this.m_user.destroyCount);
		}
		if (this.m_destroyIcon != null)
		{
			this.m_destroyIcon.enabled = this.m_user.isDestroy;
		}
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x0005803C File Offset: 0x0005623C
	public void SetMyRanker(bool myCell)
	{
		this.m_myCell = myCell;
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "Btn_damage_reward_top");
		if (uisprite != null)
		{
			if (uisprite.color.r < 1f || uisprite.color.g < 1f || uisprite.color.b < 1f)
			{
				uisprite.color = new Color(1f, 1f, 1f, 1f);
			}
			uisprite.spriteName = ((!myCell) ? "ui_event_raidboss_damage_reward_bar1" : "ui_event_raidboss_damage_reward_bar2");
		}
		for (int i = 0; i < this.m_myRanker.Length; i++)
		{
			if (this.m_myRanker[i] != null)
			{
				this.m_myRanker[i].enabled = myCell;
			}
		}
		if (myCell)
		{
			this.m_isImgLoad = false;
			this.LoadImage();
		}
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x00058140 File Offset: 0x00056340
	public void UpdateSendChallenge(string id)
	{
		if (base.gameObject.activeSelf && this.m_user != null && this.m_user.id == id)
		{
			this.m_user.isSentEnergy = true;
			if (this.m_friendIconSprite != null)
			{
				this.m_friendIconSprite.spriteName = RankingUtil.GetFriendIconSpriteName(this.m_user);
			}
		}
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x000581B4 File Offset: 0x000563B4
	private void SetChaoTexture(UITexture uiTex, int chaoId)
	{
		if (uiTex != null)
		{
			if (chaoId >= 0 && SingletonGameObject<RankingManager>.Instance != null)
			{
				SingletonGameObject<RankingManager>.Instance.GetChaoTexture(chaoId, uiTex, 0.2f, false);
				uiTex.gameObject.SetActive(true);
			}
			else
			{
				uiTex.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x00058214 File Offset: 0x00056414
	public void SetClickCollision(bool flag)
	{
		UIButtonOffset uibuttonOffset = GameObjectUtil.FindChildGameObjectComponent<UIButtonOffset>(base.gameObject, "Btn_damage_reward_top");
		if (uibuttonOffset != null)
		{
			uibuttonOffset.enabled = flag;
		}
		UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_damage_reward_top");
		if (uibuttonMessage != null)
		{
			uibuttonMessage.enabled = flag;
		}
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x0005826C File Offset: 0x0005646C
	private void OnClickUserScroll()
	{
		if (Mathf.Abs(ui_damage_reward_scroll.s_updateLastTime - Time.realtimeSinceStartup) > 1.5f)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			ranking_window.RaidOpen(this.m_user);
		}
	}

	// Token: 0x04000CE8 RID: 3304
	[SerializeField]
	private UITexture m_faceTexture;

	// Token: 0x04000CE9 RID: 3305
	[SerializeField]
	private UISprite m_scoreRankIconSprite;

	// Token: 0x04000CEA RID: 3306
	[SerializeField]
	private UISprite m_friendIconSprite;

	// Token: 0x04000CEB RID: 3307
	[SerializeField]
	private UISprite m_charaSprite;

	// Token: 0x04000CEC RID: 3308
	[SerializeField]
	private UITexture m_mainChaoIcon;

	// Token: 0x04000CED RID: 3309
	[SerializeField]
	private UITexture m_subChaoIcon;

	// Token: 0x04000CEE RID: 3310
	[SerializeField]
	private UISprite m_chao1BgSprite;

	// Token: 0x04000CEF RID: 3311
	[SerializeField]
	private UISprite m_chao2BgSprite;

	// Token: 0x04000CF0 RID: 3312
	[SerializeField]
	private UILabel m_nameLabel;

	// Token: 0x04000CF1 RID: 3313
	[SerializeField]
	private UILabel m_damageLabel;

	// Token: 0x04000CF2 RID: 3314
	[SerializeField]
	private UILabel m_damageRateLabel;

	// Token: 0x04000CF3 RID: 3315
	[SerializeField]
	private UISlider m_damage;

	// Token: 0x04000CF4 RID: 3316
	[SerializeField]
	private UISprite m_destroyIcon;

	// Token: 0x04000CF5 RID: 3317
	[SerializeField]
	private UILabel m_destroyCountLabel;

	// Token: 0x04000CF6 RID: 3318
	[SerializeField]
	private TweenColor[] m_myRanker = new TweenColor[2];

	// Token: 0x04000CF7 RID: 3319
	private UILabel m_lvLabel;

	// Token: 0x04000CF8 RID: 3320
	private RaidBossData m_bossData;

	// Token: 0x04000CF9 RID: 3321
	private RaidBossUser m_user;

	// Token: 0x04000CFA RID: 3322
	private bool m_isImgLoad;

	// Token: 0x04000CFB RID: 3323
	private bool m_myCell;

	// Token: 0x04000CFC RID: 3324
	private static float s_updateLastTime;
}
