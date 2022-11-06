using System;

// Token: 0x0200066C RID: 1644
public struct RewardData
{
	// Token: 0x06002C09 RID: 11273 RVA: 0x0010B5F4 File Offset: 0x001097F4
	public RewardData(int reward_id, int reward_count)
	{
		this.serverId = reward_id;
		ServerItem serverItem = new ServerItem((ServerItem.Id)reward_id);
		int num = reward_id;
		ServerItem.IdType idType = serverItem.idType;
		if (idType != ServerItem.IdType.RSRING)
		{
			if (idType != ServerItem.IdType.RING)
			{
				if (idType == ServerItem.IdType.EQUIP_ITEM)
				{
					num = serverItem.idIndex;
				}
			}
			else
			{
				num = 8;
			}
		}
		else
		{
			num = 9;
		}
		this.reward_id = num;
		this.reward_count = reward_count;
	}

	// Token: 0x06002C0A RID: 11274 RVA: 0x0010B664 File Offset: 0x00109864
	public void Set(RewardData src)
	{
		this.serverId = src.serverId;
		this.reward_id = src.reward_id;
		this.reward_count = src.reward_count;
	}

	// Token: 0x0400290E RID: 10510
	public int serverId;

	// Token: 0x0400290F RID: 10511
	public int reward_id;

	// Token: 0x04002910 RID: 10512
	public int reward_count;
}
