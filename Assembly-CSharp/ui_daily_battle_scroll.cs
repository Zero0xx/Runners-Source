using System;
using UnityEngine;

// Token: 0x02000406 RID: 1030
public class ui_daily_battle_scroll : MonoBehaviour
{
	// Token: 0x06001EBD RID: 7869 RVA: 0x000B69D4 File Offset: 0x000B4BD4
	private void Start()
	{
	}

	// Token: 0x06001EBE RID: 7870 RVA: 0x000B69D8 File Offset: 0x000B4BD8
	public void Update()
	{
	}

	// Token: 0x06001EBF RID: 7871 RVA: 0x000B69DC File Offset: 0x000B4BDC
	public void DrawClear()
	{
		if (this.m_playerTexL != null && this.m_playerTexL.alpha > 0f)
		{
			UnityEngine.Object.DestroyImmediate(this.m_playerTexL.mainTexture);
			this.m_playerTexL.alpha = 0f;
		}
		if (this.m_playerTexR != null && this.m_playerTexR.alpha > 0f)
		{
			UnityEngine.Object.DestroyImmediate(this.m_playerTexR.mainTexture);
			this.m_playerTexR.alpha = 0f;
		}
		this.m_isImgLoad = false;
	}

	// Token: 0x06001EC0 RID: 7872 RVA: 0x000B6A7C File Offset: 0x000B4C7C
	public void InitSetupObject()
	{
		if (this.m_buttonMessage != null)
		{
			this.m_buttonMessage.target = base.gameObject;
			this.m_buttonMessage.functionName = "OnClickDailyBattelScroll";
		}
		if (this.m_nonParticipating == null)
		{
			this.m_nonParticipating = GameObjectUtil.FindChildGameObject(base.gameObject, "duel_lose_default_set");
			if (this.m_nonParticipating != null)
			{
				this.m_nonParticipating.SetActive(false);
			}
		}
		if (this.m_collider == null)
		{
			this.m_collider = base.gameObject.GetComponent<BoxCollider>();
		}
		if (this.m_collider != null)
		{
			this.m_collider.enabled = false;
		}
		if (this.m_date != null)
		{
			this.m_date.text = string.Empty;
		}
		if (this.m_winSet != null)
		{
			this.m_winSet.SetActive(false);
		}
		if (this.m_loseSet != null)
		{
			this.m_loseSet.SetActive(false);
		}
		if (this.m_noMatchingL != null)
		{
			this.m_noMatchingL.SetActive(false);
		}
		if (this.m_noMatchingR != null)
		{
			this.m_noMatchingR.SetActive(false);
		}
		if (this.m_playerL != null)
		{
			this.m_playerTexL = GameObjectUtil.FindChildGameObjectComponent<UITexture>(this.m_playerL, "img_icon_friends");
			this.m_playerLeagueIconMainL = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_playerL, "img_icon_league");
			this.m_playerLeagueIconSubL = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_playerL, "img_icon_league_sub");
			this.m_playerFriendIconL = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_playerL, "img_friend_icon");
			this.m_playerScoreL = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_playerL, "Lbl_score");
			this.m_playerNameL = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_playerL, "Lbl_username");
			this.m_playerL.SetActive(false);
		}
		if (this.m_playerR != null)
		{
			this.m_playerTexR = GameObjectUtil.FindChildGameObjectComponent<UITexture>(this.m_playerR, "img_icon_friends");
			this.m_playerLeagueIconMainR = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_playerR, "img_icon_league");
			this.m_playerLeagueIconSubR = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_playerR, "img_icon_league_sub");
			this.m_playerFriendIconR = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_playerR, "img_friend_icon");
			this.m_playerScoreR = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_playerR, "Lbl_score");
			this.m_playerNameR = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_playerR, "Lbl_username");
			this.m_playerR.SetActive(false);
		}
		if (this.m_playerTexL != null && this.m_playerTexR != null)
		{
			this.m_playerTexL.alpha = 0f;
			this.m_playerTexR.alpha = 0f;
		}
		if (this.m_playerLeagueIconMainL != null && this.m_playerLeagueIconSubL != null && this.m_playerLeagueIconMainR != null && this.m_playerLeagueIconSubR != null)
		{
			this.m_playerLeagueIconMainL.spriteName = string.Empty;
			this.m_playerLeagueIconSubL.spriteName = string.Empty;
			this.m_playerLeagueIconMainR.spriteName = string.Empty;
			this.m_playerLeagueIconSubR.spriteName = string.Empty;
		}
		if (this.m_playerFriendIconL != null && this.m_playerFriendIconR != null)
		{
			this.m_playerFriendIconL.spriteName = string.Empty;
			this.m_playerFriendIconR.spriteName = string.Empty;
		}
		if (this.m_playerScoreL != null && this.m_playerNameL != null && this.m_playerScoreR != null && this.m_playerNameR != null)
		{
			this.m_playerScoreL.text = "0";
			this.m_playerScoreR.text = "0";
			this.m_playerNameL.text = string.Empty;
			this.m_playerNameR.text = string.Empty;
		}
	}

	// Token: 0x06001EC1 RID: 7873 RVA: 0x000B6EA4 File Offset: 0x000B50A4
	public void UpdateView(ServerDailyBattleDataPair data)
	{
		this.m_dailyBattleData = data;
		ui_daily_battle_scroll.s_updateLastTime = Time.realtimeSinceStartup;
		base.enabled = true;
		this.InitSetupObject();
		this.m_rankerL = null;
		this.m_rankerR = null;
		if (this.m_dailyBattleData != null)
		{
			if (this.m_collider != null && !this.m_dailyBattleData.isDummyData)
			{
				this.m_collider.enabled = true;
			}
			if (this.m_date != null)
			{
				if (!this.m_dailyBattleData.isDummyData && this.m_dailyBattleData.winFlag > 0)
				{
					this.m_date.text = this.m_dailyBattleData.starDateString;
					if (this.m_nonParticipating != null)
					{
						this.m_nonParticipating.SetActive(false);
					}
				}
				else
				{
					string starDateString = this.m_dailyBattleData.starDateString;
					string dateStringHour = GeneralUtil.GetDateStringHour(this.m_dailyBattleData.endTime, -24);
					if (starDateString == dateStringHour)
					{
						this.m_date.text = starDateString;
					}
					else
					{
						this.m_date.text = starDateString + " - " + dateStringHour;
					}
					if (this.m_nonParticipating != null)
					{
						this.m_nonParticipating.SetActive(true);
					}
				}
			}
			if (this.m_winSet != null && this.m_loseSet != null)
			{
				if (this.m_dailyBattleData.winFlag >= 2)
				{
					this.m_winSet.SetActive(true);
					this.m_loseSet.SetActive(false);
				}
				else
				{
					this.m_winSet.SetActive(false);
					this.m_loseSet.SetActive(true);
				}
			}
			if (this.m_dailyBattleData.myBattleData != null && !string.IsNullOrEmpty(this.m_dailyBattleData.myBattleData.userId))
			{
				this.m_rankerL = new RankingUtil.Ranker(this.m_dailyBattleData.myBattleData);
			}
			if (this.m_dailyBattleData.rivalBattleData != null && !string.IsNullOrEmpty(this.m_dailyBattleData.rivalBattleData.userId))
			{
				this.m_rankerR = new RankingUtil.Ranker(this.m_dailyBattleData.rivalBattleData);
			}
			if (this.m_rankerL != null)
			{
				if (this.m_playerLeagueIconMainL != null && this.m_playerLeagueIconSubL != null)
				{
					this.m_playerLeagueIconMainL.spriteName = RankingUtil.GetLeagueIconName((LeagueType)this.m_rankerL.leagueIndex);
					this.m_playerLeagueIconSubL.spriteName = RankingUtil.GetLeagueIconName2((LeagueType)this.m_rankerL.leagueIndex);
				}
				if (this.m_playerFriendIconL != null)
				{
					this.m_playerFriendIconL.spriteName = RankingUtil.GetFriendIconSpriteName(this.m_rankerL);
				}
				if (this.m_playerScoreL != null && this.m_playerNameL != null)
				{
					this.m_playerNameL.text = this.m_rankerL.userName;
					this.m_playerScoreL.text = HudUtility.GetFormatNumString<long>(this.m_rankerL.score);
				}
				this.m_playerL.SetActive(true);
			}
			else
			{
				this.m_playerL.SetActive(false);
				if (this.m_noMatchingL != null)
				{
					this.m_noMatchingL.SetActive(true);
				}
			}
			if (this.m_rankerR != null)
			{
				if (this.m_playerLeagueIconMainR != null && this.m_playerLeagueIconSubR != null)
				{
					this.m_playerLeagueIconMainR.spriteName = RankingUtil.GetLeagueIconName((LeagueType)this.m_rankerR.leagueIndex);
					this.m_playerLeagueIconSubR.spriteName = RankingUtil.GetLeagueIconName2((LeagueType)this.m_rankerR.leagueIndex);
				}
				if (this.m_playerFriendIconR != null)
				{
					this.m_playerFriendIconR.spriteName = RankingUtil.GetFriendIconSpriteName(this.m_rankerR);
				}
				if (this.m_playerScoreR != null && this.m_playerNameR != null)
				{
					this.m_playerNameR.text = this.m_rankerR.userName;
					this.m_playerScoreR.text = HudUtility.GetFormatNumString<long>(this.m_rankerR.score);
				}
				this.m_playerR.SetActive(true);
			}
			else
			{
				this.m_playerR.SetActive(false);
				if (this.m_noMatchingR != null)
				{
					this.m_noMatchingR.SetActive(true);
				}
			}
		}
		this.LoadImage();
	}

	// Token: 0x06001EC2 RID: 7874 RVA: 0x000B730C File Offset: 0x000B550C
	private void LoadImage()
	{
		if (!this.m_isImgLoad)
		{
			if (this.m_rankerL != null)
			{
				this.LoadUserFaceTexture(this.m_rankerL, this.m_playerTexL);
			}
			if (this.m_rankerR != null)
			{
				this.LoadUserFaceTexture(this.m_rankerR, this.m_playerTexR);
			}
		}
		this.m_isImgLoad = true;
	}

	// Token: 0x06001EC3 RID: 7875 RVA: 0x000B7368 File Offset: 0x000B5568
	private void LoadUserFaceTexture(RankingUtil.Ranker ranker, UITexture uiTex)
	{
		if (ranker != null && (ranker.isFriend || ranker.isMy) && uiTex != null)
		{
			uiTex.mainTexture = RankingUtil.GetProfilePictureTexture(ranker, delegate(Texture2D _faceTexture)
			{
				uiTex.mainTexture = _faceTexture;
				uiTex.alpha = 1f;
			});
			if (uiTex.mainTexture != null)
			{
				uiTex.alpha = 1f;
			}
		}
	}

	// Token: 0x06001EC4 RID: 7876 RVA: 0x000B73F4 File Offset: 0x000B55F4
	private void OnClickDailyBattelScroll()
	{
		if (this.m_dailyBattleData != null && Mathf.Abs(ui_daily_battle_scroll.s_updateLastTime - Time.realtimeSinceStartup) > 1f)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			DailyBattleDetailWindow.Open(this.m_dailyBattleData);
		}
	}

	// Token: 0x04001C17 RID: 7191
	public const float IMAGE_DELAY = 0.2f;

	// Token: 0x04001C18 RID: 7192
	private static float s_updateLastTime;

	// Token: 0x04001C19 RID: 7193
	[SerializeField]
	private UIButtonMessage m_buttonMessage;

	// Token: 0x04001C1A RID: 7194
	[SerializeField]
	private UILabel m_date;

	// Token: 0x04001C1B RID: 7195
	[SerializeField]
	private GameObject m_winSet;

	// Token: 0x04001C1C RID: 7196
	[SerializeField]
	private GameObject m_loseSet;

	// Token: 0x04001C1D RID: 7197
	[SerializeField]
	private GameObject m_playerL;

	// Token: 0x04001C1E RID: 7198
	[SerializeField]
	private GameObject m_playerR;

	// Token: 0x04001C1F RID: 7199
	[SerializeField]
	private GameObject m_noMatchingL;

	// Token: 0x04001C20 RID: 7200
	[SerializeField]
	private GameObject m_noMatchingR;

	// Token: 0x04001C21 RID: 7201
	private GameObject m_nonParticipating;

	// Token: 0x04001C22 RID: 7202
	private ServerDailyBattleDataPair m_dailyBattleData;

	// Token: 0x04001C23 RID: 7203
	private RankingUtil.Ranker m_rankerL;

	// Token: 0x04001C24 RID: 7204
	private RankingUtil.Ranker m_rankerR;

	// Token: 0x04001C25 RID: 7205
	private bool m_isImgLoad;

	// Token: 0x04001C26 RID: 7206
	private BoxCollider m_collider;

	// Token: 0x04001C27 RID: 7207
	private UITexture m_playerTexL;

	// Token: 0x04001C28 RID: 7208
	private UISprite m_playerLeagueIconMainL;

	// Token: 0x04001C29 RID: 7209
	private UISprite m_playerLeagueIconSubL;

	// Token: 0x04001C2A RID: 7210
	private UISprite m_playerFriendIconL;

	// Token: 0x04001C2B RID: 7211
	private UILabel m_playerScoreL;

	// Token: 0x04001C2C RID: 7212
	private UILabel m_playerNameL;

	// Token: 0x04001C2D RID: 7213
	private UITexture m_playerTexR;

	// Token: 0x04001C2E RID: 7214
	private UISprite m_playerLeagueIconMainR;

	// Token: 0x04001C2F RID: 7215
	private UISprite m_playerLeagueIconSubR;

	// Token: 0x04001C30 RID: 7216
	private UISprite m_playerFriendIconR;

	// Token: 0x04001C31 RID: 7217
	private UILabel m_playerScoreR;

	// Token: 0x04001C32 RID: 7218
	private UILabel m_playerNameR;
}
