using System;
using UnityEngine;

// Token: 0x020001B6 RID: 438
public class DebugTraceMenu : MonoBehaviour
{
	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00047A70 File Offset: 0x00045C70
	// (set) Token: 0x06000C9A RID: 3226 RVA: 0x00047A78 File Offset: 0x00045C78
	public DebugTraceMenu.State currentState
	{
		get
		{
			return this.m_state;
		}
		private set
		{
		}
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x00047A7C File Offset: 0x00045C7C
	private void Start()
	{
		bool flag = this.m_state == DebugTraceMenu.State.ON;
		this.m_offButton = base.gameObject.AddComponent<DebugTraceButton>();
		this.m_offButton.Setup("TraceOff", new DebugTraceButton.ButtonClickedCallback(this.DebugButtonClickedCallback), new Vector2(10f, 10f));
		this.m_offButton.SetActive(flag);
		this.m_onButton = base.gameObject.AddComponent<DebugTraceButton>();
		this.m_onButton.Setup("TraceOn", new DebugTraceButton.ButtonClickedCallback(this.DebugButtonClickedCallback), new Vector2(10f, 10f));
		this.m_onButton.SetActive(!flag);
		this.m_displayMenu = base.gameObject.AddComponent<DebugTraceDisplayMenu>();
		this.m_displayMenu.Setup();
		this.m_displayMenu.SetActive(flag);
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x00047B50 File Offset: 0x00045D50
	private void DebugButtonClickedCallback(string buttonName)
	{
		if (buttonName == "TraceOff")
		{
			this.m_onButton.SetActive(true);
			this.m_offButton.SetActive(false);
			this.m_displayMenu.SetActive(false);
			GeneralWindow.Close();
			this.m_state = DebugTraceMenu.State.OFF;
		}
		else if (buttonName == "TraceOn")
		{
			this.m_onButton.SetActive(false);
			this.m_offButton.SetActive(true);
			this.m_displayMenu.SetActive(true);
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				buttonType = GeneralWindow.ButtonType.YesNo,
				caption = string.Empty,
				message = "トレース表示中"
			});
			this.m_state = DebugTraceMenu.State.ON;
		}
	}

	// Token: 0x040009DB RID: 2523
	private DebugTraceMenu.State m_state;

	// Token: 0x040009DC RID: 2524
	private DebugTraceButton m_offButton;

	// Token: 0x040009DD RID: 2525
	private DebugTraceButton m_onButton;

	// Token: 0x040009DE RID: 2526
	private DebugTraceDisplayMenu m_displayMenu;

	// Token: 0x020001B7 RID: 439
	public enum State
	{
		// Token: 0x040009E0 RID: 2528
		OFF,
		// Token: 0x040009E1 RID: 2529
		ON
	}
}
