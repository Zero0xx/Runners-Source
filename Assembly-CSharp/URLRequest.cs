using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020007C3 RID: 1987
public class URLRequest
{
	// Token: 0x06003452 RID: 13394 RVA: 0x0011CF90 File Offset: 0x0011B190
	public URLRequest() : this(string.Empty, null, null, null)
	{
	}

	// Token: 0x06003453 RID: 13395 RVA: 0x0011CFA0 File Offset: 0x0011B1A0
	public URLRequest(string url) : this(url, null, null, null)
	{
	}

	// Token: 0x06003454 RID: 13396 RVA: 0x0011CFAC File Offset: 0x0011B1AC
	public URLRequest(string url, Action begin, Action<WWW> success, Action<WWW, bool, bool> failure)
	{
		this.mEmulation = URLRequestManager.Instance.Emulation;
		this.mURL = url;
		this.mParamList = new List<URLRequestParam>();
		this.mDelegateRequest = begin;
		this.mDelegateSuccess = success;
		this.mDelegateFailure = failure;
		this.mCompleted = false;
		this.mNotReachability = false;
		this.mElapsedTime = 0f;
	}

	// Token: 0x17000724 RID: 1828
	// (get) Token: 0x06003455 RID: 13397 RVA: 0x0011D010 File Offset: 0x0011B210
	public bool Completed
	{
		get
		{
			return this.mCompleted;
		}
	}

	// Token: 0x17000725 RID: 1829
	// (get) Token: 0x06003456 RID: 13398 RVA: 0x0011D018 File Offset: 0x0011B218
	public float ElapsedTime
	{
		get
		{
			return this.mElapsedTime;
		}
	}

	// Token: 0x17000726 RID: 1830
	// (get) Token: 0x06003457 RID: 13399 RVA: 0x0011D020 File Offset: 0x0011B220
	public List<URLRequestParam> ParamList
	{
		get
		{
			return this.mParamList;
		}
	}

	// Token: 0x17000727 RID: 1831
	// (get) Token: 0x06003458 RID: 13400 RVA: 0x0011D028 File Offset: 0x0011B228
	public string FormString
	{
		get
		{
			return this.mFormString;
		}
	}

	// Token: 0x17000728 RID: 1832
	// (get) Token: 0x06003459 RID: 13401 RVA: 0x0011D030 File Offset: 0x0011B230
	// (set) Token: 0x0600345A RID: 13402 RVA: 0x0011D038 File Offset: 0x0011B238
	public string url
	{
		get
		{
			return this.mURL;
		}
		set
		{
			this.mURL = value;
		}
	}

	// Token: 0x17000729 RID: 1833
	// (get) Token: 0x0600345B RID: 13403 RVA: 0x0011D044 File Offset: 0x0011B244
	// (set) Token: 0x0600345C RID: 13404 RVA: 0x0011D04C File Offset: 0x0011B24C
	public Action beginRequest
	{
		get
		{
			return this.mDelegateRequest;
		}
		set
		{
			this.mDelegateRequest = value;
		}
	}

	// Token: 0x1700072A RID: 1834
	// (get) Token: 0x0600345D RID: 13405 RVA: 0x0011D058 File Offset: 0x0011B258
	// (set) Token: 0x0600345E RID: 13406 RVA: 0x0011D060 File Offset: 0x0011B260
	public Action<WWW> success
	{
		get
		{
			return this.mDelegateSuccess;
		}
		set
		{
			this.mDelegateSuccess = value;
		}
	}

	// Token: 0x1700072B RID: 1835
	// (get) Token: 0x0600345F RID: 13407 RVA: 0x0011D06C File Offset: 0x0011B26C
	// (set) Token: 0x06003460 RID: 13408 RVA: 0x0011D074 File Offset: 0x0011B274
	public Action<WWW, bool, bool> failure
	{
		get
		{
			return this.mDelegateFailure;
		}
		set
		{
			this.mDelegateFailure = value;
		}
	}

	// Token: 0x1700072C RID: 1836
	// (get) Token: 0x06003461 RID: 13409 RVA: 0x0011D080 File Offset: 0x0011B280
	// (set) Token: 0x06003462 RID: 13410 RVA: 0x0011D088 File Offset: 0x0011B288
	public float TimeOut
	{
		get
		{
			return NetUtil.ConnectTimeOut;
		}
		private set
		{
		}
	}

	// Token: 0x1700072D RID: 1837
	// (get) Token: 0x06003463 RID: 13411 RVA: 0x0011D08C File Offset: 0x0011B28C
	// (set) Token: 0x06003464 RID: 13412 RVA: 0x0011D094 File Offset: 0x0011B294
	public bool Emulation
	{
		get
		{
			return this.mEmulation;
		}
		set
		{
			this.mEmulation = value;
		}
	}

	// Token: 0x06003465 RID: 13413 RVA: 0x0011D0A0 File Offset: 0x0011B2A0
	public void AddParam(string propertyName, string value)
	{
		URLRequestParam item = new URLRequestParam(propertyName, value);
		this.mParamList.Add(item);
	}

	// Token: 0x06003466 RID: 13414 RVA: 0x0011D0C4 File Offset: 0x0011B2C4
	public void Add1stParam(string propertyName, string value)
	{
		URLRequestParam item = new URLRequestParam(propertyName, value);
		this.mParamList.Insert(0, item);
	}

