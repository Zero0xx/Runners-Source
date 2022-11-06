using System;
using System.Collections.Generic;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x020004F2 RID: 1266
public class RankingResultLeague : WindowBase
{
	// Token: 0x060025BB RID: 9659 RVA: 0x000E57AC File Offset: 0x000E39AC
	private void OnDestroy()
	{
		base.Destroy();
	}

	// Token: 0x060025BC RID: 9660 RVA: 0x000E57B4 File Offset: 0x000E39B4
	public void Setup(string message, bool quick)
	{
		base.gameObject.SetActive(true);
		this.m_quickMode = quick;
		this.m_close = false;
		this.m_rankingData = new RankingServerInfoConverter(message);
		this.m_animation = base.GetComponentInChildren<Animation>();
		if (this.m_animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			SoundManager.SePlay("sys_window_open", "SE");
		}
		this.m_lblInfo = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_league_resilt_ex");
		this.m_lblLeague = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_league");
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_word_down");
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_word_up");
		UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_word_stay");
		uisprite.transform.localScale = new Vector3(0f, 0f, 1f);
		uisprite2.transform.localScale = new Vector3(0f, 0f, 1f);
		uisprite3.transform.localScale = new Vector3(0f, 0f, 1f);
		this.m_imgLeague = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_icon_league");
		this.m_imgLeagueStar = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_icon_league_sub");
		LeagueType currentLeague = this.m_rankingData.currentLeague;
		this.m_imgLeague.spriteName = "ui_ranking_league_icon_" + RankingUtil.GetLeagueCategoryName(currentLeague).ToLower();
		this.m_imgLeagueStar.spriteName = "ui_ranking_league_icon_" + RankingUtil.GetLeagueCategoryClass(currentLeague);
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_caption");
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_caption_sh");
		if (uilabel != null && uilabel2 != null)
		{
			string commonText = TextUtility.GetCommonText("Ranking", (!this.m_quickMode) ? "ui_Lbl_caption_endless_result" : "ui_Lbl_caption_quickmode_result");
			uilabel.text = commonText;
			uilabel2.text = commonText;
		}
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_result_league_stay").text;
		switch (this.m_rankingData.leagueResult)
		{
		case RankingServerInfoConverter.ResultType.Up:
			text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_result_league_up").text;
			break;
		case RankingServerInfoConverter.ResultType.Down:
			text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_result_league_down").text;
			break;
		}
		string text2 = TextUtility.Replaces(text, new Dictionary<string, string>
		{
			{
				"{PARAM}",
				RankingUtil.GetLeagueName(currentLeague)
			}
		});
		this.m_lblInfo.text = this.m_rankingData.rankingResultLeagueText;
		this.m_lblLeague.text = text2;
		this.m_mode = RankingResultLeague.Mode.Wait;
	}

	// Token: 0x060025BD RID: 9661 RVA: 0x000E5AB0 File Offset: 0x000E3CB0
	public bool IsEnd()
	{
		return this.m_mode != RankingResultLeague.Mode.Wait;
	}

	// Token: 0x060025BE RID: 9662 RVA: 0x000E5AC4 File Offset: 0x000E3CC4
	public void OnClickNoButton()
	{
		this.m_close = true;
		this.m_isOpened = false;
		SoundManager.SePlay("sys_window_close", "SE");
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_close");
		UIPlayAnimation component = gameObject.GetComponent<UIPlayAnimation>();
		if (component != null)
		{
			EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			component.Play(true);
		}
	}

