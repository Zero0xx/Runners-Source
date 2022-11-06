using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009CA RID: 2506
public class PresentBoxManager : MonoBehaviour
{
	// Token: 0x170008EF RID: 2287
	// (get) Token: 0x060041BC RID: 16828 RVA: 0x001562EC File Offset: 0x001544EC
	public static PresentBoxManager Instance
	{
		get
		{
			return PresentBoxManager.instance;
		}
	}

	// Token: 0x060041BD RID: 16829 RVA: 0x001562F4 File Offset: 0x001544F4
	protected void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x060041BE RID: 16830 RVA: 0x001562FC File Offset: 0x001544FC
	private void Start()
	{
		if (this.m_present_datas == null)
		{
			this.m_present_datas = new List<PresentItem>();
		}
		base.enabled = false;
	}

	// Token: 0x060041BF RID: 16831 RVA: 0x0015631C File Offset: 0x0015451C
	private void OnDestroy()
	{
		if (PresentBoxManager.instance == this)
		{
			PresentBoxManager.instance = null;
		}
	}

	// Token: 0x060041C0 RID: 16832 RVA: 0x00156334 File Offset: 0x00154534
	private void SetInstance()
	{
		if (PresentBoxManager.instance == null)
		{
			PresentBoxManager.instance = this;
		}
		else if (this != PresentBoxManager.Instance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060041C1 RID: 16833 RVA: 0x00156378 File Offset: 0x00154578
	public PresentItem GetData(int index)
	{
		if (index < this.m_present_datas.Count)
		{
			return this.m_present_datas[index];
		}
		return null;
	}

	// Token: 0x060041C2 RID: 16834 RVA: 0x0015639C File Offset: 0x0015459C
	public int GetDataCount()
	{
		return this.m_present_datas.Count;
	}

	// Token: 0x0400381C RID: 14364
	private static PresentBoxManager instance;

	// Token: 0x0400381D RID: 14365
	private List<PresentItem> m_present_datas;
}
