using System;
using System.Collections.Generic;

namespace SaveData
{
	// Token: 0x020002AD RID: 685
	public class ItemData
	{
		// Token: 0x0600132E RID: 4910 RVA: 0x000695A8 File Offset: 0x000677A8
		public ItemData()
		{
			this.m_ring_count = 0U;
			this.m_red_ring_count = 0U;
			for (uint num = 0U; num < 8U; num += 1U)
			{
				this.m_item_count[(int)((UIntPtr)num)] = 0U;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x000695F4 File Offset: 0x000677F4
		// (set) Token: 0x06001330 RID: 4912 RVA: 0x000695FC File Offset: 0x000677FC
		public uint RingCount
		{
			get
			{
				return this.m_ring_count;
			}
			set
			{
				this.m_ring_count = value;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x00069608 File Offset: 0x00067808
		// (set) Token: 0x06001332 RID: 4914 RVA: 0x00069610 File Offset: 0x00067810
		public int RingCountOffset
		{
			get
			{
				return this.m_ring_count_offset;
			}
			set
			{
				this.m_ring_count_offset = value;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x0006961C File Offset: 0x0006781C
		public int DisplayRingCount
		{
			get
			{
				return (int)(this.RingCount + (uint)this.RingCountOffset);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x0006962C File Offset: 0x0006782C
		// (set) Token: 0x06001335 RID: 4917 RVA: 0x00069634 File Offset: 0x00067834
		public uint RedRingCount
		{
			get
			{
				return this.m_red_ring_count;
			}
			set
			{
				this.m_red_ring_count = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00069640 File Offset: 0x00067840
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x00069648 File Offset: 0x00067848
		public int RedRingCountOffset
		{
			get
			{
				return this.m_red_ring_count_offset;
			}
			set
			{
				this.m_red_ring_count_offset = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00069654 File Offset: 0x00067854
		public int DisplayRedRingCount
		{
			get
			{
				return (int)(this.RedRingCount + (uint)this.RedRingCountOffset);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x00069664 File Offset: 0x00067864
		// (set) Token: 0x0600133A RID: 4922 RVA: 0x0006966C File Offset: 0x0006786C
		public uint[] ItemCount
		{
			get
			{
				return this.m_item_count;
			}
			set
			{
				this.m_item_count = value;
			}
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00069678 File Offset: 0x00067878
		public uint GetItemCount(ItemType type)
		{
			if (this.IsValidType(type))
			{
				return this.m_item_count[(int)type];
			}
			return 0U;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00069690 File Offset: 0x00067890
		public void SetItemCount(ItemType type, uint count)
		{
			if (this.IsValidType(type))
			{
				this.m_item_count[(int)type] = count;
			}
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000696A8 File Offset: 0x000678A8
		public uint GetAllItemCount()
		{
			uint num = 0U;
			for (ItemType itemType = ItemType.INVINCIBLE; itemType < ItemType.NUM; itemType++)
			{
				num += this.GetItemCount(itemType);
			}
			return num;
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000696D4 File Offset: 0x000678D4
		private bool IsValidType(ItemType type)
		{
			return ItemType.INVINCIBLE <= type && type < ItemType.NUM;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000696E4 File Offset: 0x000678E4
		public void SetEtcItemCount(ServerItem.Id itemId, long count)
		{
			if (this.m_etc_item_count == null && this.m_etc_item_count_offset == null)
			{
				this.m_etc_item_count = new Dictionary<ServerItem.Id, long>();
				this.m_etc_item_count_offset = new Dictionary<ServerItem.Id, long>();
			}
			if (this.m_etc_item_count.ContainsKey(itemId))
			{
				this.m_etc_item_count[itemId] = count;
				this.m_etc_item_count_offset[itemId] = 0L;
			}
			else
			{
				this.m_etc_item_count.Add(itemId, count);
				this.m_etc_item_count_offset.Add(itemId, 0L);
			}
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0006976C File Offset: 0x0006796C
		public void AddEtcItemCount(ServerItem.Id itemId, long count)
		{
			if (this.m_etc_item_count == null && this.m_etc_item_count_offset == null)
			{
				this.m_etc_item_count = new Dictionary<ServerItem.Id, long>();
				this.m_etc_item_count_offset = new Dictionary<ServerItem.Id, long>();
			}
			if (this.m_etc_item_count.ContainsKey(itemId))
			{
				Dictionary<ServerItem.Id, long> etc_item_count;
				Dictionary<ServerItem.Id, long> dictionary = etc_item_count = this.m_etc_item_count;
				long num = etc_item_count[itemId];
				dictionary[itemId] = num + count;
				this.m_etc_item_count_offset[itemId] = 0L;
			}
			else
			{
				this.m_etc_item_count.Add(itemId, count);
				this.m_etc_item_count_offset.Add(itemId, 0L);
			}
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00069800 File Offset: 0x00067A00
		public bool SetEtcItemCountOffset(ServerItem.Id itemId, long offset)
		{
			bool result = false;
			if (this.m_etc_item_count != null && this.m_etc_item_count_offset != null && this.m_etc_item_count_offset.ContainsKey(itemId))
			{
				this.m_etc_item_count_offset[itemId] = offset;
				result = true;
			}
			return result;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00069848 File Offset: 0x00067A48
		public long GetEtcItemCount(ServerItem.Id itemId)
		{
			long result = 0L;
			if (this.m_etc_item_count != null && this.m_etc_item_count.ContainsKey(itemId) && this.m_etc_item_count_offset.ContainsKey(itemId))
			{
				result = this.m_etc_item_count[itemId] + this.m_etc_item_count_offset[itemId];
			}
			return result;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000698A0 File Offset: 0x00067AA0
		public long GetEtcItemCountOffset(ServerItem.Id itemId)
		{
			long result = 0L;
			if (this.m_etc_item_count != null && this.m_etc_item_count.ContainsKey(itemId) && this.m_etc_item_count_offset.ContainsKey(itemId))
			{
				result = this.m_etc_item_count_offset[itemId];
			}
			return result;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000698EC File Offset: 0x00067AEC
		public bool IsEtcItemCount(ServerItem.Id itemId)
		{
			bool result = false;
			if (this.m_etc_item_count != null && this.m_etc_item_count.ContainsKey(itemId) && this.m_etc_item_count_offset.ContainsKey(itemId))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x040010C8 RID: 4296
		private uint m_ring_count;

		// Token: 0x040010C9 RID: 4297
		private uint m_red_ring_count;

		// Token: 0x040010CA RID: 4298
		private int m_ring_count_offset;

		// Token: 0x040010CB RID: 4299
		private int m_red_ring_count_offset;

		// Token: 0x040010CC RID: 4300
		private uint[] m_item_count = new uint[8];

		// Token: 0x040010CD RID: 4301
		private Dictionary<ServerItem.Id, long> m_etc_item_count;

		// Token: 0x040010CE RID: 4302
		private Dictionary<ServerItem.Id, long> m_etc_item_count_offset;
	}
}
