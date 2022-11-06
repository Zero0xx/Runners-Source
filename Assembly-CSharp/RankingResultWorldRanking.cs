using System;
using System.Collections;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020004F7 RID: 1271
public class RankingResultWorldRanking : WindowBase
{
	// Token: 0x060025D1 RID: 9681 RVA: 0x000E5EC4 File Offset: 0x000E40C4
	private void Start()
	{
	}

	// Token: 0x060025D2 RID: 9682 RVA: 0x000E5EC8 File Offset: 0x000E40C8
	private void OnDestroy()
	{
		base.Destroy();
	}

	// Token: 0x060025D3 RID: 9683 RVA: 0x000E5ED0 File Offset: 0x000E40D0
	private void Update()
	{
	}

	// Token: 0x060025D4 RID: 9684 RVA: 0x000E5ED4 File Offset: 0x000E40D4
	private RankingResultWorldRanking.ResultType GetResultType(int id)
	{
		if (id == NetNoticeItem.OPERATORINFO_RANKINGRESULT_ID)
		{
			return RankingResultWorldRanking.ResultType.WORLD_RANKING;
		}
		if (id == NetNoticeItem.OPERATORINFO_QUICKRANKINGRESULT_ID)
		{
			return RankingResultWorldRanking.ResultType.QUICK_WORLD_RANKING;
		}
		if (id == NetNoticeItem.OPERATORINFO_EVENTRANKINGRESULT_ID)
		{
			return RankingResultWorldRanking.ResultType.EVENT_RANKING;
		}
		return RankingResultWorldRanking.ResultType.WORLD_RANKING;
	}

	// Token: 0x060025D5 RID: 9685 RVA: 0x000E5F0C File Offset: 0x000E410C
	public void Setup(NetNoticeItem item)
	{
		RankingResultWorldRanking.ResultType resultType = this.GetResultType((int)item.Id);
		this.Setup(resultType, item.Message);
	}

