using System;
using System.Collections.Generic;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x02000395 RID: 917
public class HudEventResultSpStage : HudEventResultParts
{
	// Token: 0x06001B15 RID: 6933 RVA: 0x000A03F8 File Offset: 0x0009E5F8
	public override void Init(GameObject resultRootObject, long beforeTotalPoint, HudEventResult.AnimationEndCallback callback)
	{
		this.m_resultRootObject = resultRootObject;
		this.m_beforeTotalPoint = beforeTotalPoint;
		this.m_callback = callback;
		this.m_timer = 0f;
		this.m_info = EventManager.Instance.SpecialStageInfo;
		if (this.m_info != null)
		{
			this.m_animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "EventResult_Anim");
			if (this.m_animation == null)
			{
				return;
			}
			this.m_hudEventQuota = this.m_resultRootObject.GetComponent<HudEventQuota>();
			if (this.m_hudEventQuota == null)
			{
				this.m_hudEventQuota = this.m_resultRootObject.AddComponent<HudEventQuota>();
			}
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_resultRootObject, "Lbl_object");
			if (uilabel != null)
			{
				StageScoreManager instance = StageScoreManager.Instance;
				if (instance != null)
				{
					this.m_score = new GameResultScoreInterporate();
					this.m_score.Setup(uilabel);
					this.m_score.SetLabelStartValue(instance.SpecialCrystal);
				}
			}
			UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_resultRootObject, "Lbl_total_object");
			if (uilabel2 != null)
			{
				this.m_totalScore = new GameResultScoreInterporate();
				this.m_totalScore.Setup(uilabel2);
				long labelStartValue = 0L;
				StageScoreManager instance2 = StageScoreManager.Instance;
				if (instance2 != null)
				{
					labelStartValue = this.m_info.totalPoint - instance2.FinalCountData.sp_crystal;
				}
				this.m_totalScore.SetLabelStartValue(labelStartValue);
			}
			HudEventResultSpStage.EventKind eventKind = HudEventResultSpStage.EventKind.SP_EVENT;
			EventManager instance3 = EventManager.Instance;
			if (instance3.Type == EventManager.EventType.COLLECT_OBJECT)
			{
				switch (instance3.CollectType)
				{
				case EventManager.CollectEventType.GET_ANIMALS:
					eventKind = HudEventResultSpStage.EventKind.ANIMAL_EVENT;
					break;
				case EventManager.CollectEventType.GET_RING:
					eventKind = HudEventResultSpStage.EventKind.RING_EVENT;
					break;
				case EventManager.CollectEventType.RUN_DISTANCE:
					eventKind = HudEventResultSpStage.EventKind.DISTANCE_EVENT;
					break;
				}
			}
			UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_resultRootObject, "ui_Lbl_word_object_get");
			if (uilabel3 != null)
			{
				TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Result", this.SpResultReplaceStringTable[(int)eventKind].CountText);
				if (eventKind == HudEventResultSpStage.EventKind.SP_EVENT)
				{
					string eventSpObjectName = HudUtility.GetEventSpObjectName();
					text.ReplaceTag("{PARAM_OBJ}", eventSpObjectName);
				}
				uilabel3.text = text.text;
			}
			UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_resultRootObject, "ui_Lbl_word_total_object_get");
			if (uilabel4 != null)
			{
				TextObject text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Result", this.SpResultReplaceStringTable[(int)eventKind].TotalText);
				if (eventKind == HudEventResultSpStage.EventKind.SP_EVENT)
				{
					string eventSpObjectName2 = HudUtility.GetEventSpObjectName();
					text2.ReplaceTag("{PARAM_OBJ}", eventSpObjectName2);
				}
				uilabel4.text = text2.text;
			}
			List<EventMission> list = null;
			bool currentClearMission = this.m_info.GetCurrentClearMission(this.m_beforeTotalPoint, out list, true);
			if (currentClearMission && list != null)
			{
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					string name = list[i].name;
					string formatNumString = HudUtility.GetFormatNumString<long>(list[i].point);
					int reward = list[i].reward;
					string text3 = string.Empty;
					ServerItem serverItem = new ServerItem((ServerItem.Id)list[i].reward);
					text3 = serverItem.serverItemName;
					int index = list[i].index;
					if (index > 1)
					{
						text3 = text3 + " × " + index;
					}
					bool isCleared = list[i].IsAttainment(this.m_info.totalPoint);
					QuotaInfo info = new QuotaInfo(name, formatNumString, reward, text3, isCleared);
					this.m_hudEventQuota.AddQuota(info);
				}
			}
			this.m_hudEventQuota.Setup(this.m_resultRootObject, this.m_animation, "ui_EventResult_spstage_mission_inout1_Anim", "ui_EventResult_spstage_mission_inout2_Anim");
		}
	}

	// Token: 0x06001B16 RID: 6934 RVA: 0x000A07A4 File Offset: 0x0009E9A4
	public override void PlayAnimation(HudEventResult.AnimType animType)
	{
		this.m_currentAnimType = animType;
		switch (animType)
		{
		case HudEventResult.AnimType.IN:
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_EventResult_spstage_intro_Anim", Direction.Forward, true);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.AnimationFinishCallback), true);
			break;
		}
		case HudEventResult.AnimType.IN_BONUS:
		{
			bool flag = false;
			bool flag2 = true;
			if (flag2)
			{
				Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(this.m_resultRootObject, "otomo_bonus_Anim");
				if (animation != null)
				{
					float num = StageAbilityManager.Instance.SpecialCrystalRate;
					num *= 100f;
					if (num > 0f)
					{
						animation.gameObject.SetActive(true);
						ActiveAnimation activeAnimation2 = ActiveAnimation.Play(animation, "ui_result_bonus_ev_Anim", Direction.Forward, true);
						EventDelegate.Add(activeAnimation2.onFinished, new EventDelegate.Callback(this.AnimationFinishCallback), true);
						UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(animation.gameObject, "Lbl_mileage_bonus_score");
						if (uilabel != null)
						{
							uilabel.text = num.ToString("F1") + "%";
						}
						if (this.m_score != null)
						{
							long startValue = 0L;
							long endValue = 0L;
							StageScoreManager instance = StageScoreManager.Instance;
							if (instance != null)
							{
								startValue = instance.SpecialCrystal;
								endValue = instance.FinalCountData.sp_crystal;
							}
							this.m_score.PlayStart(startValue, endValue, 0.5f);
							SoundManager.SePlay("sys_result_count", "SE");
						}
						flag = true;
					}
				}
			}
			if (!flag)
			{
				this.AnimationFinishCallback();
				this.m_waitTime = HudEventResultSpStage.WAIT_TIME;
			}
			else
			{
				this.m_waitTime = HudEventResultSpStage.WAIT_TIME_WITH_BONUS;
			}
			break;
		}
		case HudEventResult.AnimType.WAIT_ADD_COLLECT_OBJECT:
			this.m_timer = 0f;
			break;
		case HudEventResult.AnimType.ADD_COLLECT_OBJECT:
			if (this.m_totalScore != null)
			{
				long startValue2 = 0L;
				long totalPoint = this.m_info.totalPoint;
				StageScoreManager instance2 = StageScoreManager.Instance;
				if (instance2 != null)
				{
					startValue2 = this.m_info.totalPoint - instance2.FinalCountData.sp_crystal;
				}
				this.m_totalScore.PlayStart(startValue2, totalPoint, 0.5f);
				SoundManager.SePlay("sys_result_count", "SE");
			}
			break;
		case HudEventResult.AnimType.SHOW_QUOTA_LIST:
			if (this.m_hudEventQuota != null)
			{
				this.m_hudEventQuota.PlayStart(new HudEventQuota.PlayEndCallback(this.QuotaPlayEndCallback));
			}
			break;
		case HudEventResult.AnimType.OUT:
		{
			if (this.m_hudEventQuota != null)
			{
				this.m_hudEventQuota.PlayStop();
			}
			ActiveAnimation activeAnimation3 = ActiveAnimation.Play(this.m_animation, "ui_EventResult_spstage_outro_Anim", Direction.Forward, true);
			if (activeAnimation3 != null)
			{
				EventDelegate.Add(activeAnimation3.onFinished, new EventDelegate.Callback(this.AnimationFinishCallback), true);
			}
			break;
		}
		}
	}

	// Token: 0x06001B17 RID: 6935 RVA: 0x000A0A70 File Offset: 0x0009EC70
	private void Update()
	{
		switch (this.m_currentAnimType)
		{
		case HudEventResult.AnimType.IN_BONUS:
			if (this.m_score != null)
			{
				this.m_score.Update(Time.deltaTime);
				if (this.m_score.IsEnd)
				{
					SoundManager.SeStop("sys_result_count", "SE");
				}
			}
			break;
		case HudEventResult.AnimType.WAIT_ADD_COLLECT_OBJECT:
			this.m_timer += Time.deltaTime;
			if (this.m_timer >= this.m_waitTime)
			{
				this.m_callback(this.m_currentAnimType);
			}
			break;
		case HudEventResult.AnimType.ADD_COLLECT_OBJECT:
			if (this.m_totalScore != null)
			{
				this.m_totalScore.Update(Time.deltaTime);
				if (this.m_totalScore.IsEnd)
				{
					SoundManager.SeStop("sys_result_count", "SE");
					this.m_callback(this.m_currentAnimType);
				}
			}
			break;
		}
	}

	// Token: 0x06001B18 RID: 6936 RVA: 0x000A0B68 File Offset: 0x0009ED68
	private void AnimationFinishCallback()
	{
		if (this.m_callback != null)
		{
			this.m_callback(this.m_currentAnimType);
		}
	}

	// Token: 0x06001B19 RID: 6937 RVA: 0x000A0B88 File Offset: 0x0009ED88
	private void QuotaPlayEndCallback()
	{
		if (this.m_callback != null)
		{
			this.m_callback(this.m_currentAnimType);
		}
	}

	// Token: 0x0400188F RID: 6287
	private GameObject m_resultRootObject;

	// Token: 0x04001890 RID: 6288
	private HudEventResult.AnimationEndCallback m_callback;

	// Token: 0x04001891 RID: 6289
	private Animation m_animation;

	// Token: 0x04001892 RID: 6290
	private HudEventResult.AnimType m_currentAnimType;

	// Token: 0x04001893 RID: 6291
	private SpecialStageInfo m_info;

	// Token: 0x04001894 RID: 6292
	private HudEventQuota m_hudEventQuota;

	// Token: 0x04001895 RID: 6293
	private GameResultScoreInterporate m_score;

	// Token: 0x04001896 RID: 6294
	private GameResultScoreInterporate m_totalScore;

	// Token: 0x04001897 RID: 6295
	private float m_timer;

	// Token: 0x04001898 RID: 6296
	private float m_waitTime;

	// Token: 0x04001899 RID: 6297
	private long m_beforeTotalPoint;

	// Token: 0x0400189A RID: 6298
	private static readonly float WAIT_TIME = 1f;

	// Token: 0x0400189B RID: 6299
	private static readonly float WAIT_TIME_WITH_BONUS = 0.5f;

	// Token: 0x0400189C RID: 6300
	private HudEventResultSpStage.SpResultReplaceText[] SpResultReplaceStringTable = new HudEventResultSpStage.SpResultReplaceText[]
	{
		new HudEventResultSpStage.SpResultReplaceText("ui_Lbl_word_get", "ui_Lbl_word_get_total"),
		new HudEventResultSpStage.SpResultReplaceText("ui_Lbl_word_animal_get", "ui_Lbl_word_animal_get_total"),
		new HudEventResultSpStage.SpResultReplaceText("ui_Lbl_word_ring_get", "ui_Lbl_word_ring_get_total"),
		new HudEventResultSpStage.SpResultReplaceText("ui_Lbl_word_run_distance_get", "ui_Lbl_word_run_distance_get_total")
	};

	// Token: 0x02000396 RID: 918
	private enum EventKind
	{
		// Token: 0x0400189E RID: 6302
		SP_EVENT,
		// Token: 0x0400189F RID: 6303
		ANIMAL_EVENT,
		// Token: 0x040018A0 RID: 6304
		RING_EVENT,
		// Token: 0x040018A1 RID: 6305
		DISTANCE_EVENT,
		// Token: 0x040018A2 RID: 6306
		NUM
	}

	// Token: 0x02000397 RID: 919
	public class SpResultReplaceText
	{
		// Token: 0x06001B1A RID: 6938 RVA: 0x000A0BA8 File Offset: 0x0009EDA8
		public SpResultReplaceText(string s1, string s2)
		{
			this.CountText = s1;
			this.TotalText = s2;
		}

		// Token: 0x040018A3 RID: 6307
		public string CountText;

		// Token: 0x040018A4 RID: 6308
		public string TotalText;
	}
}
