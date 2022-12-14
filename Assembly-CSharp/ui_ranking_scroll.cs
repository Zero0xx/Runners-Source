using System;
using Text;
using UnityEngine;

// Token: 0x0200050B RID: 1291
public class ui_ranking_scroll : MonoBehaviour
{
	// Token: 0x17000520 RID: 1312
	// (get) Token: 0x060026C5 RID: 9925 RVA: 0x000EC818 File Offset: 0x000EAA18
	// (set) Token: 0x060026C6 RID: 9926 RVA: 0x000EC820 File Offset: 0x000EAA20
	public bool SendButtonDisable
	{
		get
		{
			return this.m_sendBtnDisable;
		}
		set
		{
			this.m_sendBtnDisable = value;
		}
	}

	// Token: 0x17000521 RID: 1313
	// (get) Token: 0x060026C7 RID: 9927 RVA: 0x000EC82C File Offset: 0x000EAA2C
	public long rankerScore
	{
		get
		{
			long result = 0L;
			if (this.m_ranker != null)
			{
				result = this.m_ranker.score;
			}
			return result;
		}
	}

	// Token: 0x060026C8 RID: 9928 RVA: 0x000EC854 File Offset: 0x000EAA54
	private void Start()
	{
	}

	// Token: 0x060026C9 RID: 9929 RVA: 0x000EC858 File Offset: 0x000EAA58
	public void Update()
	{
		if (this.m_startCount < 30)
		{
			this.m_startCount++;
			if (this.m_dragPanelContents != null && this.m_dragPanelContents.draggablePanel != null && !string.IsNullOrEmpty(this.m_dragPanelContents.draggablePanel.name) && this.m_dragPanelContents.draggablePanel.name.IndexOf("mainmenu_contents") != -1)
			{
				this.m_dragPanelContents.draggablePanel = null;
			}
		}
		float deltaTime = Time.deltaTime;
		if (this.m_chaoDrawDelay > 0f)
		{
			if (this.IsDraw())
			{
				this.m_chaoDrawDelay -= deltaTime;
				if (this.m_chaoDrawDelay <= 0f && this.m_ranker != null)
				{
					this.m_mainChaoTexTime = -1f;
					this.m_subChaoTexTime = -1f;
					if (this.m_ranker.mainChaoId >= 0)
					{
						SingletonGameObject<RankingManager>.Instance.GetChaoTexture(this.m_ranker.mainChaoId, this.m_mainChaoTex, 0.2f, false);
						this.m_mainChaoTexTime = 0f;
					}
					if (this.m_ranker.subChaoId >= 0)
					{
						SingletonGameObject<RankingManager>.Instance.GetChaoTexture(this.m_ranker.subChaoId, this.m_subChaoTex, 0.2f, false);
						this.m_subChaoTexTime = 0f;
					}
					this.m_chaoDrawDelay = 0f;
					if (this.m_mainChaoTexTime < 0f && this.m_subChaoTexTime < 0f)
					{
						this.m_isDrawChao = true;
					}
				}
			}
		}
		else if (!this.m_isDrawChao)
		{
			if (this.m_mainChaoIcon != null && this.m_mainChaoIcon.gameObject.activeSelf && this.m_mainChaoTex != null && this.m_mainChaoTexTime >= 0f)
			{
				if (this.m_mainChaoTex.alpha > 0f)
				{
					this.m_mainChaoIcon.gameObject.SetActive(false);
					this.m_mainChaoTexTime = -1f;
				}
				else
				{
					this.m_mainChaoTexTime += deltaTime;
					if (this.m_mainChaoTexTime > 0.75f)
					{
						if (this.m_ranker.mainChaoId >= 0)
						{
							SingletonGameObject<RankingManager>.Instance.GetChaoTexture(this.m_ranker.mainChaoId, this.m_mainChaoTex, 0.2f, false);
							this.m_mainChaoTexTime = 0f;
						}
						else
						{
							this.m_mainChaoTexTime = -1f;
						}
					}
				}
			}
			if (this.m_subChaoIcon != null && this.m_subChaoIcon.gameObject.activeSelf && this.m_subChaoTex != null && this.m_subChaoTexTime >= 0f)
			{
				if (this.m_subChaoTex.alpha > 0f)
				{
					this.m_subChaoIcon.gameObject.SetActive(false);
					this.m_subChaoTexTime = -1f;
				}
				else
				{
					this.m_subChaoTexTime += deltaTime;
					if (this.m_subChaoTexTime > 0.75f)
					{
						if (this.m_ranker.subChaoId >= 0)
						{
							SingletonGameObject<RankingManager>.Instance.GetChaoTexture(this.m_ranker.subChaoId, this.m_subChaoTex, 0.2f, false);
							this.m_subChaoTexTime = 0f;
						}
						else
						{
							this.m_subChaoTexTime = -1f;
						}
					}
				}
			}
			if (this.m_mainChaoTexTime < 0f && this.m_subChaoTexTime < 0f)
			{
				this.m_isDrawChao = true;
			}
		}
		if (this.m_dummy != null)
		{
			if (this.m_updTime <= 0f)
			{
				this.m_boxCollider.enabled = !this.m_dummy.IsCreating(0f);
				this.m_updTime = 0.15f;
			}
			this.m_updTime -= deltaTime;
		}
	}

