using System;
using System.Collections.Generic;

// Token: 0x02000A47 RID: 2631
public class FSMSystem<Context>
{
	// Token: 0x06004620 RID: 17952 RVA: 0x0016D250 File Offset: 0x0016B450
	public FSMSystem()
	{
		this.m_states = new List<FSMStateFactory<Context>>();
	}

	// Token: 0x17000979 RID: 2425
	// (get) Token: 0x06004621 RID: 17953 RVA: 0x0016D264 File Offset: 0x0016B464
	public StateID CurrentStateID
	{
		get
		{
			return (StateID)this.m_currentStateID;
		}
	}

	// Token: 0x1700097A RID: 2426
	// (get) Token: 0x06004622 RID: 17954 RVA: 0x0016D26C File Offset: 0x0016B46C
	public FSMState<Context> CurrentState
	{
		get
		{
			return this.m_currentState;
		}
	}

	// Token: 0x06004623 RID: 17955 RVA: 0x0016D274 File Offset: 0x0016B474
	public void AddState(int stateID, FSMState<Context> s)
	{
		FSMStateFactory<Context> stateFactory = new FSMStateFactory<Context>(stateID, s);
		this.AddState(stateFactory);
	}

	// Token: 0x06004624 RID: 17956 RVA: 0x0016D290 File Offset: 0x0016B490
	public void AddState(FSMStateFactory<Context> stateFactory)
	{
		if (this.CurrentState != null)
		{
			Debug.LogError("FSM ERROR: Impossible to add state " + stateFactory.stateID.ToString() + ". State is already Initialized");
		}
		if (stateFactory.state == null)
		{
			Debug.LogError("FSM ERROR: Null reference is not allowed");
		}
		if (this.GetStateFactory(stateFactory.stateID) != null)
		{
			Debug.LogError("FSM ERROR: Impossible to add state " + stateFactory.stateID.ToString() + " because state has already been added");
			return;
		}
		this.m_states.Add(stateFactory);
	}

	// Token: 0x06004625 RID: 17957 RVA: 0x0016D31C File Offset: 0x0016B51C
	public void Init(Context context, int id)
	{
		FSMStateFactory<Context> stateFactory = this.GetStateFactory(id);
		if (stateFactory != null)
		{
			this.m_currentState = stateFactory.state;
			this.m_currentStateID = stateFactory.stateID;
			this.m_currentState.Enter(context);
		}
		else
		{
			Debug.LogError("FSM ERROR: Impossible to Init " + id.ToString() + ". State is not Found");
		}
	}

	// Token: 0x06004626 RID: 17958 RVA: 0x0016D37C File Offset: 0x0016B57C
	private FSMStateFactory<Context> GetStateFactory(int id)
	{
		for (int i = 0; i < this.m_states.Count; i++)
		{
			if (this.m_states[i].stateID == id)
			{
				return this.m_states[i];
			}
		}
		return null;
	}

	// Token: 0x06004627 RID: 17959 RVA: 0x0016D3CC File Offset: 0x0016B5CC
	private FSMState<Context> GetState(int id)
	{
		for (int i = 0; i < this.m_states.Count; i++)
		{
			if (this.m_states[i].stateID == id)
			{
				return this.m_states[i].state;
			}
		}
		return null;
	}

	// Token: 0x06004628 RID: 17960 RVA: 0x0016D420 File Offset: 0x0016B620
	public void ReplaceState(int stateID, FSMState<Context> s)
	{
		if (this.CurrentState != null)
		{
			Debug.LogError("FSM ERROR: Impossible to replace state " + stateID.ToString() + ". State is already Initialized");
		}
		if (s == null)
		{
			Debug.LogError("FSM ERROR: Null reference is not allowed");
		}
		FSMStateFactory<Context> stateFactory = this.GetStateFactory(stateID);
		if (stateFactory != null)
		{
			stateFactory.state = s;
		}
		else
		{
			FSMStateFactory<Context> item = new FSMStateFactory<Context>(stateID, s);
			this.m_states.Add(item);
		}
	}

	// Token: 0x06004629 RID: 17961 RVA: 0x0016D494 File Offset: 0x0016B694
	public void ChangeState(Context context, int stateID)
	{
		FSMStateFactory<Context> stateFactory = this.GetStateFactory(stateID);
		if (stateFactory != null)
		{
			this.m_currentState.Leave(context);
			this.m_currentState = stateFactory.state;
			this.m_currentStateID = stateFactory.stateID;
			this.m_currentState.Enter(context);
		}
	}

	// Token: 0x04003AC3 RID: 15043
	private List<FSMStateFactory<Context>> m_states;

	// Token: 0x04003AC4 RID: 15044
	private int m_currentStateID;

	// Token: 0x04003AC5 RID: 15045
	private FSMState<Context> m_currentState;
}
