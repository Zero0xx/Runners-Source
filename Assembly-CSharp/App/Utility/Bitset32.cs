using System;

namespace App.Utility
{
	// Token: 0x02000A2F RID: 2607
	public struct Bitset32
	{
		// Token: 0x06004525 RID: 17701 RVA: 0x00163AD8 File Offset: 0x00161CD8
		public Bitset32(Bitset32 rhs)
		{
			this.m_Value = rhs.m_Value;
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x00163AE8 File Offset: 0x00161CE8
		public Bitset32(uint x)
		{
			this.m_Value = x;
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x00163AF4 File Offset: 0x00161CF4
		public override bool Equals(object o)
		{
			return true;
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x00163AF8 File Offset: 0x00161CF8
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x00163AFC File Offset: 0x00161CFC
		public bool Test(int pos)
		{
			return ((ulong)this.m_Value & (ulong)(1L << (pos & 31))) != 0UL;
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x00163B14 File Offset: 0x00161D14
		public bool Any()
		{
			return this.m_Value != 0U;
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x00163B24 File Offset: 0x00161D24
		public bool None()
		{
			return !this.Any();
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x00163B30 File Offset: 0x00161D30
		public int Count()
		{
			uint num = 1U;
			int num2 = 0;
			for (int i = 32; i > 0; i--)
			{
				if ((this.m_Value & num) != 0U)
				{
					num2++;
				}
				num <<= 1;
			}
			return num2;
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x00163B6C File Offset: 0x00161D6C
		public Bitset32 Set(int pos)
		{
			this.m_Value |= 1U << pos;
			return this;
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x00163B8C File Offset: 0x00161D8C
		public Bitset32 Set(int pos, bool flag)
		{
			if (flag)
			{
				this.m_Value |= 1U << pos;
			}
			else
			{
				this.m_Value &= ~(1U << pos);
			}
			return this;
		}

		// Token: 0x0600452F RID: 17711 RVA: 0x00163BD8 File Offset: 0x00161DD8
		public Bitset32 Set()
		{
			this.m_Value = uint.MaxValue;
			return this;
		}

		// Token: 0x06004530 RID: 17712 RVA: 0x00163BE8 File Offset: 0x00161DE8
		public Bitset32 Reset(int pos)
		{
			this.m_Value ^= 1U << pos;
			return this;
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x00163C08 File Offset: 0x00161E08
		public Bitset32 Reset()
		{
			this.m_Value = 0U;
			return this;
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x00163C18 File Offset: 0x00161E18
		public Bitset32 Flip(int pos)
		{
			this.m_Value ^= 1U << pos;
			return this;
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x00163C38 File Offset: 0x00161E38
		public Bitset32 Flip()
		{
			this.m_Value = ~this.m_Value;
			return this;
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x00163C50 File Offset: 0x00161E50
		public uint to_ulong()
		{
			return this.m_Value;
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x00163C58 File Offset: 0x00161E58
		public static bool operator ==(Bitset32 lhs, Bitset32 rhs)
		{
			return lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x00163C6C File Offset: 0x00161E6C
		public static bool operator !=(Bitset32 lhs, Bitset32 rhs)
		{
			return lhs.m_Value != rhs.m_Value;
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x00163C84 File Offset: 0x00161E84
		public static Bitset32 operator &(Bitset32 lhs, Bitset32 rhs)
		{
			return new Bitset32(lhs.m_Value & rhs.m_Value);
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x00163C9C File Offset: 0x00161E9C
		public static Bitset32 operator |(Bitset32 lhs, Bitset32 rhs)
		{
			return new Bitset32(lhs.m_Value | rhs.m_Value);
		}

		// Token: 0x040039DB RID: 14811
		private uint m_Value;
	}
}
