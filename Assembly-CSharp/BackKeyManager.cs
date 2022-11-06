using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000101 RID: 257
public class BackKeyManager : MonoBehaviour
{
	// Token: 0x1700015B RID: 347
	// (get) Token: 0x0600079C RID: 1948 RVA: 0x0002C06C File Offset: 0x0002A26C
	public static BackKeyManager Instance
	{
		get
		{
			return BackKeyManager.instance;
		}
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x0002C074 File Offset: 0x0002A274
	public static void StartScene()
	{
		if (BackKeyManager.instance != null)
		{
			BackKeyManager.instance.m_invalidFlag = false;
			BackKeyManager.instance.m_tutorialFlag = false;
			BackKeyManager.instance.m_transSceneFlag = false;
			BackKeyManager.instance.m_sequenceTransitionFlag = false;
		}
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x0002C0C0 File Offset: 0x0002A2C0
	public static void EndScene()
	{
		if (BackKeyManager.instance != null)
		{
			BackKeyManager.instance.m_windowCallBackList.Clear();
			BackKeyManager.instance.m_eventCallBackList.Clear();
			BackKeyManager.instance.m_tutorialEventCallBackList.Clear();
			BackKeyManager.instance.m_mileageCallBack = null;
			BackKeyManager.instance.m_mainMenuUICallBack = null;
			BackKeyManager.instance.m_transSceneFlag = true;
		}
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x0002C12C File Offset: 0x0002A32C
	public static void AddWindowCallBack(GameObject obj)
	{
		if (BackKeyManager.instance != null && !BackKeyManager.instance.m_windowCallBackList.Contains(obj))
		{
			BackKeyManager.instance.m_windowCallBackList.Add(obj);
		}
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x0002C164 File Offset: 0x0002A364
	public static void RemoveWindowCallBack(GameObject obj)
	{
		if (BackKeyManager.instance != null && BackKeyManager.instance.m_windowCallBackList.Contains(obj))
		{
			BackKeyManager.instance.m_windowCallBackList.Remove(obj);
		}
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x0002C1A8 File Offset: 0x0002A3A8
	public static void AddEventCallBack(GameObject obj)
	{
		if (BackKeyManager.instance != null && !BackKeyManager.instance.m_eventCallBackList.Contains(obj))
		{
			BackKeyManager.instance.m_eventCallBackList.Add(obj);
		}
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x0002C1E0 File Offset: 0x0002A3E0
	public static void AddMainMenuUI(GameObject obj)
	{
		if (BackKeyManager.instance != null && BackKeyManager.instance.m_mainMenuUICallBack == null)
		{
			BackKeyManager.instance.m_mainMenuUICallBack = obj;
		}
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0002C220 File Offset: 0x0002A420
	public static void AddMileageCallBack(GameObject obj)
	{
		if (BackKeyManager.instance != null && BackKeyManager.instance.m_mileageCallBack == null)
		{
			BackKeyManager.instance.m_mileageCallBack = obj;
		}
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0002C260 File Offset: 0x0002A460
	public static void AddTutorialEventCallBack(GameObject obj)
	{
		if (BackKeyManager.instance != null && !BackKeyManager.instance.m_tutorialEventCallBackList.Contains(obj))
		{
			BackKeyManager.instance.m_tutorialEventCallBackList.Add(obj);
		}
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0002C298 File Offset: 0x0002A498
	// (set) Token: 0x060007A6 RID: 1958 RVA: 0x0002C2B8 File Offset: 0x0002A4B8
	public static bool InvalidFlag
	{
		get
		{
			return BackKeyManager.instance != null && BackKeyManager.instance.m_invalidFlag;
		}
		set
		{
			if (BackKeyManager.instance != null)
			{
				BackKeyManager.instance.m_invalidFlag = value;
			}
		}
	}

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0002C2D8 File Offset: 0x0002A4D8
	// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0002C2F8 File Offset: 0x0002A4F8
	public static bool TutorialFlag
	{
		get
		{
			return BackKeyManager.instance != null && BackKeyManager.instance.m_tutorialFlag;
		}
		set
		{
			if (BackKeyManager.instance != null)
			{
				BackKeyManager.instance.m_tutorialFlag = value;
			}
		}
	}

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0002C318 File Offset: 0x0002A518
	// (set) Token: 0x060007AA RID: 1962 RVA: 0x0002C338 File Offset: 0x0002A538
	public static bool MenuSequenceTransitionFlag
	{
		get
		{
			return BackKeyManager.instance != null && BackKeyManager.instance.m_sequenceTransitionFlag;
		}
		set
		{
			if (BackKeyManager.instance != null)
			{
				BackKeyManager.instance.m_sequenceTransitionFlag = value;
			}
		}
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0002C358 File Offset: 0x0002A558
	protected void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x0002C360 File Offset: 0x0002A560
	private void Start()
	{
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x0002C364 File Offset: 0x0002A564
	private void LateUpdate()
	{
		if (this.m_timer > 0f)
		{
			this.m_timer -= RealTime.deltaTime;
		}
		bool flag = UICamera.touchCount > 0;
		if (Input.GetKey(KeyCode.Escape) && this.m_timer <= 0f && !flag && !this.m_touchedPrevFrame)
		{
			this.m_timer = 0.6f;
			if (NetworkErrorWindow.Created)
			{
				NetworkErrorWindow.OnClickPlatformBackButton();
				return;
			}
			if (this.m_invalidFlag || this.m_transSceneFlag)
			{
				return;
			}
			if (this.CheckConnectNetwork())
			{
				return;
			}
			if (this.m_tutorialFlag)
			{
				WindowBase.BackButtonMessage backButtonMessage = new WindowBase.BackButtonMessage();
				if (this.m_sequenceTransitionFlag)
				{
					backButtonMessage.StaySequence();
				}
				this.SentWindowMessege(ref backButtonMessage);
				this.SentMileageMessege(ref backButtonMessage);
				if (!backButtonMessage.IsFlag(WindowBase.BackButtonMessage.Flags.STAY_SEQUENCE))
				{
					foreach (GameObject gameObject in this.m_tutorialEventCallBackList)
					{
						if (gameObject != null)
						{
							gameObject.SendMessage("OnClickPlatformBackButtonTutorialEvent", null, SendMessageOptions.DontRequireReceiver);
						}
					}
				}
				return;
			}
			WindowBase.BackButtonMessage backButtonMessage2 = new WindowBase.BackButtonMessage();
			if (this.m_sequenceTransitionFlag)
			{
				backButtonMessage2.StaySequence();
			}
			this.SentWindowMessege(ref backButtonMessage2);
			this.SendMainMenuUI(ref backButtonMessage2);
			this.SentMileageMessege(ref backButtonMessage2);
			if (!backButtonMessage2.IsFlag(WindowBase.BackButtonMessage.Flags.STAY_SEQUENCE))
			{
				foreach (GameObject gameObject2 in this.m_eventCallBackList)
				{
					if (gameObject2 != null)
					{
						gameObject2.SendMessage("OnClickPlatformBackButtonEvent", null, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
		this.m_touchedPrevFrame = flag;
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x0002C56C File Offset: 0x0002A76C
	private void OnDestroy()
	{
		if (BackKeyManager.instance == this)
		{
			BackKeyManager.instance = null;
		}
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x0002C584 File Offset: 0x0002A784
	private void SetInstance()
	{
		if (BackKeyManager.instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			BackKeyManager.instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x0002C5B8 File Offset: 0x0002A7B8
	private bool CheckConnectNetwork()
	{
		return NetMonitor.Instance != null && !NetMonitor.Instance.IsIdle();
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x0002C5DC File Offset: 0x0002A7DC
	private void SentWindowMessege(ref WindowBase.BackButtonMessage msg)
	{
		if (this.m_windowCallBackList.Count > 0)
		{
			foreach (GameObject gameObject in this.m_windowCallBackList)
			{
				if (gameObject.activeSelf)
				{
					gameObject.SendMessage("OnClickPlatformBackButton", msg, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0002C668 File Offset: 0x0002A868
	private void SendMainMenuUI(ref WindowBase.BackButtonMessage msg)
	{
		if (!msg.IsFlag(WindowBase.BackButtonMessage.Flags.STAY_SEQUENCE) && this.m_mainMenuUICallBack != null)
		{
			this.m_mainMenuUICallBack.SendMessage("OnClickPlatformBackButton", msg, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0002C69C File Offset: 0x0002A89C
	private void SentMileageMessege(ref WindowBase.BackButtonMessage msg)
	{
		if (!msg.IsFlag(WindowBase.BackButtonMessage.Flags.STAY_SEQUENCE) && this.m_mileageCallBack != null)
		{
			this.m_mileageCallBack.SendMessage("OnClickPlatformBackButton", msg, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x0002C6D0 File Offset: 0x0002A8D0
	private void SendEventMessage()
	{
		foreach (GameObject gameObject in this.m_eventCallBackList)
		{
			if (gameObject != null)
			{
				gameObject.SendMessage("OnPlatformBackButtonClicked", null, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x040005CD RID: 1485
	private static BackKeyManager instance;

	// Token: 0x040005CE RID: 1486
	private List<GameObject> m_windowCallBackList = new List<GameObject>();

	// Token: 0x040005CF RID: 1487
	private List<GameObject> m_eventCallBackList = new List<GameObject>();

	// Token: 0x040005D0 RID: 1488
	private List<GameObject> m_tutorialEventCallBackList = new List<GameObject>();

	// Token: 0x040005D1 RID: 1489
	private GameObject m_mileageCallBack;

	// Token: 0x040005D2 RID: 1490
	private GameObject m_mainMenuUICallBack;

	// Token: 0x040005D3 RID: 1491
	private float m_timer;

	// Token: 0x040005D4 RID: 1492
	private bool m_invalidFlag;

	// Token: 0x040005D5 RID: 1493
	private bool m_tutorialFlag;

	// Token: 0x040005D6 RID: 1494
	private bool m_transSceneFlag = true;

	// Token: 0x040005D7 RID: 1495
	private bool m_sequenceTransitionFlag;

	// Token: 0x040005D8 RID: 1496
	private bool m_touchedPrevFrame;
}
