using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002C4 RID: 708
[AddComponentMenu("Scripts/Runners/Game/TinyFsm")]
public class TinyFsm
{
	// Token: 0x06001431 RID: 5169 RVA: 0x0006CB3C File Offset: 0x0006AD3C
	public TinyFsm()
	{
		this.m_src = TinyFsmState.Top();
		this.m_hierarchical = false;
	}

	// Token: 0x06001432 RID: 5170 RVA: 0x0006CB58 File Offset: 0x0006AD58
	public void Initialize(MonoBehaviour context, TinyFsmState state, bool hierarchical)
	{
		this.m_hierarchical = hierarchical;
		if (this.m_hierarchical)
		{
			List<TinyFsmState> list = new List<TinyFsmState>();
			TinyFsmState tinyFsmState = state;
			while (!tinyFsmState.IsTop())
			{
				list.Add(tinyFsmState);
				tinyFsmState = TinyFsm.Super(context, tinyFsmState);
			}
			list.Reverse();
			foreach (TinyFsmState state2 in list)
			{
				TinyFsm.Enter(state2);
			}
			this.m_cur = state;
		}
		else
		{
			TinyFsm.Enter(state);
			this.m_cur = state;
		}
	}

	// Token: 0x06001433 RID: 5171 RVA: 0x0006CC10 File Offset: 0x0006AE10
	public void Shutdown(MonoBehaviour context)
	{
		if (this.m_hierarchical)
		{
			TinyFsmState tinyFsmState = this.m_cur;
			while (!tinyFsmState.IsTop())
			{
				TinyFsm.Leave(tinyFsmState);
				tinyFsmState = TinyFsm.Super(context, tinyFsmState);
			}
			this.m_cur.Clear();
			this.m_src.Clear();
		}
		else
		{
			TinyFsm.Leave(this.m_cur);
			this.m_cur.Clear();
		}
	}

	// Token: 0x06001434 RID: 5172 RVA: 0x0006CC80 File Offset: 0x0006AE80
	public void Dispatch(MonoBehaviour context, TinyFsmEvent e)
	{
		if (this.m_hierarchical)
		{
			this.m_src = this.m_cur;
			while (!this.m_src.IsTop() && !this.m_src.IsEnd())
			{
				TinyFsmState tinyFsmState = TinyFsm.Trigger(context, this.m_src, e);
				if (tinyFsmState.IsEnd() || !tinyFsmState.IsValid())
				{
					return;
				}
				this.m_src = TinyFsm.Super(context, this.m_src);
			}
		}
		else
		{
			this.m_cur.Call(e);
		}
	}

	// Token: 0x06001435 RID: 5173 RVA: 0x0006CD14 File Offset: 0x0006AF14
	public bool ChangeState(MonoBehaviour context, TinyFsmState state)
	{
		if (this.m_cur == state)
		{
			return false;
		}
		if (this.m_hierarchical)
		{
			for (TinyFsmState tinyFsmState = this.m_cur; tinyFsmState != this.m_src; tinyFsmState = TinyFsm.Super(context, tinyFsmState))
			{
				TinyFsm.Leave(tinyFsmState);
			}
			if (this.m_src == state)
			{
				TinyFsm.Leave(this.m_src);
				TinyFsm.Enter(state);
			}
			else
			{
				List<TinyFsmState> list = new List<TinyFsmState>();
				TinyFsmState tinyFsmState2 = this.m_src;
				while (!tinyFsmState2.IsTop())
				{
					list.Add(tinyFsmState2);
					tinyFsmState2 = TinyFsm.Super(context, tinyFsmState2);
				}
				List<TinyFsmState> list2 = new List<TinyFsmState>();
				TinyFsmState tinyFsmState3 = state;
				while (!tinyFsmState3.IsTop())
				{
					list2.Add(tinyFsmState3);
					tinyFsmState3 = TinyFsm.Super(context, tinyFsmState3);
				}
				list.Reverse();
				list2.Reverse();
				IEnumerator<TinyFsmState> enumerator = list.GetEnumerator();
				IEnumerator<TinyFsmState> enumerator2 = list2.GetEnumerator();
				while (enumerator.Current != list[0] && enumerator2.Current != list2[0] && enumerator.Current == enumerator2.Current)
				{
					enumerator.MoveNext();
					enumerator2.MoveNext();
				}
				list.Reverse();
				foreach (TinyFsmState tinyFsmState4 in list)
				{
					TinyFsm.Leave(tinyFsmState4);
					if (tinyFsmState4 == enumerator.Current)
					{
						break;
					}
				}
				while (enumerator2.Current != list2[0])
				{
					TinyFsm.Enter(enumerator2.Current);
					enumerator2.MoveNext();
				}
				this.m_cur = state;
			}
		}
		else
		{
			TinyFsm.Leave(this.m_cur);
			TinyFsm.Enter(state);
			this.m_cur = state;
		}
		return true;
	}

	// Token: 0x06001436 RID: 5174 RVA: 0x0006CF10 File Offset: 0x0006B110
	public TinyFsmState GetCurrentState()
	{
		return this.m_cur;
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x0006CF18 File Offset: 0x0006B118
	private static TinyFsmState Trigger(MonoBehaviour context, TinyFsmState state, TinyFsmEvent e)
	{
		return state.Call(e);
	}

	// Token: 0x06001438 RID: 5176 RVA: 0x0006CF24 File Offset: 0x0006B124
	private static TinyFsmState Super(MonoBehaviour context, TinyFsmState state)
	{
		return TinyFsm.Trigger(context, state, TinyFsmEvent.CreateSuper());
	}

	// Token: 0x06001439 RID: 5177 RVA: 0x0006CF34 File Offset: 0x0006B134
	private static void Init(TinyFsmState state)
	{
		state.Call(TinyFsmEvent.CreateInit());
	}

	// Token: 0x0600143A RID: 5178 RVA: 0x0006CF44 File Offset: 0x0006B144
	private static void Enter(TinyFsmState state)
	{
		state.Call(TinyFsmEvent.CreateEnter());
	}

	// Token: 0x0600143B RID: 5179 RVA: 0x0006CF54 File Offset: 0x0006B154
	private static void Leave(TinyFsmState state)
	{
		state.Call(TinyFsmEvent.CreateLeave());
	}

	// Token: 0x04001194 RID: 4500
	private TinyFsmState m_cur;

	// Token: 0x04001195 RID: 4501
	private TinyFsmState m_src;

	// Token: 0x04001196 RID: 4502
	private bool m_hierarchical;
}
