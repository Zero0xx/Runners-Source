using System;
using System.Diagnostics;
using AnimationOrTween;
using Message;
using UnityEngine;

// Token: 0x02000349 RID: 841
public class HudCaution : MonoBehaviour
{
	// Token: 0x170003C4 RID: 964
	// (get) Token: 0x060018FC RID: 6396 RVA: 0x00090444 File Offset: 0x0008E644
	public static HudCaution Instance
	{
		get
		{
			return HudCaution.instance;
		}
	}

	// Token: 0x060018FD RID: 6397 RVA: 0x0009044C File Offset: 0x0008E64C
	private void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x060018FE RID: 6398 RVA: 0x00090454 File Offset: 0x0008E654
	private void OnDestroy()
	{
		if (HudCaution.instance == this)
		{
			HudCaution.instance = null;
		}
	}

	// Token: 0x060018FF RID: 6399 RVA: 0x0009046C File Offset: 0x0008E66C
	private void SetInstance()
	{
		if (HudCaution.instance == null)
		{
			HudCaution.instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001900 RID: 6400 RVA: 0x000904A0 File Offset: 0x0008E6A0
	private void Start()
	{
		this.m_playerDelayWorldToScreenPoint = new HudCaution.DelayWorldToScreenPoint();
		this.m_uiCamera = null;
		this.m_slider = null;
		this.m_boostSlider = null;
		if (this.m_enemyAnchorGameObject != null)
		{
			this.m_enemyAnchorGameObject.SetActive(false);
		}
		if (EventManager.Instance != null && !EventManager.Instance.IsRaidBossStage() && this.m_playerAnchorGameObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_playerAnchorGameObject, "gp_bit_WispBoost");
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001901 RID: 6401 RVA: 0x00090540 File Offset: 0x0008E740
	private void Update()
	{
		if (this.m_stageItemManager == null)
		{
			this.m_stageItemManager = StageItemManager.Instance;
		}
		if (this.m_uiCamera == null)
		{
			this.m_uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		if (this.m_gameMainCamera == null)
		{
			this.m_gameMainCamera = GameObjectUtil.FindGameObjectComponent<Camera>("GameMainCamera");
		}
		Vector2 screenPositon = this.m_playerDelayWorldToScreenPoint.GetScreenPositon(GameObject.FindWithTag("Player"), this.m_gameMainCamera, this.m_uiCamera);
		this.m_playerAnchorGameObject.transform.position = new Vector3(screenPositon.x, screenPositon.y, 0f);
		if (this.m_slider != null && this.m_stageItemManager != null)
		{
			float cautionItemTimeRate = this.m_stageItemManager.CautionItemTimeRate;
			if (cautionItemTimeRate > 0f)
			{
				this.m_slider.value = cautionItemTimeRate;
			}
			else
			{
				this.m_slider.value = 0f;
				this.m_slider = null;
				ActiveAnimation.Play(this.m_animInfos[12].m_animation, this.m_animInfos[12].m_clipName, Direction.Reverse);
			}
		}
		if (this.m_boostSlider != null && this.m_bossParameter != null)
		{
			float boostRatio = this.m_bossParameter.BoostRatio;
			if (boostRatio > 0f)
			{
				this.m_boostSlider.value = boostRatio;
			}
			else
			{
				this.m_boostSlider.value = 0f;
				this.m_boostSlider = null;
				ActiveAnimation.Play(this.m_animInfos[21].m_animation, this.m_animInfos[21].m_clipName, Direction.Reverse);
			}
		}
	}

	// Token: 0x06001902 RID: 6402 RVA: 0x00090704 File Offset: 0x0008E904
	public void SetBossWord(bool bossStage)
	{
		string spriteName = (!bossStage) ? "ui_gp_bit_word_chancetime" : "ui_gp_bit_word_attention";
		if (this.m_bossAttenion != null)
		{
			this.m_bossAttenion.spriteName = spriteName;
		}
		if (this.m_raidBossAttenion != null)
		{
			this.m_raidBossAttenion.spriteName = spriteName;
		}
	}

	// Token: 0x06001903 RID: 6403 RVA: 0x00090764 File Offset: 0x0008E964
	public void SetCaution(MsgCaution msg)
	{
		if (msg.m_cautionType < HudCaution.Type.COUNT)
		{
			HudCaution.AnimInfo animInfo = this.m_animInfos[(int)msg.m_cautionType];
			if (animInfo.m_animation != null && !string.IsNullOrEmpty(animInfo.m_clipName))
			{
				HudCaution.Type cautionType;
				if (animInfo.m_label != null)
				{
					cautionType = msg.m_cautionType;
					if (cautionType != HudCaution.Type.COMBOITEM_BONUS_N)
					{
						if (cautionType == HudCaution.Type.GET_TIMER)
						{
							string text = string.Format(animInfo.m_labelStringFormat, msg.m_second);
							if (!animInfo.m_label.enabled)
							{
								animInfo.m_label.enabled = true;
							}
							animInfo.m_label.text = text;
							goto IL_179;
						}
						if (cautionType != HudCaution.Type.BONUS_N)
						{
							if (cautionType != HudCaution.Type.GET_ITEM)
							{
								if (!string.IsNullOrEmpty(animInfo.m_labelStringFormat))
								{
									string text2 = string.Format(animInfo.m_labelStringFormat, msg.m_number);
									animInfo.m_label.text = text2;
								}
								goto IL_179;
							}
							if (animInfo.m_label.enabled)
							{
								animInfo.m_label.enabled = false;
							}
							goto IL_179;
						}
					}
					if (!string.IsNullOrEmpty(animInfo.m_labelStringFormat))
					{
						string text3 = string.Format(animInfo.m_labelStringFormat, msg.m_number);
						if (msg.m_flag)
						{
							animInfo.m_label.text = "[FF0000]" + text3;
						}
						else
						{
							animInfo.m_label.text = text3;
						}
					}
				}
				IL_179:
				float num = 0f;
				cautionType = msg.m_cautionType;
				if (cautionType != HudCaution.Type.COUNTDOWN)
				{
					if (cautionType == HudCaution.Type.WISPBOOST)
					{
						if (animInfo.m_slider != null)
						{
							this.m_bossParameter = msg.m_bossParam;
							if (this.m_bossParameter != null)
							{
								if (animInfo.m_sprite != null && this.m_bossParameter.BoostLevel != WispBoostLevel.NONE)
								{
									animInfo.m_sprite.spriteName = "ui_event_gp_gauge_power_bg_" + (int)this.m_bossParameter.BoostLevel;
								}
								if (animInfo.m_sprite2 != null)
								{
									GameObject parent = GameObjectUtil.FindChildGameObject(base.gameObject, "gp_bit_WispBoost");
									RaidBossBoostGagueColor raidBossBoostGagueColor = GameObjectUtil.FindChildGameObjectComponent<RaidBossBoostGagueColor>(parent, "img_gauge");
									Color color = animInfo.m_sprite2.color;
									switch (this.m_bossParameter.BoostLevel)
									{
									case WispBoostLevel.LEVEL1:
										color = raidBossBoostGagueColor.Level1;
										break;
									case WispBoostLevel.LEVEL2:
										color = raidBossBoostGagueColor.Level2;
										break;
									case WispBoostLevel.LEVEL3:
										color = raidBossBoostGagueColor.Level3;
										break;
									}
									animInfo.m_sprite2.color = color;
								}
								num = this.m_bossParameter.BoostRatio;
								if (num > 0f)
								{
									this.m_boostSlider = animInfo.m_slider;
								}
								if (this.m_boostSlider != null)
								{
									this.m_boostSlider.value = num;
								}
							}
						}
					}
				}
				else if (animInfo.m_slider != null)
				{
					num = msg.m_rate;
					if (num > 0f)
					{
						this.m_slider = animInfo.m_slider;
					}
					if (this.m_slider != null)
					{
						this.m_slider.value = num;
					}
				}
				if (!(animInfo.m_slider != null) || num != 0f)
				{
					cautionType = msg.m_cautionType;
					if (cautionType != HudCaution.Type.COMBO_N)
					{
						if (cautionType != HudCaution.Type.BONUS_N)
						{
							if (cautionType == HudCaution.Type.NO_RING)
							{
								animInfo.m_animation.playAutomatically = true;
								animInfo.m_animation.cullingType = AnimationCullingType.AlwaysAnimate;
								animInfo.m_animation.Rewind();
								animInfo.m_animation.Sample();
								animInfo.m_animation.Play();
								goto IL_4AA;
							}
							if (cautionType != HudCaution.Type.COMBOITEM_BONUS_N)
							{
								this.SetAnimPlay(animInfo);
								goto IL_4AA;
							}
						}
						this.SetAnimPlay(animInfo);
						float length = animInfo.m_animation[animInfo.m_clipName].length;
						animInfo.m_animation[animInfo.m_clipName].time = length * 0.01f;
					}
					else
					{
						this.SetAnimPlay(animInfo);
						float length2 = animInfo.m_animation[animInfo.m_clipName].length;
						if (msg.m_flag)
						{
							animInfo.m_animation[animInfo.m_clipName].time = length2 * 0.05f;
							animInfo.m_animation.Sample();
							animInfo.m_animation.Stop();
						}
						else
						{
							animInfo.m_animation[animInfo.m_clipName].time = length2 * 0.01f;
						}
					}
				}
				IL_4AA:
				cautionType = msg.m_cautionType;
				if (cautionType != HudCaution.Type.GET_ITEM)
				{
					if (cautionType == HudCaution.Type.GET_TIMER)
					{
						if (animInfo.m_sprite != null)
						{
							animInfo.m_sprite.spriteName = "ui_cmn_icon_item_timer_" + msg.m_number;
						}
					}
				}
				else if (animInfo.m_sprite != null)
				{
					animInfo.m_sprite.spriteName = "ui_cmn_icon_item_" + (int)msg.m_itemType;
				}
			}
		}
	}

	// Token: 0x06001904 RID: 6404 RVA: 0x00090CA8 File Offset: 0x0008EEA8
	private void SetAnimPlay(HudCaution.AnimInfo animInfo)
	{
		if (animInfo != null)
		{
			if (animInfo.m_animation != null && animInfo.m_animation.gameObject != null && !animInfo.m_animation.gameObject.activeSelf)
			{
				animInfo.m_animation.gameObject.SetActive(true);
			}
			animInfo.m_animation.Rewind(animInfo.m_clipName);
			DisableCondition disableCondition = (!animInfo.m_finishDisable) ? DisableCondition.DoNotDisable : DisableCondition.DisableAfterForward;
			ActiveAnimation.Play(animInfo.m_animation, animInfo.m_clipName, Direction.Forward, EnableCondition.DoNothing, disableCondition, false);
		}
	}

	// Token: 0x06001905 RID: 6405 RVA: 0x00090D44 File Offset: 0x0008EF44
	public void SetInvisibleCaution(MsgCaution msg)
	{
		if (msg.m_cautionType < HudCaution.Type.COUNT)
		{
			HudCaution.AnimInfo animInfo = this.m_animInfos[(int)msg.m_cautionType];
			if (animInfo.m_animation != null && !string.IsNullOrEmpty(animInfo.m_clipName))
			{
				HudCaution.Type cautionType = msg.m_cautionType;
				if (cautionType != HudCaution.Type.NO_RING)
				{
					animInfo.m_animation.Play();
					float length = animInfo.m_animation[animInfo.m_clipName].length;
					animInfo.m_animation[animInfo.m_clipName].time = length;
					animInfo.m_animation.Sample();
					animInfo.m_animation.Stop();
				}
				else
				{
					animInfo.m_animation.playAutomatically = false;
					animInfo.m_animation.cullingType = AnimationCullingType.BasedOnRenderers;
					animInfo.m_animation.Rewind();
					animInfo.m_animation.Sample();
					animInfo.m_animation.Stop();
				}
			}
		}
	}

	// Token: 0x06001906 RID: 6406 RVA: 0x00090E34 File Offset: 0x0008F034
	public void SetMsgExitStage(MsgExitStage msg)
	{
		base.enabled = false;
	}

	// Token: 0x06001907 RID: 6407 RVA: 0x00090E40 File Offset: 0x0008F040
	[Conditional("DEBUG_INFO")]
	private static void DebugLog(string s)
	{
		global::Debug.Log("@ms " + s);
	}

	// Token: 0x06001908 RID: 6408 RVA: 0x00090E54 File Offset: 0x0008F054
	[Conditional("DEBUG_INFO")]
	private static void DebugLogWarning(string s)
	{
		global::Debug.LogWarning("@ms " + s);
	}

	// Token: 0x04001657 RID: 5719
	private static HudCaution instance;

	// Token: 0x04001658 RID: 5720
	[SerializeField]
	private GameObject m_playerAnchorGameObject;

	// Token: 0x04001659 RID: 5721
	[SerializeField]
	private GameObject m_enemyAnchorGameObject;

	// Token: 0x0400165A RID: 5722
	[SerializeField]
	private HudCaution.AnimInfo[] m_animInfos = new HudCaution.AnimInfo[26];

	// Token: 0x0400165B RID: 5723
	[SerializeField]
	private UISprite m_bossAttenion;

	// Token: 0x0400165C RID: 5724
	[SerializeField]
	private UISprite m_raidBossAttenion;

	// Token: 0x0400165D RID: 5725
	private Camera m_gameMainCamera;

	// Token: 0x0400165E RID: 5726
	private StageItemManager m_stageItemManager;

	// Token: 0x0400165F RID: 5727
	private ObjBossEventBossParameter m_bossParameter;

	// Token: 0x04001660 RID: 5728
	private HudCaution.DelayWorldToScreenPoint m_playerDelayWorldToScreenPoint;

	// Token: 0x04001661 RID: 5729
	private GameObject m_enemyGameObject;

	// Token: 0x04001662 RID: 5730
	private Camera m_uiCamera;

	// Token: 0x04001663 RID: 5731
	private UISlider m_slider;

	// Token: 0x04001664 RID: 5732
	private UISlider m_boostSlider;

	// Token: 0x0200034A RID: 842
	public enum Type
	{
		// Token: 0x04001666 RID: 5734
		GO,
		// Token: 0x04001667 RID: 5735
		SPEEDUP,
		// Token: 0x04001668 RID: 5736
		BOSS,
		// Token: 0x04001669 RID: 5737
		COMBO_N,
		// Token: 0x0400166A RID: 5738
		TRICK0,
		// Token: 0x0400166B RID: 5739
		TRICK1,
		// Token: 0x0400166C RID: 5740
		TRICK2,
		// Token: 0x0400166D RID: 5741
		TRICK3,
		// Token: 0x0400166E RID: 5742
		TRICK4,
		// Token: 0x0400166F RID: 5743
		BONUS_N,
		// Token: 0x04001670 RID: 5744
		COMBO_BONUS_N,
		// Token: 0x04001671 RID: 5745
		TRICK_BONUS_N,
		// Token: 0x04001672 RID: 5746
		COUNTDOWN,
		// Token: 0x04001673 RID: 5747
		MAP_BOSS_CLEAR,
		// Token: 0x04001674 RID: 5748
		MAP_BOSS_FAILED,
		// Token: 0x04001675 RID: 5749
		STAGE_OUT,
		// Token: 0x04001676 RID: 5750
		STAGE_IN,
		// Token: 0x04001677 RID: 5751
		ZERO_POINT_TEST,
		// Token: 0x04001678 RID: 5752
		GET_ITEM,
		// Token: 0x04001679 RID: 5753
		DESTROY_ENEMY,
		// Token: 0x0400167A RID: 5754
		NO_RING,
		// Token: 0x0400167B RID: 5755
		WISPBOOST,
		// Token: 0x0400167C RID: 5756
		EVENTBOSS,
		// Token: 0x0400167D RID: 5757
		EXTREMEMODE,
		// Token: 0x0400167E RID: 5758
		COMBOITEM_BONUS_N,
		// Token: 0x0400167F RID: 5759
		GET_TIMER,
		// Token: 0x04001680 RID: 5760
		COUNT
	}

	// Token: 0x0200034B RID: 843
	[Serializable]
	private class AnimInfo
	{
		// Token: 0x04001681 RID: 5761
		[SerializeField]
		public Animation m_animation;

		// Token: 0x04001682 RID: 5762
		[SerializeField]
		public string m_clipName;

		// Token: 0x04001683 RID: 5763
		[SerializeField]
		public UILabel m_label;

		// Token: 0x04001684 RID: 5764
		[SerializeField]
		public string m_labelStringFormat;

		// Token: 0x04001685 RID: 5765
		[SerializeField]
		public UISlider m_slider;

		// Token: 0x04001686 RID: 5766
		[SerializeField]
		public UISprite m_sprite;

		// Token: 0x04001687 RID: 5767
		[SerializeField]
		public UISprite m_sprite2;

		// Token: 0x04001688 RID: 5768
		[SerializeField]
		public bool m_finishDisable;
	}

	// Token: 0x0200034C RID: 844
	private class DelayWorldToScreenPoint
	{
		// Token: 0x0600190B RID: 6411 RVA: 0x00090E78 File Offset: 0x0008F078
		public Vector2 GetScreenPositon(GameObject targetGameObject, Camera targetCamera, Camera uiCamera)
		{
			Vector2 result = new Vector2(-100f, -100f);
			if (targetGameObject != null && targetCamera != null && uiCamera != null)
			{
				Vector3 beforeTargetPosition = this.m_beforeTargetPosition;
				this.m_beforeTargetPosition = targetGameObject.transform.localPosition;
				Vector3 position = targetCamera.WorldToScreenPoint(beforeTargetPosition);
				position.z = 0f;
				result = uiCamera.ScreenToWorldPoint(position);
			}
			return result;
		}

		// Token: 0x04001689 RID: 5769
		private Vector3 m_beforeTargetPosition;
	}
}
