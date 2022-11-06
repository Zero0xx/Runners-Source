using System;

// Token: 0x020002C9 RID: 713
public class TinyFsmState
{
	// Token: 0x06001451 RID: 5201 RVA: 0x0006D0A4 File Offset: 0x0006B2A4
	public TinyFsmState(EventFunction delegator)
	{
		this.m_identifier = 1U;
		this.m_delegate = delegator;
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x0006D0BC File Offset: 0x0006B2BC
	public TinyFsmState(int identifier)
	{
		this.m_identifier = (uint)identifier;
	}

	// Token: 0x06001453 RID: 5203 RVA: 0x0006D0CC File Offset: 0x0006B2CC
	public static TinyFsmState Top()
	{
		return new TinyFsmState(2);
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x0006D0D4 File Offset: 0x0006B2D4
	public static TinyFsmState End()
	{
		return new TinyFsmState(3);
	}

	// Token: 0x06001455 RID: 5205 RVA: 0x0006D0DC File Offset: 0x0006B2DC
	public bool IsValid()
	{
		return this.m_identifier != 0U;
	}

	// Token: 0x06001456 RID: 5206 RVA: 0x0006D0EC File Offset: 0x0006B2EC
	public bool IsTop()
	{
		return this.m_identifier == 2U;
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x0006D0F8 File Offset: 0x0006B2F8
	public bool IsEnd()
	{
		return this.m_identifier == 3U;
	}

	// Token: 0x06001458 RID: 5208 RVA: 0x0006D104 File Offset: 0x0006B304
	public void Clear()
	{
		this.m_delegate = null;
	}

	// Token: 0x06001459 RID: 5209 RVA: 0x0006D110 File Offset: 0x0006B310
	public TinyFsmState Call(TinyFsmEvent e)
	{
		if (this.m_delegate != null)
		{
			return this.m_delegate(e);
		}
		return null;
	}

	// Token: 0x040011AB RID: 4523
	private const int IDENTIFIER_INVALID = 0;

	// Token: 0x040011AC RID: 4524
	private const int IDENTIFIER_VALID = 1;

	// Token: 0x040011AD RID: 4525
	private const int IDENTIFIER_TOP = 2;

	// Token: 0x040011AE RID: 4526
	private const int IDENTIFIER_END = 3;

	// Token: 0x040011AF RID: 4527
	private uint m_identifier;

	// Token: 0x040011B0 RID: 4528
	private EventFunction m_delegate;
}
