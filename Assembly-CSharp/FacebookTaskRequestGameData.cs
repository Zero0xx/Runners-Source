using System;
using System.Collections.Generic;
using LitJson;
using Message;
using UnityEngine;

// Token: 0x02000A02 RID: 2562
public class FacebookTaskRequestGameData : SocialTaskBase
{
	// Token: 0x060043A3 RID: 17315 RVA: 0x0015EC74 File Offset: 0x0015CE74
	public FacebookTaskRequestGameData()
	{
		this.m_callbackObject = null;
		this.m_isEndProcess = false;
	}

	// Token: 0x060043A4 RID: 17316 RVA: 0x0015EC98 File Offset: 0x0015CE98
	public void Request(string userId, SocialTaskManager manager, GameObject callbackObject)
	{
		this.m_userId = userId;
		this.m_manager = manager;
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x060043A5 RID: 17317 RVA: 0x0015ECB0 File Offset: 0x0015CEB0
	protected override void OnStartProcess()
	{
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		List<SocialUserData> friendWithMeList = socialInterface.FriendWithMeList;
		foreach (SocialUserData socialUserData in friendWithMeList)
		{
			if (socialUserData != null)
			{
				if (socialUserData.Id == this.m_userId)
				{
					this.m_targetUser = socialUserData;
				}
			}
		}
		if (!FacebookUtil.IsLoggedIn())
		{
			this.m_isEndProcess = true;
			if (this.m_callbackObject != null)
			{
				MsgSocialCustomUserDataResponse msgSocialCustomUserDataResponse = new MsgSocialCustomUserDataResponse();
				msgSocialCustomUserDataResponse.m_isCreated = false;
				msgSocialCustomUserDataResponse.m_userData = this.m_targetUser;
				this.m_callbackObject.SendMessage("RequestGameDataEndCallback", msgSocialCustomUserDataResponse, SendMessageOptions.DontRequireReceiver);
			}
			return;
		}
		if (this.m_manager != null)
		{
			FacebookTaskRequestAction facebookTaskRequestAction = new FacebookTaskRequestAction();
			facebookTaskRequestAction.Request(this.m_userId, "testrunners:store", new FacebookTaskRequestAction.TaskFinishedCallback(this.RequestActionEndCallback));
			this.m_manager.RequestProcess(facebookTaskRequestAction);
		}
		this.m_isEndProcess = true;
	}

	// Token: 0x060043A6 RID: 17318 RVA: 0x0015EDDC File Offset: 0x0015CFDC
	protected override void OnUpdate()
	{
	}

	// Token: 0x060043A7 RID: 17319 RVA: 0x0015EDE0 File Offset: 0x0015CFE0
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x060043A8 RID: 17320 RVA: 0x0015EDE8 File Offset: 0x0015CFE8
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x060043A9 RID: 17321 RVA: 0x0015EDF0 File Offset: 0x0015CFF0
	private void RequestActionEndCallback(string responseText)
	{
		string actionId = FacebookUtil.GetActionId(responseText);
		string objectIdFromAction = FacebookUtil.GetObjectIdFromAction(responseText, "gamedata");
		if (string.IsNullOrEmpty(actionId) || string.IsNullOrEmpty(objectIdFromAction))
		{
			MsgSocialCustomUserDataResponse msgSocialCustomUserDataResponse = new MsgSocialCustomUserDataResponse();
			msgSocialCustomUserDataResponse.m_isCreated = false;
			msgSocialCustomUserDataResponse.m_userData = this.m_targetUser;
			this.m_callbackObject.SendMessage("RequestGameDataEndCallback", msgSocialCustomUserDataResponse, SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			this.m_targetUser.CustomData.ActionId = actionId;
			this.m_targetUser.CustomData.ObjectId = objectIdFromAction;
			if (this.m_manager != null)
			{
				FacebookTaskRequestObject facebookTaskRequestObject = new FacebookTaskRequestObject();
				facebookTaskRequestObject.Request(objectIdFromAction, new FacebookTaskRequestObject.TaskFinishedCallback(this.RequestObjectEndCallback));
				this.m_manager.RequestProcess(facebookTaskRequestObject);
			}
		}
	}

	// Token: 0x060043AA RID: 17322 RVA: 0x0015EEA8 File Offset: 0x0015D0A8
	private void RequestObjectEndCallback(string responseText)
	{
		bool flag = true;
		JsonData jsonData = JsonMapper.ToObject(responseText);
		if (jsonData == null)
		{
			global::Debug.Log("Failed transform plainText to Json");
			flag = false;
		}
		if (flag && NetUtil.GetJsonObject(jsonData, "data") == null)
		{
			global::Debug.Log("Not found object in json data");
			flag = false;
		}
		SocialInterface x = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (x == null)
		{
			flag = false;
		}
		if (flag)
		{
			if (this.m_targetUser != null)
			{
			}
			if (this.m_callbackObject != null)
			{
				MsgSocialCustomUserDataResponse msgSocialCustomUserDataResponse = new MsgSocialCustomUserDataResponse();
				msgSocialCustomUserDataResponse.m_isCreated = true;
				msgSocialCustomUserDataResponse.m_userData = this.m_targetUser;
				this.m_callbackObject.SendMessage("RequestGameDataEndCallback", msgSocialCustomUserDataResponse, SendMessageOptions.DontRequireReceiver);
			}
		}
		else if (this.m_callbackObject != null)
		{
			MsgSocialCustomUserDataResponse msgSocialCustomUserDataResponse2 = new MsgSocialCustomUserDataResponse();
			msgSocialCustomUserDataResponse2.m_isCreated = false;
			msgSocialCustomUserDataResponse2.m_userData = this.m_targetUser;
			this.m_callbackObject.SendMessage("RequestGameDataEndCallback", msgSocialCustomUserDataResponse2, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0400393F RID: 14655
	private readonly string TaskName = "FacebookTaskRequestGameData";

	// Token: 0x04003940 RID: 14656
	private SocialTaskManager m_manager;

	// Token: 0x04003941 RID: 14657
	private GameObject m_callbackObject;

	// Token: 0x04003942 RID: 14658
	private SocialUserData m_targetUser;

	// Token: 0x04003943 RID: 14659
	private bool m_isEndProcess;

	// Token: 0x04003944 RID: 14660
	private string m_userId;
}
