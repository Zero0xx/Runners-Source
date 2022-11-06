using System;
using UnityEngine;

namespace Message
{
	// Token: 0x02000583 RID: 1411
	public class MsgActivateBlock : MessageBase
	{
		// Token: 0x06002AE8 RID: 10984 RVA: 0x00109704 File Offset: 0x00107904
		public MsgActivateBlock(string stageName, int block, int layer, int activateID, Vector3 originPosition, Quaternion originrotation) : base(12299)
		{
			this.m_stageName = stageName;
			this.m_blockNo = block;
			this.m_layerNo = layer;
			this.m_activateID = activateID;
			this.m_originPosition = originPosition;
			this.m_originRotation = originrotation;
			this.m_replaceStage = false;
		}

		// Token: 0x04002740 RID: 10048
		public string m_stageName;

		// Token: 0x04002741 RID: 10049
		public int m_blockNo;

		// Token: 0x04002742 RID: 10050
		public int m_layerNo;

		// Token: 0x04002743 RID: 10051
		public int m_activateID;

		// Token: 0x04002744 RID: 10052
		public bool m_replaceStage;

		// Token: 0x04002745 RID: 10053
		public Vector3 m_originPosition;

		// Token: 0x04002746 RID: 10054
		public Quaternion m_originRotation;

		// Token: 0x04002747 RID: 10055
		public MsgActivateBlock.CheckPoint m_checkPoint;

		// Token: 0x02000584 RID: 1412
		public enum CheckPoint
		{
			// Token: 0x04002749 RID: 10057
			None,
			// Token: 0x0400274A RID: 10058
			First,
			// Token: 0x0400274B RID: 10059
			Internal
		}
	}
}
