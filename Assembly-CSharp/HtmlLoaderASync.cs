using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003A8 RID: 936
public class HtmlLoaderASync : HtmlLoader
{
	// Token: 0x06001B70 RID: 7024 RVA: 0x000A2404 File Offset: 0x000A0604
	protected override void OnSetup()
	{
	}

	// Token: 0x06001B71 RID: 7025 RVA: 0x000A2408 File Offset: 0x000A0608
	private void Start()
	{
		base.StartCoroutine(this.WaitLoadAsync());
	}

	// Token: 0x06001B72 RID: 7026 RVA: 0x000A2418 File Offset: 0x000A0618
	private IEnumerator WaitLoadAsync()
	{
		WWW www = base.GetWWW();
		while (!www.isDone)
		{
			yield return null;
		}
		base.IsEndLoad = true;
		yield break;
	}
}
