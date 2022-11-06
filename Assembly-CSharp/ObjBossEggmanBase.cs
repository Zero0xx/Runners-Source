using System;
using Message;
using UnityEngine;

// Token: 0x0200085F RID: 2143
public class ObjBossEggmanBase : ObjBossBase
{
	// Token: 0x06003A4B RID: 14923 RVA: 0x0013378C File Offset: 0x0013198C
	protected override string GetModelName()
	{
		return "enm_eggmobile";
	}

	// Token: 0x06003A4C RID: 14924 RVA: 0x00133794 File Offset: 0x00131994
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.ENEMY_RESOURCE;
	}

	// Token: 0x06003A4D RID: 14925 RVA: 0x00133798 File Offset: 0x00131998
	protected int GetMapBossLevel()
	{
		MsgGetMileageMapState msgGetMileageMapState = new MsgGetMileageMapState();
		GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnGetMileageMapState", msgGetMileageMapState, SendMessageOptions.DontRequireReceiver);
		if (msgGetMileageMapState.m_succeed && msgGetMileageMapState.m_mileageMapState != null)
		{
			int episode = msgGetMileageMapState.m_mileageMapState.m_episode;
			if (episode > 40)
			{
				return 5;
			}
			if (episode > 30)
			{
				return 4;
			}
			if (episode > 20)
			{
				return 3;
			}
			if (episode > 10)
			{
				return 2;
			}
		}
		return 1;
	}

	// Token: 0x04003183 RID: 12675
	private const string ModelName = "enm_eggmobile";
}
