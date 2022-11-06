using System;
using Facebook;
using Message;
using UnityEngine;

// Token: 0x02000A05 RID: 2565
public class FacebookTaskRequestPermission : SocialTaskBase
{
	// Token: 0x060043B9 RID: 17337 RVA: 0x0015F2A8 File Offset: 0x0015D4A8
	public FacebookTaskRequestPermission()
	{
		this.m_callbackObject = null;
		this.m_isEndProcess = false;
	}

	// Token: 0x060043BA RID: 17338 RVA: 0x0015F2CC File Offset: 0x0015D4CC
	public void Request(GameObject callbackObject)
	{
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x060043BB RID: 17339 RVA: 0x0015F2D8 File Offset: 0x0015D4D8
	protected override void OnStartProcess()
	{
		if (!FacebookUtil.IsLoggedIn())
		{
			if (this.m_callbackObject != null)
			{
				this.m_callbackObject.SendMessage("RequestPermissionEndCallback", null, SendMessageOptions.DontRequireReceiver);
				this.m_callbackObject = null;
			}
			this.m_isEndProcess = true;
			return;
		}
		string query = FacebookUtil.FBVersionString + "me/permissions";
		FB.API(query, HttpMethod.GET, new FacebookDelegate(this.RequestPermissionEndCallback), null);
	}

	// Token: 0x060043BC RID: 17340 RVA: 0x0015F34C File Offset: 0x0015D54C
	protected override void OnUpdate()
	{
	}

	// Token: 0x060043BD RID: 17341 RVA: 0x0015F350 File Offset: 0x0015D550
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x060043BE RID: 17342 RVA: 0x0015F358 File Offset: 0x0015D558
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x060043BF RID: 17343 RVA: 0x0015F360 File Offset: 0x0015D560
	private void RequestPermissionEndCallback(FBResult fbResult)
	{
		this.m_isEndProcess = true;
		if (fbResult == null)
		{
			if (this.m_callbackObject != null)
			{
				this.m_callbackObject.SendMessage("RequestPermissionEndCallback", null, SendMessageOptions.DontRequireReceiver);
				this.m_callbackObject = null;
			}
			return;
		}
		if (fbResult.Text == null)
		{
			if (this.m_callbackObject != null)
			{
				this.m_callbackObject.SendMessage("RequestPermissionEndCallback", null, SendMessageOptions.DontRequireReceiver);
				this.m_callbackObject = null;
			}
			return;
		}
		if (this.m_callbackObject == null)
		{
			return;
		}
		string text = fbResult.Text;
		FacebookUtil.UpdatePermissionInfo(text);
		MsgSocialNormalResponse msgSocialNormalResponse = new MsgSocialNormalResponse();
		msgSocialNormalResponse.m_result = new SocialResult
		{
			ResultId = 0,
			Result = fbResult.Text,
			IsError = false
		};
		this.m_callbackObject.SendMessage("RequestPermissionEndCallback", msgSocialNormalResponse, SendMessageOptions.DontRequireReceiver);
		this.m_callbackObject = null;
		global::Debug.Log("Facebook Request Permission is finished");
	}

	// Token: 0x0400394B RID: 14667
	private readonly string TaskName = "FacebookTaskRequestPermission";

	// Token: 0x0400394C RID: 14668
	private GameObject m_callbackObject;

	// Token: 0x0400394D RID: 14669
	private bool m_isEndProcess;
}
