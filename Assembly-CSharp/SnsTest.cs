using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000A23 RID: 2595
public class SnsTest : MonoBehaviour
{
	// Token: 0x060044BB RID: 17595 RVA: 0x00161EB0 File Offset: 0x001600B0
	private void Start()
	{
		GameObject gameObject = GameObject.Find("SocialInterface");
		if (gameObject != null)
		{
			this.m_socialInterface = gameObject.GetComponent<SocialInterface>();
			if (this.m_socialInterface != null)
			{
				this.m_socialInterface.Initialize(base.gameObject);
			}
		}
	}

	// Token: 0x060044BC RID: 17596 RVA: 0x00161F04 File Offset: 0x00160104
	private void Update()
	{
	}

	// Token: 0x060044BD RID: 17597 RVA: 0x00161F08 File Offset: 0x00160108
	private void OnGUI()
	{
		if (GUI.Button(new Rect(100f, 100f, 300f, 150f), "Login") && this.m_socialInterface != null)
		{
			this.m_socialInterface.Login(base.gameObject);
		}
		if (GUI.Button(new Rect(100f, 300f, 300f, 150f), "Logout") && this.m_socialInterface != null)
		{
			this.m_socialInterface.Logout();
		}
		if (GUI.Button(new Rect(450f, 100f, 300f, 150f), "Feed") && this.m_socialInterface != null)
		{
			this.m_socialInterface.Feed("マイレージ達成!", "マイレージマップ10を終えて、11に進んでます。", base.gameObject);
		}
		if (GUI.Button(new Rect(450f, 300f, 300f, 150f), "GetFriendList") && this.m_socialInterface != null)
		{
			this.m_socialInterface.RequestFriendList(base.gameObject);
		}
		if (this.m_profileMsg != null)
		{
			GUI.Label(new Rect(750f, 200f, 300f, 150f), this.m_profileMsg.m_profile.Name);
			GUI.Label(new Rect(750f, 300f, 300f, 150f), this.m_profileMsg.m_profile.Url);
		}
		if (this.m_friendListMsg != null)
		{
			List<SocialUserData> friends = this.m_friendListMsg.m_friends;
			int num = 0;
			foreach (SocialUserData socialUserData in friends)
			{
				if (socialUserData != null)
				{
					GUI.Label(new Rect(750f, (float)(430 + 100 * num), 300f, 150f), socialUserData.Name);
					GUI.Label(new Rect(750f, (float)(480 + 100 * num), 300f, 150f), socialUserData.Id);
					num++;
				}
			}
			if (!GUI.Button(new Rect(100f, 500f, 300f, 150f), "InviteFriend") || this.m_socialInterface != null)
			{
			}
			if (GUI.Button(new Rect(450f, 500f, 300f, 150f), "GetFriendScore") && this.m_socialInterface != null)
			{
				this.m_socialInterface.RequestGameData(friends[0].Id, base.gameObject);
			}
		}
		if (this.m_score != 0)
		{
			GUI.Label(new Rect(850f, 400f, 300f, 150f), this.m_score.ToString());
		}
	}

	// Token: 0x060044BE RID: 17598 RVA: 0x00162234 File Offset: 0x00160434
	private void RequestMyProfileEndCallback(MsgSocialMyProfileResponse msg)
	{
		global::Debug.Log("RequestMyProfileEndCallback");
		if (msg == null)
		{
			return;
		}
		this.m_profileMsg = msg;
	}

	// Token: 0x060044BF RID: 17599 RVA: 0x00162250 File Offset: 0x00160450
	private void RequestFriendListEndCallback(MsgSocialFriendListResponse msg)
	{
		global::Debug.Log("RequestFriendListEndCallback");
		if (msg == null)
		{
			return;
		}
		this.m_friendListMsg = msg;
	}

	// Token: 0x060044C0 RID: 17600 RVA: 0x0016226C File Offset: 0x0016046C
	private void RequestGameDataEndCallback(MsgSocialCustomUserDataResponse msg)
	{
	}

	// Token: 0x040039B3 RID: 14771
	private SocialInterface m_socialInterface;

	// Token: 0x040039B4 RID: 14772
	private MsgSocialMyProfileResponse m_profileMsg;

	// Token: 0x040039B5 RID: 14773
	private MsgSocialFriendListResponse m_friendListMsg;

	// Token: 0x040039B6 RID: 14774
	private int m_score;
}
