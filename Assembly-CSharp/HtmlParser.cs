using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003A9 RID: 937
public class HtmlParser : MonoBehaviour
{
	// Token: 0x170003F3 RID: 1011
	// (get) Token: 0x06001B74 RID: 7028 RVA: 0x000A243C File Offset: 0x000A063C
	public bool IsEndParse
	{
		get
		{
			return this.m_isEndParse;
		}
	}

	// Token: 0x170003F4 RID: 1012
	// (get) Token: 0x06001B75 RID: 7029 RVA: 0x000A2444 File Offset: 0x000A0644
	public string ParsedString
	{
		get
		{
			return this.m_parsedString;
		}
	}

	// Token: 0x06001B76 RID: 7030 RVA: 0x000A244C File Offset: 0x000A064C
	public void Setup(string url, HtmlParser.SyncType loadSyncType, HtmlParser.SyncType parseSyncType)
	{
		this.m_loadSyncType = loadSyncType;
		this.m_parseSyncType = parseSyncType;
		this.m_fsm = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		if (this.m_fsm == null)
		{
			return;
		}
		TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
		description.ignoreDeltaTime = true;
		this.m_isEndParse = false;
		HtmlParser.SyncType loadSyncType2 = this.m_loadSyncType;
		if (loadSyncType2 != HtmlParser.SyncType.TYPE_SYNC)
		{
			if (loadSyncType2 == HtmlParser.SyncType.TYPE_ASYNC)
			{
				HtmlParser.SyncType parseSyncType2 = this.m_parseSyncType;
				if (parseSyncType2 != HtmlParser.SyncType.TYPE_SYNC)
				{
					if (parseSyncType2 == HtmlParser.SyncType.TYPE_ASYNC)
					{
						this.m_loader = base.gameObject.AddComponent<HtmlLoaderASync>();
						this.m_loader.Setup(url);
						description.initState = new TinyFsmState(new EventFunction(this.StateLoadHtml));
					}
				}
				else
				{
					this.m_loader = base.gameObject.AddComponent<HtmlLoaderASync>();
					this.m_loader.Setup(url);
					description.initState = new TinyFsmState(new EventFunction(this.StateLoadHtml));
				}
			}
		}
		else
		{
			HtmlParser.SyncType parseSyncType2 = this.m_parseSyncType;
			if (parseSyncType2 != HtmlParser.SyncType.TYPE_SYNC)
			{
				if (parseSyncType2 == HtmlParser.SyncType.TYPE_ASYNC)
				{
					this.m_loader = base.gameObject.AddComponent<HtmlLoaderSync>();
					this.m_loader.Setup(url);
					description.initState = new TinyFsmState(new EventFunction(this.StateParseHtml));
				}
			}
			else
			{
				this.m_loader = base.gameObject.AddComponent<HtmlLoaderSync>();
				this.m_loader.Setup(url);
				this.ParseSync(this.m_loader.GetUrlContentsText());
				description.initState = new TinyFsmState(new EventFunction(this.StateIdle));
			}
		}
		this.m_loader.Setup(url);
		this.m_fsm.SetUp(description);
	}

	// Token: 0x06001B77 RID: 7031 RVA: 0x000A2610 File Offset: 0x000A0810
	private void Start()
	{
	}

	// Token: 0x06001B78 RID: 7032 RVA: 0x000A2614 File Offset: 0x000A0814
	private void Update()
	{
	}

	// Token: 0x06001B79 RID: 7033 RVA: 0x000A2618 File Offset: 0x000A0818
	private TinyFsmState StateLoadHtml(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			if (this.m_loader != null && this.m_loader.IsEndLoad)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateParseHtml)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001B7A RID: 7034 RVA: 0x000A26A4 File Offset: 0x000A08A4
	private TinyFsmState StateParseHtml(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			HtmlParser.SyncType parseSyncType = this.m_parseSyncType;
			if (parseSyncType != HtmlParser.SyncType.TYPE_SYNC)
			{
				if (parseSyncType == HtmlParser.SyncType.TYPE_ASYNC)
				{
					base.StartCoroutine(this.ParseASync(this.m_loader.GetUrlContentsText()));
				}
			}
			else
			{
				this.ParseSync(this.m_loader.GetUrlContentsText());
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_isEndParse)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001B7B RID: 7035 RVA: 0x000A2764 File Offset: 0x000A0964
	private TinyFsmState StateIdle(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001B7C RID: 7036 RVA: 0x000A27B0 File Offset: 0x000A09B0
	private void ParseSync(string htmlString)
	{
		this.BeginParse(htmlString);
		bool flag;
		do
		{
			flag = this.Parse();
		}
		while (!flag);
		this.EndParse();
	}

	// Token: 0x06001B7D RID: 7037 RVA: 0x000A27E4 File Offset: 0x000A09E4
	private IEnumerator ParseASync(string htmlString)
	{
		this.BeginParse(htmlString);
		for (;;)
		{
			bool isEndParse = this.Parse();
			if (isEndParse)
			{
				break;
			}
			yield return null;
		}
		this.EndParse();
		yield break;
	}

	// Token: 0x06001B7E RID: 7038 RVA: 0x000A2810 File Offset: 0x000A0A10
	private void BeginParse(string htmlString)
	{
		if (htmlString == null)
		{
			return;
		}
		string text = "<body>";
		int num = htmlString.IndexOf(text);
		this.m_parsedString = htmlString.Remove(0, num + text.Length);
	}

	// Token: 0x06001B7F RID: 7039 RVA: 0x000A2848 File Offset: 0x000A0A48
	private bool Parse()
	{
		int num = this.m_parsedString.IndexOf("<");
		int num2 = this.m_parsedString.IndexOf(">");
		if (num < 0 || num2 < 0)
		{
			return true;
		}
		int count = num2 + 1 - num;
		this.m_parsedString = this.m_parsedString.Remove(num, count);
		return false;
	}

	// Token: 0x06001B80 RID: 7040 RVA: 0x000A28A4 File Offset: 0x000A0AA4
	private void EndParse()
	{
		this.m_isEndParse = true;
	}

	// Token: 0x040018F9 RID: 6393
	private HtmlLoader m_loader;

	// Token: 0x040018FA RID: 6394
	private HtmlParser.SyncType m_loadSyncType;

	// Token: 0x040018FB RID: 6395
	private HtmlParser.SyncType m_parseSyncType;

	// Token: 0x040018FC RID: 6396
	private TinyFsmBehavior m_fsm;

	// Token: 0x040018FD RID: 6397
	private bool m_isEndParse;

	// Token: 0x040018FE RID: 6398
	private string m_parsedString;

	// Token: 0x040018FF RID: 6399
	private string m_url;

	// Token: 0x020003AA RID: 938
	public enum SyncType
	{
		// Token: 0x04001901 RID: 6401
		TYPE_SYNC,
		// Token: 0x04001902 RID: 6402
		TYPE_ASYNC
	}
}
