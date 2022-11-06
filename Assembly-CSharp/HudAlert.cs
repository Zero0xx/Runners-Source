using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000347 RID: 839
public class HudAlert : MonoBehaviour
{
	// Token: 0x060018E9 RID: 6377 RVA: 0x0008FBEC File Offset: 0x0008DDEC
	public void StartAlert(GameObject chaseObject)
	{
		HudAlertIcon hudAlertIcon = base.gameObject.AddComponent<HudAlertIcon>();
		hudAlertIcon.Setup(this.m_camera, chaseObject, 1f);
		this.m_iconList.Add(hudAlertIcon);
	}

	// Token: 0x060018EA RID: 6378 RVA: 0x0008FC24 File Offset: 0x0008DE24
	public void EndAlert(GameObject chaseObject)
	{
		HudAlertIcon hudAlertIcon = null;
		foreach (HudAlertIcon hudAlertIcon2 in this.m_iconList)
		{
			if (!(hudAlertIcon2 == null))
			{
				if (hudAlertIcon2.IsChasingObject(chaseObject))
				{
					hudAlertIcon = hudAlertIcon2;
				}
			}
		}
		if (hudAlertIcon != null)
		{
			this.m_iconList.Remove(hudAlertIcon);
			UnityEngine.Object.Destroy(hudAlertIcon);
		}
	}

	// Token: 0x060018EB RID: 6379 RVA: 0x0008FCC4 File Offset: 0x0008DEC4
	private void Start()
	{
		this.m_iconList = new List<HudAlertIcon>();
		GameObject gameObject = GameObject.Find("GameMainCamera");
		if (gameObject != null)
		{
			this.m_camera = gameObject.GetComponent<Camera>();
			if (this.m_camera == null)
			{
			}
		}
	}

	// Token: 0x060018EC RID: 6380 RVA: 0x0008FD10 File Offset: 0x0008DF10
	private void Update()
	{
		if (this.m_iconList.Count <= 0)
		{
			return;
		}
		List<HudAlertIcon> list = new List<HudAlertIcon>();
		foreach (HudAlertIcon hudAlertIcon in this.m_iconList)
		{
			if (!(hudAlertIcon == null))
			{
				if (hudAlertIcon.IsEnd)
				{
					list.Add(hudAlertIcon);
				}
			}
		}
		foreach (HudAlertIcon hudAlertIcon2 in list)
		{
			if (!(hudAlertIcon2 == null))
			{
				this.m_iconList.Remove(hudAlertIcon2);
				UnityEngine.Object.Destroy(hudAlertIcon2);
			}
		}
		list.Clear();
	}

	// Token: 0x060018ED RID: 6381 RVA: 0x0008FE20 File Offset: 0x0008E020
	private void OnMsgExitStage(MsgExitStage msg)
	{
		base.enabled = false;
	}

	// Token: 0x0400164B RID: 5707
	private const float IconDisplayTime = 1f;

	// Token: 0x0400164C RID: 5708
	private List<HudAlertIcon> m_iconList;

	// Token: 0x0400164D RID: 5709
	private Camera m_camera;
}
