using System;
using Message;
using UnityEngine;

// Token: 0x02000841 RID: 2113
public class ObjBossBase : SpawnableObject
{
	// Token: 0x06003917 RID: 14615 RVA: 0x0012ED0C File Offset: 0x0012CF0C
	protected override void OnSpawned()
	{
		if (ObjBossUtil.IsNowLastChance(ObjUtil.GetPlayerInformation()))
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		base.SetOnlyOneObject();
		base.SetNotRageout(true);
	}

	// Token: 0x06003918 RID: 14616 RVA: 0x0012ED44 File Offset: 0x0012CF44
	private void OnMsgBossInfo(MsgBossInfo msg)
	{
		msg.m_boss = base.gameObject;
		msg.m_position = base.transform.position;
		msg.m_rotation = base.transform.rotation;
		msg.m_succeed = true;
	}
}
