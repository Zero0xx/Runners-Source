using System;
using UnityEngine;

// Token: 0x020003AB RID: 939
public class HtmlParserFactory
{
	// Token: 0x06001B82 RID: 7042 RVA: 0x000A28B8 File Offset: 0x000A0AB8
	public static GameObject Create(string url, HtmlParser.SyncType loadSyncType, HtmlParser.SyncType parseSyncType)
	{
		GameObject gameObject = new GameObject("HtmlParser");
		HtmlParser htmlParser = gameObject.AddComponent<HtmlParser>();
		if (htmlParser != null)
		{
			htmlParser.Setup(url, loadSyncType, parseSyncType);
		}
		return gameObject;
	}

	// Token: 0x06001B83 RID: 7043 RVA: 0x000A28F0 File Offset: 0x000A0AF0
	public static string GetUrlString(string url)
	{
		GameObject gameObject = HtmlParserFactory.Create(url, HtmlParser.SyncType.TYPE_SYNC, HtmlParser.SyncType.TYPE_SYNC);
		if (gameObject == null)
		{
			return null;
		}
		HtmlParser component = gameObject.GetComponent<HtmlParser>();
		if (component == null)
		{
			return null;
		}
		return component.ParsedString;
	}
}
