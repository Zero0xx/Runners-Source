using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000391 RID: 913
public class HudEventResultCollect : HudEventResultParts
{
	// Token: 0x06001AFA RID: 6906 RVA: 0x0009F2AC File Offset: 0x0009D4AC
	public override void Init(GameObject resultRootObject, long beforeTotalPoint, HudEventResult.AnimationEndCallback callback)
	{
		this.m_resultRootObject = resultRootObject;
		this.m_beforeTotalPoint = beforeTotalPoint;
		this.m_callback = callback;
		this.m_timer = 0f;
		this.m_info = EventManager.Instance.EtcEventInfo;
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
				this.m_score = new GameResultScoreInterporate();
				this.m_score.Setup(uilabel);
				this.m_score.SetLabelStartValue(HudEventResultCollect.GetCollectNum(false));
			}
			UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_resultRootObject, "Lbl_total_object");
			if (uilabel2 != null)
			{
				this.m_totalScore = new GameResultScoreInterporate();
				this.m_totalScore.Setup(uilabel2);
				long labelStartValue = this.m_info.totalPoint - HudEventResultCollect.GetCollectNum(true);
				this.m_totalScore.SetLabelStartValue(labelStartValue);
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
					string text = string.Empty;
					ServerItem serverItem = new ServerItem((ServerItem.Id)list[i].reward);
					text = serverItem.serverItemName;
					int index = list[i].index;
					if (index > 1)
					{
						text = text + " × " + index;
					}
					bool isCleared = list[i].IsAttainment(this.m_info.totalPoint);
					QuotaInfo info = new QuotaInfo(name, formatNumString, reward, text, isCleared);
					this.m_hudEventQuota.AddQuota(info);
				}
			}
			this.m_hudEventQuota.Setup(this.m_resultRootObject, this.m_animation, "ui_EventResult_animal_mission_inout1_Anim", "ui_EventResult_animal_mission_inout2_Anim");
		}
	}

	// Token: 0x06001AFB RID: 6907 RVA: 0x0009F508 File Offset: 0x0009D708
	public override void PlayAnimation(HudEventResult.AnimType animType)
	{
		this.m_currentAnimType = animType;
		switch (animType)
		{
		case HudEventResult.AnimType.IN:
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_EventResult_animal_intro_Anim", Direction.Forward, true);
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
					float num = HudEventResultCollect.GetBonusRate();
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
								startValue = HudEventResultCollect.GetCollectNum(false);
								endValue = HudEventResultCollect.GetCollectNum(true);
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
				this.m_waitTime = HudEventResultCollect.WAIT_TIME;
			}
			else
			{
				this.m_waitTime = HudEventResultCollect.WAIT_TIME_WITH_BONUS;
			}
			break;
		}
		case HudEventResult.AnimType.WAIT_ADD_COLLECT_OBJECT:
			this.m_timer = 0f;
			break;
		case HudEventResult.AnimType.ADD_COLLECT_OBJECT:
			if (this.m_totalScore != null)
			{
				long startValue2 = this.m_info.totalPoint - HudEventResultCollect.GetCollectNum(true);
				long totalPoint = this.m_info.totalPoint;
				this.m_totalScore.PlayStart(startValue2, totalPoint, 0.5f);
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
			ActiveAnimation activeAnimation3 = ActiveAnimation.Play(this.m_animation, "ui_EventResult_animal_outro_Anim", Direction.Forward, true);
			EventDelegate.Add(activeAnimation3.onFinished, new EventDelegate.Callback(this.AnimationFinishCallback), true);
			break;
		}
		}
	}

	// Token: 0x06001AFC RID: 6908 RVA: 0x0009F790 File Offset: 0x0009D990
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

	// Token: 0x06001AFD RID: 6909 RVA: 0x0009F888 File Offset: 0x0009DA88
	private void AnimationFinishCallback()
	{
		if (this.m_callback != null)
		{
			this.m_callback(this.m_currentAnimType);
		}
	}

	// Token: 0x06001AFE RID: 6910 RVA: 0x0009F8A8 File Offset: 0x0009DAA8
	private void QuotaPlayEndCallback()
	{
		if (this.m_callback != null)
		{
			this.m_callback(this.m_currentAnimType);
		}
	}

	// Token: 0x06001AFF RID: 6911 RVA: 0x0009F8C8 File Offset: 0x0009DAC8
	private static float GetBonusRate()
	{
		float result = 0f;
		StageAbilityManager.BonusRate bonusValueRate = StageAbilityManager.Instance.BonusValueRate;
		switch (EventManager.Instance.CollectType)
		{
		case EventManager.CollectEventType.GET_ANIMALS:
			result = bonusValueRate.animal;
			break;
		case EventManager.CollectEventType.GET_RING:
			result = bonusValueRate.ring;
			break;
		case EventManager.CollectEventType.RUN_DISTANCE:
			result = bonusValueRate.distance;
			break;
		}
		return result;
	}

	// Token: 0x06001B00 RID: 6912 RVA: 0x0009F934 File Offset: 0x0009DB34
	private static long GetCollectNum(bool isBonused)
	{
		long result = 0L;
		StageScoreManager instance = StageScoreManager.Instance;
		if (instance == null)
		{
			return result;
		}
		StageScoreManager.ResultData resultData;
		if (isBonused)
		{
			resultData = instance.FinalCountData;
		}
		else
		{
			resultData = instance.CountData;
		}
		switch (EventManager.Instance.CollectType)
		{
		case EventManager.CollectEventType.GET_ANIMALS:
			result = resultData.animal;
			break;
		case EventManager.CollectEventType.GET_RING:
			result = resultData.ring;
			break;
		case EventManager.CollectEventType.RUN_DISTANCE:
			result = resultData.distance;
			break;
		}
		return result;
	}

	// Token: 0x04001866 RID: 6246
	private GameObject m_resultRootObject;

	// Token: 0x04001867 RID: 6247
	private HudEventResult.AnimationEndCallback m_callback;

	// Token: 0x04001868 RID: 6248
	private Animation m_animation;

	// Token: 0x04001869 RID: 6249
	private HudEventResult.AnimType m_currentAnimType;

	// Token: 0x0400186A RID: 6250
	private EtcEventInfo m_info;

	// Token: 0x0400186B RID: 6251
	private HudEventQuota m_hudEventQuota;

	// Token: 0x0400186C RID: 6252
	private GameResultScoreInterporate m_score;

	// Token: 0x0400186D RID: 6253
	private GameResultScoreInterporate m_totalScore;

	// Token: 0x0400186E RID: 6254
	private float m_timer;

	// Token: 0x0400186F RID: 6255
	private float m_waitTime;

	// Token: 0x04001870 RID: 6256
	private static readonly float WAIT_TIME = 1f;

	// Token: 0x04001871 RID: 6257
	private static readonly float WAIT_TIME_WITH_BONUS = 0.5f;

	// Token: 0x04001872 RID: 6258
	private long m_beforeTotalPoint;
}
