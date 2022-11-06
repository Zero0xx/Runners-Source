using System;
using System.Collections.Generic;

// Token: 0x02000277 RID: 631
public class StageSpawnableParameterContainer
{
	// Token: 0x06001141 RID: 4417 RVA: 0x000622DC File Offset: 0x000604DC
	public StageSpawnableParameterContainer()
	{
		this.m_setData = new Dictionary<uint, BlockSpawnableParameterContainer>();
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x000622F0 File Offset: 0x000604F0
	public void AddData(int block, int layer, BlockSpawnableParameterContainer data)
	{
		uint uniqueID = StageSpawnableParameterContainer.GetUniqueID(block, layer);
		this.m_setData.Add(uniqueID, data);
	}

	// Token: 0x06001143 RID: 4419 RVA: 0x00062314 File Offset: 0x00060514
	public BlockSpawnableParameterContainer GetBlockData(int blockID, int layerID)
	{
		uint uniqueID = StageSpawnableParameterContainer.GetUniqueID(blockID, layerID);
		BlockSpawnableParameterContainer result;
		this.m_setData.TryGetValue(uniqueID, out result);
		return result;
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x0006233C File Offset: 0x0006053C
	public SpawnableParameter GetParameter(int blockID, int layerID, int id)
	{
		uint uniqueID = StageSpawnableParameterContainer.GetUniqueID(blockID, layerID);
		if (!this.m_setData.ContainsKey(uniqueID))
		{
			return null;
		}
		BlockSpawnableParameterContainer blockSpawnableParameterContainer = this.m_setData[uniqueID];
		if (blockSpawnableParameterContainer != null)
		{
			return blockSpawnableParameterContainer.GetParameter(id);
		}
		return null;
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x00062380 File Offset: 0x00060580
	private static uint GetUniqueID(int block, int layer)
	{
		return (uint)((block << 4) + layer);
	}

	// Token: 0x04000F8F RID: 3983
	private Dictionary<uint, BlockSpawnableParameterContainer> m_setData;
}
