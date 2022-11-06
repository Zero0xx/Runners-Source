using System;
using System.Collections.Generic;
using Text;
using UnityEngine;

// Token: 0x02000416 RID: 1046
public class ui_mm_news_page : MonoBehaviour
{
	// Token: 0x06001F6D RID: 8045 RVA: 0x000BA550 File Offset: 0x000B8750
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x06001F6E RID: 8046 RVA: 0x000BA55C File Offset: 0x000B875C
	private void SetInfomation()
	{
		string commonText = TextUtility.GetCommonText("Informaion", "announcement");
		this.m_bannerInfoList.Clear();
		ServerNoticeInfo noticeInfo = ServerInterface.NoticeInfo;
		if (noticeInfo != null)
		{
			this.m_noticeItems = noticeInfo.m_noticeItems;
			if (this.m_noticeItems != null)
			{
				foreach (NetNoticeItem netNoticeItem in this.m_noticeItems)
				{
					bool flag = true;
					ui_mm_new_page_ad_banner.BannerInfo bannerInfo = new ui_mm_new_page_ad_banner.BannerInfo();
					bannerInfo.info = default(InformationWindow.Information);
					bannerInfo.info.pattern = (InformationWindow.ButtonPattern)netNoticeItem.WindowType;
					bannerInfo.info.bodyText = netNoticeItem.Message;
					bannerInfo.info.imageId = netNoticeItem.ImageId;
					bannerInfo.info.texture = null;
					bannerInfo.info.url = netNoticeItem.Adress;
					bannerInfo.item = netNoticeItem;
					if (netNoticeItem.Id == (long)NetNoticeItem.OPERATORINFO_RANKINGRESULT_ID)
					{
						bannerInfo.info.rankingType = InformationWindow.RankingType.LEAGUE;
					}
					else if (netNoticeItem.Id == (long)NetNoticeItem.OPERATORINFO_QUICKRANKINGRESULT_ID)
					{
						bannerInfo.info.rankingType = InformationWindow.RankingType.QUICK_LEAGUE;
					}
					else if (netNoticeItem.Id == (long)NetNoticeItem.OPERATORINFO_EVENTRANKINGRESULT_ID)
					{
						bannerInfo.info.rankingType = InformationWindow.RankingType.EVENT;
					}
					else
					{
						bannerInfo.info.caption = commonText;
						bannerInfo.info.rankingType = InformationWindow.RankingType.NON;
						flag = noticeInfo.IsOnTime(netNoticeItem);
					}
					if (flag)
					{
						this.m_bannerInfoList.Add(bannerInfo);
					}
				}
			}
		}
	}

	// Token: 0x06001F6F RID: 8047 RVA: 0x000BA718 File Offset: 0x000B8918
	private void UpdatePage()
	{
		this.UpdateRectItemStorage();
		if (this.m_scrollBar != null)
		{
			this.m_scrollBar.value = 0f;
		}
	}

	// Token: 0x06001F70 RID: 8048 RVA: 0x000BA744 File Offset: 0x000B8944
	private void UpdateRectItemStorage()
	{
		if (this.m_itemStorage != null)
		{
			int count = this.m_bannerInfoList.Count;
			this.m_itemStorage.maxItemCount = count;
			this.m_itemStorage.maxRows = count;
			this.m_itemStorage.Restart();
			ui_mm_new_page_ad_banner[] componentsInChildren = this.m_itemStorage.GetComponentsInChildren<ui_mm_new_page_ad_banner>(true);
			int num = componentsInChildren.Length;
			for (int i = 0; i < count; i++)
			{
				if (i < num)
				{
					componentsInChildren[i].UpdateView(this.m_bannerInfoList[i]);
				}
			}
		}
	}

	// Token: 0x06001F71 RID: 8049 RVA: 0x000BA7D0 File Offset: 0x000B89D0
	public void StartInformation()
	{
		this.SetInfomation();
		this.UpdatePage();
	}

	// Token: 0x04001C77 RID: 7287
	private const int DISPLAY_MAX_ITEM_COUNT = 10;

	// Token: 0x04001C78 RID: 7288
	[SerializeField]
	private UIRectItemStorage m_itemStorage;

	// Token: 0x04001C79 RID: 7289
	[SerializeField]
	private UIScrollBar m_scrollBar;

	// Token: 0x04001C7A RID: 7290
	private List<ui_mm_new_page_ad_banner.BannerInfo> m_bannerInfoList = new List<ui_mm_new_page_ad_banner.BannerInfo>();

	// Token: 0x04001C7B RID: 7291
	private List<NetNoticeItem> m_noticeItems;
}
