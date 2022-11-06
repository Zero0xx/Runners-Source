using System;
using UnityEngine;

// Token: 0x02000420 RID: 1056
public class ItemSetRingManagement : MonoBehaviour
{
	// Token: 0x06001FD1 RID: 8145 RVA: 0x000BDE14 File Offset: 0x000BC014
	public void AddOffset(int offset)
	{
		this.m_offset += offset;
		if (this.m_offset > 0)
		{
			this.m_offset = 0;
			return;
		}
		this.UpdateRingCount();
	}

	// Token: 0x06001FD2 RID: 8146 RVA: 0x000BDE4C File Offset: 0x000BC04C
	public void UpdateRingCount()
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			instance.ItemData.RingCountOffset = this.m_offset;
		}
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x06001FD3 RID: 8147 RVA: 0x000BDE84 File Offset: 0x000BC084
	public bool IsEnablePurchase(int itemCost)
	{
		int displayRingCount = this.GetDisplayRingCount();
		return itemCost <= displayRingCount;
	}

	// Token: 0x06001FD4 RID: 8148 RVA: 0x000BDEA4 File Offset: 0x000BC0A4
	public int GetDisplayRingCount()
	{
		int result = 0;
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			result = instance.ItemData.DisplayRingCount;
		}
		return result;
	}

	// Token: 0x06001FD5 RID: 8149 RVA: 0x000BDED4 File Offset: 0x000BC0D4
	private void Start()
	{
	}

	// Token: 0x06001FD6 RID: 8150 RVA: 0x000BDED8 File Offset: 0x000BC0D8
	private void Update()
	{
	}

	// Token: 0x04001CCA RID: 7370
	private int m_offset;
}
