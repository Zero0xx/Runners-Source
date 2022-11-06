using System;
using System.Collections.Generic;

// Token: 0x0200028A RID: 650
public class Terrain
{
	// Token: 0x060011D2 RID: 4562 RVA: 0x000648D0 File Offset: 0x00062AD0
	public Terrain(string name, float meter)
	{
		this.m_name = name;
		this.m_meter = meter;
		this.m_blocks = new List<TerrainBlock>();
	}

	// Token: 0x170002DC RID: 732
	// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00064900 File Offset: 0x00062B00
	// (set) Token: 0x060011D3 RID: 4563 RVA: 0x000648F4 File Offset: 0x00062AF4
	public string m_name { get; private set; }

	// Token: 0x170002DD RID: 733
	// (get) Token: 0x060011D6 RID: 4566 RVA: 0x00064914 File Offset: 0x00062B14
	// (set) Token: 0x060011D5 RID: 4565 RVA: 0x00064908 File Offset: 0x00062B08
	public float m_meter { get; private set; }

	// Token: 0x060011D7 RID: 4567 RVA: 0x0006491C File Offset: 0x00062B1C
	public void AddTerrainBlock(TerrainBlock terrainBlock)
	{
		this.m_blocks.Add(terrainBlock);
	}

	// Token: 0x060011D8 RID: 4568 RVA: 0x0006492C File Offset: 0x00062B2C
	public int GetBlockCount()
	{
		return this.m_blocks.Count;
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x0006493C File Offset: 0x00062B3C
	public TerrainBlock GetBlock(int index)
	{
		if (index >= this.GetBlockCount())
		{
			return null;
		}
		return this.m_blocks[index];
	}

	// Token: 0x04001023 RID: 4131
	private List<TerrainBlock> m_blocks;
}
