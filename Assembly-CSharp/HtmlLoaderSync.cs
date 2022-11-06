using System;
using UnityEngine;

// Token: 0x020003A7 RID: 935
public class HtmlLoaderSync : HtmlLoader
{
	// Token: 0x06001B6E RID: 7022 RVA: 0x000A23CC File Offset: 0x000A05CC
	protected override void OnSetup()
	{
		WWW www;
		do
		{
			www = base.GetWWW();
		}
		while (!www.isDone);
		base.IsEndLoad = true;
	}
}
