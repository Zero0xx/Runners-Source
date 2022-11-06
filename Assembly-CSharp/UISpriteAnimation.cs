using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E4 RID: 228
[AddComponentMenu("NGUI/UI/Sprite Animation")]
[RequireComponent(typeof(UISprite))]
[ExecuteInEditMode]
public class UISpriteAnimation : MonoBehaviour
{
	// Token: 0x1700013F RID: 319
	// (get) Token: 0x060006DC RID: 1756 RVA: 0x00027A4C File Offset: 0x00025C4C
	public int frames
	{
		get
		{
			return this.mSpriteNames.Count;
		}
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x060006DD RID: 1757 RVA: 0x00027A5C File Offset: 0x00025C5C
	// (set) Token: 0x060006DE RID: 1758 RVA: 0x00027A64 File Offset: 0x00025C64
	public int framesPerSecond
	{
		get
		{
			return this.mFPS;
		}
		set
		{
			this.mFPS = value;
		}
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x060006DF RID: 1759 RVA: 0x00027A70 File Offset: 0x00025C70
	// (set) Token: 0x060006E0 RID: 1760 RVA: 0x00027A78 File Offset: 0x00025C78
	public string namePrefix
	{
		get
		{
			return this.mPrefix;
		}
		set
		{
			if (this.mPrefix != value)
			{
				this.mPrefix = value;
				this.RebuildSpriteList();
			}
		}
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00027A98 File Offset: 0x00025C98
	// (set) Token: 0x060006E2 RID: 1762 RVA: 0x00027AA0 File Offset: 0x00025CA0
	public bool loop
	{
		get
		{
			return this.mLoop;
		}
		set
		{
			this.mLoop = value;
		}
	}

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00027AAC File Offset: 0x00025CAC
	public bool isPlaying
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x00027AB4 File Offset: 0x00025CB4
	private void Start()
	{
		this.RebuildSpriteList();
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x00027ABC File Offset: 0x00025CBC
	private void Update()
	{
		if (this.mActive && this.mSpriteNames.Count > 1 && Application.isPlaying && (float)this.mFPS > 0f)
		{
			this.mDelta += Time.deltaTime;
			float num = 1f / (float)this.mFPS;
			if (num < this.mDelta)
			{
				this.mDelta = ((num <= 0f) ? 0f : (this.mDelta - num));
				if (++this.mIndex >= this.mSpriteNames.Count)
				{
					this.mIndex = 0;
					this.mActive = this.loop;
				}
				if (this.mActive)
				{
					this.mSprite.spriteName = this.mSpriteNames[this.mIndex];
					this.mSprite.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x00027BB8 File Offset: 0x00025DB8
	private void RebuildSpriteList()
	{
		if (this.mSprite == null)
		{
			this.mSprite = base.GetComponent<UISprite>();
		}
		this.mSpriteNames.Clear();
		if (this.mSprite != null && this.mSprite.atlas != null)
		{
			List<UISpriteData> spriteList = this.mSprite.atlas.spriteList;
			int i = 0;
			int count = spriteList.Count;
			while (i < count)
			{
				UISpriteData uispriteData = spriteList[i];
				if (string.IsNullOrEmpty(this.mPrefix) || uispriteData.name.StartsWith(this.mPrefix))
				{
					this.mSpriteNames.Add(uispriteData.name);
				}
				i++;
			}
			this.mSpriteNames.Sort();
		}
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x00027C88 File Offset: 0x00025E88
	public void Reset()
	{
		this.mActive = true;
		this.mIndex = 0;
		if (this.mSprite != null && this.mSpriteNames.Count > 0)
		{
			this.mSprite.spriteName = this.mSpriteNames[this.mIndex];
			this.mSprite.MakePixelPerfect();
		}
	}

	// Token: 0x0400051D RID: 1309
	[HideInInspector]
	[SerializeField]
	private int mFPS = 30;

	// Token: 0x0400051E RID: 1310
	[SerializeField]
	[HideInInspector]
	private string mPrefix = string.Empty;

	// Token: 0x0400051F RID: 1311
	[SerializeField]
	[HideInInspector]
	private bool mLoop = true;

	// Token: 0x04000520 RID: 1312
	private UISprite mSprite;

	// Token: 0x04000521 RID: 1313
	private float mDelta;

	// Token: 0x04000522 RID: 1314
	private int mIndex;

	// Token: 0x04000523 RID: 1315
	private bool mActive = true;

	// Token: 0x04000524 RID: 1316
	private List<string> mSpriteNames = new List<string>();
}