	// Token: 0x060026CA RID: 9930 RVA: 0x000ECC64 File Offset: 0x000EAE64
	private bool IsDraw()
	{
		bool result = false;
		if (this.m_dragPanelContents != null)
		{
			if (this.m_dragPanelContents.draggablePanel != null && this.m_dragPanelContents.draggablePanel.panel != null)
			{
				if (!this.m_isDraw)
				{
					float num = this.m_dragPanelContents.draggablePanel.panel.transform.localPosition.y * -1f;
					float w = this.m_dragPanelContents.draggablePanel.panel.clipRange.w;
					float num2 = num - w;
					float y = base.gameObject.transform.localPosition.y;
					if (y > num2)
					{
						this.m_isDraw = true;
					}
				}
				result = this.m_isDraw;
				this.m_nfPanelTime = 0f;
			}
			else
			{
				this.m_nfPanelTime += Time.deltaTime;
				if (this.m_nfPanelTime > 0.5f)
				{
					this.m_isDraw = true;
					result = this.m_isDraw;
				}
			}
		}
		return result;
	}

	// Token: 0x060026CB RID: 9931 RVA: 0x000ECD80 File Offset: 0x000EAF80
	public void UpdateViewAsync(RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType, RankingUtil.Ranker ranker, bool end, bool myCell, float delay, ui_ranking_scroll_dummy target)
	{
		this.m_isImgLoad = false;
		this.m_myCell = false;
		ui_ranking_scroll.s_updateLastTime = Time.realtimeSinceStartup;
		this.m_updTime = 0f;
		this.m_dummy = target;
		if (this.m_boxCollider == null)
		{
			this.m_boxCollider = base.gameObject.GetComponentInChildren<BoxCollider>();
		}
		if (this.m_boxCollider != null)
		{
			this.m_boxCollider.enabled = false;
		}
		this.SetMyRanker(myCell);
		this.UpdateView(scoreType, rankerType, ranker, end);
		if (target != null)
		{
			target.SetMask(0.98f);
		}
	}

	// Token: 0x060026CC RID: 9932 RVA: 0x000ECE24 File Offset: 0x000EB024
	public void DrawClear()
	{
	}

	// Token: 0x060026CD RID: 9933 RVA: 0x000ECE28 File Offset: 0x000EB028
	public void UpdateViewScore(long score)
	{
		if (this.m_scodeLabel != null)
		{
			if (score >= 0L)
			{
				this.m_scodeLabel.text = HudUtility.GetFormatNumString<long>(score);
			}
			else if (this.m_ranker != null)
			{
				this.m_scodeLabel.text = HudUtility.GetFormatNumString<long>(this.m_ranker.score);
			}
		}
	}

