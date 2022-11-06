using System;
using UnityEngine;

// Token: 0x02000290 RID: 656
public class TerrainPlacementInfo
{
	// Token: 0x060011F2 RID: 4594 RVA: 0x00065078 File Offset: 0x00063278
	public TerrainPlacementInfo()
	{
		this.m_destroyed = false;
	}

	// Token: 0x170002DF RID: 735
	// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0006509C File Offset: 0x0006329C
	// (set) Token: 0x060011F3 RID: 4595 RVA: 0x00065090 File Offset: 0x00063290
	public int m_terrainIndex { get; set; }

	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x060011F6 RID: 4598 RVA: 0x000650B0 File Offset: 0x000632B0
	// (set) Token: 0x060011F5 RID: 4597 RVA: 0x000650A4 File Offset: 0x000632A4
	public TerrainBlock m_block { get; set; }

	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x060011F8 RID: 4600 RVA: 0x000650C4 File Offset: 0x000632C4
	// (set) Token: 0x060011F7 RID: 4599 RVA: 0x000650B8 File Offset: 0x000632B8
	public GameObject m_gameObject { get; set; }

	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x060011FA RID: 4602 RVA: 0x000650D8 File Offset: 0x000632D8
	// (set) Token: 0x060011F9 RID: 4601 RVA: 0x000650CC File Offset: 0x000632CC
	public int ReserveIndex
	{
		get
		{
			return this.m_reserveIndex;
		}
		set
		{
			this.m_reserveIndex = value;
		}
	}

	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x060011FB RID: 4603 RVA: 0x000650E0 File Offset: 0x000632E0
	public bool Created
	{
		get
		{
			return this.m_gameObject != null;
		}
	}

	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x060011FC RID: 4604 RVA: 0x000650F0 File Offset: 0x000632F0
	public bool Destroyed
	{
		get
		{
			return this.m_destroyed;
		}
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x000650F8 File Offset: 0x000632F8
	public bool IsReserveTerrain()
	{
		return this.m_reserveIndex != -1;
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x00065108 File Offset: 0x00063308
	public void DestroyObject()
	{
		this.m_gameObject = null;
		this.m_destroyed = true;
	}

	// Token: 0x04001034 RID: 4148
	private int m_reserveIndex = -1;

	// Token: 0x04001035 RID: 4149
	private bool m_destroyed;
}