	// Token: 0x060025D6 RID: 9686 RVA: 0x000E5F34 File Offset: 0x000E4134
	public void Setup(RankingResultWorldRanking.ResultType resultType, string messageInfo)
	{
		base.gameObject.SetActive(true);
		this.m_resultType = resultType;
		switch (this.m_resultType)
		{
		case RankingResultWorldRanking.ResultType.WORLD_RANKING:
			this.m_decoder = new InfoDecoderWorldRanking(messageInfo);
			break;
		case RankingResultWorldRanking.ResultType.QUICK_WORLD_RANKING:
			this.m_decoder = new InfoDecoderWorldRanking(messageInfo);
			break;
		case RankingResultWorldRanking.ResultType.EVENT_RANKING:
			this.m_decoder = new InfoDecoderEvent(messageInfo);
			break;
		}
		if (this.m_decoder == null)
		{
			return;
		}
		if (!this.m_isSetup)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_close");
			if (gameObject != null)
			{
				UIButtonMessage uibuttonMessage = gameObject.GetComponent<UIButtonMessage>();
				if (uibuttonMessage == null)
				{
					uibuttonMessage = gameObject.AddComponent<UIButtonMessage>();
				}
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OnClickCloseButton";
			}
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "blinder");
			if (gameObject2 != null)
			{
				UIButtonMessage uibuttonMessage2 = gameObject2.GetComponent<UIButtonMessage>();
				if (uibuttonMessage2 == null)
				{
					uibuttonMessage2 = gameObject2.AddComponent<UIButtonMessage>();
				}
				uibuttonMessage2.target = base.gameObject;
				uibuttonMessage2.functionName = "OnClickCloseButton";
			}
			this.m_isSetup = true;
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_caption");
		if (uilabel != null)
		{
			uilabel.text = this.m_decoder.GetCaption();
		}
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_caption_sh");
		if (uilabel2 != null)
		{
			uilabel2.text = this.m_decoder.GetCaption();
		}
		UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_ranking_ex");
		if (uilabel3 != null)
		{
			uilabel3.text = this.m_decoder.GetResultString();
		}
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_icon_medal_blue");
		if (uisprite != null)
		{
			uisprite.spriteName = this.m_decoder.GetMedalSpriteName();
		}
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(base.gameObject, "word_anim");
		if (gameObject3 != null)
		{
			gameObject3.transform.localScale = new Vector3(0f, 0f, 1f);
		}
	}

	// Token: 0x060025D7 RID: 9687 RVA: 0x000E6164 File Offset: 0x000E4364
	public void PlayStart()
	{
		this.m_isEnd = false;
		this.m_isOpened = false;
		SoundManager.SePlay("sys_window_open", "SE");
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "ranking_window");
		if (animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(animation, "ui_cmn_window_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.InAnimationFinishCallack), true);
			SoundManager.SePlay("sys_result_best", "SE");
		}
	}

	// Token: 0x170004F3 RID: 1267
	// (get) Token: 0x060025D8 RID: 9688 RVA: 0x000E61E4 File Offset: 0x000E43E4
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x060025D9 RID: 9689 RVA: 0x000E61EC File Offset: 0x000E43EC
	private void OnClickCloseButton()
	{
		SoundManager.SePlay("sys_window_close", "SE");
		this.m_isOpened = false;
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "ranking_window");
		if (animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(animation, "ui_cmn_window_Anim", Direction.Reverse);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OutAnimationFinishCallback), true);
			SoundManager.SePlay("sys_window_close", "SE");
		}
	}

	// Token: 0x060025DA RID: 9690 RVA: 0x000E6264 File Offset: 0x000E4464
	private void InAnimationFinishCallack()
	{
		SoundManager.SePlay("sys_league_up", "SE");
		base.StartCoroutine(this.OnInAnimationFinishCallback());
		this.m_isOpened = true;
	}

	// Token: 0x060025DB RID: 9691 RVA: 0x000E6298 File Offset: 0x000E4498
	private IEnumerator OnInAnimationFinishCallback()
	{
		yield return null;
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "ranking_window");
		if (animation != null)
		{
			ActiveAnimation.Play(animation, "ui_ranking_world_event_Anim", Direction.Forward);
		}
		yield break;
	}

	// Token: 0x060025DC RID: 9692 RVA: 0x000E62B4 File Offset: 0x000E44B4
	private void OutAnimationFinishCallback()
	{
		this.m_isEnd = true;
		base.gameObject.SetActive(false);
	}

	// Token: 0x060025DD RID: 9693 RVA: 0x000E62CC File Offset: 0x000E44CC
	public static RankingResultWorldRanking GetResultWorldRanking()
	{
		RankingResultWorldRanking result = null;
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "menu_Anim");
			if (gameObject2 != null)
			{
				result = GameObjectUtil.FindChildGameObjectComponent<RankingResultWorldRanking>(gameObject2, "WorldRankingWindowUI");
			}
		}
		return result;
	}

	// Token: 0x060025DE RID: 9694 RVA: 0x000E6318 File Offset: 0x000E4518
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

	// Token: 0x04002216 RID: 8726
	private bool m_isSetup;

	// Token: 0x04002217 RID: 8727
	private bool m_isOpened;

	// Token: 0x04002218 RID: 8728
	private bool m_isEnd;

	// Token: 0x04002219 RID: 8729
	private RankingResultWorldRanking.ResultType m_resultType;

	// Token: 0x0400221A RID: 8730
	private InfoDecoder m_decoder;

	// Token: 0x020004F8 RID: 1272
	public enum ResultType
	{
		// Token: 0x0400221C RID: 8732
		WORLD_RANKING,
		// Token: 0x0400221D RID: 8733
		QUICK_WORLD_RANKING,
		// Token: 0x0400221E RID: 8734
		EVENT_RANKING,
		// Token: 0x0400221F RID: 8735
		NUM
	}
}
