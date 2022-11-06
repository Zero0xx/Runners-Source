using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006F RID: 111
[AddComponentMenu("NGUI/Internal/Update Manager")]
[ExecuteInEditMode]
public class UpdateManager : MonoBehaviour
{
	// Token: 0x060002FB RID: 763 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
	private static int Compare(UpdateManager.UpdateEntry a, UpdateManager.UpdateEntry b)
	{
		if (a.index < b.index)
		{
			return 1;
		}
		if (a.index > b.index)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x060002FC RID: 764 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
	private static void CreateInstance()
	{
		if (UpdateManager.mInst == null)
		{
			UpdateManager.mInst = (UnityEngine.Object.FindObjectOfType(typeof(UpdateManager)) as UpdateManager);
			if (UpdateManager.mInst == null && Application.isPlaying)
			{
				GameObject gameObject = new GameObject("_UpdateManager");
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				UpdateManager.mInst = gameObject.AddComponent<UpdateManager>();
			}
		}
	}

	// Token: 0x060002FD RID: 765 RVA: 0x0000D740 File Offset: 0x0000B940
	private void UpdateList(List<UpdateManager.UpdateEntry> list, float delta)
	{
		int i = list.Count;
		while (i > 0)
		{
			UpdateManager.UpdateEntry updateEntry = list[--i];
			if (updateEntry.isMonoBehaviour)
			{
				if (updateEntry.mb == null)
				{
					list.RemoveAt(i);
					continue;
				}
				if (!updateEntry.mb.enabled || !NGUITools.GetActive(updateEntry.mb.gameObject))
				{
					continue;
				}
			}
			updateEntry.func(delta);
		}
	}

	// Token: 0x060002FE RID: 766 RVA: 0x0000D7CC File Offset: 0x0000B9CC
	private void Start()
	{
		if (Application.isPlaying)
		{
			this.mTime = Time.realtimeSinceStartup;
			base.StartCoroutine(this.CoroutineFunction());
		}
	}

	// Token: 0x060002FF RID: 767 RVA: 0x0000D7FC File Offset: 0x0000B9FC
	private void OnApplicationQuit()
	{
		UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x06000300 RID: 768 RVA: 0x0000D80C File Offset: 0x0000BA0C
	private void Update()
	{
		if (UpdateManager.mInst != this)
		{
			NGUITools.Destroy(base.gameObject);
		}
		else
		{
			this.UpdateList(this.mOnUpdate, Time.deltaTime);
		}
	}

	// Token: 0x06000301 RID: 769 RVA: 0x0000D84C File Offset: 0x0000BA4C
	private void LateUpdate()
	{
		this.UpdateList(this.mOnLate, Time.deltaTime);
		if (!Application.isPlaying)
		{
			this.CoroutineUpdate();
		}
	}

	// Token: 0x06000302 RID: 770 RVA: 0x0000D87C File Offset: 0x0000BA7C
	private bool CoroutineUpdate()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - this.mTime;
		if (num < 0.001f)
		{
			return true;
		}
		this.mTime = realtimeSinceStartup;
		this.UpdateList(this.mOnCoro, num);
		bool isPlaying = Application.isPlaying;
		int i = this.mDest.size;
		while (i > 0)
		{
			UpdateManager.DestroyEntry destroyEntry = this.mDest.buffer[--i];
			if (!isPlaying || destroyEntry.time < this.mTime)
			{
				if (destroyEntry.obj != null)
				{
					NGUITools.Destroy(destroyEntry.obj);
					destroyEntry.obj = null;
				}
				this.mDest.RemoveAt(i);
			}
		}
		if (this.mOnUpdate.Count == 0 && this.mOnLate.Count == 0 && this.mOnCoro.Count == 0 && this.mDest.size == 0)
		{
			NGUITools.Destroy(base.gameObject);
			return false;
		}
		return true;
	}

	// Token: 0x06000303 RID: 771 RVA: 0x0000D984 File Offset: 0x0000BB84
	private IEnumerator CoroutineFunction()
	{
		while (Application.isPlaying)
		{
			if (!this.CoroutineUpdate())
			{
				break;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000304 RID: 772 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
	private void Add(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func, List<UpdateManager.UpdateEntry> list)
	{
		int i = 0;
		int count = list.Count;
		while (i < count)
		{
			UpdateManager.UpdateEntry updateEntry = list[i];
			if (updateEntry.func == func)
			{
				return;
			}
			i++;
		}
		list.Add(new UpdateManager.UpdateEntry
		{
			index = updateOrder,
			func = func,
			mb = mb,
			isMonoBehaviour = (mb != null)
		});
		if (updateOrder != 0)
		{
			list.Sort(new Comparison<UpdateManager.UpdateEntry>(UpdateManager.Compare));
		}
	}

	// Token: 0x06000305 RID: 773 RVA: 0x0000DA2C File Offset: 0x0000BC2C
	public static void AddUpdate(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func)
	{
		UpdateManager.CreateInstance();
		UpdateManager.mInst.Add(mb, updateOrder, func, UpdateManager.mInst.mOnUpdate);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x0000DA4C File Offset: 0x0000BC4C
	public static void AddLateUpdate(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func)
	{
		UpdateManager.CreateInstance();
		UpdateManager.mInst.Add(mb, updateOrder, func, UpdateManager.mInst.mOnLate);
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0000DA6C File Offset: 0x0000BC6C
	public static void AddCoroutine(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func)
	{
		UpdateManager.CreateInstance();
		UpdateManager.mInst.Add(mb, updateOrder, func, UpdateManager.mInst.mOnCoro);
	}

	// Token: 0x06000308 RID: 776 RVA: 0x0000DA8C File Offset: 0x0000BC8C
	public static void AddDestroy(UnityEngine.Object obj, float delay)
	{
		if (obj == null)
		{
			return;
		}
		if (Application.isPlaying)
		{
			if (delay > 0f)
			{
				UpdateManager.CreateInstance();
				UpdateManager.DestroyEntry destroyEntry = new UpdateManager.DestroyEntry();
				destroyEntry.obj = obj;
				destroyEntry.time = Time.realtimeSinceStartup + delay;
				UpdateManager.mInst.mDest.Add(destroyEntry);
			}
			else
			{
				UnityEngine.Object.Destroy(obj);
			}
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(obj);
		}
	}

	// Token: 0x040001B4 RID: 436
	private static UpdateManager mInst;

	// Token: 0x040001B5 RID: 437
	private List<UpdateManager.UpdateEntry> mOnUpdate = new List<UpdateManager.UpdateEntry>();

	// Token: 0x040001B6 RID: 438
	private List<UpdateManager.UpdateEntry> mOnLate = new List<UpdateManager.UpdateEntry>();

	// Token: 0x040001B7 RID: 439
	private List<UpdateManager.UpdateEntry> mOnCoro = new List<UpdateManager.UpdateEntry>();

	// Token: 0x040001B8 RID: 440
	private BetterList<UpdateManager.DestroyEntry> mDest = new BetterList<UpdateManager.DestroyEntry>();

	// Token: 0x040001B9 RID: 441
	private float mTime;

	// Token: 0x02000070 RID: 112
	public class UpdateEntry
	{
		// Token: 0x040001BA RID: 442
		public int index;

		// Token: 0x040001BB RID: 443
		public UpdateManager.OnUpdate func;

		// Token: 0x040001BC RID: 444
		public MonoBehaviour mb;

		// Token: 0x040001BD RID: 445
		public bool isMonoBehaviour;
	}

	// Token: 0x02000071 RID: 113
	public class DestroyEntry
	{
		// Token: 0x040001BE RID: 446
		public UnityEngine.Object obj;

		// Token: 0x040001BF RID: 447
		public float time;
	}

	// Token: 0x02000A62 RID: 2658
	// (Invoke) Token: 0x060047C2 RID: 18370
	public delegate void OnUpdate(float delta);
}
