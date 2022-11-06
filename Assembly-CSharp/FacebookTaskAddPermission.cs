using System;
using System.Collections.Generic;
using Facebook;
using Message;
using UnityEngine;

// Token: 0x020009F9 RID: 2553
public class FacebookTaskAddPermission : SocialTaskBase
{
	// Token: 0x06004366 RID: 17254 RVA: 0x0015DC80 File Offset: 0x0015BE80
	public FacebookTaskAddPermission()
	{
		this.m_callbackObject = null;
		this.m_isEndProcess = false;
	}

	// Token: 0x06004367 RID: 17255 RVA: 0x0015DCA4 File Offset: 0x0015BEA4
	public void Request(List<SocialInterface.Permission> permissions, GameObject callbackObject)
	{
		this.m_permissions = permissions;
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x06004368 RID: 17256 RVA: 0x0015DCB4 File Offset: 0x0015BEB4
	protected override void OnStartProcess()
	{
		if (!FacebookUtil.IsLoggedIn())
		{
			if (this.m_callbackObject != null)
			{
				this.m_callbackObject.SendMessage("AddPermissionEndCallback", null, SendMessageOptions.DontRequireReceiver);
				this.m_callbackObject = null;
			}
			this.m_isEndProcess = true;
			return;
		}
		if (this.m_permissions == null && this.m_callbackObject != null)
		{
			this.m_callbackObject.SendMessage("AddPermissionEndCallback", null, SendMessageOptions.DontRequireReceiver);
			this.m_callbackObject = null;
		}
		int count = this.m_permissions.Count;
		if (count <= 0 && this.m_callbackObject != null)
		{
			this.m_callbackObject.SendMessage("AddPermissionEndCallback", null, SendMessageOptions.DontRequireReceiver);
			this.m_callbackObject = null;
		}
		string text = string.Empty;
		for (int i = 0; i < count; i++)
		{
			if (i > 0)
			{
				text += ",";
			}
			SocialInterface.Permission permission = this.m_permissions[i];
			text += FacebookUtil.PermissionString[(int)permission];
		}
		FB.Login(text, new FacebookDelegate(this.AddPermissionEndCallback));
	}

	// Token: 0x06004369 RID: 17257 RVA: 0x0015DDC8 File Offset: 0x0015BFC8
	protected override void OnUpdate()
	{
	}

	// Token: 0x0600436A RID: 17258 RVA: 0x0015DDCC File Offset: 0x0015BFCC
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x0600436B RID: 17259 RVA: 0x0015DDD4 File Offset: 0x0015BFD4
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x0600436C RID: 17260 RVA: 0x0015DDDC File Offset: 0x0015BFDC
	private void AddPermissionEndCallback(FBResult fbResult)
	{
		this.m_fbResult = fbResult;
		string query = FacebookUtil.FBVersionString + "me/permissions";
		FB.API(query, HttpMethod.GET, new FacebookDelegate(this.RequestPermissionEndCallback), null);
	}

	// Token: 0x0600436D RID: 17261 RVA: 0x0015DE18 File Offset: 0x0015C018
	private void RequestPermissionEndCallback(FBResult fbResult)
	{
		if (this.m_callbackObject == null)
		{
			return;
		}
		if (fbResult == null)
		{
			this.m_callbackObject.SendMessage("AddPermissionEndCallback", null, SendMessageOptions.DontRequireReceiver);
			this.m_callbackObject = null;
			return;
		}
		if (fbResult.Text == null)
		{
			this.m_callbackObject.SendMessage("AddPermissionEndCallback", null, SendMessageOptions.DontRequireReceiver);
			this.m_callbackObject = null;
			return;
		}
		string text = fbResult.Text;
		FacebookUtil.UpdatePermissionInfo(text);
		this.m_isEndProcess = true;
		MsgSocialNormalResponse msgSocialNormalResponse = new MsgSocialNormalResponse();
		msgSocialNormalResponse.m_result = new SocialResult
		{
			ResultId = 0,
			Result = this.m_fbResult.Text,
			IsError = false
		};
		this.m_callbackObject.SendMessage("AddPermissionEndCallback", msgSocialNormalResponse, SendMessageOptions.DontRequireReceiver);
		this.m_callbackObject = null;
		global::Debug.Log("Facebook Add Permission is finished");
	}

	// Token: 0x04003921 RID: 14625
	private readonly string TaskName = "FacebookTaskAddPermission";

	// Token: 0x04003922 RID: 14626
	private GameObject m_callbackObject;

	// Token: 0x04003923 RID: 14627
	private List<SocialInterface.Permission> m_permissions;

	// Token: 0x04003924 RID: 14628
	private FBResult m_fbResult;

	// Token: 0x04003925 RID: 14629
	private bool m_isEndProcess;
}
