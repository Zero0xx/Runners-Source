using System;

namespace SaveData
{
	// Token: 0x020002AE RID: 686
	public class OptionData
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00069934 File Offset: 0x00067B34
		// (set) Token: 0x06001347 RID: 4935 RVA: 0x0006993C File Offset: 0x00067B3C
		public uint Dummy
		{
			get
			{
				return this.dummy;
			}
			set
			{
				this.dummy = value;
			}
		}

		// Token: 0x040010CF RID: 4303
		private uint dummy;
	}
}
