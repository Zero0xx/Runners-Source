using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020001A9 RID: 425
public class AtlasListTrace : MonoBehaviour
{
	// Token: 0x06000C2F RID: 3119 RVA: 0x00046178 File Offset: 0x00044378
	private void Start()
	{
		base.StartCoroutine(this.ProcessCoroutine());
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x00046188 File Offset: 0x00044388
	private IEnumerator ProcessCoroutine()
	{
		yield return null;
		yield return null;
		yield return null;
		yield return null;
		yield return null;
		GameObject rootObject = base.gameObject;
		if (rootObject == null)
		{
			yield break;
		}
		List<AtlasListTrace.SpriteInfo> spriteInfoList = new List<AtlasListTrace.SpriteInfo>();
		this.SearchSpriteInfoList(rootObject, ref spriteInfoList);
		if (spriteInfoList.Count > 0)
		{
			StringBuilder log = new StringBuilder();
			log.AppendLine("-----" + rootObject.name + "'s AtlasList-----");
			foreach (AtlasListTrace.SpriteInfo info in spriteInfoList)
			{
				if (info != null)
				{
					UISprite sprite = info.sprite;
					if (!(sprite == null))
					{
						UIAtlas atlas = info.atlas;
						if (!(atlas == null))
						{
							log.AppendLine(string.Concat(new string[]
							{
								"[",
								atlas.name,
								"] is fount from [",
								sprite.name,
								"]"
							}));
						}
					}
				}
			}
			global::Debug.Log(log.ToString());
		}
		yield break;
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x000461A4 File Offset: 0x000443A4
	private void Update()
	{
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x000461A8 File Offset: 0x000443A8
	private void SearchSpriteInfoList(GameObject parentObject, ref List<AtlasListTrace.SpriteInfo> spriteInfoList)
	{
		if (parentObject == null)
		{
			return;
		}
		if (spriteInfoList == null)
		{
			return;
		}
		int childCount = parentObject.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = parentObject.transform.GetChild(i).gameObject;
			if (!(gameObject == null))
			{
				this.SearchSpriteInfoList(gameObject, ref spriteInfoList);
				UISprite uisprite = null;
				UIAtlas uiatlas = null;
				uisprite = gameObject.GetComponent<UISprite>();
				if (!(uisprite == null))
				{
					uiatlas = uisprite.atlas;
					if (!(uiatlas == null))
					{
						bool flag = false;
						foreach (AtlasListTrace.SpriteInfo spriteInfo in spriteInfoList)
						{
							if (spriteInfo != null)
							{
								if (spriteInfo.atlas.name == uiatlas.name)
								{
									flag = true;
								}
							}
						}
						if (!flag || this.m_showAll)
						{
							AtlasListTrace.SpriteInfo item = new AtlasListTrace.SpriteInfo(uisprite, uiatlas);
							spriteInfoList.Add(item);
						}
					}
				}
			}
		}
	}

	// Token: 0x0400099B RID: 2459
	public bool m_showAll;

	// Token: 0x020001AA RID: 426
	private class SpriteInfo
	{
		// Token: 0x06000C33 RID: 3123 RVA: 0x000462F4 File Offset: 0x000444F4
		public SpriteInfo(UISprite sprite, UIAtlas atlas)
		{
			this.m_sprite = sprite;
			this.m_atlas = atlas;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0004630C File Offset: 0x0004450C
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x00046314 File Offset: 0x00044514
		public UISprite sprite
		{
			get
			{
				return this.m_sprite;
			}
			private set
			{
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00046318 File Offset: 0x00044518
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x00046320 File Offset: 0x00044520
		public UIAtlas atlas
		{
			get
			{
				return this.m_atlas;
			}
			private set
			{
			}
		}

		// Token: 0x0400099C RID: 2460
		private UISprite m_sprite;

		// Token: 0x0400099D RID: 2461
		private UIAtlas m_atlas;
	}
}
