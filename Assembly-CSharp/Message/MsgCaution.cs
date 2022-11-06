using System;
using UnityEngine;

namespace Message
{
	// Token: 0x02000599 RID: 1433
	public class MsgCaution : MessageBase
	{
		// Token: 0x06002AFD RID: 11005 RVA: 0x00109934 File Offset: 0x00107B34
		public MsgCaution(HudCaution.Type cautionType) : base(12317)
		{
			this.m_cautionType = cautionType;
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x00109948 File Offset: 0x00107B48
		public MsgCaution(HudCaution.Type cautionType, int number) : base(12317)
		{
			this.m_cautionType = cautionType;
			this.m_number = number;
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x00109964 File Offset: 0x00107B64
		public MsgCaution(HudCaution.Type cautionType, int number, bool flag) : base(12317)
		{
			this.m_cautionType = cautionType;
			this.m_number = number;
			this.m_flag = flag;
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x00109994 File Offset: 0x00107B94
		public MsgCaution(HudCaution.Type cautionType, float rate) : base(12317)
		{
			this.m_cautionType = cautionType;
			this.m_rate = rate;
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x001099B0 File Offset: 0x00107BB0
		public MsgCaution(HudCaution.Type cautionType, ObjBossEventBossParameter bossParam) : base(12317)
		{
			this.m_cautionType = cautionType;
			this.m_bossParam = bossParam;
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x001099CC File Offset: 0x00107BCC
		public MsgCaution(HudCaution.Type cautionType, ItemType itemType) : base(12317)
		{
			this.m_cautionType = cautionType;
			this.m_itemType = itemType;
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x001099E8 File Offset: 0x00107BE8
		public MsgCaution(HudCaution.Type cautionType, int number, int second) : base(12317)
		{
			this.m_cautionType = cautionType;
			this.m_number = number;
			this.m_second = second;
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x00109A18 File Offset: 0x00107C18
		public MsgCaution(HudCaution.Type cautionType, int score, Vector3 position) : base(12317)
		{
			this.m_cautionType = cautionType;
			this.m_number = score;
			this.m_position = position;
		}

		// Token: 0x0400276E RID: 10094
		public readonly HudCaution.Type m_cautionType;

		// Token: 0x0400276F RID: 10095
		public readonly int m_number;

		// Token: 0x04002770 RID: 10096
		public readonly int m_second;

		// Token: 0x04002771 RID: 10097
		public readonly float m_rate;

		// Token: 0x04002772 RID: 10098
		public readonly ItemType m_itemType;

		// Token: 0x04002773 RID: 10099
		public readonly Vector3 m_position;

		// Token: 0x04002774 RID: 10100
		public readonly bool m_flag;

		// Token: 0x04002775 RID: 10101
		public readonly ObjBossEventBossParameter m_bossParam;
	}
}
