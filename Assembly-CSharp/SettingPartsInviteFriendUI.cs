using System;
using Message;
using UnityEngine;

// Token: 0x02000525 RID: 1317
public class SettingPartsInviteFriendUI : SettingBase
{
	// Token: 0x060028B9 RID: 10425 RVA: 0x000FC0D0 File Offset: 0x000FA2D0
	protected override void OnSetup(string anthorPath)
	{
	}

	// Token: 0x060028BA RID: 10426 RVA: 0x000FC0D4 File Offset: 0x000FA2D4
	protected override void OnPlayStart()
	{
		GameObject gameObject = GameObject.Find("SocialInterface");
		if (gameObject == null)
		{
			return;
		}
		SocialInterface component = gameObject.GetComponent<SocialInterface>();
		if (component == null)
		{
			return;
		}
		component.InviteFriend(base.gameObject);
	}

	// Token: 0x060028BB RID: 10427 RVA: 0x000FC11C File Offset: 0x000FA31C
	protected override bool OnIsEndPlay()
	{
		return this.m_isEnd;
	}

	// Token: 0x060028BC RID: 10428 RVA: 0x000FC124 File Offset: 0x000FA324
	protected override void OnUpdate()
	{
	}

	// Token: 0x060028BD RID: 10429 RVA: 0x000FC128 File Offset: 0x000FA328
	private void InviteFriendEndCallback(MsgSocialNormalResponse msg)
	{
		this.m_isEnd = true;
	}

	// Token: 0x0400242B RID: 9259
	private bool m_isEnd;
}
