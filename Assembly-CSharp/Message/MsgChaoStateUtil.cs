using System;
using UnityEngine;

namespace Message
{
	// Token: 0x020005A2 RID: 1442
	public class MsgChaoStateUtil
	{
		// Token: 0x06002B10 RID: 11024 RVA: 0x00109B48 File Offset: 0x00107D48
		public static void SendMsgChaoState(MsgChaoState.State state)
		{
			MsgChaoState msgChaoState = new MsgChaoState(state);
			if (msgChaoState != null)
			{
				GameObjectUtil.SendMessageToTagObjects("Chao", "OnMsgReceive", msgChaoState, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
