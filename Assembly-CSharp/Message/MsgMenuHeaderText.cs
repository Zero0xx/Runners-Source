using System;

namespace Message
{
	// Token: 0x020005C3 RID: 1475
	public class MsgMenuHeaderText : MessageBase
	{
		// Token: 0x06002B34 RID: 11060 RVA: 0x00109E98 File Offset: 0x00108098
		public MsgMenuHeaderText(string cellName) : base(57344)
		{
			this.m_cellName = cellName;
		}

		// Token: 0x040027E1 RID: 10209
		public string m_cellName = string.Empty;
	}
}
