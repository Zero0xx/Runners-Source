using System;
using UnityEngine;

// Token: 0x020009EA RID: 2538
internal class WWWRequest
{
	// Token: 0x060042F0 RID: 17136 RVA: 0x0015BCBC File Offset: 0x00159EBC
	public WWWRequest(string url, bool checkTime = false)
	{
		this.m_www = new WWW(url);
		this.m_startTime = Time.realtimeSinceStartup;
		this.m_checkTime = checkTime;
	}

	// Token: 0x060042F2 RID: 17138 RVA: 0x0015BCFC File Offset: 0x00159EFC
	public void SetConnectTime(float connectTime)
	{
		if (connectTime > 180f)
		{
			connectTime = 180f;
		}
		this.m_connectTime = connectTime;
	}

	// Token: 0x060042F3 RID: 17139 RVA: 0x0015BD24 File Offset: 0x00159F24
	public void Remove()
	{
		try
		{
			if (this.m_www != null)
			{
			}
		}
		catch (Exception ex)
		{
			global::Debug.Log("WWWRequest.Remove():Exception->Message = " + ex.Message + ",ToString() = " + ex.ToString());
		}
		this.m_www = null;
	}

	// Token: 0x060042F4 RID: 17140 RVA: 0x0015BD8C File Offset: 0x00159F8C
	public void Cancel()
	{
		this.Remove();
	}

	// Token: 0x060042F5 RID: 17141 RVA: 0x0015BD94 File Offset: 0x00159F94
	public void Update()
	{
		if (!this.m_isTimeOut)
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float num = realtimeSinceStartup - this.m_startTime;
			if (num >= this.m_connectTime)
			{
				this.m_isTimeOut = true;
			}
		}
		if (!this.m_isEnd)
		{
			if (this.m_www != null)
			{
				if (this.m_www.isDone)
				{
					this.m_isEnd = true;
				}
			}
			else
			{
				this.m_isEnd = true;
			}
			if (this.m_checkTime)
			{
				float realtimeSinceStartup2 = Time.realtimeSinceStartup;
				global::Debug.Log("LS:Load File: " + this.m_www.url + " Time is " + (realtimeSinceStartup2 - this.m_startTime).ToString());
			}
		}
	}

	// Token: 0x060042F6 RID: 17142 RVA: 0x0015BE48 File Offset: 0x0015A048
	public bool IsEnd()
	{
		return this.m_isEnd;
	}

	// Token: 0x060042F7 RID: 17143 RVA: 0x0015BE50 File Offset: 0x0015A050
	public bool IsTimeOut()
	{
		return this.m_isTimeOut;
	}

	// Token: 0x060042F8 RID: 17144 RVA: 0x0015BE58 File Offset: 0x0015A058
	public string GetError()
	{
		if (this.m_www != null)
		{
			return this.m_www.error;
		}
		return null;
	}

	// Token: 0x060042F9 RID: 17145 RVA: 0x0015BE74 File Offset: 0x0015A074
	public byte[] GetResult()
	{
		if (this.m_www != null)
		{
			return this.m_www.bytes;
		}
		return null;
	}

	// Token: 0x060042FA RID: 17146 RVA: 0x0015BE90 File Offset: 0x0015A090
	public string GetResultString()
	{
		if (this.m_www != null)
		{
			return this.m_www.text;
		}
		return null;
	}

	// Token: 0x060042FB RID: 17147 RVA: 0x0015BEAC File Offset: 0x0015A0AC
	public int GetResultSize()
	{
		if (this.m_www != null)
		{
			return this.m_www.size;
		}
		return 0;
	}

	// Token: 0x040038C4 RID: 14532
	private WWW m_www;

	// Token: 0x040038C5 RID: 14533
	private bool m_checkTime;

	// Token: 0x040038C6 RID: 14534
	private float m_startTime;

	// Token: 0x040038C7 RID: 14535
	private bool m_isEnd;

	// Token: 0x040038C8 RID: 14536
	private bool m_isTimeOut;

	// Token: 0x040038C9 RID: 14537
	public static readonly float DefaultConnectTime = 60f;

	// Token: 0x040038CA RID: 14538
	private float m_connectTime = WWWRequest.DefaultConnectTime;
}