	// Token: 0x06003467 RID: 13415 RVA: 0x0011D0E8 File Offset: 0x0011B2E8
	public WWWForm CreateWWWForm()
	{
		if (this.mParamList.Count == 0)
		{
			return null;
		}
		WWWForm wwwform = new WWWForm();
		int count = this.mParamList.Count;
		for (int i = 0; i < count; i++)
		{
			URLRequestParam urlrequestParam = this.mParamList[i];
			if (urlrequestParam != null)
			{
				wwwform.AddField(urlrequestParam.propertyName, urlrequestParam.value);
			}
		}
		return wwwform;
	}

	// Token: 0x06003468 RID: 13416 RVA: 0x0011D158 File Offset: 0x0011B358
	public void DidReceiveSuccess(WWW www)
	{
		if (this.mDelegateSuccess != null)
		{
			this.mDelegateSuccess(www);
		}
	}

	// Token: 0x06003469 RID: 13417 RVA: 0x0011D174 File Offset: 0x0011B374
	public void DidReceiveFailure(WWW www)
	{
		if (this.mDelegateFailure != null)
		{
			this.mDelegateFailure(www, this.IsTimeOut(), this.IsNotReachability());
		}
	}

	// Token: 0x0600346A RID: 13418 RVA: 0x0011D1A4 File Offset: 0x0011B3A4
	public void PreBegin()
	{
		if (this.mDelegateRequest != null)
		{
			this.mDelegateRequest();
		}
	}

	// Token: 0x0600346B RID: 13419 RVA: 0x0011D1BC File Offset: 0x0011B3BC
	public void Begin()
	{
		this.PreBegin();
		this.mElapsedTime = 0f;
		WWWForm wwwform = this.CreateWWWForm();
		if (wwwform == null)
		{
			this.mWWW = new WWW(this.mURL);
			global::Debug.Log("URLRequestManager.ExecuteRequest:" + URLRequest.UriDecode(this.mURL), DebugTraceManager.TraceType.SERVER);
			this.mFormString = null;
		}
		else
		{
			this.mWWW = new WWW(this.mURL, wwwform);
			this.mFormString = Encoding.ASCII.GetString(wwwform.data);
			global::Debug.Log("URLRequestManager.ExecuteRequest:" + URLRequest.UriDecode(this.mURL) + "  params:" + URLRequest.UriDecode(this.mFormString), DebugTraceManager.TraceType.SERVER);
		}
	}

	// Token: 0x0600346C RID: 13420 RVA: 0x0011D274 File Offset: 0x0011B474
	public float UpdateElapsedTime(float addElapsedTime)
	{
		this.mElapsedTime += addElapsedTime;
		return 0.1f;
	}

	// Token: 0x0600346D RID: 13421 RVA: 0x0011D28C File Offset: 0x0011B48C
	public bool IsDone()
	{
		return this.mWWW.isDone;
	}

	// Token: 0x0600346E RID: 13422 RVA: 0x0011D29C File Offset: 0x0011B49C
	public bool IsTimeOut()
	{
		return NetUtil.ConnectTimeOut <= this.mElapsedTime;
	}

	// Token: 0x0600346F RID: 13423 RVA: 0x0011D2B4 File Offset: 0x0011B4B4
	public bool IsNotReachability()
	{
		return this.mNotReachability;
	}

	// Token: 0x06003470 RID: 13424 RVA: 0x0011D2BC File Offset: 0x0011B4BC
	public void Result()
	{
		if (this.IsTimeOut())
		{
			global::Debug.Log("Request : TimeOut : " + this.mURL, DebugTraceManager.TraceType.SERVER);
			this.DidReceiveFailure(null);
			this.mWWW = null;
			return;
		}
		if (!this.mWWW.isDone)
		{
			global::Debug.Log("WWW doesn't begin yet.", DebugTraceManager.TraceType.SERVER);
		}
		bool flag = null == this.mWWW.error;
		if (flag)
		{
			this.DidReceiveSuccess(this.mWWW);
		}
		else
		{
			this.DidReceiveFailure(this.mWWW);
		}
	}

	// Token: 0x06003471 RID: 13425 RVA: 0x0011D348 File Offset: 0x0011B548
	private static string UriDecode(string stringToUnescape)
	{
		return Uri.UnescapeDataString(stringToUnescape.Replace("+", "%20"));
	}

	// Token: 0x04002C14 RID: 11284
	private bool mEmulation;

	// Token: 0x04002C15 RID: 11285
	private string mURL;

	// Token: 0x04002C16 RID: 11286
	private List<URLRequestParam> mParamList;

	// Token: 0x04002C17 RID: 11287
	private Action mDelegateRequest;

	// Token: 0x04002C18 RID: 11288
	private Action<WWW> mDelegateSuccess;

	// Token: 0x04002C19 RID: 11289
	private Action<WWW, bool, bool> mDelegateFailure;

	// Token: 0x04002C1A RID: 11290
	private bool mCompleted;

	// Token: 0x04002C1B RID: 11291
	private bool mNotReachability;

	// Token: 0x04002C1C RID: 11292
	private float mElapsedTime;

	// Token: 0x04002C1D RID: 11293
	private WWW mWWW;

	// Token: 0x04002C1E RID: 11294
	private string mFormString;
}
