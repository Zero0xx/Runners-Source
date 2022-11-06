using System;
using System.Collections.Generic;
using Text;
using UnityEngine;

// Token: 0x02000558 RID: 1368
public class ServerInformationWindow : MonoBehaviour
{
	// Token: 0x17000586 RID: 1414
	// (get) Token: 0x06002A41 RID: 10817 RVA: 0x00106030 File Offset: 0x00104230
	// (set) Token: 0x06002A42 RID: 10818 RVA: 0x00106064 File Offset: 0x00104264
	public bool IsReady
	{
		get
		{
			return this.m_state != ServerInformationWindow.State.IDLE && this.m_state != ServerInformationWindow.State.INIT && this.m_state != ServerInformationWindow.State.SETUP;
		}
		private set
		{
		}
	}

	// Token: 0x06002A43 RID: 10819 RVA: 0x00106068 File Offset: 0x00104268
	public bool IsEnd()
	{
		return this.m_state == ServerInformationWindow.State.END;
	}

	// Token: 0x06002A44 RID: 10820 RVA: 0x00106074 File Offset: 0x00104274
	private void Start()
	{
		if (ServerInterface.NoticeInfo != null)
		{
			List<NetNoticeItem> noticeItems = ServerInterface.NoticeInfo.m_noticeItems;
			if (noticeItems != null)
			{
				this.m_infos = new List<NetNoticeItem>();
				if (this.m_infos != null)
				{
					foreach (NetNoticeItem netNoticeItem in noticeItems)
					{
						if (netNoticeItem != null && netNoticeItem.Id != (long)NetNoticeItem.OPERATORINFO_RANKINGRESULT_ID && netNoticeItem.Id != (long)NetNoticeItem.OPERATORINFO_EVENTRANKINGRESULT_ID && !netNoticeItem.IsOnlyInformationPage() && !ServerInterface.NoticeInfo.IsChecked(netNoticeItem) && ServerInterface.NoticeInfo.IsOnTime(netNoticeItem))
						{
							this.m_infos.Add(netNoticeItem);
						}
					}
					if (this.m_infos.Count > 0)
					{
						this.m_state = ServerInformationWindow.State.INIT;
					}
				}
			}
		}
	}

