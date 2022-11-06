using System;

// Token: 0x02000A46 RID: 2630
public class FSMStateFactory<Context>
{
	// Token: 0x0600461F RID: 17951 RVA: 0x0016D238 File Offset: 0x0016B438
	public FSMStateFactory(int id, FSMState<Context> st)
	{
		this.stateID = id;
		this.state = st;
	}

	// Token: 0x04003AC1 RID: 15041
	public int stateID;

	// Token: 0x04003AC2 RID: 15042
	public FSMState<Context> state;
}
