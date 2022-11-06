using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AA RID: 170
[Serializable]
public class EventDelegate
{
	// Token: 0x06000464 RID: 1124 RVA: 0x00016574 File Offset: 0x00014774
	public EventDelegate()
	{
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x0001657C File Offset: 0x0001477C
	public EventDelegate(EventDelegate.Callback call)
	{
		this.Set(call);
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x0001658C File Offset: 0x0001478C
	public EventDelegate(MonoBehaviour target, string methodName)
	{
		this.Set(target, methodName);
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x06000468 RID: 1128 RVA: 0x000165B0 File Offset: 0x000147B0
	// (set) Token: 0x06000469 RID: 1129 RVA: 0x000165B8 File Offset: 0x000147B8
	public MonoBehaviour target
	{
		get
		{
			return this.mTarget;
		}
		set
		{
			this.mTarget = value;
			this.mCachedCallback = null;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x0600046A RID: 1130 RVA: 0x000165C8 File Offset: 0x000147C8
	// (set) Token: 0x0600046B RID: 1131 RVA: 0x000165D0 File Offset: 0x000147D0
	public string methodName
	{
		get
		{
			return this.mMethodName;
		}
		set
		{
			this.mMethodName = value;
			this.mCachedCallback = null;
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x0600046C RID: 1132 RVA: 0x000165E0 File Offset: 0x000147E0
	public bool isValid
	{
		get
		{
			return this.mTarget != null && !string.IsNullOrEmpty(this.mMethodName);
		}
	}

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x0600046D RID: 1133 RVA: 0x00016610 File Offset: 0x00014810
	public bool isEnabled
	{
		get
		{
			return this.mTarget != null && this.mTarget.enabled;
		}
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00016634 File Offset: 0x00014834
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return !this.isValid;
		}
		if (obj is EventDelegate.Callback)
		{
			EventDelegate.Callback callback = obj as EventDelegate.Callback;
			return this.mTarget == callback.Target && string.Equals(this.mMethodName, callback.Method.Name);
		}
		if (obj is EventDelegate)
		{
			EventDelegate eventDelegate = obj as EventDelegate;
			return this.mTarget == eventDelegate.mTarget && string.Equals(this.mMethodName, eventDelegate.mMethodName);
		}
		return false;
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x000166CC File Offset: 0x000148CC
	public override int GetHashCode()
	{
		return EventDelegate.s_Hash;
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x000166D4 File Offset: 0x000148D4
	private EventDelegate.Callback Get()
	{
		if (this.mCachedCallback == null || this.mCachedCallback.Target != this.mTarget || this.mCachedCallback.Method.Name != this.mMethodName)
		{
			if (!(this.mTarget != null) || string.IsNullOrEmpty(this.mMethodName))
			{
				return null;
			}
			this.mCachedCallback = (EventDelegate.Callback)Delegate.CreateDelegate(typeof(EventDelegate.Callback), this.mTarget, this.mMethodName);
		}
		return this.mCachedCallback;
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00016778 File Offset: 0x00014978
	private void Set(EventDelegate.Callback call)
	{
		if (call == null || call.Method == null)
		{
			this.mTarget = null;
			this.mMethodName = null;
			this.mCachedCallback = null;
		}
		else
		{
			this.mTarget = (call.Target as MonoBehaviour);
			this.mMethodName = call.Method.Name;
		}
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x000167D4 File Offset: 0x000149D4
	public void Set(MonoBehaviour target, string methodName)
	{
		this.mTarget = target;
		this.mMethodName = methodName;
		this.mCachedCallback = null;
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x000167EC File Offset: 0x000149EC
	public bool Execute()
	{
		EventDelegate.Callback callback = this.Get();
		if (callback != null)
		{
			callback();
			return true;
		}
		return false;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x00016810 File Offset: 0x00014A10
	public override string ToString()
	{
		if (this.mTarget != null && !string.IsNullOrEmpty(this.methodName))
		{
			string text = this.mTarget.GetType().ToString();
			int num = text.LastIndexOf('.');
			if (num > 0)
			{
				text = text.Substring(num + 1);
			}
			return text + "." + this.methodName;
		}
		return null;
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x0001687C File Offset: 0x00014A7C
	public static void Execute(List<EventDelegate> list)
	{
		if (list != null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null)
				{
					eventDelegate.Execute();
					if (eventDelegate.oneShot)
					{
						list.RemoveAt(i);
						continue;
					}
				}
			}
		}
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x000168D4 File Offset: 0x00014AD4
	public static bool IsValid(List<EventDelegate> list)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.isValid)
				{
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x0001691C File Offset: 0x00014B1C
	public static void Set(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		if (list != null)
		{
			list.Clear();
			list.Add(new EventDelegate(callback));
		}
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x00016938 File Offset: 0x00014B38
	public static void Add(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		EventDelegate.Add(list, callback, false);
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x00016944 File Offset: 0x00014B44
	public static void Add(List<EventDelegate> list, EventDelegate.Callback callback, bool oneShot)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(callback))
				{
					return;
				}
				i++;
			}
			list.Add(new EventDelegate(callback)
			{
				oneShot = oneShot
			});
		}
		else
		{
			global::Debug.LogWarning("Attempting to add a callback to a list that's null");
		}
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x000169B0 File Offset: 0x00014BB0
	public static void Add(List<EventDelegate> list, EventDelegate ev)
	{
		EventDelegate.Add(list, ev, false);
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x000169BC File Offset: 0x00014BBC
	public static void Add(List<EventDelegate> list, EventDelegate ev, bool oneShot)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(ev))
				{
					return;
				}
				i++;
			}
			list.Add(new EventDelegate(ev.target, ev.methodName)
			{
				oneShot = oneShot
			});
		}
		else
		{
			global::Debug.LogWarning("Attempting to add a callback to a list that's null");
		}
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x00016A34 File Offset: 0x00014C34
	public static bool Remove(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(callback))
				{
					list.RemoveAt(i);
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x04000349 RID: 841
	[SerializeField]
	private MonoBehaviour mTarget;

	// Token: 0x0400034A RID: 842
	[SerializeField]
	private string mMethodName;

	// Token: 0x0400034B RID: 843
	public bool oneShot;

	// Token: 0x0400034C RID: 844
	private EventDelegate.Callback mCachedCallback;

	// Token: 0x0400034D RID: 845
	private static int s_Hash = "EventDelegate".GetHashCode();

	// Token: 0x02000A67 RID: 2663
	// (Invoke) Token: 0x060047D6 RID: 18390
	public delegate void Callback();
}
