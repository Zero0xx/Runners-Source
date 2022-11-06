using System;
using UnityEngine;

// Token: 0x020003A6 RID: 934
public abstract class HtmlLoader : MonoBehaviour
{
	// Token: 0x06001B65 RID: 7013 RVA: 0x000A2358 File Offset: 0x000A0558
	public HtmlLoader()
	{
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x000A2360 File Offset: 0x000A0560
	private void OnDestroy()
	{
		if (this.m_www != null)
		{
			this.m_www.Dispose();
		}
	}

	// Token: 0x06001B67 RID: 7015 RVA: 0x000A2378 File Offset: 0x000A0578
	public void Setup(string url)
	{
		this.m_www = new WWW(url);
		this.OnSetup();
	}

	// Token: 0x170003F2 RID: 1010
	// (get) Token: 0x06001B68 RID: 7016 RVA: 0x000A238C File Offset: 0x000A058C
	// (set) Token: 0x06001B69 RID: 7017 RVA: 0x000A2394 File Offset: 0x000A0594
	public bool IsEndLoad
	{
		get
		{
			return this.m_isEndLoad;
		}
		protected set
		{
			this.m_isEndLoad = value;
		}
	}

	// Token: 0x06001B6A RID: 7018 RVA: 0x000A23A0 File Offset: 0x000A05A0
	public string GetUrlContentsText()
	{
		if (this.m_www == null)
		{
			return null;
		}
		return this.m_www.text;
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x000A23BC File Offset: 0x000A05BC
	protected WWW GetWWW()
	{
		return this.m_www;
	}

	// Token: 0x06001B6C RID: 7020
	protected abstract void OnSetup();

	// Token: 0x040018F7 RID: 6391
	private WWW m_www;

	// Token: 0x040018F8 RID: 6392
	private bool m_isEndLoad;
}
