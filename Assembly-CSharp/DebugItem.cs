using System;
using UnityEngine;

// Token: 0x020001AD RID: 429
public class DebugItem : MonoBehaviour
{
	// Token: 0x06000C41 RID: 3137 RVA: 0x000468AC File Offset: 0x00044AAC
	private void Start()
	{
		this.m_item_object[(int)((UIntPtr)0)] = GameObject.Find("InvincibleCountLabel");
		this.m_item_object[(int)((UIntPtr)1)] = GameObject.Find("BarrierCountLabel");
		this.m_item_object[(int)((UIntPtr)2)] = GameObject.Find("MagnetCountLabel");
		this.m_item_object[(int)((UIntPtr)3)] = GameObject.Find("TrampolineCountLabel");
		this.m_item_object[(int)((UIntPtr)4)] = GameObject.Find("ComboCountLabel");
		this.m_item_object[(int)((UIntPtr)5)] = GameObject.Find("LaserCountLabel");
		this.m_item_object[(int)((UIntPtr)6)] = GameObject.Find("DrillCountLabel");
		this.m_item_object[(int)((UIntPtr)7)] = GameObject.Find("AsteroidCountLabel");
		this.m_ring_object = GameObject.Find("RingCountLabel");
		this.m_red_ring_object = GameObject.Find("RedRingCountLabel");
		ItemPool.Initialize();
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x00046978 File Offset: 0x00044B78
	private void Update()
	{
		for (uint num = 0U; num < 8U; num += 1U)
		{
			if (this.m_item_object[(int)((UIntPtr)num)])
			{
				uint itemCount = ItemPool.GetItemCount((ItemType)num);
				UILabel component = this.m_item_object[(int)((UIntPtr)num)].GetComponent<UILabel>();
				if (component)
				{
					component.text = itemCount.ToString();
				}
			}
		}
		if (this.m_ring_object)
		{
			uint ringCount = ItemPool.RingCount;
			UILabel component2 = this.m_ring_object.GetComponent<UILabel>();
			if (component2)
			{
				component2.text = ringCount.ToString();
			}
		}
		if (this.m_red_ring_object)
		{
			uint redRingCount = ItemPool.RedRingCount;
			UILabel component3 = this.m_red_ring_object.GetComponent<UILabel>();
			if (component3)
			{
				component3.text = redRingCount.ToString();
			}
		}
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x00046A54 File Offset: 0x00044C54
	private void OnAddInvincibleCount(GameObject obj)
	{
		if (obj.name == "InvincibleAddButton")
		{
			uint itemCount = ItemPool.GetItemCount(ItemType.INVINCIBLE);
			ItemPool.SetItemCount(ItemType.INVINCIBLE, itemCount + 1U);
		}
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x00046A88 File Offset: 0x00044C88
	private void OnSubInvincibleCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.INVINCIBLE);
		if (itemCount > 0U)
		{
			ItemPool.SetItemCount(ItemType.INVINCIBLE, itemCount - 1U);
		}
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x00046AAC File Offset: 0x00044CAC
	private void OnAddBarrierCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.BARRIER);
		ItemPool.SetItemCount(ItemType.BARRIER, itemCount + 1U);
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x00046ACC File Offset: 0x00044CCC
	private void OnSubBarrierCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.BARRIER);
		if (itemCount > 0U)
		{
			ItemPool.SetItemCount(ItemType.BARRIER, itemCount - 1U);
		}
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x00046AF0 File Offset: 0x00044CF0
	private void OnAddMagnetCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.MAGNET);
		ItemPool.SetItemCount(ItemType.MAGNET, itemCount + 1U);
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x00046B10 File Offset: 0x00044D10
	private void OnSubMagnetCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.MAGNET);
		if (itemCount > 0U)
		{
			ItemPool.SetItemCount(ItemType.MAGNET, itemCount - 1U);
		}
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x00046B34 File Offset: 0x00044D34
	private void OnAddTrampolineCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.TRAMPOLINE);
		ItemPool.SetItemCount(ItemType.TRAMPOLINE, itemCount + 1U);
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x00046B54 File Offset: 0x00044D54
	private void OnSubTrampolineCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.TRAMPOLINE);
		if (itemCount > 0U)
		{
			ItemPool.SetItemCount(ItemType.TRAMPOLINE, itemCount - 1U);
		}
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x00046B78 File Offset: 0x00044D78
	private void OnAddComboCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.COMBO);
		ItemPool.SetItemCount(ItemType.COMBO, itemCount + 1U);
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x00046B98 File Offset: 0x00044D98
	private void OnSubComboCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.COMBO);
		if (itemCount > 0U)
		{
			ItemPool.SetItemCount(ItemType.COMBO, itemCount - 1U);
		}
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x00046BBC File Offset: 0x00044DBC
	private void OnAddLaserCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.LASER);
		ItemPool.SetItemCount(ItemType.LASER, itemCount + 1U);
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x00046BDC File Offset: 0x00044DDC
	private void OnSubLaserCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.LASER);
		if (itemCount > 0U)
		{
			ItemPool.SetItemCount(ItemType.LASER, itemCount - 1U);
		}
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x00046C00 File Offset: 0x00044E00
	private void OnAddDrillCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.DRILL);
		ItemPool.SetItemCount(ItemType.DRILL, itemCount + 1U);
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x00046C20 File Offset: 0x00044E20
	private void OnSubDrillCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.DRILL);
		if (itemCount > 0U)
		{
			ItemPool.SetItemCount(ItemType.DRILL, itemCount - 1U);
		}
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x00046C44 File Offset: 0x00044E44
	private void OnAddAsteroidCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.ASTEROID);
		ItemPool.SetItemCount(ItemType.ASTEROID, itemCount + 1U);
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x00046C64 File Offset: 0x00044E64
	private void OnSubAsteroidCount()
	{
		uint itemCount = ItemPool.GetItemCount(ItemType.ASTEROID);
		if (itemCount > 0U)
		{
			ItemPool.SetItemCount(ItemType.ASTEROID, itemCount - 1U);
		}
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x00046C88 File Offset: 0x00044E88
	private void OnAddRingCount()
	{
		uint ringCount = ItemPool.RingCount;
		ItemPool.RingCount = ringCount + 1U;
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x00046CA4 File Offset: 0x00044EA4
	private void OnSubRingCount()
	{
		uint ringCount = ItemPool.RingCount;
		if (ringCount > 0U)
		{
			ItemPool.RingCount = ringCount - 1U;
		}
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x00046CC8 File Offset: 0x00044EC8
	private void OnAddRedRingCount()
	{
		uint redRingCount = ItemPool.RedRingCount;
		ItemPool.RedRingCount = redRingCount + 1U;
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00046CE4 File Offset: 0x00044EE4
	private void OnSubRedRingCount()
	{
		uint redRingCount = ItemPool.RedRingCount;
		if (redRingCount > 0U)
		{
			ItemPool.RedRingCount = redRingCount - 1U;
		}
	}

	// Token: 0x040009A6 RID: 2470
	private GameObject[] m_item_object = new GameObject[8];

	// Token: 0x040009A7 RID: 2471
	private GameObject m_ring_object;

	// Token: 0x040009A8 RID: 2472
	private GameObject m_red_ring_object;
}
