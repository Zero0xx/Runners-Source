using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000266 RID: 614
public class FriendSignManager : MonoBehaviour
{
	// Token: 0x06001075 RID: 4213 RVA: 0x0006030C File Offset: 0x0005E50C
	protected void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x00060318 File Offset: 0x0005E518
	public void SetupFriendSignManager()
	{
		this.DebugDraw("SetupFriendSignManager");
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		PlayerImageManager playerImageManager = GameObjectUtil.FindGameObjectComponent<PlayerImageManager>("PlayerImageManager");
		List<ServerDistanceFriendEntry> distanceFriendEntry = ServerInterface.DistanceFriendEntry;
		if (socialInterface != null && playerImageManager != null && socialInterface.FriendList != null)
		{
			int num = UnityEngine.Random.Range(0, socialInterface.FriendList.Count);
			int num2 = num;
			for (int i = 0; i < socialInterface.FriendList.Count; i++)
			{
				if (num2 >= socialInterface.FriendList.Count)
				{
					num2 = 0;
				}
				Texture2D playerImage = playerImageManager.GetPlayerImage(socialInterface.FriendList[num2].Id);
				int distance = this.GetDistance(distanceFriendEntry, socialInterface.FriendList[num2].CustomData);
				if (distance > 0)
				{
					FriendSignData item = new FriendSignData(this.m_data.Count, (float)distance, playerImage, false);
					this.m_data.Add(item);
					if (this.m_data.Count >= FriendSignManager.CREATE_MAX)
					{
						break;
					}
				}
				num2++;
			}
		}
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x00060440 File Offset: 0x0005E640
	public List<FriendSignData> GetFriendSignDataList()
	{
		return this.m_data;
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x00060448 File Offset: 0x0005E648
	public void SetAppear(int index)
	{
		if (index < this.m_data.Count)
		{
			this.m_data[index].m_appear = true;
			this.DebugDraw("SetAppear " + FriendSignManager.GetDebugDataString(this.m_data[index]));
		}
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x0006049C File Offset: 0x0005E69C
	private void DebugDraw(string msg)
	{
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x000604A0 File Offset: 0x0005E6A0
	public static string GetDebugDataString(FriendSignData data)
	{
		if (data != null)
		{
			string text = (!(data.m_texture == null)) ? "ok" : "null";
			return string.Concat(new object[]
			{
				"Friend(idx=",
				data.m_index,
				", dis=",
				data.m_distance,
				", tex=",
				text,
				", app=",
				data.m_appear.ToString(),
				")"
			});
		}
		return "Friend(null)";
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x0006053C File Offset: 0x0005E73C
	private int GetDistance(List<ServerDistanceFriendEntry> friendEntry, SocialUserCustomData customData)
	{
		if (friendEntry != null && customData != null)
		{
			foreach (ServerDistanceFriendEntry serverDistanceFriendEntry in friendEntry)
			{
				if (serverDistanceFriendEntry != null)
				{
					this.DebugDraw(string.Concat(new object[]
					{
						"fe.m_friendId=",
						serverDistanceFriendEntry.m_friendId,
						" customData.GameId=",
						customData.GameId,
						" fe.m_distance=",
						serverDistanceFriendEntry.m_distance
					}));
					if (serverDistanceFriendEntry.m_friendId == customData.GameId)
					{
						return serverDistanceFriendEntry.m_distance;
					}
				}
			}
			return 0;
		}
		return 0;
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x00060618 File Offset: 0x0005E818
	private void OnMsgExitStage(MsgExitStage msg)
	{
		base.enabled = false;
	}

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x0600107D RID: 4221 RVA: 0x00060624 File Offset: 0x0005E824
	public static FriendSignManager Instance
	{
		get
		{
			if (FriendSignManager.instance == null)
			{
				FriendSignManager.instance = (UnityEngine.Object.FindObjectOfType(typeof(FriendSignManager)) as FriendSignManager);
			}
			return FriendSignManager.instance;
		}
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x00060660 File Offset: 0x0005E860
	protected bool CheckInstance()
	{
		if (FriendSignManager.instance == null)
		{
			FriendSignManager.instance = this;
			return true;
		}
		if (this == FriendSignManager.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x000606A4 File Offset: 0x0005E8A4
	private void OnDestroy()
	{
		if (FriendSignManager.instance == this)
		{
			FriendSignManager.instance = null;
		}
	}

	// Token: 0x04000F0B RID: 3851
	public bool m_debugDraw;

	// Token: 0x04000F0C RID: 3852
	public bool m_debugFriend;

	// Token: 0x04000F0D RID: 3853
	private Texture2D m_debugTexture;

	// Token: 0x04000F0E RID: 3854
	private static int CREATE_MAX = 10;

	// Token: 0x04000F0F RID: 3855
	private List<FriendSignData> m_data = new List<FriendSignData>();

	// Token: 0x04000F10 RID: 3856
	private static FriendSignManager instance;
}
