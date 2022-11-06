using System;
using AnimationOrTween;
using UI;
using UnityEngine;

// Token: 0x02000386 RID: 902
public class GameResultScoresRaidBoss : GameResultScores
{
	// Token: 0x06001AB3 RID: 6835 RVA: 0x0009DE20 File Offset: 0x0009C020
	public void SetBossDestroyFlag(bool flag)
	{
		this.m_isBossDestroy = flag;
	}

	// Token: 0x06001AB4 RID: 6836 RVA: 0x0009DE2C File Offset: 0x0009C02C
	protected override bool IsBonus(StageScoreManager.ResultData data1, StageScoreManager.ResultData data2, StageScoreManager.ResultData data3)
	{
		long num = 0L;
		long num2 = 0L;
		if (data1 != null)
		{
			num += data1.raid_boss_ring;
			num2 += data1.raid_boss_reward;
		}
		if (data2 != null)
		{
			num += data2.raid_boss_ring;
			num2 += data2.raid_boss_reward;
		}
		if (data3 != null)
		{
			num += data3.raid_boss_ring;
			num2 += data3.raid_boss_reward;
		}
		return num > 0L;
	}

	// Token: 0x06001AB5 RID: 6837 RVA: 0x0009DE94 File Offset: 0x0009C094
	protected override void OnSetup(GameObject resultRoot)
	{
		global::Debug.Log("GameResultScoresRaidBoss:OnSetup");
		this.m_destroyBonusViewTime = 0f;
		this.m_raidbossResultRoot = GameObjectUtil.FindChildGameObject(base.gameObject, "EventResult_raidboss");
		if (this.m_raidbossResultRoot != null)
		{
			this.m_destroyBonusAnim = GameObjectUtil.FindChildGameObjectComponent<Animation>(this.m_raidbossResultRoot, "destroy_bonus_Anim");
			if (this.m_destroyBonusAnim != null)
			{
				this.m_destroyBonusAnim.gameObject.SetActive(false);
				this.m_destroyBonusViewTime = this.m_destroyBonusAnim["ui_EventResult_raidboss_destroy_bonus_Anim"].length * 0.3f;
			}
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(resultRoot, "nomiss_bonus_Anim");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(resultRoot, "window_result");
		if (gameObject2 != null)
		{
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject2, "Lbl_word_ring");
			if (uilabel != null)
			{
				uilabel.text = HudUtility.GetEventSpObjectName();
				UILocalizeText uilocalizeText = GameObjectUtil.FindChildGameObjectComponent<UILocalizeText>(gameObject2, "Lbl_word_ring");
				if (uilocalizeText != null)
				{
					uilocalizeText.enabled = false;
				}
			}
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject2, "img_icon_ring");
			if (uisprite != null)
			{
				uisprite.spriteName = "ui_event_ring_icon";
			}
		}
		base.SetEnableNextButton(true);
		this.m_animState = GameResultScoresRaidBoss.AnimState.IDLE;
		this.m_isBossResult = true;
	}

	// Token: 0x06001AB6 RID: 6838 RVA: 0x0009DFEC File Offset: 0x0009C1EC
	protected override void OnFinish()
	{
	}

	// Token: 0x06001AB7 RID: 6839 RVA: 0x0009DFF0 File Offset: 0x0009C1F0
	protected override void OnStartBeginning()
	{
		base.SetBonusEventScoreActive(GameResultScores.Category.NONE);
		if (this.m_isReplay)
		{
			this.m_animState = GameResultScoresRaidBoss.AnimState.FINISHED;
		}
		else if (!this.m_isBossDestroy)
		{
			this.m_animState = GameResultScoresRaidBoss.AnimState.FINISHED;
		}
		else
		{
			if (this.m_destroyBonusAnim != null)
			{
				UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_raidbossResultRoot, "Lbl_destroy_bonus");
				if (uilabel != null)
				{
					uilabel.text = GameResultUtility.GetRaidbossBeatBonus().ToString();
				}
				this.m_destroyBonusAnim.gameObject.SetActive(true);
			}
			SoundManager.SePlay("sys_specialegg", "SE");
			this.m_animState = GameResultScoresRaidBoss.AnimState.PLAYING1;
			base.SetEnableNextButton(false);
		}
	}

	// Token: 0x06001AB8 RID: 6840 RVA: 0x0009E0A4 File Offset: 0x0009C2A4
	protected override void OnUpdateBeginning()
	{
		if (!this.m_isBossDestroy)
		{
			return;
		}
		if (this.m_animState == GameResultScoresRaidBoss.AnimState.PLAYING1)
		{
			float time = this.m_destroyBonusAnim["ui_EventResult_raidboss_destroy_bonus_Anim"].time;
			if (time > this.m_destroyBonusViewTime)
			{
				base.SetEnableNextButton(true);
				this.m_animState = GameResultScoresRaidBoss.AnimState.PLAYING2;
			}
		}
		if (this.m_animState == GameResultScoresRaidBoss.AnimState.PLAYING2 && !this.m_destroyBonusAnim.isPlaying)
		{
			this.m_animState = GameResultScoresRaidBoss.AnimState.FINISHED;
		}
	}

	// Token: 0x06001AB9 RID: 6841 RVA: 0x0009E11C File Offset: 0x0009C31C
	protected override void OnSkipBeginning()
	{
		if (!this.m_isBossDestroy)
		{
			return;
		}
		if (this.m_animState == GameResultScoresRaidBoss.AnimState.PLAYING2 && this.m_destroyBonusAnim != null)
		{
			this.m_destroyBonusAnim.Stop();
			this.m_destroyBonusAnim.gameObject.SetActive(false);
			this.m_animState = GameResultScoresRaidBoss.AnimState.FINISHED;
		}
	}

	// Token: 0x06001ABA RID: 6842 RVA: 0x0009E178 File Offset: 0x0009C378
	protected override bool IsEndBeginning()
	{
		return this.m_animState == GameResultScoresRaidBoss.AnimState.FINISHED;
	}

	// Token: 0x06001ABB RID: 6843 RVA: 0x0009E18C File Offset: 0x0009C38C
	protected override void OnEndBeginning()
	{
		base.SetEnableNextButton(true);
		HudEventResultRaidBoss hudEventResultRaidBoss = GameObjectUtil.FindChildGameObjectComponent<HudEventResultRaidBoss>(base.gameObject, "EventResult_raidboss");
		if (hudEventResultRaidBoss != null)
		{
			hudEventResultRaidBoss.SetEnableDamageDetailsButton(true);
		}
	}

	// Token: 0x06001ABC RID: 6844 RVA: 0x0009E1C4 File Offset: 0x0009C3C4
	protected override void OnScoreInAnimation(EventDelegate.Callback callback)
	{
		Animation animation = GameResultUtility.SearchAnimation(this.m_resultRoot);
		if (animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(animation, "ui_result_boss_intro_score_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, callback, true);
		}
	}

	// Token: 0x06001ABD RID: 6845 RVA: 0x0009E204 File Offset: 0x0009C404
	protected override void OnScoreOutAnimation(EventDelegate.Callback callback)
	{
		Animation animation = GameResultUtility.SearchAnimation(this.m_resultRoot);
		if (animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(animation, "ui_result_boss_intro_score_Anim", Direction.Reverse);
			EventDelegate.Add(activeAnimation.onFinished, callback, true);
		}
		HudEventResultRaidBoss hudEventResultRaidBoss = GameObjectUtil.FindChildGameObjectComponent<HudEventResultRaidBoss>(base.gameObject, "EventResult_raidboss");
		if (hudEventResultRaidBoss != null)
		{
			hudEventResultRaidBoss.SetEnableDamageDetailsButton(false);
		}
	}

	// Token: 0x0400181B RID: 6171
	private const string destroyBonusClip = "ui_EventResult_raidboss_destroy_bonus_Anim";

	// Token: 0x0400181C RID: 6172
	private const string introScoreAnimClip = "ui_EventResult_raidboss_intro_Anim";

	// Token: 0x0400181D RID: 6173
	private GameObject m_raidbossResultRoot;

	// Token: 0x0400181E RID: 6174
	private GameResultScoresRaidBoss.AnimState m_animState;

	// Token: 0x0400181F RID: 6175
	private Animation m_destroyBonusAnim;

	// Token: 0x04001820 RID: 6176
	private float m_destroyBonusViewTime;

	// Token: 0x04001821 RID: 6177
	private bool m_isBossDestroy;

	// Token: 0x02000387 RID: 903
	private enum AnimState
	{
		// Token: 0x04001823 RID: 6179
		NONE = -1,
		// Token: 0x04001824 RID: 6180
		IDLE,
		// Token: 0x04001825 RID: 6181
		PLAYING1,
		// Token: 0x04001826 RID: 6182
		PLAYING2,
		// Token: 0x04001827 RID: 6183
		FINISHED,
		// Token: 0x04001828 RID: 6184
		END
	}
}
