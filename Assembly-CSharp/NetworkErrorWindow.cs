using System;
using System.Collections;
using AnimationOrTween;
using UnityEngine;

// Token: 0x0200055F RID: 1375
public class NetworkErrorWindow : MonoBehaviour
{
	// Token: 0x06002A69 RID: 10857 RVA: 0x00106DAC File Offset: 0x00104FAC
	private void Start()
	{
		NetworkErrorWindow.m_windowObject = base.gameObject;
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		Transform parent;
		if (NetworkErrorWindow.m_info.parentGameObject != null)
		{
			parent = GameObjectUtil.FindChildGameObject(NetworkErrorWindow.m_info.parentGameObject, "Anchor_5_MC").transform;
		}
		else if (NetworkErrorWindow.m_info.anchor_path != null)
		{
			parent = gameObject.transform.Find(NetworkErrorWindow.m_info.anchor_path);
		}
		else
		{
			parent = gameObject.transform.Find("Camera/Anchor_5_MC");
		}
		Vector3 localPosition = base.transform.localPosition;
		Vector3 localScale = base.transform.localScale;
		base.transform.parent = parent;
		base.transform.localPosition = localPosition;
		base.transform.localScale = localScale;
		NetworkErrorWindow.SetDisableButton(NetworkErrorWindow.m_disableButton);
		GameObject gameObject2 = NetworkErrorWindow.m_windowObject.transform.Find("window/Lbl_caption").gameObject;
		NetworkErrorWindow.m_captionLabel = gameObject2.GetComponent<UILabel>();
		NetworkErrorWindow.m_captionLabel.text = NetworkErrorWindow.m_info.caption;
		GameObject gameObject3 = NetworkErrorWindow.m_windowObject.transform.Find("window/pattern_btn_use/textView/ScrollView/Lbl_body").gameObject;
		string str = "window/pattern_btn_use/textView/";
		Transform transform = NetworkErrorWindow.m_windowObject.transform.Find("window/pattern_btn_use");
		Transform transform2 = NetworkErrorWindow.m_windowObject.transform.Find("window/pattern_btn_use/textView");
		Transform transform3 = NetworkErrorWindow.m_windowObject.transform.Find("window/pattern_btn_use/pattern_0");
		Transform transform4 = NetworkErrorWindow.m_windowObject.transform.Find("window/pattern_btn_use/pattern_1");
		Transform transform5 = NetworkErrorWindow.m_windowObject.transform.Find("window/pattern_btn_use/pattern_2");
		Transform transform6 = NetworkErrorWindow.m_windowObject.transform.Find("window/pattern_btn_use/pattern_3");
		transform.gameObject.SetActive(true);
		transform2.gameObject.SetActive(true);
		switch (NetworkErrorWindow.m_info.buttonType)
		{
		case NetworkErrorWindow.ButtonType.Ok:
			transform3.gameObject.SetActive(true);
			transform4.gameObject.SetActive(false);
			transform5.gameObject.SetActive(false);
			transform6.gameObject.SetActive(false);
			break;
		case NetworkErrorWindow.ButtonType.YesNo:
			transform3.gameObject.SetActive(false);
			transform4.gameObject.SetActive(true);
			transform5.gameObject.SetActive(false);
			transform6.gameObject.SetActive(false);
			break;
		case NetworkErrorWindow.ButtonType.HomePage:
			transform3.gameObject.SetActive(false);
			transform4.gameObject.SetActive(false);
			transform5.gameObject.SetActive(true);
			transform6.gameObject.SetActive(false);
			break;
		case NetworkErrorWindow.ButtonType.Repayment:
			transform3.gameObject.SetActive(false);
			transform4.gameObject.SetActive(false);
			transform5.gameObject.SetActive(false);
			transform6.gameObject.SetActive(true);
			break;
		case NetworkErrorWindow.ButtonType.TextOnly:
			transform3.gameObject.SetActive(false);
			transform4.gameObject.SetActive(false);
			transform5.gameObject.SetActive(false);
			transform6.gameObject.SetActive(false);
			break;
		}
		NetworkErrorWindow.m_messages = null;
		NetworkErrorWindow.m_messageCount = 0;
		if (NetworkErrorWindow.m_info.message != null)
		{
			NetworkErrorWindow.m_messages = NetworkErrorWindow.m_info.message.Split(new char[]
			{
				'\f'
			});
			UILabel component = gameObject3.GetComponent<UILabel>();
			component.text = ((NetworkErrorWindow.m_messages.Length < 1) ? null : NetworkErrorWindow.m_messages[NetworkErrorWindow.m_messageCount++]);
			GameObject gameObject4 = NetworkErrorWindow.m_windowObject.transform.Find(str + "ScrollView").gameObject;
			UIPanel component2 = gameObject4.GetComponent<UIPanel>();
			float w = component2.clipRange.w;
			float num = component.printedSize.y * component.transform.localScale.y;
			if (num <= w)
			{
				BoxCollider component3 = NetworkErrorWindow.m_windowObject.transform.Find(str + "ScrollOutline").GetComponent<BoxCollider>();
				component3.enabled = false;
			}
		}
		if (NetworkErrorWindow.m_info.isPlayErrorSe)
		{
			SoundManager.SePlay("sys_error", "SE");
		}
	}

