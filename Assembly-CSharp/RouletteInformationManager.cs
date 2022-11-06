using System;
using System.Collections.Generic;
using Text;
using UnityEngine;

// Token: 0x02000514 RID: 1300
public class RouletteInformationManager : MonoBehaviour
{
	// Token: 0x1700052E RID: 1326
	// (get) Token: 0x0600275B RID: 10075 RVA: 0x000F3B4C File Offset: 0x000F1D4C
	public bool IsSetuped
	{
		get
		{
			return this.m_isSetuped;
		}
	}

	// Token: 0x1700052F RID: 1327
	// (get) Token: 0x0600275C RID: 10076 RVA: 0x000F3B54 File Offset: 0x000F1D54
	public static RouletteInformationManager Instance
	{
		get
		{
			return RouletteInformationManager.m_instance;
		}
	}

	// Token: 0x0600275D RID: 10077 RVA: 0x000F3B5C File Offset: 0x000F1D5C
	public bool GetCurrentInfoParam(out Dictionary<RouletteCategory, InformationWindow.Information> infoParam)
	{
		if (this.m_isSetuped)
		{
			infoParam = new Dictionary<RouletteCategory, InformationWindow.Information>(this.m_rouletteInfo);
			return true;
		}
		infoParam = null;
		return false;
	}

	// Token: 0x0600275E RID: 10078 RVA: 0x000F3B7C File Offset: 0x000F1D7C
	public void SetUp()
	{
		this.m_rouletteInfo.Clear();
		ServerNoticeInfo noticeInfo = ServerInterface.NoticeInfo;
		if (noticeInfo != null)
		{
			List<NetNoticeItem> rouletteItems = noticeInfo.m_rouletteItems;
			if (rouletteItems != null)
			{
				string commonText = TextUtility.GetCommonText("Informaion", "announcement");
				using (List<NetNoticeItem>.Enumerator enumerator = rouletteItems.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						NetNoticeItem netNoticeItem = enumerator.Current;
						InformationWindow.Information value = default(InformationWindow.Information);
						value.pattern = (InformationWindow.ButtonPattern)netNoticeItem.WindowType;
						value.bodyText = netNoticeItem.Message;
						value.imageId = netNoticeItem.ImageId;
						value.texture = null;
						value.caption = commonText;
						value.url = netNoticeItem.Adress;
						this.m_rouletteInfo.Add(RouletteCategory.PREMIUM, value);
					}
				}
				EventManager.EventType typeInTime = EventManager.Instance.TypeInTime;
				if (typeInTime == EventManager.EventType.RAID_BOSS)
				{
					InformationWindow.Information value2 = default(InformationWindow.Information);
					this.m_rouletteInfo.Add(RouletteCategory.RAID, value2);
				}
				this.m_isSetuped = true;
			}
		}
	}

	// Token: 0x0600275F RID: 10079 RVA: 0x000F3CA0 File Offset: 0x000F1EA0
	public void LoadInfoBaner(RouletteInformationManager.InfoBannerRequest bannerRequest, RouletteCategory category = RouletteCategory.PREMIUM)
	{
		if (this.m_isSetuped && this.m_rouletteInfo != null && this.m_rouletteInfo.ContainsKey(category))
		{
			InformationWindow.Information information = this.m_rouletteInfo[category];
			InformationImageManager instance = InformationImageManager.Instance;
			if (instance != null)
			{
				bool bannerFlag = true;
				instance.Load(information.imageId, bannerFlag, delegate(Texture2D tex)
				{
					bannerRequest.LoadDone(tex);
				});
			}
		}
	}

	// Token: 0x06002760 RID: 10080 RVA: 0x000F3D20 File Offset: 0x000F1F20
	private void Awake()
	{
		if (RouletteInformationManager.m_instance == null)
		{
			RouletteInformationManager.m_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040023A3 RID: 9123
	private bool m_isSetuped;

	// Token: 0x040023A4 RID: 9124
	private Dictionary<RouletteCategory, InformationWindow.Information> m_rouletteInfo = new Dictionary<RouletteCategory, InformationWindow.Information>();

	// Token: 0x040023A5 RID: 9125
	private static RouletteInformationManager m_instance;

	// Token: 0x02000515 RID: 1301
	public class InfoBannerRequest
	{
		// Token: 0x06002761 RID: 10081 RVA: 0x000F3D54 File Offset: 0x000F1F54
		public InfoBannerRequest(UITexture texture)
		{
			this.m_texture = texture;
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000F3D64 File Offset: 0x000F1F64
		public void LoadDone(Texture2D loadedTex)
		{
			if (loadedTex == null)
			{
				return;
			}
			if (this.m_texture == null)
			{
				return;
			}
			this.m_texture.mainTexture = loadedTex;
		}

		// Token: 0x040023A6 RID: 9126
		private UITexture m_texture;
	}
}