	// Token: 0x060026CE RID: 9934 RVA: 0x000ECE8C File Offset: 0x000EB08C
	public void UpdateViewRank(int rank)
	{
		if (rank > 0)
		{
			if (rank <= 3)
			{
				this.m_rankingLabel.alpha = 0f;
				this.m_rankingSprite.gameObject.SetActive(true);
				this.m_rankingSprite.spriteName = "ui_ranking_scroll_num_" + rank;
				this.m_rankingSprite.alpha = 1f;
			}
			else
			{
				this.m_rankingSprite.alpha = 0f;
				this.m_rankingLabel.gameObject.SetActive(true);
				this.m_rankingLabel.text = rank.ToString();
				this.m_rankingLabel.alpha = 1f;
			}
			this.m_scoreRankIconSprite.spriteName = "ui_ranking_scroll_crown_" + (rank - 1);
		}
		else
		{
			if (this.m_ranker.rankIndex < 3)
			{
				this.m_rankingLabel.alpha = 0f;
				this.m_rankingSprite.gameObject.SetActive(true);
				this.m_rankingSprite.spriteName = "ui_ranking_scroll_num_" + (this.m_ranker.rankIndex + 1);
				this.m_rankingSprite.alpha = 1f;
			}
			else
			{
				this.m_rankingSprite.alpha = 0f;
				this.m_rankingLabel.gameObject.SetActive(true);
				this.m_rankingLabel.text = (this.m_ranker.rankIndex + 1).ToString();
				this.m_rankingLabel.alpha = 1f;
			}
			this.m_scoreRankIconSprite.spriteName = "ui_ranking_scroll_crown_" + this.m_ranker.rankIconIndex;
		}
	}

