using System;
using UnityEngine;

// Token: 0x02000291 RID: 657
public class TerrainReserveObject
{
	// Token: 0x060011FF RID: 4607 RVA: 0x00065118 File Offset: 0x00063318
	public TerrainReserveObject(GameObject obj, string name, int reserveIndex)
	{
		this.m_gameObject = obj;
		this.m_blockName = name;
		this.m_reserveIndex = reserveIndex;
		this.m_rented = false;
	}

	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x06001200 RID: 4608 RVA: 0x0006515C File Offset: 0x0006335C
	public string blockName
	{
		get
		{
			return this.m_blockName;
		}
	}

	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x06001201 RID: 4609 RVA: 0x00065164 File Offset: 0x00063364
	public bool EableReservation
	{
		get
		{
			return !this.m_rented;
		}
	}

	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x06001202 RID: 4610 RVA: 0x00065170 File Offset: 0x00063370
	public int ReserveIndex
	{
		get
		{
			return this.m_reserveIndex;
		}
	}

	// Token: 0x06001203 RID: 4611 RVA: 0x00065178 File Offset: 0x00063378
	public GameObject ReserveObject()
	{
		this.m_rented = true;
		return this.m_gameObject;
	}

	// Token: 0x06001204 RID: 4612 RVA: 0x00065188 File Offset: 0x00063388
	public void ReturnObject()
	{
		this.m_rented = false;
		if (this.m_gameObject != null && this.m_gameObject.activeSelf)
		{
			this.m_gameObject.SetActive(false);
		}
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x000651CC File Offset: 0x000633CC
	public GameObject GetGameObject()
	{
		return this.m_gameObject;
	}

	// Token: 0x04001039 RID: 4153
	private GameObject m_gameObject;

	// Token: 0x0400103A RID: 4154
	private string m_blockName = string.Empty;

	// Token: 0x0400103B RID: 4155
	private int m_reserveIndex = -1;

	// Token: 0x0400103C RID: 4156
	private bool m_rented;
}