	// Token: 0x06002A45 RID: 10821 RVA: 0x00106178 File Offset: 0x00104378
	private void SetWindowData()
	{
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			this.m_windowObj = GameObjectUtil.FindChildGameObject(cameraUIObject, "NewsWindow");
			if (this.m_windowObj != null)
			{
				this.m_windowObj.SetActive(false);
			}
		}
	}

	// Token: 0x06002A46 RID: 10822 RVA: 0x001061C8 File Offset: 0x001043C8
	private void Update()
	{
		switch (this.m_state)
		{
		case ServerInformationWindow.State.INIT:
			this.SetWindowData();
			if (this.m_playStartCue)
			{
				this.m_playStartCue = false;
				this.CreateInformationWindow();
				this.m_state = ServerInformationWindow.State.PLAYING;
			}
			break;
		case ServerInformationWindow.State.PLAYING:
			if (this.m_window != null)
			{
				if (this.m_window.IsButtonPress(InformationWindow.ButtonType.LEFT))
				{
					this.m_state = ServerInformationWindow.State.WAIT_PLAY;
				}
				else if (this.m_window.IsButtonPress(InformationWindow.ButtonType.RIGHT))
				{
					this.m_state = ServerInformationWindow.State.WAIT_PLAY;
				}
				else if (this.m_window.IsButtonPress(InformationWindow.ButtonType.CLOSE))
				{
					this.m_state = ServerInformationWindow.State.WAIT_PLAY;
				}
			}
			break;
		case ServerInformationWindow.State.WAIT_PLAY:
			if (this.m_window != null && this.m_window.IsEnd())
			{
				this.UpdateInformaitionSaveData();
				if (this.HasNext())
				{
					this.PlayNext();
				}
				else
				{
					this.DestroyInformationWindow();
					this.m_state = ServerInformationWindow.State.END;
				}
			}
			break;
		}
	}

	// Token: 0x06002A47 RID: 10823 RVA: 0x001062F0 File Offset: 0x001044F0
	public void Clear()
	{
		this.m_current_info = 0;
	}

	// Token: 0x06002A48 RID: 10824 RVA: 0x001062FC File Offset: 0x001044FC
	public bool HasNext()
	{
		if (this.m_infos != null)
		{
			int num = this.m_current_info + 1;
			if (num < this.m_infos.Count)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002A49 RID: 10825 RVA: 0x00106334 File Offset: 0x00104534
	public void PlayStart()
	{
		if (this.m_infos != null && this.m_infos.Count > 0)
		{
			if (this.m_state != ServerInformationWindow.State.INIT)
			{
				this.CreateInformationWindow();
				this.m_state = ServerInformationWindow.State.PLAYING;
			}
			else
			{
				this.m_playStartCue = true;
			}
		}
		else
		{
			this.m_state = ServerInformationWindow.State.END;
		}
	}

	// Token: 0x06002A4A RID: 10826 RVA: 0x00106390 File Offset: 0x00104590
	public void PlayNext()
	{
		if (!this.HasNext())
		{
			return;
		}
		this.m_current_info++;
		this.CreateInformationWindow();
		this.m_state = ServerInformationWindow.State.PLAYING;
	}

	// Token: 0x06002A4B RID: 10827 RVA: 0x001063BC File Offset: 0x001045BC
	public void SetSaveFlag()
	{
		this.m_saveFlag = true;
	}

	// Token: 0x06002A4C RID: 10828 RVA: 0x001063C8 File Offset: 0x001045C8
	private void CreateInformationWindow()
	{
		this.m_window = base.gameObject.GetComponent<InformationWindow>();
		if (this.m_window == null)
		{
			this.m_window = base.gameObject.AddComponent<InformationWindow>();
		}
		if (this.m_window != null)
		{
			NetNoticeItem netNoticeItem = this.m_infos[this.m_current_info];
			InformationWindow.Information info = default(InformationWindow.Information);
			if (netNoticeItem.WindowType == 0 || netNoticeItem.WindowType == 16)
			{
				info.pattern = InformationWindow.ButtonPattern.TEXT;
				info.imageId = "-1";
			}
			else
			{
				info.pattern = InformationWindow.ButtonPattern.OK;
				info.imageId = netNoticeItem.ImageId;
			}
			info.caption = TextUtility.GetCommonText("Informaion", "announcement");
			info.bodyText = netNoticeItem.Message;
			this.m_window.Create(info, this.m_windowObj);
		}
	}

	// Token: 0x06002A4D RID: 10829 RVA: 0x001064B0 File Offset: 0x001046B0
	private void UpdateInformaitionSaveData()
	{
		ServerNoticeInfo noticeInfo = ServerInterface.NoticeInfo;
		if (noticeInfo != null)
		{
			noticeInfo.UpdateChecked(this.m_infos[this.m_current_info]);
			if (this.m_saveFlag)
			{
				noticeInfo.SaveInformation();
			}
		}
	}

	// Token: 0x06002A4E RID: 10830 RVA: 0x001064F4 File Offset: 0x001046F4
	private void DestroyInformationWindow()
	{
		if (this.m_window != null)
		{
			UnityEngine.Object.Destroy(this.m_window);
			this.m_window = null;
		}
	}

	// Token: 0x040025A5 RID: 9637
	private InformationWindow m_window;

	// Token: 0x040025A6 RID: 9638
	private GameObject m_windowObj;

	// Token: 0x040025A7 RID: 9639
	private List<NetNoticeItem> m_infos = new List<NetNoticeItem>();

	// Token: 0x040025A8 RID: 9640
	private int m_current_info;

	// Token: 0x040025A9 RID: 9641
	private bool m_playStartCue;

	// Token: 0x040025AA RID: 9642
	private bool m_saveFlag;

	// Token: 0x040025AB RID: 9643
	private ServerInformationWindow.State m_state;

	// Token: 0x02000559 RID: 1369
	private enum State
	{
		// Token: 0x040025AD RID: 9645
		IDLE,
		// Token: 0x040025AE RID: 9646
		INIT,
		// Token: 0x040025AF RID: 9647
		SETUP,
		// Token: 0x040025B0 RID: 9648
		SETUP_END,
		// Token: 0x040025B1 RID: 9649
		PLAYING,
		// Token: 0x040025B2 RID: 9650
		WAIT_PLAY,
		// Token: 0x040025B3 RID: 9651
		END
	}
}
