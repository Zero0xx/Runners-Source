using System;
using AnimationOrTween;
using Message;
using UnityEngine;

// Token: 0x02000343 RID: 835
public class AgeVerificationWindow : MonoBehaviour
{
	// Token: 0x060018C0 RID: 6336 RVA: 0x0008EF20 File Offset: 0x0008D120
	public void Setup(string anchorPath)
	{
		if (!this.m_isLoad)
		{
			this.SetWindowData();
			this.m_isLoad = true;
		}
	}

	// Token: 0x060018C1 RID: 6337 RVA: 0x0008EF3C File Offset: 0x0008D13C
	private void SetWindowData()
	{
		this.m_prefabObject = base.gameObject;
		if (this.m_prefabObject != null)
		{
			this.m_yearButton = this.m_prefabObject.AddComponent<AgeVerificationYear>();
			if (this.m_yearButton != null)
			{
				this.m_yearButton.SetCallback(new AgeVerificationButton.ButtonClickedCallback(this.ButtonClickedCallback));
				this.m_yearButton.Setup();
			}
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_prefabObject, "month_set");
			if (gameObject != null)
			{
				this.m_monthButtons = gameObject.AddComponent<AgeVerificationButton>();
				UILabel label = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_month_x0");
				this.m_monthButtons.SetLabel(AgeVerificationButton.LabelType.TYPE_TEN, label);
				GameObject upObject = GameObjectUtil.FindChildGameObject(gameObject, "Btn_month_x0_up");
				GameObject downObject = GameObjectUtil.FindChildGameObject(gameObject, "Btn_month_x0_down");
				this.m_monthButtons.SetButton(upObject, downObject);
				this.m_monthButtons.Setup(new AgeVerificationButton.ButtonClickedCallback(this.ButtonClickedCallback));
				for (int i = 1; i <= 12; i++)
				{
					this.m_monthButtons.AddValuePreset(i);
				}
				this.m_monthButtons.SetDefaultValue(1);
			}
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_prefabObject, "day_set");
			if (gameObject2 != null)
			{
				this.m_dayButtons = gameObject2.AddComponent<AgeVerificationButton>();
				UILabel label2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject2, "Lbl_day_x0");
				this.m_dayButtons.SetLabel(AgeVerificationButton.LabelType.TYPE_TEN, label2);
				GameObject upObject2 = GameObjectUtil.FindChildGameObject(gameObject2, "Btn_day_up");
				GameObject downObject2 = GameObjectUtil.FindChildGameObject(gameObject2, "Btn_day_down");
				this.m_dayButtons.SetButton(upObject2, downObject2);
				this.m_dayButtons.Setup(new AgeVerificationButton.ButtonClickedCallback(this.ButtonClickedCallback));
				for (int j = 1; j <= 31; j++)
				{
					this.m_dayButtons.AddValuePreset(j);
				}
				this.m_dayButtons.SetDefaultValue(1);
			}
			UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_prefabObject, "Btn_ok");
			if (uibuttonMessage != null)
			{
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OkButtonClickedCallback";
			}
			UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_prefabObject, "Btn_ok");
			if (uiimageButton != null)
			{
				uiimageButton.isEnabled = false;
			}
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(this.m_prefabObject, "window");
			if (gameObject3 != null)
			{
				gameObject3.SetActive(false);
			}
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(this.m_prefabObject, "blinder");
			if (gameObject4 != null)
			{
				gameObject4.SetActive(false);
			}
		}
	}

	// Token: 0x060018C2 RID: 6338 RVA: 0x0008F1C0 File Offset: 0x0008D3C0
	public void PlayStart()
	{
		this.m_isEnd = false;
		if (this.m_prefabObject == null)
		{
			return;
		}
		this.m_prefabObject.SetActive(true);
		Animation component = this.m_prefabObject.GetComponent<Animation>();
		if (component == null)
		{
			return;
		}
		ActiveAnimation.Play(component, Direction.Forward);
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_prefabObject, "window");
		if (gameObject != null)
		{
			gameObject.SetActive(true);
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_prefabObject, "blinder");
		if (gameObject2 != null)
		{
			gameObject2.SetActive(true);
		}
		BackKeyManager.AddWindowCallBack(base.gameObject);
	}

	// Token: 0x170003BD RID: 957
	// (get) Token: 0x060018C3 RID: 6339 RVA: 0x0008F268 File Offset: 0x0008D468
	// (set) Token: 0x060018C4 RID: 6340 RVA: 0x0008F270 File Offset: 0x0008D470
	public bool IsReady
	{
		get
		{
			return this.m_isLoad;
		}
		private set
		{
		}
	}

	// Token: 0x170003BE RID: 958
	// (get) Token: 0x060018C5 RID: 6341 RVA: 0x0008F274 File Offset: 0x0008D474
	// (set) Token: 0x060018C6 RID: 6342 RVA: 0x0008F27C File Offset: 0x0008D47C
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
		private set
		{
		}
	}

	// Token: 0x060018C7 RID: 6343 RVA: 0x0008F280 File Offset: 0x0008D480
	public int GetYearValue()
	{
		if (this.m_yearButton != null)
		{
			return this.m_yearButton.CurrentValue;
		}
		return 1970;
	}

	// Token: 0x060018C8 RID: 6344 RVA: 0x0008F2B0 File Offset: 0x0008D4B0
	public int GetMonthValue()
	{
		if (this.m_monthButtons != null)
		{
			return this.m_monthButtons.CurrentValue;
		}
		return 1;
	}

	// Token: 0x060018C9 RID: 6345 RVA: 0x0008F2D0 File Offset: 0x0008D4D0
	public int GetDayValue()
	{
		if (this.m_dayButtons != null)
		{
			return this.m_dayButtons.CurrentValue;
		}
		return 1;
	}

	// Token: 0x170003BF RID: 959
	// (get) Token: 0x060018CA RID: 6346 RVA: 0x0008F2F0 File Offset: 0x0008D4F0
	public bool NoInput
	{
		get
		{
			return (this.m_yearButton != null && this.m_yearButton.NoInput) || (this.m_monthButtons != null && this.m_monthButtons.NoInput) || (this.m_dayButtons != null && this.m_dayButtons.NoInput);
		}
	}

	// Token: 0x060018CB RID: 6347 RVA: 0x0008F368 File Offset: 0x0008D568
	private void Start()
	{
	}

	// Token: 0x060018CC RID: 6348 RVA: 0x0008F36C File Offset: 0x0008D56C
	private void Update()
	{
	}

	// Token: 0x060018CD RID: 6349 RVA: 0x0008F370 File Offset: 0x0008D570
	private void ButtonClickedCallback()
	{
		UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_prefabObject, "Btn_ok");
		if (uiimageButton != null)
		{
			bool isEnabled = true;
			int yearValue = this.GetYearValue();
			int monthValue = this.GetMonthValue();
			int dayValue = this.GetDayValue();
			if (!AgeVerificationUtility.IsValidDate(yearValue, monthValue, dayValue))
			{
				isEnabled = false;
			}
			if (this.NoInput)
			{
				isEnabled = false;
			}
			uiimageButton.isEnabled = isEnabled;
		}
	}

	// Token: 0x060018CE RID: 6350 RVA: 0x0008F3D8 File Offset: 0x0008D5D8
	private void OkButtonClickedCallback()
	{
		global::Debug.Log("AgeVerificationWindow.OKButtonClickedCallback");
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			string text = this.GetYearValue().ToString();
			string text2 = this.GetMonthValue().ToString();
			string text3 = this.GetDayValue().ToString();
			string birthday = string.Concat(new string[]
			{
				text,
				"-",
				text2,
				"-",
				text3
			});
			loggedInServerInterface.RequestServerSetBirthday(birthday, base.gameObject);
		}
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x060018CF RID: 6351 RVA: 0x0008F478 File Offset: 0x0008D678
	private void ServerSetBirthday_Succeeded(MsgSetBirthday msg)
	{
		this.m_isEnd = true;
		RankingUI.CheckSnsUse();
		if (this.m_prefabObject == null)
		{
			return;
		}
		Animation component = this.m_prefabObject.GetComponent<Animation>();
		if (component == null)
		{
			return;
		}
		ActiveAnimation activeAnimation = ActiveAnimation.Play(component, Direction.Reverse);
		if (activeAnimation == null)
		{
			return;
		}
		EventDelegate.Add(activeAnimation.onFinished, new EventDelegate(delegate()
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_prefabObject, "blinder");
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
			BackKeyManager.RemoveWindowCallBack(base.gameObject);
		}), true);
	}

	// Token: 0x060018D0 RID: 6352 RVA: 0x0008F4F0 File Offset: 0x0008D6F0
	public void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null)
		{
			msg.StaySequence();
		}
	}

	// Token: 0x04001631 RID: 5681
	private GameObject m_prefabObject;

	// Token: 0x04001632 RID: 5682
	private GameObject m_sceneLoaderObj;

	// Token: 0x04001633 RID: 5683
	private AgeVerificationYear m_yearButton;

	// Token: 0x04001634 RID: 5684
	private AgeVerificationButton m_monthButtons;

	// Token: 0x04001635 RID: 5685
	private AgeVerificationButton m_dayButtons;

	// Token: 0x04001636 RID: 5686
	private bool m_isEnd;

	// Token: 0x04001637 RID: 5687
	private bool m_isLoad;
}
