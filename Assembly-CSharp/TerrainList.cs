using System;
using System.Collections.Generic;

// Token: 0x0200028B RID: 651
internal class TerrainList
{
	// Token: 0x060011DA RID: 4570 RVA: 0x00064958 File Offset: 0x00062B58
	public TerrainList(string name)
	{
		this.m_name = name;
		this.m_terrainList = new Dictionary<string, Terrain>();
	}

	// Token: 0x170002DE RID: 734
	// (get) Token: 0x060011DC RID: 4572 RVA: 0x00064980 File Offset: 0x00062B80
	// (set) Token: 0x060011DB RID: 4571 RVA: 0x00064974 File Offset: 0x00062B74
	public string m_name { get; private set; }

	// Token: 0x060011DD RID: 4573 RVA: 0x00064988 File Offset: 0x00062B88
	public void AddTerrain(Terrain terrain)
	{
		this.m_terrainList.Add(terrain.m_name, terrain);
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x0006499C File Offset: 0x00062B9C
	public int GetTerrainCount()
	{
		return this.m_terrainList.Count;
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x000649AC File Offset: 0x00062BAC
	public Terrain GetTerrain(string name)
	{
		if (!this.m_terrainList.ContainsKey(name))
		{
			return null;
		}
		return this.m_terrainList[name];
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x000649D0 File Offset: 0x00062BD0
	public Terrain GetTerrain(int index)
	{
		string name = string.Format("{0:00}", index);
		return this.GetTerrain(name);
	}

	// Token: 0x04001026 RID: 4134
	private Dictionary<string, Terrain> m_terrainList;
}
