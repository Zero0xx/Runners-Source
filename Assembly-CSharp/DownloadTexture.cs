using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200005E RID: 94
[RequireComponent(typeof(UITexture))]
public class DownloadTexture : MonoBehaviour
{
	// Token: 0x060002CA RID: 714 RVA: 0x0000C520 File Offset: 0x0000A720
	private IEnumerator Start()
	{
		WWW www = new WWW(this.url);
		yield return www;
		this.mTex = www.texture;
		if (this.mTex != null)
		{
			UITexture ut = base.GetComponent<UITexture>();
			ut.mainTexture = this.mTex;
			ut.MakePixelPerfect();
		}
		www.Dispose();
		yield break;
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0000C53C File Offset: 0x0000A73C
	private void OnDestroy()
	{
		if (this.mTex != null)
		{
			UnityEngine.Object.Destroy(this.mTex);
		}
	}

	// Token: 0x0400017C RID: 380
	public string url = "http://www.yourwebsite.com/logo.png";

	// Token: 0x0400017D RID: 381
	private Texture2D mTex;
}
