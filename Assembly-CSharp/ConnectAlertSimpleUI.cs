using System;
using UnityEngine;

// Token: 0x020003FA RID: 1018
public class ConnectAlertSimpleUI : MonoBehaviour
{
	// Token: 0x06001E3D RID: 7741 RVA: 0x000B233C File Offset: 0x000B053C
	private void Start()
	{
		this.m_alertObj = GameObjectUtil.FindChildGameObject(base.gameObject, "Alert");
		if (this.m_alertObj != null)
		{
			this.m_alertObj.SetActive(false);
		}
		base.enabled = false;
	}

	// Token: 0x06001E3E RID: 7742 RVA: 0x000B2384 File Offset: 0x000B0584
	public void StartCollider()
	{
		this.m_refCount++;
		if (this.m_alertObj != null)
		{
			this.m_alertObj.SetActive(true);
		}
	}

	// Token: 0x06001E3F RID: 7743 RVA: 0x000B23B4 File Offset: 0x000B05B4
	public void EndCollider()
	{
		this.m_refCount--;
		if (this.m_refCount > 0)
		{
			return;
		}
		this.m_refCount = 0;
		if (this.m_alertObj != null)
		{
			this.m_alertObj.SetActive(false);
		}
	}

	// Token: 0x04001B9B RID: 7067
	private int m_refCount;

	// Token: 0x04001B9C RID: 7068
	private GameObject m_alertObj;
}
