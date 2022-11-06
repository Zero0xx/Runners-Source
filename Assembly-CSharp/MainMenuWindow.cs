using System;
using Text;
using UnityEngine;

// Token: 0x02000467 RID: 1127
public class MainMenuWindow : MonoBehaviour
{
	// Token: 0x060021C9 RID: 8649 RVA: 0x000CB918 File Offset: 0x000C9B18
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x060021CA RID: 8650 RVA: 0x000CB924 File Offset: 0x000C9B24
	public void CreateWindow(MainMenuWindow.WindowType window_type, MainMenuWindow.ButtonClickedCallback callback = null)
	{
		if (window_type < MainMenuWindow.WindowType.NUM)
		{
			this.m_window_type = window_type;
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				buttonType = this.m_windowInfo[(int)this.m_window_type].ButtonType,
				caption = TextUtility.GetCommonText(this.m_windowInfo[(int)this.m_window_type].CaptionGroup, this.m_windowInfo[(int)this.m_window_type].CaptionCell),
				message = TextUtility.GetCommonText(this.m_windowInfo[(int)this.m_window_type].MessageGroup, this.m_windowInfo[(int)this.m_window_type].MessageCell),
				isPlayErrorSe = this.m_windowInfo[(int)this.m_window_type].ErrorSe,
				name = "MainMenuWindow"
			});
			base.enabled = true;
			this.m_buttonClickedCallback = callback;
		}
	}

	// Token: 0x060021CB RID: 8651 RVA: 0x000CB9FC File Offset: 0x000C9BFC
	public void Update()
	{
		if (GeneralWindow.IsCreated("MainMenuWindow") && GeneralWindow.IsButtonPressed)
		{
			if (MainMenuWindow.WindowType.ChallengeGoShop <= this.m_window_type && this.m_window_type < MainMenuWindow.WindowType.NUM && this.m_windowInfo[(int)this.m_window_type].ButtonType == GeneralWindow.ButtonType.ShopCancel)
			{
				bool isYesButtonPressed = GeneralWindow.IsYesButtonPressed;
			}
			if (this.m_buttonClickedCallback != null)
			{
				this.m_buttonClickedCallback(GeneralWindow.IsYesButtonPressed);
				this.m_buttonClickedCallback = null;
			}
			GeneralWindow.Close();
			base.enabled = false;
			this.m_window_type = MainMenuWindow.WindowType.UNKNOWN;
		}
	}

	// Token: 0x04001E84 RID: 7812
	private readonly MainMenuWindow.WindowInfo[] m_windowInfo = new MainMenuWindow.WindowInfo[]
	{
		new MainMenuWindow.WindowInfo("MainMenu", "no_challenge_count", "MainMenu", "no_challenge_count_info", GeneralWindow.ButtonType.ShopCancel, true),
		new MainMenuWindow.WindowInfo("MainMenu", "no_challenge_count", "MainMenu", "no_challenge_count_info", GeneralWindow.ButtonType.ShopCancel, true),
		new MainMenuWindow.WindowInfo("ItemRoulette", "gw_remain_caption", "Event", "ui_Lbl_new_event_start", GeneralWindow.ButtonType.Ok, true),
		new MainMenuWindow.WindowInfo("ItemRoulette", "gw_remain_caption", "Event", "ui_Lbl_event_out_of_time_2", GeneralWindow.ButtonType.Ok, true),
		new MainMenuWindow.WindowInfo("ItemRoulette", "gw_remain_caption", "Event", "ui_Lbl_event_last_time", GeneralWindow.ButtonType.Ok, false)
	};

	// Token: 0x04001E85 RID: 7813
	private MainMenuWindow.WindowType m_window_type = MainMenuWindow.WindowType.UNKNOWN;

	// Token: 0x04001E86 RID: 7814
	private MainMenuWindow.ButtonClickedCallback m_buttonClickedCallback;

	// Token: 0x02000468 RID: 1128
	public enum WindowType
	{
		// Token: 0x04001E88 RID: 7816
		ChallengeGoShop,
		// Token: 0x04001E89 RID: 7817
		ChallengeGoShopFromItem,
		// Token: 0x04001E8A RID: 7818
		EventStart,
		// Token: 0x04001E8B RID: 7819
		EventOutOfTime,
		// Token: 0x04001E8C RID: 7820
		EventLastPlay,
		// Token: 0x04001E8D RID: 7821
		NUM,
		// Token: 0x04001E8E RID: 7822
		UNKNOWN = -1
	}

	// Token: 0x02000469 RID: 1129
	public class WindowInfo
	{
		// Token: 0x060021CC RID: 8652 RVA: 0x000CBA90 File Offset: 0x000C9C90
		public WindowInfo()
		{
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000CBA98 File Offset: 0x000C9C98
		public WindowInfo(string captionGroup, string captionCell, string messageGroup, string messageCell, GeneralWindow.ButtonType buttonType, bool errorSe)
		{
			this.m_captionGroup = captionGroup;
			this.m_captionCell = captionCell;
			this.m_messageGroup = messageGroup;
			this.m_messageCell = messageCell;
			this.m_buttonType = buttonType;
			this.m_errorSe = errorSe;
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x000CBAD0 File Offset: 0x000C9CD0
		public string CaptionGroup
		{
			get
			{
				return this.m_captionGroup;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x000CBAD8 File Offset: 0x000C9CD8
		public string CaptionCell
		{
			get
			{
				return this.m_captionCell;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x000CBAE0 File Offset: 0x000C9CE0
		public string MessageGroup
		{
			get
			{
				return this.m_messageGroup;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x000CBAE8 File Offset: 0x000C9CE8
		public string MessageCell
		{
			get
			{
				return this.m_messageCell;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x000CBAF0 File Offset: 0x000C9CF0
		public GeneralWindow.ButtonType ButtonType
		{
			get
			{
				return this.m_buttonType;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x000CBAF8 File Offset: 0x000C9CF8
		public bool ErrorSe
		{
			get
			{
				return this.m_errorSe;
			}
		}

		// Token: 0x04001E8F RID: 7823
		private string m_captionGroup;

		// Token: 0x04001E90 RID: 7824
		private string m_captionCell;

		// Token: 0x04001E91 RID: 7825
		private string m_messageGroup;

		// Token: 0x04001E92 RID: 7826
		private string m_messageCell;

		// Token: 0x04001E93 RID: 7827
		private GeneralWindow.ButtonType m_buttonType;

		// Token: 0x04001E94 RID: 7828
		private bool m_errorSe;
	}

	// Token: 0x02000A92 RID: 2706
	// (Invoke) Token: 0x06004882 RID: 18562
	public delegate void ButtonClickedCallback(bool yesButtonClicked);
}
