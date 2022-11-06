using System;
using App.Utility;
using UnityEngine;

// Token: 0x020003D8 RID: 984
public abstract class WindowBase : MonoBehaviour
{
	// Token: 0x06001C91 RID: 7313 RVA: 0x000A99EC File Offset: 0x000A7BEC
	public void Destroy()
	{
		this.RemoveBackKeyCallBack();
	}

	// Token: 0x06001C92 RID: 7314 RVA: 0x000A99F4 File Offset: 0x000A7BF4
	private void Awake()
	{
		this.EntryBackKeyCallBack();
	}

	// Token: 0x06001C93 RID: 7315
	public abstract void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg);

	// Token: 0x06001C94 RID: 7316 RVA: 0x000A99FC File Offset: 0x000A7BFC
	public void EntryBackKeyCallBack()
	{
		BackKeyManager.AddWindowCallBack(base.gameObject);
	}

	// Token: 0x06001C95 RID: 7317 RVA: 0x000A9A0C File Offset: 0x000A7C0C
	public void RemoveBackKeyCallBack()
	{
		BackKeyManager.RemoveWindowCallBack(base.gameObject);
	}

	// Token: 0x020003D9 RID: 985
	public class BackButtonMessage
	{
		// Token: 0x06001C97 RID: 7319 RVA: 0x000A9A24 File Offset: 0x000A7C24
		public void StaySequence()
		{
			this.SetFlag(WindowBase.BackButtonMessage.Flags.STAY_SEQUENCE, true);
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x000A9A30 File Offset: 0x000A7C30
		public bool IsFlag(WindowBase.BackButtonMessage.Flags flag)
		{
			return this.m_flags.Test((int)flag);
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x000A9A40 File Offset: 0x000A7C40
		private void SetFlag(WindowBase.BackButtonMessage.Flags flag, bool value)
		{
			this.m_flags.Set((int)flag, value);
		}

		// Token: 0x04001A72 RID: 6770
		private Bitset32 m_flags;

		// Token: 0x020003DA RID: 986
		public enum Flags
		{
			// Token: 0x04001A74 RID: 6772
			STAY_SEQUENCE
		}
	}
}
