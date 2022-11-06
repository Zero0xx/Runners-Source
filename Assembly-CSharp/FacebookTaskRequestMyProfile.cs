using System;
using Facebook;
using LitJson;
using Message;
using UnityEngine;

// Token: 0x02000A04 RID: 2564
public class FacebookTaskRequestMyProfile : SocialTaskBase
{
	// Token: 0x060043B2 RID: 17330 RVA: 0x0015F12C File Offset: 0x0015D32C
	public FacebookTaskRequestMyProfile()
	{
		this.m_callbackObject = null;
		this.m_isEndProcess = false;
	}

	// Token: 0x060043B3 RID: 17331 RVA: 0x0015F150 File Offset: 0x0015D350
	public void Request(GameObject callbackObject)
	{
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x060043B4 RID: 17332 RVA: 0x0015F15C File Offset: 0x0015D35C
	protected override void OnStartProcess()
	{
		if (!FacebookUtil.IsLoggedIn())
		{
			this.m_isEndProcess = true;
			return;
		}
		string query = FacebookUtil.FBVersionString + "me?fields=id,picture,name";
		FB.API(query, HttpMethod.GET, new FacebookDelegate(this.RequestMyProfileEndCallback), null);
	}

	// Token: 0x060043B5 RID: 17333 RVA: 0x0015F1A4 File Offset: 0x0015D3A4
	protected override void OnUpdate()
	{
	}

	// Token: 0x060043B6 RID: 17334 RVA: 0x0015F1A8 File Offset: 0x0015D3A8
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x060043B7 RID: 17335 RVA: 0x0015F1B0 File Offset: 0x0015D3B0
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x060043B8 RID: 17336 RVA: 0x0015F1B8 File Offset: 0x0015D3B8
	private void RequestMyProfileEndCallback(FBResult fbResult)
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
		string text = fbResult.Text;
		global::Debug.Log("FacebookTaskRequestMyProfile.responseText = " + text);
		JsonData jsonData = JsonMapper.ToObject(text);
		if (jsonData == null)
		{
			global::Debug.Log("Failed transform plainText to Json");
			return;
		}
		bool flag = false;
		SocialUserData userData = FacebookUtil.GetUserData(jsonData, ref flag);
		if (userData == null)
		{
			return;
		}
		SocialResult socialResult = new SocialResult();
		socialResult.ResultId = 0;
		socialResult.Result = fbResult.Text;
		socialResult.IsError = false;
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null)
		{
			socialInterface.MyProfile = userData;
		}
		MsgSocialMyProfileResponse msgSocialMyProfileResponse = new MsgSocialMyProfileResponse();
		msgSocialMyProfileResponse.m_result = socialResult;
		msgSocialMyProfileResponse.m_profile = userData;
		this.m_callbackObject.SendMessage("RequestMyProfileEndCallback", msgSocialMyProfileResponse, SendMessageOptions.DontRequireReceiver);
		this.m_callbackObject = null;
		global::Debug.Log("Facebook Request My Profile is finished");
	}

	// Token: 0x04003948 RID: 14664
	private readonly string TaskName = "FacebookTaskRequestMyProfile";

	// Token: 0x04003949 RID: 14665
	private GameObject m_callbackObject;

	// Token: 0x0400394A RID: 14666
	private bool m_isEndProcess;
}
