using System;
using System.Collections.Generic;

// Token: 0x02000276 RID: 630
public class BlockSpawnableParameterContainer
{
	// Token: 0x0600113A RID: 4410 RVA: 0x00062258 File Offset: 0x00060458
	public BlockSpawnableParameterContainer(int blk, int ptn)
	{
		this.m_parameters = new List<SpawnableParameter>();
		this.m_block = blk;
		this.m_layer = ptn;
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x0006227C File Offset: 0x0006047C
	private BlockSpawnableParameterContainer()
	{
		this.m_parameters = new List<SpawnableParameter>();
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x00062290 File Offset: 0x00060490
	public void AddParameter(SpawnableParameter param)
	{
		this.m_parameters.Add(param);
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x000622A0 File Offset: 0x000604A0
	public SpawnableParameter GetParameter(int id)
	{
		if (id >= this.m_parameters.Count)
		{
			return null;
		}
		return this.m_parameters[id];
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x000622C4 File Offset: 0x000604C4
	public List<SpawnableParameter> GetParameters()
	{
		return this.m_parameters;
	}

	// Token: 0x170002BB RID: 699
	// (get) Token: 0x0600113F RID: 4415 RVA: 0x000622CC File Offset: 0x000604CC
	public int Block
	{
		get
		{
			return this.m_block;
		}
	}

	// Token: 0x170002BC RID: 700
	// (get) Token: 0x06001140 RID: 4416 RVA: 0x000622D4 File Offset: 0x000604D4
	public int Layer
	{
		get
		{
			return this.m_layer;
		}
	}

	// Token: 0x04000F8C RID: 3980
	public readonly int m_block;

	// Token: 0x04000F8D RID: 3981
	public readonly int m_layer;

	// Token: 0x04000F8E RID: 3982
	private List<SpawnableParameter> m_parameters;
}
