using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001C3 RID: 451
public class FacebookTestUserCreater : MonoBehaviour
{
	// Token: 0x06000CC7 RID: 3271 RVA: 0x00048DDC File Offset: 0x00046FDC
	public void Create(int createCount)
	{
		this.m_requestCreateCount = createCount;
		string empty = string.Empty;
		string empty2 = string.Empty;
		for (int i = 0; i < this.m_requestCreateCount; i++)
		{
			string text = "https://graph.facebook.com/";
			text += empty;
			text += "/accounts/test-users?installed=true&locale=ja_JP&permissions=read_stream&method=post&access_token=";
			text += empty2;
			global::Debug.Log("FacebookTestUserCreater.Create.query = " + text);
			WWW www = new WWW(text);
			base.StartCoroutine(this.WaitWWW(www, new FacebookTestUserCreater.UpdateInfoCallback(this.CreateTestUserCallback)));
		}
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x00048E68 File Offset: 0x00047068
	private void CreateTestUserCallback(WWW wwwResult)
	{
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x00048E6C File Offset: 0x0004706C
	private IEnumerator WaitWWW(WWW www, FacebookTestUserCreater.UpdateInfoCallback callback)
	{
		while (!www.isDone)
		{
			yield return null;
		}
		callback(www);
		yield break;
	}

	// Token: 0x04000A1A RID: 2586
	private int m_requestCreateCount;

	// Token: 0x02000A77 RID: 2679
	// (Invoke) Token: 0x06004816 RID: 18454
	public delegate void UpdateInfoCallback(WWW resultWWW);
}
