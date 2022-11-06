using System;
using UnityEngine;

// Token: 0x02000130 RID: 304
public class ChaoModelPostureController : MonoBehaviour
{
	// Token: 0x06000910 RID: 2320 RVA: 0x000337C8 File Offset: 0x000319C8
	private void Start()
	{
		this.CreateTinyFsm();
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x000337D0 File Offset: 0x000319D0
	private void Update()
	{
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x000337D4 File Offset: 0x000319D4
	private void CreateTinyFsm()
	{
		this.m_fsmBehavior = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		if (this.m_fsmBehavior != null)
		{
			TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
			description.initState = new TinyFsmState(new EventFunction(this.StateIdle));
			this.m_fsmBehavior.SetUp(description);
		}
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x0003383C File Offset: 0x00031A3C
	private void OnDestroy()
	{
		if (this.m_fsmBehavior != null)
		{
			this.m_fsmBehavior.ShutDown();
			this.m_fsmBehavior = null;
		}
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x00033864 File Offset: 0x00031A64
	public void SetModelObject(GameObject modelObject)
	{
		this.m_modelObject = modelObject;
		this.m_initRotaion = this.LocalRotaion;
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x0003387C File Offset: 0x00031A7C
	public void ChangeStateToSpin(Vector3 velocity)
	{
		this.m_velocity = velocity;
		this.ChangeState(new TinyFsmState(new EventFunction(this.StateSpin)));
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x0003389C File Offset: 0x00031A9C
	public void ChangeStateToReturnIdle()
	{
		this.ChangeState(new TinyFsmState(new EventFunction(this.StateReturnToIdle)));
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x000338B8 File Offset: 0x00031AB8
	private void ChangeState(TinyFsmState nextState)
	{
		if (this.m_fsmBehavior != null)
		{
			this.m_fsmBehavior.ChangeState(nextState);
		}
	}

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x06000919 RID: 2329 RVA: 0x00033908 File Offset: 0x00031B08
	// (set) Token: 0x06000918 RID: 2328 RVA: 0x000338D8 File Offset: 0x00031AD8
	private Quaternion LocalRotaion
	{
		get
		{
			if (this.m_modelObject != null)
			{
				return this.m_modelObject.transform.localRotation;
			}
			return Quaternion.identity;
		}
		set
		{
			if (this.m_modelObject != null)
			{
				this.m_modelObject.transform.localRotation = value;
			}
		}
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x00033934 File Offset: 0x00031B34
	private void AddRotation(Quaternion rot)
	{
		this.LocalRotaion *= rot;
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x00033948 File Offset: 0x00031B48
	private TinyFsmState StateIdle(TinyFsmEvent fsmEvent)
	{
		int signal = fsmEvent.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x000339A0 File Offset: 0x00031BA0
	private TinyFsmState StateSpin(TinyFsmEvent fsmEvent)
	{
		int signal = fsmEvent.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
		{
			float getDeltaTime = fsmEvent.GetDeltaTime;
			Quaternion rot = Quaternion.AngleAxis(this.m_velocity.y * getDeltaTime, Vector3.up) * Quaternion.AngleAxis(this.m_velocity.x * getDeltaTime, Vector3.right) * Quaternion.AngleAxis(this.m_velocity.z * getDeltaTime, Vector3.forward);
			this.AddRotation(rot);
			return TinyFsmState.End();
		}
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00033A54 File Offset: 0x00031C54
	private TinyFsmState StateReturnToIdle(TinyFsmEvent fsmEvent)
	{
		int signal = fsmEvent.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (this.m_velocity.sqrMagnitude < 1E-45f)
			{
				this.m_velocity = Vector3.one;
			}
			return TinyFsmState.End();
		case 4:
		{
			float getDeltaTime = fsmEvent.GetDeltaTime;
			Quaternion quaternion = this.LocalRotaion;
			quaternion = Quaternion.RotateTowards(quaternion, this.m_initRotaion, this.m_velocity.magnitude * getDeltaTime);
			this.LocalRotaion = quaternion;
			if (quaternion == this.m_initRotaion)
			{
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
			}
			return TinyFsmState.End();
		}
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x040006BC RID: 1724
	private GameObject m_modelObject;

	// Token: 0x040006BD RID: 1725
	private TinyFsmBehavior m_fsmBehavior;

	// Token: 0x040006BE RID: 1726
	private Vector3 m_velocity = Vector3.zero;

	// Token: 0x040006BF RID: 1727
	private Quaternion m_initRotaion = Quaternion.identity;
}
