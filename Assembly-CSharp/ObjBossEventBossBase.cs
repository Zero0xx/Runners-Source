using System;

// Token: 0x02000847 RID: 2119
public class ObjBossEventBossBase : ObjBossBase
{
	// Token: 0x060039C8 RID: 14792 RVA: 0x00130CB0 File Offset: 0x0012EEB0
	protected virtual BossType GetBossType()
	{
		return BossType.NONE;
	}

	// Token: 0x060039C9 RID: 14793 RVA: 0x00130CB4 File Offset: 0x0012EEB4
	protected int GetEventBossLevel()
	{
		int num = 0;
		if (RaidBossInfo.currentRaidData != null)
		{
			num = RaidBossInfo.currentRaidData.lv;
		}
		if (num > (int)EventBossParamTable.GetItemData(EventBossParamTableItem.Level4))
		{
			return 5;
		}
		if (num > (int)EventBossParamTable.GetItemData(EventBossParamTableItem.Level3))
		{
			return 4;
		}
		if (num > (int)EventBossParamTable.GetItemData(EventBossParamTableItem.Level2))
		{
			return 3;
		}
		if (num > (int)EventBossParamTable.GetItemData(EventBossParamTableItem.Level1))
		{
			return 2;
		}
		return 1;
	}

	// Token: 0x060039CA RID: 14794 RVA: 0x00130D18 File Offset: 0x0012EF18
	protected int GetEventBossHpMax()
	{
		int result = 10;
		if (RaidBossInfo.currentRaidData != null)
		{
			result = (int)RaidBossInfo.currentRaidData.hpMax;
		}
		return result;
	}
}
