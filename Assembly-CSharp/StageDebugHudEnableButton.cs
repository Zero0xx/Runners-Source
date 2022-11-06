using System;
using UnityEngine;

// Token: 0x020002EB RID: 747
public class StageDebugHudEnableButton : MonoBehaviour
{
	// Token: 0x06001588 RID: 5512 RVA: 0x00076F40 File Offset: 0x00075140
	private void Awake()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001589 RID: 5513 RVA: 0x00076F50 File Offset: 0x00075150
	private void Start()
	{
		this.m_hudCockpit = GameObject.Find("HudCockpit");
		this.m_DebugTraceManager = GameObject.Find("DebugTraceManager");
		this.m_allocationStatus = GameObjectUtil.FindGameObjectComponent<AllocationStatus>("AllocationStatus");
		float num = 100f;
		this.m_buttonRect = new Rect(((float)Screen.width - num) * 0.5f, 0f, num, num);
	}

	// Token: 0x0600158A RID: 5514 RVA: 0x00076FB4 File Offset: 0x000751B4
	private void OnGUI()
	{
		if (GUI.Button(this.m_buttonRect, string.Empty, GUIStyle.none))
		{
			bool flag = true;
			if (this.m_hudCockpit != null)
			{
				flag = !this.m_hudCockpit.activeSelf;
				this.m_hudCockpit.SetActive(flag);
			}
			if (this.m_DebugTraceManager != null)
			{
				this.m_DebugTraceManager.SetActive(flag);
			}
			if (this.m_allocationStatus != null)
			{
				this.m_allocationStatus.show = flag;
			}
		}
	}

	// Token: 0x04001307 RID: 4871
	private GameObject m_hudCockpit;

	// Token: 0x04001308 RID: 4872
	private GameObject m_DebugTraceManager;

	// Token: 0x04001309 RID: 4873
	private AllocationStatus m_allocationStatus;

	// Token: 0x0400130A RID: 4874
	private Rect m_buttonRect;
}
