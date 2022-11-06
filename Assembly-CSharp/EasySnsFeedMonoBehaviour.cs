using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020009EE RID: 2542
internal class EasySnsFeedMonoBehaviour : MonoBehaviour
{
	// Token: 0x0600431B RID: 17179 RVA: 0x0015C8B4 File Offset: 0x0015AAB4
	public void Init()
	{
		this.m_isFeeded = null;
		this.m_feedIncentiveList = null;
	}

	// Token: 0x0600431C RID: 17180 RVA: 0x0015C8D8 File Offset: 0x0015AAD8
	private void FeedEndCallback(MsgSocialNormalResponse msg)
	{
		this.m_isFeeded = new bool?(!msg.m_result.IsError);
	}

	// Token: 0x0600431D RID: 17181 RVA: 0x0015C8F4 File Offset: 0x0015AAF4
	private void ServerGetFacebookIncentive_Succeeded(MsgGetNormalIncentiveSucceed msg)
	{
		this.m_feedIncentiveList = msg.m_incentive;
	}

	// Token: 0x0600431E RID: 17182 RVA: 0x0015C904 File Offset: 0x0015AB04
	private void ServerGetFacebookIncentive_Failed(MsgServerConnctFailed mag)
	{
		this.m_feedIncentiveList = new List<ServerPresentState>();
	}

	// Token: 0x040038E2 RID: 14562
	public bool? m_isFeeded;

	// Token: 0x040038E3 RID: 14563
	public List<ServerPresentState> m_feedIncentiveList;
}
