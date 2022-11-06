using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F7 RID: 503
public class UIDebugMenuTask : MonoBehaviour
{
	// Token: 0x06000D64 RID: 3428 RVA: 0x0004F078 File Offset: 0x0004D278
	private void Start()
	{
		this.m_childList = new Dictionary<string, UIDebugMenuTask>();
		this.m_isActive = false;
		this.OnStartFromTask();
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x0004F094 File Offset: 0x0004D294
	private void OnGUI()
	{
		if (this.IsActive())
		{
			this.OnGuiFromTask();
		}
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x0004F0A8 File Offset: 0x0004D2A8
	public bool IsActive()
	{
		return this.m_isActive;
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x0004F0B0 File Offset: 0x0004D2B0
	public void AddChild(string childName, GameObject child)
	{
		if (this.m_childList == null)
		{
			return;
		}
		if (child == null)
		{
			return;
		}
		UIDebugMenuTask component = child.GetComponent<UIDebugMenuTask>();
		this.AddChild(childName, component);
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x0004F0E8 File Offset: 0x0004D2E8
	public void AddChild(string childName, UIDebugMenuTask child)
	{
		if (this.m_childList == null)
		{
			return;
		}
		if (child == null)
		{
			return;
		}
		child.SetParent(this);
		this.m_childList.Add(childName, child);
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x0004F118 File Offset: 0x0004D318
	public void TransitionFrom()
	{
		global::Debug.Log(string.Format("Transition From:{0}", this.ToString()));
		this.m_isActive = true;
		this.OnTransitionFrom();
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x0004F148 File Offset: 0x0004D348
	public void TransitionToParent()
	{
		if (this.m_parent != null)
		{
			this.m_parent.TransitionFrom();
			this.TransitionTo();
		}
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x0004F178 File Offset: 0x0004D378
	public void TransitionToChild(string childName)
	{
		if (this.m_childList.ContainsKey(childName))
		{
			UIDebugMenuTask uidebugMenuTask = this.m_childList[childName];
			uidebugMenuTask.TransitionFrom();
			this.TransitionTo();
			this.m_isActive = false;
			global::Debug.Log(string.Format("Transition to ChildMenu:{0}", childName));
		}
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x0004F1C8 File Offset: 0x0004D3C8
	protected virtual void OnStartFromTask()
	{
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x0004F1CC File Offset: 0x0004D3CC
	protected virtual void OnGuiFromTask()
	{
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x0004F1D0 File Offset: 0x0004D3D0
	protected virtual void OnTransitionFrom()
	{
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x0004F1D4 File Offset: 0x0004D3D4
	protected virtual void OnTransitionTo()
	{
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x0004F1D8 File Offset: 0x0004D3D8
	private void SetParent(UIDebugMenuTask parent)
	{
		this.m_parent = parent;
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x0004F1E4 File Offset: 0x0004D3E4
	private void TransitionTo()
	{
		this.m_isActive = false;
		global::Debug.Log(string.Format("Transition To:{0}", this.ToString()));
		this.OnTransitionTo();
	}

	// Token: 0x04000B3D RID: 2877
	private UIDebugMenuTask m_parent;

	// Token: 0x04000B3E RID: 2878
	private Dictionary<string, UIDebugMenuTask> m_childList;

	// Token: 0x04000B3F RID: 2879
	private bool m_isActive;
}