	// Token: 0x06002A6A RID: 10858 RVA: 0x001071F8 File Offset: 0x001053F8
	public static GameObject Create(NetworkErrorWindow.CInfo info)
	{
		NetworkErrorWindow.SetUIEffect(false);
		NetworkErrorWindow.m_info = info;
		NetworkErrorWindow.m_disableButton = info.disableButton;
		NetworkErrorWindow.m_pressed.m_isButtonPressed = false;
		NetworkErrorWindow.m_pressed.m_isOkButtonPressed = false;
		NetworkErrorWindow.m_pressed.m_isYesButtonPressed = false;
		NetworkErrorWindow.m_pressed.m_isNoButtonPressed = false;
		NetworkErrorWindow.m_resultPressed = NetworkErrorWindow.m_pressed;
		NetworkErrorWindow.m_created = true;
		if (NetworkErrorWindow.m_windowPrefab == null)
		{
			NetworkErrorWindow.m_windowPrefab = (Resources.Load("Prefabs/UI/NetworkErrorWindow") as GameObject);
			GameObject gameObject = GameObjectUtil.FindChildGameObject(NetworkErrorWindow.m_windowPrefab, "pattern_0");
			if (gameObject != null)
			{
				UIPlayAnimation uiplayAnimation = GameObjectUtil.FindChildGameObjectComponent<UIPlayAnimation>(gameObject, "Btn_ok");
				if (uiplayAnimation != null)
				{
					uiplayAnimation.enabled = false;
				}
			}
		}
		if (NetworkErrorWindow.m_windowObject != null)
		{
			return null;
		}
		NetworkErrorWindow.m_windowObject = (UnityEngine.Object.Instantiate(NetworkErrorWindow.m_windowPrefab, Vector3.zero, Quaternion.identity) as GameObject);
		NetworkErrorWindow.m_windowObject.SetActive(true);
		SoundManager.SePlay("sys_window_open", "SE");
		Animation component = NetworkErrorWindow.m_windowObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation.Play(component, Direction.Forward);
		}
		return NetworkErrorWindow.m_windowObject;
	}

	// Token: 0x06002A6B RID: 10859 RVA: 0x00107328 File Offset: 0x00105528
	public static bool Close()
	{
		NetworkErrorWindow.m_info = default(NetworkErrorWindow.CInfo);
		NetworkErrorWindow.m_pressed = default(NetworkErrorWindow.Pressed);
		NetworkErrorWindow.m_resultPressed = default(NetworkErrorWindow.Pressed);
		NetworkErrorWindow.m_created = false;
		if (NetworkErrorWindow.m_windowObject != null)
		{
			SoundManager.SePlay("sys_window_close", "SE");
			NetworkErrorWindow.DestroyWindow();
			return true;
		}
		return false;
	}

	// Token: 0x06002A6C RID: 10860 RVA: 0x00107390 File Offset: 0x00105590
	public static void ResetButton()
	{
		NetworkErrorWindow.m_pressed = default(NetworkErrorWindow.Pressed);
		NetworkErrorWindow.m_resultPressed = default(NetworkErrorWindow.Pressed);
	}

	// Token: 0x1700058C RID: 1420
	// (get) Token: 0x06002A6D RID: 10861 RVA: 0x001073BC File Offset: 0x001055BC
	public static NetworkErrorWindow.CInfo Info
	{
		get
		{
			return NetworkErrorWindow.m_info;
		}
	}

	// Token: 0x1700058D RID: 1421
	// (get) Token: 0x06002A6E RID: 10862 RVA: 0x001073C4 File Offset: 0x001055C4
	public static bool Created
	{
		get
		{
			return NetworkErrorWindow.m_created;
		}
	}

	// Token: 0x06002A6F RID: 10863 RVA: 0x001073CC File Offset: 0x001055CC
	public static bool IsCreated(string name)
	{
		return NetworkErrorWindow.Info.name == name;
	}

	// Token: 0x1700058E RID: 1422
	// (get) Token: 0x06002A70 RID: 10864 RVA: 0x001073EC File Offset: 0x001055EC
	public static bool IsButtonPressed
	{
		get
		{
			return NetworkErrorWindow.m_resultPressed.m_isButtonPressed;
		}
	}

	// Token: 0x1700058F RID: 1423
	// (get) Token: 0x06002A71 RID: 10865 RVA: 0x001073F8 File Offset: 0x001055F8
	public static bool IsOkButtonPressed
	{
		get
		{
			return NetworkErrorWindow.m_resultPressed.m_isOkButtonPressed;
		}
	}

	// Token: 0x17000590 RID: 1424
	// (get) Token: 0x06002A72 RID: 10866 RVA: 0x00107404 File Offset: 0x00105604
	public static bool IsYesButtonPressed
	{
		get
		{
			return NetworkErrorWindow.m_resultPressed.m_isYesButtonPressed;
		}
	}

	// Token: 0x17000591 RID: 1425
	// (get) Token: 0x06002A73 RID: 10867 RVA: 0x00107410 File Offset: 0x00105610
	public static bool IsNoButtonPressed
	{
		get
		{
			return NetworkErrorWindow.m_resultPressed.m_isNoButtonPressed;
		}
	}

	// Token: 0x06002A74 RID: 10868 RVA: 0x0010741C File Offset: 0x0010561C
	private void OnClickOkButton()
	{
		if (!NetworkErrorWindow.m_pressed.m_isButtonPressed)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			NetworkErrorWindow.m_pressed.m_isOkButtonPressed = true;
			NetworkErrorWindow.m_pressed.m_isButtonPressed = true;
			Animation component = NetworkErrorWindow.m_windowObject.GetComponent<Animation>();
			if (component != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(component, Direction.Reverse);
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedCloseAnim), true);
			}
		}
	}

	// Token: 0x06002A75 RID: 10869 RVA: 0x00107498 File Offset: 0x00105698
	private void OnClickRepaymetButton()
	{
		if (!NetworkErrorWindow.m_pressed.m_isButtonPressed)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			NetworkErrorWindow.m_pressed.m_isButtonPressed = true;
			NetworkErrorWindow.m_resultPressed.m_isButtonPressed = true;
		}
	}

	// Token: 0x06002A76 RID: 10870 RVA: 0x001074D0 File Offset: 0x001056D0
	private void OnClickYesButton()
	{
		if (!NetworkErrorWindow.m_pressed.m_isButtonPressed)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			NetworkErrorWindow.m_pressed.m_isYesButtonPressed = true;
			NetworkErrorWindow.m_pressed.m_isButtonPressed = true;
		}
	}

	// Token: 0x06002A77 RID: 10871 RVA: 0x00107508 File Offset: 0x00105708
	private void OnClickNoButton()
	{
		if (!NetworkErrorWindow.m_pressed.m_isButtonPressed)
		{
			SoundManager.SePlay("sys_window_close", "SE");
			NetworkErrorWindow.m_pressed.m_isNoButtonPressed = true;
			NetworkErrorWindow.m_pressed.m_isButtonPressed = true;
		}
	}

	// Token: 0x06002A78 RID: 10872 RVA: 0x00107540 File Offset: 0x00105740
	public void OnFinishedCloseAnim()
	{
		base.StartCoroutine(this.OnFinishedCloseAnimCoroutine());
	}

	// Token: 0x06002A79 RID: 10873 RVA: 0x00107550 File Offset: 0x00105750
	private IEnumerator OnFinishedCloseAnimCoroutine()
	{
		float waitStartTime = Time.unscaledTime;
		for (;;)
		{
			float currentTime = Time.unscaledTime;
			float elapseTime = currentTime - waitStartTime;
			if (elapseTime >= 0.5f)
			{
				break;
			}
			yield return null;
		}
		NetworkErrorWindow.m_resultPressed = NetworkErrorWindow.m_pressed;
		if (NetworkErrorWindow.m_info.finishedCloseDelegate != null)
		{
			NetworkErrorWindow.m_info.finishedCloseDelegate();
		}
		NetworkErrorWindow.DestroyWindow();
		yield break;
	}

	// Token: 0x06002A7A RID: 10874 RVA: 0x00107564 File Offset: 0x00105764
	private static void DestroyWindow()
	{
		if (NetworkErrorWindow.m_windowObject != null)
		{
			UnityEngine.Object.Destroy(NetworkErrorWindow.m_windowObject);
			NetworkErrorWindow.m_windowObject = null;
		}
		NetworkErrorWindow.SetUIEffect(true);
	}

	// Token: 0x06002A7B RID: 10875 RVA: 0x00107598 File Offset: 0x00105798
	public static void SetDisableButton(bool disableButton)
	{
		NetworkErrorWindow.m_disableButton = disableButton;
		if (NetworkErrorWindow.m_windowObject != null)
		{
			foreach (UIButton uibutton in NetworkErrorWindow.m_windowObject.GetComponentsInChildren<UIButton>(true))
			{
				uibutton.isEnabled = !NetworkErrorWindow.m_disableButton;
			}
			foreach (UIImageButton uiimageButton in NetworkErrorWindow.m_windowObject.GetComponentsInChildren<UIImageButton>(true))
			{
				uiimageButton.isEnabled = !NetworkErrorWindow.m_disableButton;
			}
		}
	}

	// Token: 0x06002A7C RID: 10876 RVA: 0x00107628 File Offset: 0x00105828
	private static void SetUIEffect(bool flag)
	{
		if (UIEffectManager.Instance != null)
		{
			UIEffectManager.Instance.SetActiveEffect(HudMenuUtility.EffectPriority.Menu, flag);
		}
	}

	// Token: 0x06002A7D RID: 10877 RVA: 0x00107648 File Offset: 0x00105848
	private static void SendButtonMessage(string patternName, string btnName)
	{
		if (NetworkErrorWindow.m_windowObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(NetworkErrorWindow.m_windowObject, patternName);
			if (gameObject != null)
			{
				UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(gameObject, btnName);
				if (uibuttonMessage != null)
				{
					uibuttonMessage.SendMessage("OnClick");
				}
			}
		}
	}

	// Token: 0x06002A7E RID: 10878 RVA: 0x0010769C File Offset: 0x0010589C
	public static void OnClickPlatformBackButton()
	{
		if (NetworkErrorWindow.m_created)
		{
			switch (NetworkErrorWindow.m_info.buttonType)
			{
			case NetworkErrorWindow.ButtonType.Ok:
				NetworkErrorWindow.SendButtonMessage("pattern_0", "Btn_ok");
				break;
			case NetworkErrorWindow.ButtonType.YesNo:
				NetworkErrorWindow.SendButtonMessage("pattern_1", "Btn_no");
				break;
			case NetworkErrorWindow.ButtonType.HomePage:
				NetworkErrorWindow.SendButtonMessage("pattern_2", "Btn_no");
				break;
			}
		}
	}

	// Token: 0x040025D0 RID: 9680
	private const char FORM_FEED_CODE = '\f';

	// Token: 0x040025D1 RID: 9681
	private static GameObject m_windowPrefab;

	// Token: 0x040025D2 RID: 9682
	private static GameObject m_windowObject;

	// Token: 0x040025D3 RID: 9683
	private static NetworkErrorWindow.CInfo m_info;

	// Token: 0x040025D4 RID: 9684
	private static bool m_disableButton;

	// Token: 0x040025D5 RID: 9685
	private static bool m_created;

	// Token: 0x040025D6 RID: 9686
	private static UILabel m_captionLabel;

	// Token: 0x040025D7 RID: 9687
	private static NetworkErrorWindow.Pressed m_pressed;

	// Token: 0x040025D8 RID: 9688
	private static NetworkErrorWindow.Pressed m_resultPressed;

	// Token: 0x040025D9 RID: 9689
	private static string[] m_messages;

	// Token: 0x040025DA RID: 9690
	private static int m_messageCount;

	// Token: 0x02000560 RID: 1376
	public enum ButtonType
	{
		// Token: 0x040025DC RID: 9692
		Ok,
		// Token: 0x040025DD RID: 9693
		YesNo,
		// Token: 0x040025DE RID: 9694
		HomePage,
		// Token: 0x040025DF RID: 9695
		Repayment,
		// Token: 0x040025E0 RID: 9696
		TextOnly
	}

	// Token: 0x02000561 RID: 1377
	public struct CInfo
	{
		// Token: 0x040025E1 RID: 9697
		public string name;

		// Token: 0x040025E2 RID: 9698
		public NetworkErrorWindow.ButtonType buttonType;

		// Token: 0x040025E3 RID: 9699
		public string anchor_path;

		// Token: 0x040025E4 RID: 9700
		public GameObject parentGameObject;

		// Token: 0x040025E5 RID: 9701
		public string caption;

		// Token: 0x040025E6 RID: 9702
		public string message;

		// Token: 0x040025E7 RID: 9703
		public NetworkErrorWindow.CInfo.FinishedCloseDelegate finishedCloseDelegate;

		// Token: 0x040025E8 RID: 9704
		public bool disableButton;

		// Token: 0x040025E9 RID: 9705
		public bool isPlayErrorSe;

		// Token: 0x02000AA1 RID: 2721
		// (Invoke) Token: 0x060048BE RID: 18622
		public delegate void FinishedCloseDelegate();
	}

	// Token: 0x02000562 RID: 1378
	private struct Pressed
	{
		// Token: 0x040025EA RID: 9706
		public bool m_isButtonPressed;

		// Token: 0x040025EB RID: 9707
		public bool m_isOkButtonPressed;

		// Token: 0x040025EC RID: 9708
		public bool m_isYesButtonPressed;

		// Token: 0x040025ED RID: 9709
		public bool m_isNoButtonPressed;
	}
}