	// Token: 0x060026CF RID: 9935 RVA: 0x000ED044 File Offset: 0x000EB244
	public void UpdateView(RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType, RankingUtil.Ranker ranker, bool end)
	{
		this.m_isDraw = false;
		this.m_isDrawChao = false;
		this.m_chaoDrawDelay = 0f;
		this.m_nfPanelTime = 0f;
		this.m_sendBtnDisable = false;
		this.m_isImgLoad = false;
		this.m_updTime = 0f;
		this.m_startCount = 0;
		base.enabled = true;
		if (this.m_boxCollider == null)
		{
			this.m_boxCollider = base.gameObject.GetComponentInChildren<BoxCollider>();
		}
		if (this.m_boxCollider != null)
		{
			this.m_boxCollider.enabled = ranker.isBoxCollider;
		}
		if (this.m_rankingSet == null)
		{
			this.m_rankingSet = GameObjectUtil.FindChildGameObject(base.gameObject, "ranking_set");
		}
		if (this.m_rankingSet != null)
		{
			this.m_rankingSet.SetActive(true);
		}
		this.m_lvLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_lv");
		this.m_scoreType = scoreType;
		this.m_rankerType = rankerType;
		this.m_ranker = ranker;
		if (this.m_mainChaoTex == null)
		{
			this.m_mainChaoTex = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_chao_main_icon");
		}
		if (this.m_subChaoTex == null)
		{
			this.m_subChaoTex = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_chao_sub_icon");
		}
		if (this.m_mainChaoTex != null)
		{
			this.m_mainChaoTex.enabled = (this.m_ranker.mainChaoId >= 0);
		}
		if (this.m_subChaoTex != null)
		{
			this.m_subChaoTex.enabled = (this.m_ranker.subChaoId >= 0);
		}
		if (this.m_ranker.mainChaoId >= 0)
		{
			this.m_chaoDrawDelay = 0.2f;
			this.m_mainChaoTex.alpha = 0f;
		}
		if (this.m_ranker.subChaoId >= 0)
		{
			this.m_chaoDrawDelay = 0.2f;
			this.m_subChaoTex.alpha = 0f;
		}
		if (this.m_chaoDrawDelay <= 0f)
		{
			this.m_isDrawChao = true;
		}
		if (this.m_mainChaoIcon != null)
		{
			if (this.m_ranker.mainChaoId >= 0)
			{
				this.m_mainChaoIcon.gameObject.SetActive(true);
			}
			else
			{
				this.m_mainChaoIcon.gameObject.SetActive(false);
			}
		}
		if (this.m_subChaoIcon != null)
		{
			if (this.m_ranker.subChaoId >= 0)
			{
				this.m_subChaoIcon.gameObject.SetActive(true);
			}
			else
			{
				this.m_subChaoIcon.gameObject.SetActive(false);
			}
		}
		this.m_faceTexture.alpha = 0f;
		this.LoadImage();
		if (ranker.userDataType != RankingUtil.UserDataType.DAILY_BATTLE)
		{
			if (ranker.rankIndex < 3)
			{
				this.m_rankingSprite.spriteName = "ui_ranking_scroll_num_" + (ranker.rankIndex + 1);
			}
			else
			{
				this.m_rankingLabel.text = (ranker.rankIndex + 1).ToString();
			}
			this.m_rankingSprite.alpha = ((ranker.rankIndex >= 3) ? 0f : 1f);
			this.m_rankingLabel.alpha = ((ranker.rankIndex >= 3) ? 1f : 0f);
		}
		else
		{
			string text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "battle_continuous_win");
			if (ranker.rankIndex > 1)
			{
				this.m_rankingLabel.text = text.Replace("{PARAM}", ranker.rankIndex.ToString());
			}
			else
			{
				this.m_rankingLabel.text = string.Empty;
				if (this.m_rankingSet != null)
				{
					this.m_rankingSet.SetActive(false);
				}
			}
			this.m_rankingSprite.alpha = 0f;
			this.m_rankingLabel.alpha = 1f;
		}
		this.m_rankingSprite.gameObject.SetActive(true);
		this.m_rankingLabel.gameObject.SetActive(true);
		if (this.m_lvLabel != null)
		{
			string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_LevelNumber").text;
			this.m_lvLabel.text = text2.Replace("{PARAM}", ranker.charaLevel.ToString());
		}
		if (ranker.userDataType != RankingUtil.UserDataType.DAILY_BATTLE)
		{
			this.m_scoreRankIconSprite.spriteName = "ui_ranking_scroll_crown_" + ranker.rankIconIndex;
		}
		else
		{
			this.m_scoreRankIconSprite.spriteName = string.Empty;
		}
		this.m_friendIconSprite.spriteName = RankingUtil.GetFriendIconSpriteName(ranker);
		if (ranker.charaType != CharaType.UNKNOWN)
		{
			this.m_charaSprite.spriteName = "ui_tex_player_set_" + CharaTypeUtil.GetCharaSpriteNameSuffix(ranker.charaType);
		}
		if (this.m_chao1BgSprite != null)
		{
			if (ranker.mainChaoId == -1)
			{
				this.m_chao1BgSprite.gameObject.SetActive(false);
			}
			else
			{
				this.m_chao1BgSprite.gameObject.SetActive(true);
				this.m_chao1BgSprite.spriteName = "ui_ranking_scroll_char_bg_" + ranker.mainChaoRarity;
			}
		}
		if (this.m_chao2BgSprite != null)
		{
			if (ranker.subChaoId == -1)
			{
				this.m_chao2BgSprite.gameObject.SetActive(false);
			}
			else
			{
				this.m_chao2BgSprite.gameObject.SetActive(true);
				this.m_chao2BgSprite.spriteName = "ui_ranking_scroll_char_bg_S" + ranker.subChaoRarity;
			}
		}
		this.m_nameLabel.text = ranker.userName;
		this.m_scodeLabel.text = HudUtility.GetFormatNumString<long>(ranker.score);
		this.m_leagueIcon.spriteName = RankingUtil.GetLeagueIconName((LeagueType)ranker.leagueIndex);
		this.m_leagueIcon2.spriteName = RankingUtil.GetLeagueIconName2((LeagueType)ranker.leagueIndex);
	}

	// Token: 0x060026D0 RID: 9936 RVA: 0x000ED654 File Offset: 0x000EB854
	public void UpdateViewForRaidbossDesired(ServerEventRaidBossDesiredState desiredState)
	{
		this.m_isDraw = false;
		this.m_isDrawChao = false;
		this.m_chaoDrawDelay = 0f;
		this.m_nfPanelTime = 0f;
		this.m_isImgLoad = false;
		this.m_updTime = 0f;
		this.m_startCount = 0;
		base.enabled = true;
		this.m_lvLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_lv");
		this.m_scoreType = RankingUtil.RankingScoreType.HIGH_SCORE;
		this.m_rankerType = RankingUtil.RankingRankerType.ALL;
		RaidBossUser raidBossUser = new RaidBossUser(desiredState);
		this.m_ranker = raidBossUser.ConvertRankerData();
		if (this.m_mainChaoTex == null)
		{
			this.m_mainChaoTex = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_chao_main_icon");
		}
		if (this.m_subChaoTex == null)
		{
			this.m_subChaoTex = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_chao_sub_icon");
		}
		if (this.m_mainChaoTex != null)
		{
			this.m_mainChaoTex.enabled = (this.m_ranker.mainChaoId >= 0);
		}
		if (this.m_subChaoTex != null)
		{
			this.m_subChaoTex.enabled = (this.m_ranker.subChaoId >= 0);
		}
		if (this.m_ranker.mainChaoId >= 0)
		{
			this.m_chaoDrawDelay = 0.2f;
			this.m_mainChaoTex.alpha = 0f;
		}
		if (this.m_ranker.subChaoId >= 0)
		{
			this.m_chaoDrawDelay = 0.2f;
			this.m_subChaoTex.alpha = 0f;
		}
		if (this.m_chaoDrawDelay <= 0f)
		{
			this.m_isDrawChao = true;
		}
		if (this.m_mainChaoIcon != null)
		{
			if (this.m_ranker.mainChaoId >= 0)
			{
				this.m_mainChaoIcon.gameObject.SetActive(true);
			}
			else
			{
				this.m_mainChaoIcon.gameObject.SetActive(false);
			}
		}
		if (this.m_subChaoIcon != null)
		{
			if (this.m_ranker.subChaoId >= 0)
			{
				this.m_subChaoIcon.gameObject.SetActive(true);
			}
			else
			{
				this.m_subChaoIcon.gameObject.SetActive(false);
			}
		}
		this.m_faceTexture.alpha = 0f;
		this.LoadImage();
		if (this.m_lvLabel != null)
		{
			string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_LevelNumber").text;
			this.m_lvLabel.text = text.Replace("{PARAM}", this.m_ranker.charaLevel.ToString());
		}
		this.m_friendIconSprite.spriteName = RankingUtil.GetFriendIconSpriteName(this.m_ranker);
		if (this.m_ranker.charaType != CharaType.UNKNOWN)
		{
			if (AtlasManager.Instance != null)
			{
				AtlasManager.Instance.ReplacePlayerAtlasForRaidResult(this.m_charaSprite.atlas);
			}
			this.m_charaSprite.spriteName = "ui_tex_player_set_" + CharaTypeUtil.GetCharaSpriteNameSuffix(this.m_ranker.charaType);
		}
		if (this.m_chao1BgSprite != null)
		{
			if (this.m_ranker.mainChaoId == -1)
			{
				this.m_chao1BgSprite.gameObject.SetActive(false);
			}
			else
			{
				this.m_chao1BgSprite.gameObject.SetActive(true);
				this.m_chao1BgSprite.spriteName = "ui_ranking_scroll_char_bg_" + this.m_ranker.mainChaoRarity;
			}
		}
		if (this.m_chao2BgSprite != null)
		{
			if (this.m_ranker.subChaoId == -1)
			{
				this.m_chao2BgSprite.gameObject.SetActive(false);
			}
			else
			{
				this.m_chao2BgSprite.gameObject.SetActive(true);
				this.m_chao2BgSprite.spriteName = "ui_ranking_scroll_char_bg_S" + this.m_ranker.subChaoRarity;
			}
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "ranking_set");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "event_ui");
		if (gameObject2 != null)
		{
			gameObject2.SetActive(true);
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject2, "Lbl_event_score");
			if (uilabel != null)
			{
				uilabel.text = HudUtility.GetFormatNumString<long>((long)desiredState.NumBeatedEnterprise);
			}
		}
		this.m_scodeLabel.gameObject.SetActive(false);
		this.m_nameLabel.text = this.m_ranker.userName;
		this.m_leagueIcon.spriteName = RankingUtil.GetLeagueIconName((LeagueType)this.m_ranker.leagueIndex);
		this.m_leagueIcon2.spriteName = RankingUtil.GetLeagueIconName2((LeagueType)this.m_ranker.leagueIndex);
		UIButtonOffset uibuttonOffset = GameObjectUtil.FindChildGameObjectComponent<UIButtonOffset>(base.gameObject, "Btn_ranking_top");
		if (uibuttonOffset != null)
		{
			uibuttonOffset.enabled = false;
		}
		UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_ranking_top");
		if (uibuttonMessage != null)
		{
			uibuttonMessage.enabled = false;
		}
	}

	// Token: 0x060026D1 RID: 9937 RVA: 0x000EDB5C File Offset: 0x000EBD5C
	public void SetMyRanker(bool myCell)
	{
		this.m_myCell = myCell;
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "Btn_ranking_top");
		if (uisprite != null)
		{
			if (uisprite.color.r < 1f || uisprite.color.g < 1f || uisprite.color.b < 1f)
			{
				uisprite.color = new Color(1f, 1f, 1f, 1f);
			}
			uisprite.spriteName = ((!myCell) ? "ui_ranking_bg_8" : "ui_ranking_bg_8m");
		}
		for (int i = 0; i < this.m_myRanker.Length; i++)
		{
			if (this.m_myRanker[i] != null)
			{
				this.m_myRanker[i].enabled = myCell;
			}
		}
		if (this.m_mainChaoTex != null)
		{
			this.m_mainChaoTex.depth += 50;
		}
		if (this.m_subChaoTex != null)
		{
			this.m_subChaoTex.depth += 50;
		}
		if (myCell)
		{
			this.m_isImgLoad = false;
			this.LoadImage();
		}
	}

	// Token: 0x060026D2 RID: 9938 RVA: 0x000EDCA8 File Offset: 0x000EBEA8
	public void UpdateSendChallenge(string id)
	{
		if (base.gameObject.activeSelf && this.m_ranker != null && this.m_ranker.id == id)
		{
			this.m_ranker.isSentEnergy = true;
			this.m_friendIconSprite.spriteName = RankingUtil.GetFriendIconSpriteName(this.m_ranker);
		}
	}

	// Token: 0x060026D3 RID: 9939 RVA: 0x000EDD08 File Offset: 0x000EBF08
	private void LoadImage()
	{
		if (this.m_ranker != null && !this.m_isImgLoad && (this.m_myCell || this.m_ranker.isFriend))
		{
			this.m_faceTexture.mainTexture = RankingUtil.GetProfilePictureTexture(this.m_ranker, delegate(Texture2D _faceTexture)
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

	// Token: 0x060026D4 RID: 9940 RVA: 0x000EDD98 File Offset: 0x000EBF98
	private void OnClickRankingScroll()
	{
		if (this.m_ranker.userDataType == RankingUtil.UserDataType.RANK_UP)
		{
			return;
		}
		if (Mathf.Abs(ui_ranking_scroll.s_updateLastTime - Time.realtimeSinceStartup) > 1.5f)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			ranking_window.Open(base.gameObject.transform.root.gameObject, this.m_scoreType, this.m_rankerType, this.m_ranker, this.m_sendBtnDisable || this.m_ranker.isMy);
		}
	}

	// Token: 0x04002308 RID: 8968
	public const float IMAGE_DELAY = 0.2f;

	// Token: 0x04002309 RID: 8969
	private static float s_updateLastTime;

	// Token: 0x0400230A RID: 8970
	[SerializeField]
	private UITexture m_faceTexture;

	// Token: 0x0400230B RID: 8971
	[SerializeField]
	private UISprite m_rankingSprite;

	// Token: 0x0400230C RID: 8972
	[SerializeField]
	private UILabel m_rankingLabel;

	// Token: 0x0400230D RID: 8973
	[SerializeField]
	private UISprite m_scoreRankIconSprite;

	// Token: 0x0400230E RID: 8974
	[SerializeField]
	private UISprite m_friendIconSprite;

	// Token: 0x0400230F RID: 8975
	[SerializeField]
	private UISprite m_charaSprite;

	// Token: 0x04002310 RID: 8976
	[SerializeField]
	private UISprite m_mainChaoIcon;

	// Token: 0x04002311 RID: 8977
	[SerializeField]
	private UISprite m_subChaoIcon;

	// Token: 0x04002312 RID: 8978
	[SerializeField]
	private UISprite m_chao1BgSprite;

	// Token: 0x04002313 RID: 8979
	[SerializeField]
	private UISprite m_chao2BgSprite;

	// Token: 0x04002314 RID: 8980
	[SerializeField]
	private UILabel m_nameLabel;

	// Token: 0x04002315 RID: 8981
	[SerializeField]
	private UILabel m_scodeLabel;

	// Token: 0x04002316 RID: 8982
	[SerializeField]
	private UIDragPanelContents m_dragPanelContents;

	// Token: 0x04002317 RID: 8983
	[SerializeField]
	private TweenColor[] m_myRanker = new TweenColor[2];

	// Token: 0x04002318 RID: 8984
	[SerializeField]
	private UISprite m_leagueIcon;

	// Token: 0x04002319 RID: 8985
	[SerializeField]
	private UISprite m_leagueIcon2;

	// Token: 0x0400231A RID: 8986
	private RankingUtil.RankingScoreType m_scoreType;

	// Token: 0x0400231B RID: 8987
	private RankingUtil.RankingRankerType m_rankerType;

	// Token: 0x0400231C RID: 8988
	private RankingUtil.Ranker m_ranker;

	// Token: 0x0400231D RID: 8989
	private UILabel m_lvLabel;

	// Token: 0x0400231E RID: 8990
	private bool m_myCell;

	// Token: 0x0400231F RID: 8991
	private bool m_isImgLoad;

	// Token: 0x04002320 RID: 8992
	private bool m_sendBtnDisable;

	// Token: 0x04002321 RID: 8993
	private int m_startCount;

	// Token: 0x04002322 RID: 8994
	private BoxCollider m_boxCollider;

	// Token: 0x04002323 RID: 8995
	private ui_ranking_scroll_dummy m_dummy;

	// Token: 0x04002324 RID: 8996
	private float m_updTime;

	// Token: 0x04002325 RID: 8997
	private bool m_isDraw;

	// Token: 0x04002326 RID: 8998
	private bool m_isDrawChao;

	// Token: 0x04002327 RID: 8999
	private float m_chaoDrawDelay;

	// Token: 0x04002328 RID: 9000
	private float m_nfPanelTime;

	// Token: 0x04002329 RID: 9001
	private GameObject m_rankingSet;

	// Token: 0x0400232A RID: 9002
	private UITexture m_mainChaoTex;

	// Token: 0x0400232B RID: 9003
	private UITexture m_subChaoTex;

	// Token: 0x0400232C RID: 9004
	private float m_mainChaoTexTime;

	// Token: 0x0400232D RID: 9005
	private float m_subChaoTexTime;
}
