using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A2E RID: 2606
public class AtlasIntermediary : MonoBehaviour
{
	// Token: 0x17000953 RID: 2387
	// (get) Token: 0x06004519 RID: 17689 RVA: 0x001636F4 File Offset: 0x001618F4
	public static AtlasIntermediary instance
	{
		get
		{
			return AtlasIntermediary.m_instance;
		}
	}

	// Token: 0x0600451A RID: 17690 RVA: 0x001636FC File Offset: 0x001618FC
	public void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x0600451B RID: 17691 RVA: 0x00163704 File Offset: 0x00161904
	private void OnDestroy()
	{
		if (AtlasIntermediary.m_instance == this)
		{
			AtlasIntermediary.m_instance = null;
		}
	}

	// Token: 0x0600451C RID: 17692 RVA: 0x0016371C File Offset: 0x0016191C
	private void SetInstance()
	{
		if (AtlasIntermediary.m_instance == null)
		{
			AtlasIntermediary.m_instance = this;
			this.Init();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600451D RID: 17693 RVA: 0x00163758 File Offset: 0x00161958
	public void Init()
	{
		if (this.m_atlasList == null)
		{
			this.m_atlasList = new Dictionary<string, UIAtlas>();
			foreach (UIAtlas uiatlas in this.atlasList)
			{
				this.m_atlasList.Add(uiatlas.name, uiatlas);
			}
		}
	}

	// Token: 0x0600451E RID: 17694 RVA: 0x001637AC File Offset: 0x001619AC
	public UIAtlas GetAtlas(string atlasName)
	{
		UIAtlas result = null;
		if (!this.isInit)
		{
			this.Init();
		}
		if (this.m_atlasList.ContainsKey(atlasName))
		{
			result = this.m_atlasList[atlasName];
		}
		return result;
	}

	// Token: 0x0600451F RID: 17695 RVA: 0x001637EC File Offset: 0x001619EC
	public UIAtlas GetAtlasServerItemId(int serverItemId)
	{
		UIAtlas result = null;
		ServerItem serverItem = new ServerItem((ServerItem.Id)serverItemId);
		string idTypeAtlasName = ServerItem.GetIdTypeAtlasName(serverItem.idType);
		if (idTypeAtlasName != null && idTypeAtlasName != string.Empty)
		{
			result = this.GetAtlas(idTypeAtlasName);
		}
		return result;
	}

	// Token: 0x06004520 RID: 17696 RVA: 0x00163834 File Offset: 0x00161A34
	public UIAtlas GetAtlasItemIdType(ServerItem.IdType idType)
	{
		UIAtlas result = null;
		string idTypeAtlasName = ServerItem.GetIdTypeAtlasName(idType);
		if (idTypeAtlasName != null && idTypeAtlasName != string.Empty)
		{
			result = this.GetAtlas(idTypeAtlasName);
		}
		return result;
	}

	// Token: 0x06004521 RID: 17697 RVA: 0x0016386C File Offset: 0x00161A6C
	public static List<string> GetSpriteNameList(UIAtlas atlas)
	{
		List<string> list = null;
		if (atlas != null)
		{
			list = new List<string>();
			List<UISpriteData> spriteList = atlas.spriteList;
			foreach (UISpriteData uispriteData in spriteList)
			{
				list.Add(uispriteData.name);
			}
		}
		return list;
	}

	// Token: 0x06004522 RID: 17698 RVA: 0x001638F0 File Offset: 0x00161AF0
	public static UISpriteData GetSpriteData(UIAtlas atlas, string spriteName)
	{
		UISpriteData result = null;
		if (atlas != null)
		{
			List<UISpriteData> spriteList = atlas.spriteList;
			foreach (UISpriteData uispriteData in spriteList)
			{
				if (uispriteData.name == spriteName)
				{
					result = uispriteData;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06004523 RID: 17699 RVA: 0x00163978 File Offset: 0x00161B78
	public bool SetSprite(ref UISprite target, ServerItem itemData, float scale = 1f)
	{
		bool result = false;
		if (target != null)
		{
			UIAtlas atlasItemIdType = this.GetAtlasItemIdType(itemData.idType);
			if (atlasItemIdType != null)
			{
				string serverItemSpriteName = itemData.serverItemSpriteName;
				if (serverItemSpriteName != null && serverItemSpriteName != string.Empty)
				{
					List<string> spriteNameList = AtlasIntermediary.GetSpriteNameList(atlasItemIdType);
					foreach (string a in spriteNameList)
					{
						if (a == serverItemSpriteName)
						{
							UISpriteData spriteData = AtlasIntermediary.GetSpriteData(atlasItemIdType, serverItemSpriteName);
							target.atlas = atlasItemIdType;
							target.spriteName = serverItemSpriteName;
							if (scale >= 0f)
							{
								int num = spriteData.paddingLeft + spriteData.paddingRight;
								int num2 = spriteData.paddingTop + spriteData.paddingBottom;
								target.width = (int)((float)(spriteData.width + num) * scale);
								target.height = (int)((float)(spriteData.height + num2) * scale);
							}
							result = true;
							break;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x17000954 RID: 2388
	// (get) Token: 0x06004524 RID: 17700 RVA: 0x00163AA8 File Offset: 0x00161CA8
	public bool isInit
	{
		get
		{
			bool result = false;
			if (this.m_atlasList != null && this.m_atlasList.Count > 0)
			{
				result = true;
			}
			return result;
		}
	}

	// Token: 0x040039D8 RID: 14808
	[SerializeField]
	private UIAtlas[] atlasList;

	// Token: 0x040039D9 RID: 14809
	private Dictionary<string, UIAtlas> m_atlasList;

	// Token: 0x040039DA RID: 14810
	private static AtlasIntermediary m_instance;
}
