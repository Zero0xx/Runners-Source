using System;
using Message;

// Token: 0x02000A45 RID: 2629
public abstract class FSMState<Context>
{
	// Token: 0x0600461A RID: 17946 RVA: 0x0016D224 File Offset: 0x0016B424
	public virtual void Enter(Context context)
	{
	}

	// Token: 0x0600461B RID: 17947 RVA: 0x0016D228 File Offset: 0x0016B428
	public virtual void Leave(Context context)
	{
	}

	// Token: 0x0600461C RID: 17948 RVA: 0x0016D22C File Offset: 0x0016B42C
	public virtual void Step(Context context, float deltatTime)
	{
	}

	// Token: 0x0600461D RID: 17949 RVA: 0x0016D230 File Offset: 0x0016B430
	public virtual void OnGUI(Context context)
	{
	}

	// Token: 0x0600461E RID: 17950 RVA: 0x0016D234 File Offset: 0x0016B434
	public virtual bool DispatchMessage(Context context, int messageId, MessageBase msg)
	{
		return false;
	}
}
