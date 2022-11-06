using System;
using App;
using App.Utility;
using UnityEngine;

// Token: 0x02000172 RID: 370
[AddComponentMenu("Scripts/Runners/Game/Components/TinyFSM")]
public class TinyFsmBehavior : MonoBehaviour
{
	// Token: 0x06000A85 RID: 2693 RVA: 0x0003F040 File Offset: 0x0003D240
	public void SetUp(TinyFsmBehavior.Description desc)
	{
		if (this.m_fsm == null)
		{
			this.m_fsm = new TinyFsm();
			this.m_controlBehavior = desc.controlBehavior;
			this.m_fsm.Initialize(this.m_controlBehavior, desc.initState, desc.hierarchical);
			this.m_statusFlags.Set(4, desc.onFixedUpdate);
			this.m_statusFlags.Set(5, desc.ignoreDeltaTime);
			this.m_statusFlags.Set(0, true);
			this.m_statusFlags.Set(1, true);
		}
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0003F0D0 File Offset: 0x0003D2D0
	public void ShutDown()
	{
		this.m_statusFlags.Set(6, true);
		if (this.m_controlBehavior != null && this.m_fsm != null)
		{
			this.m_fsm.Shutdown(this.m_controlBehavior);
		}
		this.m_fsm = null;
		this.m_controlBehavior = null;
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x0003F128 File Offset: 0x0003D328
	private void Update()
	{
		if (App.Math.NearZero(Time.deltaTime, 1E-06f) && !this.m_statusFlags.Test(5))
		{
			return;
		}
		if (this.m_statusFlags.Test(4))
		{
			return;
		}
		this.UpdateImpl(Time.deltaTime);
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x0003F178 File Offset: 0x0003D378
	private void FixedUpdate()
	{
		if (!this.m_statusFlags.Test(4))
		{
			return;
		}
		this.UpdateImpl(Time.deltaTime);
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0003F198 File Offset: 0x0003D398
	private void OnDestroy()
	{
		this.ShutDown();
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x0003F1A0 File Offset: 0x0003D3A0
	public bool ChangeState(TinyFsmState state)
	{
		if (this.m_fsm == null || this.m_controlBehavior == null)
		{
			return false;
		}
		bool result = false;
		if (!this.m_statusFlags.Test(2))
		{
			this.m_statusFlags.Set(2);
			result = this.m_fsm.ChangeState(this.m_controlBehavior, state);
			this.m_statusFlags.Reset(2);
		}
		return result;
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x0003F214 File Offset: 0x0003D414
	public TinyFsmState GetCurrentState()
	{
		if (this.m_fsm != null)
		{
			return this.m_fsm.GetCurrentState();
		}
		return TinyFsmState.Top();
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x0003F234 File Offset: 0x0003D434
	private void SetEnableUpdate(bool isEnableUpdate)
	{
		this.m_statusFlags.Set(1, isEnableUpdate);
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x0003F244 File Offset: 0x0003D444
	private void UpdateImpl(float deltaTime)
	{
		if (this.m_fsm == null || this.m_controlBehavior == null)
		{
			return;
		}
		if (this.m_statusFlags.Test(1) && this.m_statusFlags.Test(0))
		{
			this.m_fsm.Dispatch(this.m_controlBehavior, TinyFsmEvent.CreateUpdate(deltaTime));
		}
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x0003F2A8 File Offset: 0x0003D4A8
	public void Dispatch(TinyFsmEvent signal)
	{
		if (this.m_fsm == null || this.m_controlBehavior == null)
		{
			return;
		}
		if (signal.Signal < 1)
		{
			global::Debug.Log("Cannot Dispatch Signal ID is not for User.\n");
		}
		this.m_fsm.Dispatch(this.m_controlBehavior, signal);
	}

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0003F2FC File Offset: 0x0003D4FC
	public bool NowShutDown
	{
		get
		{
			return this.m_statusFlags.Test(6);
		}
	}

	// Token: 0x04000872 RID: 2162
	private TinyFsm m_fsm;

	// Token: 0x04000873 RID: 2163
	private MonoBehaviour m_controlBehavior;

	// Token: 0x04000874 RID: 2164
	private Bitset32 m_statusFlags;

	// Token: 0x02000173 RID: 371
	public class Description
	{
		// Token: 0x06000A90 RID: 2704 RVA: 0x0003F30C File Offset: 0x0003D50C
		public Description(MonoBehaviour control)
		{
			this.controlBehavior = control;
			this.initState = TinyFsmState.Top();
			this.hierarchical = false;
			this.onFixedUpdate = false;
			this.ignoreDeltaTime = false;
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0003F348 File Offset: 0x0003D548
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x0003F350 File Offset: 0x0003D550
		public TinyFsmState initState { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0003F35C File Offset: 0x0003D55C
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x0003F364 File Offset: 0x0003D564
		public bool hierarchical { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0003F370 File Offset: 0x0003D570
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x0003F378 File Offset: 0x0003D578
		public bool onFixedUpdate { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0003F384 File Offset: 0x0003D584
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x0003F38C File Offset: 0x0003D58C
		public bool ignoreDeltaTime { get; set; }

		// Token: 0x04000875 RID: 2165
		public MonoBehaviour controlBehavior;
	}

	// Token: 0x02000174 RID: 372
	private enum StatusFlag
	{
		// Token: 0x0400087B RID: 2171
		STATUS_END_SETUP,
		// Token: 0x0400087C RID: 2172
		STATUS_ENABLE_UPDATE,
		// Token: 0x0400087D RID: 2173
		STATUS_ON_CHANGE_STATE,
		// Token: 0x0400087E RID: 2174
		STATUS_HIERARCHICAL,
		// Token: 0x0400087F RID: 2175
		STATUS_FIXEDUPDATE,
		// Token: 0x04000880 RID: 2176
		STATUS_IGNORE_DELTATIME,
		// Token: 0x04000881 RID: 2177
		STATUS_SHUTDOWN
	}
}
