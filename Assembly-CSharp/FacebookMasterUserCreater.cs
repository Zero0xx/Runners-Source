using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

// Token: 0x020001C4 RID: 452
public class FacebookMasterUserCreater : MonoBehaviour
{
	// Token: 0x06000CCC RID: 3276 RVA: 0x00048ED4 File Offset: 0x000470D4
	public void AttachFriend(string userId, string userAccessToken)
	{
		this.m_appUserList.Clear();
		this.m_friendList.Clear();
		this.m_meInfo = new UserInfo();
		this.m_meInfo.id = userId;
		this.m_meInfo.accessToken = userAccessToken;
		string text = "https://graph.facebook.com/";
		text += FacebookMasterUserCreater.AppId;
		text += "/accounts/test-users?access_token=";
		text += FacebookMasterUserCreater.AppAccessToken;
		text += "&limit=5000";
		WWW www = new WWW(text);
		base.StartCoroutine(this.WaitWWW(www, new FacebookMasterUserCreater.UpdateInfoCallback(this.GetAllTestUserCallback)));
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x00048F74 File Offset: 0x00047174
	private void GetAllTestUserCallback(WWW wwwResult)
	{
		string text = wwwResult.text;
		global::Debug.Log(text);
		JsonData jsonData = JsonMapper.ToObject(text);
		if (jsonData != null)
		{
			JsonData jsonArray = NetUtil.GetJsonArray(jsonData, "data");
			if (jsonArray != null)
			{
				for (int i = 0; i < jsonArray.Count; i++)
				{
					UserInfo userInfo = new UserInfo();
					userInfo.id = NetUtil.GetJsonString(jsonArray[i], "id");
					userInfo.accessToken = NetUtil.GetJsonString(jsonArray[i], "access_token");
					userInfo.loginUrl = NetUtil.GetJsonString(jsonArray[i], "login_url");
					if (!string.IsNullOrEmpty(userInfo.id))
					{
						if (!string.IsNullOrEmpty(userInfo.accessToken))
						{
							this.m_appUserList.Add(userInfo);
						}
					}
				}
			}
		}
		string text2 = "https://graph.facebook.com/";
		text2 += this.m_meInfo.id;
		text2 += "/friends?access_token=";
		text2 += this.m_meInfo.accessToken;
		text2 += "&limit=5000";
		WWW www = new WWW(text2);
		base.StartCoroutine(this.WaitWWW(www, new FacebookMasterUserCreater.UpdateInfoCallback(this.GetMasterUserFriendsCallback)));
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x000490BC File Offset: 0x000472BC
	private void GetMasterUserFriendsCallback(WWW wwwResult)
	{
		string text = wwwResult.text;
		global::Debug.Log(text);
		JsonData jsonData = JsonMapper.ToObject(text);
		if (jsonData != null)
		{
			JsonData jsonArray = NetUtil.GetJsonArray(jsonData, "data");
			if (jsonArray != null)
			{
				for (int i = 0; i < jsonArray.Count; i++)
				{
					UserInfo userInfo = new UserInfo();
					userInfo.id = NetUtil.GetJsonString(jsonArray[i], "id");
					userInfo.accessToken = NetUtil.GetJsonString(jsonArray[i], "access_token");
					userInfo.loginUrl = NetUtil.GetJsonString(jsonArray[i], "login_url");
					this.m_friendList.Add(userInfo.id, userInfo);
				}
			}
		}
		foreach (UserInfo userInfo2 in this.m_appUserList)
		{
			if (userInfo2 != null)
			{
				if (this.m_meInfo != userInfo2)
				{
					if (!this.m_friendList.ContainsKey(userInfo2.id))
					{
						string text2 = "https://graph.facebook.com/";
						text2 += this.m_meInfo.id;
						text2 += "/friends/";
						text2 += userInfo2.id;
						text2 += "?access_token=";
						text2 += this.m_meInfo.accessToken;
						text2 += "&method=post";
						string text3 = "https://graph.facebook.com/";
						text3 += userInfo2.id;
						text3 += "/friends/";
						text3 += this.m_meInfo.id;
						text3 += "?access_token=";
						text3 += userInfo2.accessToken;
						text3 += "&method=post";
						base.StartCoroutine(this.WaitWWWScheduled(text2, text3, new FacebookMasterUserCreater.UpdateInfoCallbackScheduled(this.AttachFriendCallback)));
					}
				}
			}
		}
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x000492EC File Offset: 0x000474EC
	private void AttachFriendCallback(WWW wwwResult1, WWW wwwResult2)
	{
		global::Debug.Log(wwwResult1.text);
		global::Debug.Log(wwwResult2.text);
		wwwResult1.Dispose();
		wwwResult2.Dispose();
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0004931C File Offset: 0x0004751C
	private IEnumerator WaitWWW(WWW www, FacebookMasterUserCreater.UpdateInfoCallback callback)
	{
		while (!www.isDone)
		{
			yield return null;
		}
		callback(www);
		yield break;
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x0004934C File Offset: 0x0004754C
	private IEnumerator WaitWWWScheduled(string request1, string request2, FacebookMasterUserCreater.UpdateInfoCallbackScheduled callback)
	{
		WWW www = new WWW(request1);
		while (!www.isDone)
		{
			yield return null;
		}
		WWW www2 = new WWW(request2);
		while (!www2.isDone)
		{
			yield return null;
		}
		callback(www, www2);
		yield break;
	}

	// Token: 0x04000A1B RID: 2587
	private UserInfo m_meInfo;

	// Token: 0x04000A1C RID: 2588
	private List<UserInfo> m_appUserList = new List<UserInfo>();

	// Token: 0x04000A1D RID: 2589
	private Dictionary<string, UserInfo> m_friendList = new Dictionary<string, UserInfo>();

	// Token: 0x04000A1E RID: 2590
	private static readonly string AppId = "203227836537595";

	// Token: 0x04000A1F RID: 2591
	private static readonly string AppAccessToken = "203227836537595|PkqHXt8JyVfadw4sjwxAlqGyEig";

	// Token: 0x02000A78 RID: 2680
	// (Invoke) Token: 0x0600481A RID: 18458
	public delegate void UpdateInfoCallback(WWW resultWWW);

	// Token: 0x02000A79 RID: 2681
	// (Invoke) Token: 0x0600481E RID: 18462
	public delegate void UpdateInfoCallbackScheduled(WWW resultWWW1, WWW resultWWW2);
}
