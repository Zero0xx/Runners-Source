using System;
using System.Collections;
using NoahUnity;
using UnityEngine;

// Token: 0x02000A5B RID: 2651
public class NoahHandler : MonoBehaviour, NoahHandlerInterface
{
	// Token: 0x06004769 RID: 18281 RVA: 0x00177CB8 File Offset: 0x00175EB8
	public void SetCallback(GameObject callbackObject)
	{
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x0600476A RID: 18282 RVA: 0x00177CC4 File Offset: 0x00175EC4
	public void SetGUID(string guid)
	{
		this.m_guid = guid;
		global::Debug.Log("NoahHandler.SetGUID=" + ((guid != null) ? guid : "null"));
		if (this.m_isEndConnect && !this.m_isEndSetGUID)
		{
			Noah.Instance.SetGUID(this.m_guid);
		}
	}

	// Token: 0x0600476B RID: 18283 RVA: 0x00177D20 File Offset: 0x00175F20
	public string GetGUID()
	{
		return this.m_guid;
	}

	// Token: 0x1700099D RID: 2461
	// (get) Token: 0x0600476C RID: 18284 RVA: 0x00177D28 File Offset: 0x00175F28
	// (set) Token: 0x0600476D RID: 18285 RVA: 0x00177D30 File Offset: 0x00175F30
	public static NoahHandler Instance
	{
		get
		{
			return NoahHandler.m_instance;
		}
		private set
		{
		}
	}

	// Token: 0x1700099E RID: 2462
	// (get) Token: 0x0600476E RID: 18286 RVA: 0x00177D34 File Offset: 0x00175F34
	// (set) Token: 0x0600476F RID: 18287 RVA: 0x00177D3C File Offset: 0x00175F3C
	public bool IsEndConnect
	{
		get
		{
			return this.m_isEndConnect;
		}
		set
		{
			this.m_isEndConnect = value;
		}
	}

	// Token: 0x1700099F RID: 2463
	// (get) Token: 0x06004770 RID: 18288 RVA: 0x00177D48 File Offset: 0x00175F48
	// (set) Token: 0x06004771 RID: 18289 RVA: 0x00177D50 File Offset: 0x00175F50
	public bool IsEndSetGUID
	{
		get
		{
			return this.m_isEndSetGUID;
		}
		set
		{
			this.m_isEndSetGUID = value;
		}
	}

	// Token: 0x06004772 RID: 18290 RVA: 0x00177D5C File Offset: 0x00175F5C
	public void Awake()
	{
		if (NoahHandler.m_instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
			if (Application.platform == RuntimePlatform.Android)
			{
				NoahHandler.consumer_key = "APP_65453158ce4ca288";
				NoahHandler.secret_key = "KEY_89353158ce4ca33f";
				NoahHandler.action_id = "OFF_42953158cf5ccd0c";
			}
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				NoahHandler.consumer_key = "APP_20953158ca2030ad";
				NoahHandler.secret_key = "KEY_78053158ca20310c";
				NoahHandler.action_id = "OFF_60353158cb641b5c";
			}
			NoahHandler.m_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.transform.gameObject);
		}
	}

	// Token: 0x06004773 RID: 18291 RVA: 0x00177DF8 File Offset: 0x00175FF8
	public void Start()
	{
		Screen.sleepTimeout = 0;
		Noah.Instance.SetDebugMode(NoahHandler.noahDebug);
	}

	// Token: 0x06004774 RID: 18292 RVA: 0x00177E10 File Offset: 0x00176010
	public void OnApplicationPause(bool flag)
	{
		if (flag)
		{
			Noah.Instance.CloseBanner();
			Noah.Instance.Suspend();
			this.m_isEndConnect = false;
		}
		else
		{
			this.m_connectFailCount = 0;
			this.m_commitFailCount = 0;
			Noah.Instance.Resume();
			if (NoahHandler.reconnect)
			{
				Noah.Instance.Connect(NoahHandler.consumer_key, NoahHandler.secret_key, NoahHandler.action_id);
			}
		}
	}

	// Token: 0x06004775 RID: 18293 RVA: 0x00177E80 File Offset: 0x00176080
	public void OnDestroy()
	{
		Noah.Instance.Suspend();
		Noah.Instance.Close();
	}

	// Token: 0x06004776 RID: 18294 RVA: 0x00177E98 File Offset: 0x00176098
	public void On15minutes()
	{
	}

	// Token: 0x06004777 RID: 18295 RVA: 0x00177E9C File Offset: 0x0017609C
	public void OnCommit(string arg)
	{
		Hashtable hashtable = NewJSON.JsonDecode(arg) as Hashtable;
		if (hashtable == null)
		{
			return;
		}
		string text = hashtable["result"].ToString();
		string str = hashtable["action_id"].ToString();
		switch (Noah.ConvertResultState(int.Parse(text)))
		{
		case Noah.ResultState.CommitOver:
			this.m_commitFailCount = 0;
			break;
		case Noah.ResultState.Success:
			this.m_commitFailCount = 0;
			break;
		case Noah.ResultState.Failure:
			if (this.m_commitFailCount < NoahHandler.FailRetryCount)
			{
				Noah.Instance.Commit(NoahHandler.action_id);
				global::Debug.Log("NoahHandler.OnConnect:Retry for failed");
			}
			this.m_commitFailCount++;
			break;
		}
		string text2 = "OnCommit\n" + Noah.ConvertResultState(int.Parse(text));
		text2 = text2 + "\n" + str;
		global::Debug.Log(text2);
		if (this.m_callbackObject != null)
		{
			this.m_callbackObject.SendMessage("OnCommitNoah", text, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06004778 RID: 18296 RVA: 0x00177FBC File Offset: 0x001761BC
	public void OnConnect(string arg)
	{
		Hashtable hashtable = NewJSON.JsonDecode(arg) as Hashtable;
		if (hashtable == null)
		{
			return;
		}
		string text = hashtable["result"].ToString();
		switch (Noah.ConvertResultState(int.Parse(text)))
		{
		case Noah.ResultState.Success:
			if (this.m_guid != null)
			{
				Noah.Instance.SetGUID(this.m_guid);
			}
			this.m_isEndConnect = true;
			NoahHandler.reconnect = true;
			this.m_connectFailCount = 0;
			break;
		case Noah.ResultState.Failure:
			if (this.m_connectFailCount < NoahHandler.FailRetryCount)
			{
				Noah.Instance.Connect(NoahHandler.consumer_key, NoahHandler.secret_key, NoahHandler.action_id);
				global::Debug.Log("NoahHandler.OnConnect:Retry for failed");
			}
			this.m_connectFailCount++;
			break;
		}
		string message = "OnConnect\n" + Noah.ConvertResultState(int.Parse(text));
		global::Debug.Log(message);
		if (this.m_callbackObject != null)
		{
			this.m_callbackObject.SendMessage("OnConnectNoah", text, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06004779 RID: 18297 RVA: 0x001780D8 File Offset: 0x001762D8
	public void OnDelete(string arg)
	{
		Hashtable hashtable = NewJSON.JsonDecode(arg) as Hashtable;
		if (hashtable == null)
		{
			return;
		}
		string s = hashtable["result"].ToString();
		switch (Noah.ConvertResultState(int.Parse(s)))
		{
		}
		string message = "OnDelete\n" + Noah.ConvertResultState(int.Parse(s));
		global::Debug.Log(message);
	}

	// Token: 0x0600477A RID: 18298 RVA: 0x00178160 File Offset: 0x00176360
	public void OnGetPoint(string arg)
	{
		Hashtable hashtable = NewJSON.JsonDecode(arg) as Hashtable;
		if (hashtable == null)
		{
			return;
		}
		string s = hashtable["result"].ToString();
		string str = hashtable["point"].ToString();
		switch (Noah.ConvertResultState(int.Parse(s)))
		{
		}
		string text = "OnGetPoint\n" + Noah.ConvertResultState(int.Parse(s));
		text = text + "\n" + str;
		global::Debug.Log(text);
	}

	// Token: 0x0600477B RID: 18299 RVA: 0x00178208 File Offset: 0x00176408
	public void OnGUID(string arg)
	{
		Hashtable hashtable = NewJSON.JsonDecode(arg) as Hashtable;
		if (hashtable == null)
		{
			return;
		}
		string text = hashtable["result"].ToString();
		global::Debug.Log("NoahHandler.OnGUID:" + text);
		switch (Noah.ConvertResultState(int.Parse(text)))
		{
		case Noah.ResultState.Success:
			this.m_isEndSetGUID = true;
			break;
		}
		string message = "OnGUID\n" + Noah.ConvertResultState(int.Parse(text));
		global::Debug.Log(message);
		if (this.m_callbackObject != null)
		{
			this.m_callbackObject.SendMessage("OnGUIDNoah", text, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600477C RID: 18300 RVA: 0x001782C8 File Offset: 0x001764C8
	public void OnPurchased(string arg)
	{
		Hashtable hashtable = NewJSON.JsonDecode(arg) as Hashtable;
		if (hashtable == null)
		{
			return;
		}
		string s = hashtable["result"].ToString();
		string s2 = hashtable["goods_count"].ToString();
		switch (Noah.ConvertResultState(int.Parse(s)))
		{
		}
		string text = "OnPurchased\n" + Noah.ConvertResultState(int.Parse(s));
		if (int.Parse(s2) > 0)
		{
			for (int i = 0; i < int.Parse(s2); i++)
			{
				text = text + "\n" + hashtable["goods_id" + i].ToString();
			}
		}
		global::Debug.Log(text);
	}

	// Token: 0x0600477D RID: 18301 RVA: 0x001783B0 File Offset: 0x001765B0
	public void OnUsedPoint(string arg)
	{
		Hashtable hashtable = NewJSON.JsonDecode(arg) as Hashtable;
		if (hashtable == null)
		{
			return;
		}
		string s = hashtable["result"].ToString();
		string str = hashtable["point"].ToString();
		switch (Noah.ConvertResultState(int.Parse(s)))
		{
		}
		string text = "OnUsedPoint\n" + Noah.ConvertResultState(int.Parse(s));
		text = text + "\n" + str;
		global::Debug.Log(text);
	}

	// Token: 0x0600477E RID: 18302 RVA: 0x00178468 File Offset: 0x00176668
	public void OnBannerView(string arg)
	{
		Hashtable hashtable = NewJSON.JsonDecode(arg) as Hashtable;
		if (hashtable == null)
		{
			return;
		}
		string s = hashtable["result"].ToString();
		switch (Noah.ConvertResultState(int.Parse(s)))
		{
		}
		string message = "OnBannerView\n" + Noah.ConvertResultState(int.Parse(s));
		global::Debug.Log(message);
	}

	// Token: 0x0600477F RID: 18303 RVA: 0x0017850C File Offset: 0x0017670C
	public void OnReview(string arg)
	{
		Hashtable hashtable = NewJSON.JsonDecode(arg) as Hashtable;
		if (hashtable == null)
		{
			return;
		}
		string s = hashtable["result"].ToString();
		switch (Noah.ConvertResultState(int.Parse(s)))
		{
		}
		string message = "OnReview\n" + Noah.ConvertResultState(int.Parse(s));
		global::Debug.Log(message);
	}

	// Token: 0x06004780 RID: 18304 RVA: 0x001785A4 File Offset: 0x001767A4
	public void OnRewardView(string arg)
	{
		Hashtable hashtable = NewJSON.JsonDecode(arg) as Hashtable;
		if (hashtable == null)
		{
			return;
		}
		string s = hashtable["result"].ToString();
		switch (Noah.ConvertResultState(int.Parse(s)))
		{
		}
		string message = "OnRewardView\n" + Noah.ConvertResultState(int.Parse(s));
		global::Debug.Log(message);
	}

	// Token: 0x06004781 RID: 18305 RVA: 0x0017862C File Offset: 0x0017682C
	public void OnOffer(string arg)
	{
		Hashtable hashtable = NewJSON.JsonDecode(arg) as Hashtable;
		if (hashtable == null)
		{
			return;
		}
		string text = hashtable["result"].ToString();
		switch (Noah.ConvertResultState(int.Parse(text)))
		{
		}
		string message = "OnOffer\n" + Noah.ConvertResultState(int.Parse(text));
		global::Debug.Log(message);
		if (this.m_callbackObject != null)
		{
			this.m_callbackObject.SendMessage("OnOfferNoah", text, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06004782 RID: 18306 RVA: 0x001786D4 File Offset: 0x001768D4
	public void OnShop(string arg)
	{
		Hashtable hashtable = NewJSON.JsonDecode(arg) as Hashtable;
		if (hashtable == null)
		{
			return;
		}
		string s = hashtable["result"].ToString();
		switch (Noah.ConvertResultState(int.Parse(s)))
		{
		}
		string message = "OnShop\n" + Noah.ConvertResultState(int.Parse(s));
		global::Debug.Log(message);
	}

	// Token: 0x06004783 RID: 18307 RVA: 0x0017875C File Offset: 0x0017695C
	public void NoahBannerWallViewControllerDidFnish(string arg)
	{
	}

	// Token: 0x04003B7D RID: 15229
	public static string app_name = string.Empty;

	// Token: 0x04003B7E RID: 15230
	public static string consumer_key = string.Empty;

	// Token: 0x04003B7F RID: 15231
	public static string secret_key = string.Empty;

	// Token: 0x04003B80 RID: 15232
	public static string action_id = string.Empty;

	// Token: 0x04003B81 RID: 15233
	public static bool noahDebug;

	// Token: 0x04003B82 RID: 15234
	public static bool reconnect;

	// Token: 0x04003B83 RID: 15235
	private GameObject m_callbackObject;

	// Token: 0x04003B84 RID: 15236
	private static NoahHandler m_instance;

	// Token: 0x04003B85 RID: 15237
	private string m_guid;

	// Token: 0x04003B86 RID: 15238
	private bool m_isEndConnect;

	// Token: 0x04003B87 RID: 15239
	private bool m_isEndSetGUID;

	// Token: 0x04003B88 RID: 15240
	private static readonly int FailRetryCount = 3;

	// Token: 0x04003B89 RID: 15241
	private int m_connectFailCount;

	// Token: 0x04003B8A RID: 15242
	private int m_commitFailCount;
}
