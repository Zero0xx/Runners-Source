using System;
using Message;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000165 RID: 357
	public class StageChao : MonoBehaviour
	{
		// Token: 0x06000A55 RID: 2645 RVA: 0x0003DD00 File Offset: 0x0003BF00
		private void Start()
		{
			base.enabled = false;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0003DD0C File Offset: 0x0003BF0C
		private void OnMsgExitStage(MsgExitStage msg)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
