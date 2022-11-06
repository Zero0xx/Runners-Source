using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009F3 RID: 2547
public class SocialPlatformFacebook : SocialPlatform
{
	// Token: 0x06004331 RID: 17201 RVA: 0x0015D42C File Offset: 0x0015B62C
	private void Start()
	{
		this.m_manager = new SocialTaskManager();
	}

	// Token: 0x06004332 RID: 17202 RVA: 0x0015D43C File Offset: 0x0015B63C
	public override void Initialize(GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskInitialize facebookTaskInitialize = new FacebookTaskInitialize();
		facebookTaskInitialize.Request(callbackObject);
		this.m_manager.RequestProcess(facebookTaskInitialize);
	}

	// Token: 0x06004333 RID: 17203 RVA: 0x0015D470 File Offset: 0x0015B670
	public override void Login(GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskLogin facebookTaskLogin = new FacebookTaskLogin();
		facebookTaskLogin.Request(callbackObject);
		this.m_manager.RequestProcess(facebookTaskLogin);
	}

	// Token: 0x06004334 RID: 17204 RVA: 0x0015D4A4 File Offset: 0x0015B6A4
	public override void Logout()
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskLogout facebookTaskLogout = new FacebookTaskLogout();
		facebookTaskLogout.Request();
		this.m_manager.RequestProcess(facebookTaskLogout);
	}

	// Token: 0x06004335 RID: 17205 RVA: 0x0015D4D8 File Offset: 0x0015B6D8
	public override void RequestMyProfile(GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskRequestMyProfile facebookTaskRequestMyProfile = new FacebookTaskRequestMyProfile();
		facebookTaskRequestMyProfile.Request(callbackObject);
		this.m_manager.RequestProcess(facebookTaskRequestMyProfile);
	}

	// Token: 0x06004336 RID: 17206 RVA: 0x0015D50C File Offset: 0x0015B70C
	public override void RequestFriendList(GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskRequestFriendList facebookTaskRequestFriendList = new FacebookTaskRequestFriendList();
		facebookTaskRequestFriendList.Request(callbackObject);
		this.m_manager.RequestProcess(facebookTaskRequestFriendList);
	}

	// Token: 0x06004337 RID: 17207 RVA: 0x0015D540 File Offset: 0x0015B740
	public override void SetScore(SocialDefine.ScoreType type, int score, GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskSetScore facebookTaskSetScore = new FacebookTaskSetScore();
		facebookTaskSetScore.Request(type, score, callbackObject);
		this.m_manager.RequestProcess(facebookTaskSetScore);
	}

	// Token: 0x06004338 RID: 17208 RVA: 0x0015D574 File Offset: 0x0015B774
	public override void CreateMyGameData(string gameId, GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskCreateGameData facebookTaskCreateGameData = new FacebookTaskCreateGameData();
		facebookTaskCreateGameData.Request(gameId, this.m_manager, callbackObject);
		this.m_manager.RequestProcess(facebookTaskCreateGameData);
	}

	// Token: 0x06004339 RID: 17209 RVA: 0x0015D5B0 File Offset: 0x0015B7B0
	public override void RequestGameData(string userId, GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskRequestGameData facebookTaskRequestGameData = new FacebookTaskRequestGameData();
		facebookTaskRequestGameData.Request(userId, this.m_manager, callbackObject);
		this.m_manager.RequestProcess(facebookTaskRequestGameData);
	}

	// Token: 0x0600433A RID: 17210 RVA: 0x0015D5EC File Offset: 0x0015B7EC
	public override void DeleteGameData(GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskDeleteGameData facebookTaskDeleteGameData = new FacebookTaskDeleteGameData();
		facebookTaskDeleteGameData.Request(this.m_manager, callbackObject);
		this.m_manager.RequestProcess(facebookTaskDeleteGameData);
	}

	// Token: 0x0600433B RID: 17211 RVA: 0x0015D624 File Offset: 0x0015B824
	public override void InviteFriend(GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskInviteFriend facebookTaskInviteFriend = new FacebookTaskInviteFriend();
		facebookTaskInviteFriend.Request(callbackObject);
		this.m_manager.RequestProcess(facebookTaskInviteFriend);
	}

	// Token: 0x0600433C RID: 17212 RVA: 0x0015D658 File Offset: 0x0015B858
	public override void ReceiveEnergy(string energyId, GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
	}

	// Token: 0x0600433D RID: 17213 RVA: 0x0015D668 File Offset: 0x0015B868
	public override void Feed(string feedCaption, string feedText, GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskFeed facebookTaskFeed = new FacebookTaskFeed();
		facebookTaskFeed.Request(feedCaption, feedText, callbackObject);
		this.m_manager.RequestProcess(facebookTaskFeed);
	}

	// Token: 0x0600433E RID: 17214 RVA: 0x0015D69C File Offset: 0x0015B89C
	public override void SendEnergy(SocialUserData userData, GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskSendEnergy facebookTaskSendEnergy = new FacebookTaskSendEnergy();
		facebookTaskSendEnergy.Request(userData, callbackObject);
		this.m_manager.RequestProcess(facebookTaskSendEnergy);
	}

	// Token: 0x0600433F RID: 17215 RVA: 0x0015D6D0 File Offset: 0x0015B8D0
	public override void RequestInvitedFriend(GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskRequestInviteList facebookTaskRequestInviteList = new FacebookTaskRequestInviteList();
		facebookTaskRequestInviteList.Request(callbackObject);
		this.m_manager.RequestProcess(facebookTaskRequestInviteList);
	}

	// Token: 0x06004340 RID: 17216 RVA: 0x0015D704 File Offset: 0x0015B904
	public override void RequestPermission(GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskRequestPermission facebookTaskRequestPermission = new FacebookTaskRequestPermission();
		facebookTaskRequestPermission.Request(callbackObject);
		this.m_manager.RequestProcess(facebookTaskRequestPermission);
	}

	// Token: 0x06004341 RID: 17217 RVA: 0x0015D738 File Offset: 0x0015B938
	public override void AddPermission(List<SocialInterface.Permission> permissions, GameObject callbackObject)
	{
		if (this.m_manager == null)
		{
			return;
		}
		FacebookTaskAddPermission facebookTaskAddPermission = new FacebookTaskAddPermission();
		facebookTaskAddPermission.Request(permissions, callbackObject);
		this.m_manager.RequestProcess(facebookTaskAddPermission);
	}

	// Token: 0x06004342 RID: 17218 RVA: 0x0015D76C File Offset: 0x0015B96C
	private void Update()
	{
		if (this.m_manager != null)
		{
			this.m_manager.Update();
		}
	}

	// Token: 0x04003905 RID: 14597
	private SocialTaskManager m_manager;

	// Token: 0x04003906 RID: 14598
	private GameObject m_callbackObject;
}
