using System;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x0200040B RID: 1035
public class FirstLaunchRecommendReview : MonoBehaviour
{
	// Token: 0x06001ED9 RID: 7897 RVA: 0x000B7850 File Offset: 0x000B5A50
	public void Setup(string anchorPath)
	{
		this.m_anchorPath = anchorPath;
	}

	// Token: 0x06001EDA RID: 7898 RVA: 0x000B785C File Offset: 0x000B5A5C
	public void PlayStart()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
		this.m_isEndPlay = false;
	}

	// Token: 0x17000468 RID: 1128
	// (get) Token: 0x06001EDB RID: 7899 RVA: 0x000B7898 File Offset: 0x000B5A98
	// (set) Token: 0x06001EDC RID: 7900 RVA: 0x000B78A0 File Offset: 0x000B5AA0
	public bool IsEndPlay
	{
		get
		{
			return this.m_isEndPlay;
		}
		private set
		{
		}
	}

	// Token: 0x06001EDD RID: 7901 RVA: 0x000B78A4 File Offset: 0x000B5AA4
	private void Start()
	{
		this.m_fsm = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
		description.initState = new TinyFsmState(new EventFunction(this.StateIdle));
		description.onFixedUpdate = true;
		this.m_fsm.SetUp(description);
	}

	// Token: 0x06001EDE RID: 7902 RVA: 0x000B7904 File Offset: 0x000B5B04
	private void Update()
	{
	}

	// Token: 0x06001EDF RID: 7903 RVA: 0x000B7908 File Offset: 0x000B5B08
	private TinyFsmState StateIdle(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		default:
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSendApolloStart)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001EE0 RID: 7904 RVA: 0x000B7980 File Offset: 0x000B5B80
	private TinyFsmState StateSendApolloStart(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_sendApollo != null)
			{
				UnityEngine.Object.Destroy(this.m_sendApollo.gameObject);
				this.m_sendApollo = null;
			}
			return TinyFsmState.End();
		case 1:
		{
			string[] value = new string[1];
			SendApollo.GetTutorialValue(ApolloTutorialIndex.START_STEP8, ref value);
			this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_START, value);
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_sendApollo != null && this.m_sendApollo.IsEnd())
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateRecommendReview)));
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001EE1 RID: 7905 RVA: 0x000B7A58 File Offset: 0x000B5C58
	private TinyFsmState StateRecommendReview(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
			info.caption = TextUtility.GetCommonText("FaceBook", "ui_Lbl_recommend_review_caption");
			string commonText = TextUtility.GetCommonText("FaceBook", "ui_Lbl_recommend_review_text", "{RED_STAR_RING_NUM}", "5");
			info.message = commonText;
			info.anchor_path = this.m_anchorPath;
			info.buttonType = GeneralWindow.ButtonType.YesNo;
			GeneralWindow.Create(info);
			return TinyFsmState.End();
		}
		case 4:
			if (GeneralWindow.IsYesButtonPressed)
			{
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateUploadReview)));
			}
			else if (GeneralWindow.IsNoButtonPressed)
			{
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001EE2 RID: 7906 RVA: 0x000B7B60 File Offset: 0x000B5D60
	private TinyFsmState StateUploadReview(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			Application.OpenURL(NetBaseUtil.RedirectInstallPageUrl);
			return TinyFsmState.End();
		case 4:
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001EE3 RID: 7907 RVA: 0x000B7BD4 File Offset: 0x000B5DD4
	private TinyFsmState StateSendApolloEnd(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_sendApollo != null)
			{
				UnityEngine.Object.Destroy(this.m_sendApollo.gameObject);
				this.m_sendApollo = null;
			}
			return TinyFsmState.End();
		case 1:
		{
			string[] value = new string[1];
			SendApollo.GetTutorialValue(ApolloTutorialIndex.START_STEP8, ref value);
			this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_END, value);
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_sendApollo != null && this.m_sendApollo.IsEnd())
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001EE4 RID: 7908 RVA: 0x000B7CB0 File Offset: 0x000B5EB0
	private TinyFsmState StateEnd(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			HudMenuUtility.SaveSystemDataFlagStatus(SystemData.FlagStatus.RECOMMEND_REVIEW_END);
			this.m_isEndPlay = true;
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001EE5 RID: 7909 RVA: 0x000B7D0C File Offset: 0x000B5F0C
	private void ServerGetFacebookIncentive_Succeeded(MsgGetNormalIncentiveSucceed msg)
	{
		foreach (ServerPresentState serverPresentState in msg.m_incentive)
		{
			if (serverPresentState != null)
			{
				IncentiveWindow window = new IncentiveWindow(serverPresentState.m_itemId, serverPresentState.m_numItem, this.m_anchorPath);
				this.m_windowQueue.AddWindow(window);
			}
		}
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(102);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x04001C43 RID: 7235
	private TinyFsmBehavior m_fsm;

	// Token: 0x04001C44 RID: 7236
	private bool m_isEndPlay;

	// Token: 0x04001C45 RID: 7237
	private string m_anchorPath;

	// Token: 0x04001C46 RID: 7238
	private SendApollo m_sendApollo;

	// Token: 0x04001C47 RID: 7239
	private IncentiveWindowQueue m_windowQueue = new IncentiveWindowQueue();

	// Token: 0x0200040C RID: 1036
	private enum EventSignal
	{
		// Token: 0x04001C49 RID: 7241
		PLAY_START = 100,
		// Token: 0x04001C4A RID: 7242
		REVIEW_END,
		// Token: 0x04001C4B RID: 7243
		INCENTIVE_CONNECT_END
	}
}
