using System;
using DataTable;
using Facebook;
using Message;
using UnityEngine;

// Token: 0x02000A08 RID: 2568
public class FacebookTaskFeed : SocialTaskBase
{
	// Token: 0x060043CE RID: 17358 RVA: 0x0015F70C File Offset: 0x0015D90C
	public FacebookTaskFeed()
	{
		this.m_callbackObject = null;
		this.m_isEndProcess = false;
	}

	// Token: 0x060043CF RID: 17359 RVA: 0x0015F730 File Offset: 0x0015D930
	public void Request(string feedCaption, string feedText, GameObject callbackObject)
	{
		this.m_feedCaption = feedCaption;
		this.m_feedText = feedText;
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x060043D0 RID: 17360 RVA: 0x0015F748 File Offset: 0x0015D948
	protected override void OnStartProcess()
	{
		if (!FacebookUtil.IsLoggedIn())
		{
			this.m_isEndProcess = true;
			return;
		}
		string picture = string.Empty;
		picture = InformationDataTable.GetUrl(InformationDataTable.Type.FB_FEED_PICTURE_ANDROID);
		FB.Feed(string.Empty, "http://sonicrunners.sega-net.com/upredirect/index.html", "SonicRunners", this.m_feedCaption, this.m_feedText, picture, string.Empty, string.Empty, string.Empty, string.Empty, null, new FacebookDelegate(this.FeedEndCallback));
	}

	// Token: 0x060043D1 RID: 17361 RVA: 0x0015F7B8 File Offset: 0x0015D9B8
	protected override void OnUpdate()
	{
	}

	// Token: 0x060043D2 RID: 17362 RVA: 0x0015F7BC File Offset: 0x0015D9BC
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x060043D3 RID: 17363 RVA: 0x0015F7C4 File Offset: 0x0015D9C4
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x060043D4 RID: 17364 RVA: 0x0015F7CC File Offset: 0x0015D9CC
	private void FeedEndCallback(FBResult fbResult)
	{
		this.m_isEndProcess = true;
		if (fbResult == null)
		{
			global::Debug.Log("Facebook.Login:fbResult is null");
			return;
		}
		global::Debug.Log("Facebook.Login:response= " + fbResult.Text);
		if (this.m_callbackObject == null)
		{
			return;
		}
		SocialResult socialResult = new SocialResult();
		socialResult.ResultId = 0;
		socialResult.Result = fbResult.Text;
		if (socialResult.Result.IndexOf("cancelled") >= 0)
		{
			socialResult.IsError = true;
		}
		else
		{
			socialResult.IsError = false;
		}
		MsgSocialNormalResponse msgSocialNormalResponse = new MsgSocialNormalResponse();
		msgSocialNormalResponse.m_result = socialResult;
		this.m_callbackObject.SendMessage("FeedEndCallback", msgSocialNormalResponse, SendMessageOptions.DontRequireReceiver);
		this.m_callbackObject = null;
		global::Debug.Log("Facebook Feed is finished");
	}

	// Token: 0x04003956 RID: 14678
	private readonly string TaskName = "FacebookTaskFeed";

	// Token: 0x04003957 RID: 14679
	private string m_feedCaption;

	// Token: 0x04003958 RID: 14680
	private string m_feedText;

	// Token: 0x04003959 RID: 14681
	private GameObject m_callbackObject;

	// Token: 0x0400395A RID: 14682
	private bool m_isEndProcess;
}
