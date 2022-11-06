using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000382 RID: 898
public class GameResultScoresBoss : GameResultScores
{
	// Token: 0x06001A9A RID: 6810 RVA: 0x0009D104 File Offset: 0x0009B304
	public void SetNoMissFlag(bool flag)
	{
		this.m_isNomiss = flag;
	}

	// Token: 0x06001A9B RID: 6811 RVA: 0x0009D110 File Offset: 0x0009B310
	protected override bool IsBonus(StageScoreManager.ResultData data1, StageScoreManager.ResultData data2, StageScoreManager.ResultData data3)
	{
		long num = 0L;
		if (data1 != null)
		{
			num += data1.ring;
		}
		if (data2 != null)
		{
			num += data2.ring;
		}
		if (data3 != null)
		{
			num += data3.ring;
		}
		return num > 0L;
	}

	// Token: 0x06001A9C RID: 6812 RVA: 0x0009D158 File Offset: 0x0009B358
	protected override void OnSetup(GameObject resultRoot)
	{
		this.m_noMissBonusViewTime = 0f;
		this.m_noMissBonusAnim = GameObjectUtil.FindChildGameObjectComponent<Animation>(resultRoot, "nomiss_bonus_Anim");
		if (this.m_noMissBonusAnim != null)
		{
			this.m_noMissBonusAnim.gameObject.SetActive(false);
			this.m_noMissBonusViewTime = this.m_noMissBonusAnim["ui_result_nomiss_bonus_Anim"].length * 0.3f;
		}
		if (base.IsBonusEvent())
		{
			base.SetEnableNextButton(true);
		}
		this.m_animState = GameResultScoresBoss.AnimState.IDLE;
		this.m_isBossResult = true;
	}

	// Token: 0x06001A9D RID: 6813 RVA: 0x0009D1E4 File Offset: 0x0009B3E4
	protected override void OnFinish()
	{
	}

	// Token: 0x06001A9E RID: 6814 RVA: 0x0009D1E8 File Offset: 0x0009B3E8
	protected override void OnStartFinished()
	{
		base.SetBonusEventScoreActive(GameResultScores.Category.NONE);
		if (this.m_isReplay)
		{
			this.m_animState = GameResultScoresBoss.AnimState.FINISHED;
		}
		else if (!this.m_isNomiss)
		{
			this.m_animState = GameResultScoresBoss.AnimState.FINISHED;
		}
		else
		{
			if (this.m_noMissBonusAnim != null)
			{
				this.m_noMissBonusAnim.gameObject.SetActive(true);
			}
			SoundManager.SePlay("sys_specialegg", "SE");
			this.m_animState = GameResultScoresBoss.AnimState.PLAYING1;
			base.SetEnableNextButton(false);
		}
	}

	// Token: 0x06001A9F RID: 6815 RVA: 0x0009D26C File Offset: 0x0009B46C
	protected override void OnUpdateFinished()
	{
		if (!this.m_isNomiss)
		{
			return;
		}
		if (this.m_animState == GameResultScoresBoss.AnimState.PLAYING1)
		{
			float time = this.m_noMissBonusAnim["ui_result_nomiss_bonus_Anim"].time;
			if (time > this.m_noMissBonusViewTime)
			{
				base.SetEnableNextButton(true);
				this.m_animState = GameResultScoresBoss.AnimState.PLAYING2;
			}
		}
		if (this.m_animState == GameResultScoresBoss.AnimState.PLAYING2 && !this.m_noMissBonusAnim.isPlaying)
		{
			this.m_animState = GameResultScoresBoss.AnimState.FINISHED;
		}
	}

	// Token: 0x06001AA0 RID: 6816 RVA: 0x0009D2E4 File Offset: 0x0009B4E4
	protected override void OnSkipFinished()
	{
		if (!this.m_isNomiss)
		{
			return;
		}
		if (this.m_animState == GameResultScoresBoss.AnimState.PLAYING2 && this.m_noMissBonusAnim != null)
		{
			this.m_noMissBonusAnim.Stop();
			this.m_noMissBonusAnim.gameObject.SetActive(false);
			this.m_animState = GameResultScoresBoss.AnimState.FINISHED;
		}
	}

	// Token: 0x06001AA1 RID: 6817 RVA: 0x0009D340 File Offset: 0x0009B540
	protected override bool IsEndFinished()
	{
		return this.m_animState == GameResultScoresBoss.AnimState.FINISHED;
	}

	// Token: 0x06001AA2 RID: 6818 RVA: 0x0009D354 File Offset: 0x0009B554
	protected override void OnEndFinished()
	{
		base.SetEnableNextButton(true);
	}

	// Token: 0x06001AA3 RID: 6819 RVA: 0x0009D360 File Offset: 0x0009B560
	protected override void OnScoreInAnimation(EventDelegate.Callback callback)
	{
		Animation animation = GameResultUtility.SearchAnimation(this.m_resultRoot);
		if (animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(animation, "ui_result_boss_intro_score_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, callback, true);
		}
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x0009D3A0 File Offset: 0x0009B5A0
	protected override void OnScoreOutAnimation(EventDelegate.Callback callback)
	{
		Animation animation = GameResultUtility.SearchAnimation(this.m_resultRoot);
		if (animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(animation, "ui_result_boss_intro_score_Anim", Direction.Reverse);
			EventDelegate.Add(activeAnimation.onFinished, callback, true);
		}
	}

	// Token: 0x040017FC RID: 6140
	private const string noMissBonusClip = "ui_result_nomiss_bonus_Anim";

	// Token: 0x040017FD RID: 6141
	private GameResultScoresBoss.AnimState m_animState;

	// Token: 0x040017FE RID: 6142
	private Animation m_noMissBonusAnim;

	// Token: 0x040017FF RID: 6143
	private float m_noMissBonusViewTime;

	// Token: 0x04001800 RID: 6144
	private bool m_isNomiss;

	// Token: 0x02000383 RID: 899
	private enum AnimState
	{
		// Token: 0x04001802 RID: 6146
		NONE = -1,
		// Token: 0x04001803 RID: 6147
		IDLE,
		// Token: 0x04001804 RID: 6148
		PLAYING1,
		// Token: 0x04001805 RID: 6149
		PLAYING2,
		// Token: 0x04001806 RID: 6150
		FINISHED,
		// Token: 0x04001807 RID: 6151
		END
	}
}