	// Token: 0x060025BF RID: 9663 RVA: 0x000E5B34 File Offset: 0x000E3D34
	private void WindowAnimationFinishCallback()
	{
		if (this.m_rankingData != null && this.m_rankingData.leagueResult != RankingServerInfoConverter.ResultType.Error && !this.m_close)
		{
			switch (this.m_rankingData.leagueResult)
			{
			case RankingServerInfoConverter.ResultType.Up:
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_ranking_league_up_Anim", Direction.Forward);
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.ResultAnimationFinishCallback), true);
				SoundManager.SePlay("sys_league_up", "SE");
				break;
			}
			case RankingServerInfoConverter.ResultType.Stay:
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_ranking_league_stay_Anim", Direction.Forward);
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.ResultAnimationFinishCallback), true);
				SoundManager.SePlay("sys_league_stay", "SE");
				break;
			}
			case RankingServerInfoConverter.ResultType.Down:
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_ranking_league_down_Anim", Direction.Forward);
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.ResultAnimationFinishCallback), true);
				SoundManager.SePlay("sys_league_down", "SE");
				break;
			}
			}
			this.m_isOpened = true;
		}
		else if (this.m_close)
		{
			base.gameObject.SetActive(false);
			Transform parent = base.transform.parent;
			if (parent != null)
			{
				Transform parent2 = parent.transform.parent;
				if (parent2 != null && parent2.name == "LeagueResultWindowUI")
				{
					parent2.gameObject.SetActive(false);
				}
			}
			this.m_mode = RankingResultLeague.Mode.End;
		}
	}

	// Token: 0x060025C0 RID: 9664 RVA: 0x000E5CC4 File Offset: 0x000E3EC4
	private void ResultAnimationFinishCallback()
	{
	}

	// Token: 0x060025C1 RID: 9665 RVA: 0x000E5CC8 File Offset: 0x000E3EC8
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (this.m_isOpened)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_close");
			if (gameObject != null)
			{
				UIButtonMessage component = gameObject.GetComponent<UIButtonMessage>();
				if (component != null)
				{
					component.SendMessage("OnClick");
				}
			}
		}
		if (msg != null)
		{
			msg.StaySequence();
		}
	}

	// Token: 0x060025C2 RID: 9666 RVA: 0x000E5D28 File Offset: 0x000E3F28
	public static RankingResultLeague Create(NetNoticeItem item)
	{
		return RankingResultLeague.Create(item.Message, item.Id == (long)NetNoticeItem.OPERATORINFO_QUICKRANKINGRESULT_ID);
	}

	// Token: 0x060025C3 RID: 9667 RVA: 0x000E5D44 File Offset: 0x000E3F44
	public static RankingResultLeague Create(string message, bool quick)
	{
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(cameraUIObject, "LeagueResultWindowUI");
			if (gameObject != null)
			{
				gameObject.SetActive(true);
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "league_window");
				if (gameObject2 != null)
				{
					RankingResultLeague rankingResultLeague = gameObject2.GetComponent<RankingResultLeague>();
					if (rankingResultLeague == null)
					{
						rankingResultLeague = gameObject2.AddComponent<RankingResultLeague>();
					}
					if (rankingResultLeague != null)
					{
						rankingResultLeague.Setup(message, quick);
					}
					return rankingResultLeague;
				}
			}
		}
		return null;
	}

	// Token: 0x04002204 RID: 8708
	private RankingResultLeague.Mode m_mode;

	// Token: 0x04002205 RID: 8709
	private RankingServerInfoConverter m_rankingData;

	// Token: 0x04002206 RID: 8710
	private Animation m_animation;

	// Token: 0x04002207 RID: 8711
	private UILabel m_lblInfo;

	// Token: 0x04002208 RID: 8712
	private UILabel m_lblLeague;

	// Token: 0x04002209 RID: 8713
	private UISprite m_imgLeague;

	// Token: 0x0400220A RID: 8714
	private UISprite m_imgLeagueStar;

	// Token: 0x0400220B RID: 8715
	private bool m_quickMode;

	// Token: 0x0400220C RID: 8716
	private bool m_isOpened;

	// Token: 0x0400220D RID: 8717
	private bool m_close;

	// Token: 0x020004F3 RID: 1267
	private enum Mode
	{
		// Token: 0x0400220F RID: 8719
		Idle,
		// Token: 0x04002210 RID: 8720
		Wait,
		// Token: 0x04002211 RID: 8721
		End
	}
}
