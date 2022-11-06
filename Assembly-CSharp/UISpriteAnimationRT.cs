using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003C4 RID: 964
[ExecuteInEditMode]
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/UI/Sprite AnimationRT")]
public class UISpriteAnimationRT : MonoBehaviour
{
	// Token: 0x17000400 RID: 1024
	// (get) Token: 0x06001C0B RID: 7179 RVA: 0x000A6C9C File Offset: 0x000A4E9C
	public int frames
	{
		get
		{
			return this.mSpriteNames.Count;
		}
	}

	// Token: 0x17000401 RID: 1025
	// (get) Token: 0x06001C0C RID: 7180 RVA: 0x000A6CAC File Offset: 0x000A4EAC
	// (set) Token: 0x06001C0D RID: 7181 RVA: 0x000A6CB4 File Offset: 0x000A4EB4
	public int framesPerSecond
	{
		get
		{
			return this.m_frameRate;
		}
		set
		{
			this.m_frameRate = value;
		}
	}

	// Token: 0x17000402 RID: 1026
	// (get) Token: 0x06001C0E RID: 7182 RVA: 0x000A6CC0 File Offset: 0x000A4EC0
	// (set) Token: 0x06001C0F RID: 7183 RVA: 0x000A6CC8 File Offset: 0x000A4EC8
	public string namePrefix
	{
		get
		{
			return this.m_namePrefix;
		}
		set
		{
			if (this.m_namePrefix != value)
			{
				this.m_namePrefix = value;
				this.RebuildSpriteList();
			}
		}
	}

	// Token: 0x17000403 RID: 1027
	// (get) Token: 0x06001C10 RID: 7184 RVA: 0x000A6CE8 File Offset: 0x000A4EE8
	// (set) Token: 0x06001C11 RID: 7185 RVA: 0x000A6CF0 File Offset: 0x000A4EF0
	public bool loop
	{
		get
		{
			return this.m_loop;
		}
		set
		{
			this.m_loop = value;
		}
	}

	// Token: 0x17000404 RID: 1028
	// (get) Token: 0x06001C12 RID: 7186 RVA: 0x000A6CFC File Offset: 0x000A4EFC
	public bool isPlaying
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x06001C13 RID: 7187 RVA: 0x000A6D04 File Offset: 0x000A4F04
	private void Start()
	{
		this.RebuildSpriteList();
	}

	// Token: 0x06001C14 RID: 7188 RVA: 0x000A6D0C File Offset: 0x000A4F0C
	private void Update()
	{
		if (this.mActive && this.mSpriteNames.Count > 1 && Application.isPlaying && (float)this.m_frameRate > 0f)
		{
			this.mDelta += Time.unscaledDeltaTime;
			float num = 1f / (float)this.m_frameRate;
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

	// Token: 0x06001C15 RID: 7189 RVA: 0x000A6E08 File Offset: 0x000A5008
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
				if (string.IsNullOrEmpty(this.m_namePrefix) || uispriteData.name.StartsWith(this.m_namePrefix))
				{
					this.mSpriteNames.Add(uispriteData.name);
				}
				i++;
			}
			this.mSpriteNames.Sort();
		}
	}

	// Token: 0x06001C16 RID: 7190 RVA: 0x000A6ED8 File Offset: 0x000A50D8
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

	// Token: 0x040019C9 RID: 6601
	[SerializeField]
	private int m_frameRate = 30;

	// Token: 0x040019CA RID: 6602
	[SerializeField]
	private string m_namePrefix = string.Empty;

	// Token: 0x040019CB RID: 6603
	[SerializeField]
	private bool m_loop = true;

	// Token: 0x040019CC RID: 6604
	private UISprite mSprite;

	// Token: 0x040019CD RID: 6605
	private float mDelta;

	// Token: 0x040019CE RID: 6606
	private int mIndex;

	// Token: 0x040019CF RID: 6607
	private bool mActive = true;

	// Token: 0x040019D0 RID: 6608
	private List<string> mSpriteNames = new List<string>();
}
