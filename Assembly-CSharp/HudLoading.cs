using System;
using System.Diagnostics;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020003BB RID: 955
public class HudLoading : MonoBehaviour
{
	// Token: 0x06001BCF RID: 7119 RVA: 0x000A543C File Offset: 0x000A363C
	private void Awake()
	{
		HudLoading.s_instance = this;
	}

	// Token: 0x06001BD0 RID: 7120 RVA: 0x000A5444 File Offset: 0x000A3644
	public void Play(string clipName, EventDelegate.Callback callback)
	{
		ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_screenAnimation, clipName, Direction.Forward);
		if (activeAnimation != null)
		{
			EventDelegate.Add(activeAnimation.onFinished, callback, true);
		}
	}

	// Token: 0x06001BD1 RID: 7121 RVA: 0x000A5478 File Offset: 0x000A3678
	private void SetView()
	{
		bool flag = StageModeManager.Instance != null && StageModeManager.Instance.IsQuickMode();
		this.m_optionTutorial = false;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Anchor_1_TL");
		if (gameObject != null)
		{
			bool flag2 = false;
			if (EventManager.Instance != null)
			{
				if (EventManager.Instance.Type == EventManager.EventType.QUICK)
				{
					flag2 = flag;
				}
				else if (EventManager.Instance.Type == EventManager.EventType.BGM)
				{
					EventStageData stageData = EventManager.Instance.GetStageData();
					if (stageData != null)
					{
						flag2 = ((!flag) ? stageData.IsEndlessModeBGM() : stageData.IsQuickModeBGM());
					}
				}
			}
			if (flag2 && AtlasManager.Instance != null)
			{
				AtlasManager.Instance.ReplaceAtlasForStage();
				gameObject.SetActive(true);
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
		GameObject gameObject2 = GameObject.Find("LoadingInfo");
		if (gameObject2 != null)
		{
			LoadingInfo component = gameObject2.GetComponent<LoadingInfo>();
			if (component != null)
			{
				this.m_titleLabel.text = component.GetInfo().m_titleText;
				this.m_tipsLabel.text = component.GetInfo().m_mainText;
				if (!flag && component.GetInfo().m_texture != null)
				{
					this.m_texture.mainTexture = component.GetInfo().m_texture;
				}
				this.m_optionTutorial = component.GetInfo().m_optionTutorial;
			}
			UnityEngine.Object.Destroy(gameObject2);
		}
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			int mainChaoID = instance.PlayerData.MainChaoID;
			if (mainChaoID >= 0)
			{
				this.m_Chao1GameObject.SetActive(true);
				this.m_chao1BonusNameLabel.text = HudUtility.GetChaoLoadingAbilityText(mainChaoID);
				string chaoSPLoadingAbilityText = HudUtility.GetChaoSPLoadingAbilityText(mainChaoID);
				if (string.IsNullOrEmpty(chaoSPLoadingAbilityText))
				{
					this.m_Chao1SPGameObject.SetActive(false);
				}
				else
				{
					this.m_Chao1SPGameObject.SetActive(true);
					this.m_chao1SPBonusNameLabel.text = chaoSPLoadingAbilityText;
				}
			}
			else
			{
				this.m_Chao1GameObject.SetActive(false);
				this.m_Chao1SPGameObject.SetActive(false);
			}
			HudUtility.SetChaoTexture(this.m_chao1Texture, mainChaoID, true);
			int subChaoID = instance.PlayerData.SubChaoID;
			if (subChaoID >= 0)
			{
				this.m_Chao2GameObject.SetActive(true);
				this.m_chao2BonusNameLabel.text = HudUtility.GetChaoLoadingAbilityText(subChaoID);
				string chaoSPLoadingAbilityText2 = HudUtility.GetChaoSPLoadingAbilityText(subChaoID);
				if (string.IsNullOrEmpty(chaoSPLoadingAbilityText2))
				{
					this.m_Chao2SPGameObject.SetActive(false);
				}
				else
				{
					this.m_Chao2SPGameObject.SetActive(true);
					this.m_chao2SPBonusNameLabel.text = chaoSPLoadingAbilityText2;
				}
			}
			else
			{
				this.m_Chao2GameObject.SetActive(false);
				this.m_Chao2SPGameObject.SetActive(false);
			}
			HudUtility.SetChaoTexture(this.m_chao2Texture, subChaoID, true);
		}
		StageAbilityManager stageAbilityManager = UnityEngine.Object.FindObjectOfType<StageAbilityManager>();
		if (stageAbilityManager != null)
		{
			stageAbilityManager.RecalcAbilityVaue();
			int chaoCount = stageAbilityManager.GetChaoCount();
			if (chaoCount > 0 && !this.m_optionTutorial)
			{
				this.m_ChaoCountGameObject.SetActive(true);
				this.m_chaoCountLabel.text = chaoCount.ToString();
				this.m_chaoCountLabel2.text = chaoCount.ToString();
				this.m_chaoCountScoreLabel.text = HudUtility.GetChaoCountBonusText(stageAbilityManager.GetChaoCountBonusValue());
			}
			else
			{
				this.m_ChaoCountGameObject.SetActive(false);
				this.m_chaoCountLabel.text = string.Empty;
				this.m_chaoCountLabel2.text = string.Empty;
				this.m_chaoCountScoreLabel.text = string.Empty;
			}
			if (this.m_optionTutorial)
			{
				this.m_campaignBonusGameObject.SetActive(false);
			}
			else
			{
				float num = stageAbilityManager.CampaignValueRate;
				if (num != 0f)
				{
					num *= 100f;
					this.m_campaignBonus.text = "＋" + num.ToString() + "％";
					if (AtlasManager.Instance != null)
					{
						UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_campaignBonusGameObject, "img_word_campaign_bonus");
						if (uisprite != null)
						{
							AtlasManager.Instance.ReplaceAtlasForLoading(uisprite.atlas);
						}
					}
					this.m_campaignBonusGameObject.SetActive(true);
				}
				else
				{
					this.m_campaignBonusGameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06001BD2 RID: 7122 RVA: 0x000A58D4 File Offset: 0x000A3AD4
	public static void StartScreen(Action onFinishedAnimationAction = null)
	{
		if (HudLoading.s_instance != null)
		{
			HudLoading.s_instance.SetView();
			HudLoading.s_instance.m_loadingGameObject.SetActive(true);
			HudLoading.s_instance.m_onFinishedStartAnimationAction = onFinishedAnimationAction;
			HudLoading.s_instance.Play("ui_load_intro_Anim", new EventDelegate.Callback(HudLoading.s_instance.OnFinishedStartAnimation));
		}
	}

	// Token: 0x06001BD3 RID: 7123 RVA: 0x000A5938 File Offset: 0x000A3B38
	public static void EndScreen(Action onFinishedAnimationAction = null)
	{
		if (HudLoading.s_instance != null)
		{
			HudLoading.s_instance.m_onFinishedEndAnimationAction = onFinishedAnimationAction;
			HudLoading.s_instance.Play("ui_load_outro_Anim", new EventDelegate.Callback(HudLoading.s_instance.OnFinishedEndAnimation));
		}
	}

	// Token: 0x06001BD4 RID: 7124 RVA: 0x000A5980 File Offset: 0x000A3B80
	private void OnFinishedStartAnimation()
	{
		if (this.m_onFinishedStartAnimationAction != null)
		{
			this.m_onFinishedStartAnimationAction();
		}
	}

	// Token: 0x06001BD5 RID: 7125 RVA: 0x000A5998 File Offset: 0x000A3B98
	private void OnFinishedEndAnimation()
	{
		if (this.m_onFinishedEndAnimationAction != null)
		{
			this.m_onFinishedEndAnimationAction();
		}
		UnityEngine.Object.Destroy(base.gameObject);
		EventUtility.DestroyLoadingFaceTexture();
		HudLoading.s_instance = null;
	}

	// Token: 0x06001BD6 RID: 7126 RVA: 0x000A59D4 File Offset: 0x000A3BD4
	[Conditional("DEBUG_INFO")]
	private static void DebugLog(string s)
	{
		global::Debug.Log("@ms " + s);
	}

	// Token: 0x06001BD7 RID: 7127 RVA: 0x000A59E8 File Offset: 0x000A3BE8
	[Conditional("DEBUG_INFO")]
	private static void DebugLogWarning(string s)
	{
		global::Debug.LogWarning("@ms " + s);
	}

	// Token: 0x04001982 RID: 6530
	[SerializeField]
	private GameObject m_loadingGameObject;

	// Token: 0x04001983 RID: 6531
	[SerializeField]
	private Animation m_screenAnimation;

	// Token: 0x04001984 RID: 6532
	[SerializeField]
	private UILabel m_titleLabel;

	// Token: 0x04001985 RID: 6533
	[SerializeField]
	private UILabel m_tipsLabel;

	// Token: 0x04001986 RID: 6534
	[SerializeField]
	private UITexture m_texture;

	// Token: 0x04001987 RID: 6535
	[SerializeField]
	private GameObject m_Chao1GameObject;

	// Token: 0x04001988 RID: 6536
	[SerializeField]
	private UILabel m_chao1BonusNameLabel;

	// Token: 0x04001989 RID: 6537
	[SerializeField]
	private GameObject m_Chao1SPGameObject;

	// Token: 0x0400198A RID: 6538
	[SerializeField]
	private UILabel m_chao1SPBonusNameLabel;

	// Token: 0x0400198B RID: 6539
	[SerializeField]
	private UITexture m_chao1Texture;

	// Token: 0x0400198C RID: 6540
	[SerializeField]
	private GameObject m_Chao2GameObject;

	// Token: 0x0400198D RID: 6541
	[SerializeField]
	private UILabel m_chao2BonusNameLabel;

	// Token: 0x0400198E RID: 6542
	[SerializeField]
	private GameObject m_Chao2SPGameObject;

	// Token: 0x0400198F RID: 6543
	[SerializeField]
	private UILabel m_chao2SPBonusNameLabel;

	// Token: 0x04001990 RID: 6544
	[SerializeField]
	private UITexture m_chao2Texture;

	// Token: 0x04001991 RID: 6545
	[SerializeField]
	private GameObject m_ChaoCountGameObject;

	// Token: 0x04001992 RID: 6546
	[SerializeField]
	private UILabel m_chaoCountLabel;

	// Token: 0x04001993 RID: 6547
	[SerializeField]
	private UILabel m_chaoCountLabel2;

	// Token: 0x04001994 RID: 6548
	[SerializeField]
	private UILabel m_chaoCountScoreLabel;

	// Token: 0x04001995 RID: 6549
	[SerializeField]
	private GameObject m_campaignBonusGameObject;

	// Token: 0x04001996 RID: 6550
	[SerializeField]
	private UILabel m_campaignBonus;

	// Token: 0x04001997 RID: 6551
	private static HudLoading s_instance;

	// Token: 0x04001998 RID: 6552
	private Action m_onFinishedStartAnimationAction;

	// Token: 0x04001999 RID: 6553
	private Action m_onFinishedEndAnimationAction;

	// Token: 0x0400199A RID: 6554
	private bool m_optionTutorial;
}
