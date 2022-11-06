using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000A06 RID: 2566
public class FacebookTaskSendEnergy : SocialTaskBase
{
	// Token: 0x060043C0 RID: 17344 RVA: 0x0015F44C File Offset: 0x0015D64C
	public FacebookTaskSendEnergy()
	{
		this.m_callbackObject = null;
		this.m_isEndProcess = false;
	}

	// Token: 0x060043C1 RID: 17345 RVA: 0x0015F470 File Offset: 0x0015D670
	public void Request(SocialUserData userData, GameObject callbackObject)
	{
		this.m_userData = userData;
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x060043C2 RID: 17346 RVA: 0x0015F480 File Offset: 0x0015D680
	protected override void OnStartProcess()
	{
		bool flag = true;
		if (!FacebookUtil.IsLoggedIn())
		{
			flag = false;
		}
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null && !socialInterface.IsGrantedPermission[1])
		{
			flag = false;
		}
		if (!flag)
		{
			this.m_isEndProcess = true;
			MsgSocialNormalResponse msgSocialNormalResponse = new MsgSocialNormalResponse();
			msgSocialNormalResponse.m_result = null;
			this.m_callbackObject.SendMessage("SendEnergyEndCallback", msgSocialNormalResponse, SendMessageOptions.DontRequireReceiver);
			this.m_callbackObject = null;
			return;
		}
		if (this.m_userData == null)
		{
			if (this.m_callbackObject != null)
			{
				this.m_callbackObject.SendMessage("SendEnergyEndCallback", null, SendMessageOptions.DontRequireReceiver);
			}
			return;
		}
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "SnsFeed", "gw_send_challenge_caption").text;
		string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "SnsFeed", "gw_send_challenge_text").text;
	}

	// Token: 0x060043C3 RID: 17347 RVA: 0x0015F554 File Offset: 0x0015D754
	protected override void OnUpdate()
	{
	}

	// Token: 0x060043C4 RID: 17348 RVA: 0x0015F558 File Offset: 0x0015D758
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x060043C5 RID: 17349 RVA: 0x0015F560 File Offset: 0x0015D760
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x060043C6 RID: 17350 RVA: 0x0015F568 File Offset: 0x0015D768
	private void SendEnergyEndCallback(FBResult fbResult)
	{
		this.m_isEndProcess = true;
		if (fbResult == null)
		{
			return;
		}
		if (this.m_callbackObject == null)
		{
			return;
		}
		SocialResult socialResult = new SocialResult();
		socialResult.ResultId = 0;
		socialResult.Result = fbResult.Text;
		socialResult.IsError = false;
		MsgSocialNormalResponse msgSocialNormalResponse = new MsgSocialNormalResponse();
		msgSocialNormalResponse.m_result = socialResult;
		this.m_callbackObject.SendMessage("SendEnergyEndCallback", msgSocialNormalResponse, SendMessageOptions.DontRequireReceiver);
		this.m_callbackObject = null;
		global::Debug.Log("Facebook sendEnergy is finished");
	}

	// Token: 0x0400394E RID: 14670
	private readonly string TaskName = "FacebookTaskSendEnergy";

	// Token: 0x0400394F RID: 14671
	private SocialUserData m_userData;

	// Token: 0x04003950 RID: 14672
	private GameObject m_callbackObject;

	// Token: 0x04003951 RID: 14673
	private bool m_isEndProcess;
}
