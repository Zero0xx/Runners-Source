using System;
using App.Utility;
using UnityEngine;

// Token: 0x0200026B RID: 619
public class SpawnableInfo
{
	// Token: 0x170002AE RID: 686
	// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00060DD0 File Offset: 0x0005EFD0
	public bool Spawned
	{
		get
		{
			return this.m_object != null;
		}
	}

	// Token: 0x170002AF RID: 687
	// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00060DE0 File Offset: 0x0005EFE0
	public bool Destroyed
	{
		get
		{
			return this.m_flag.Test(0);
		}
	}

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x060010F4 RID: 4340 RVA: 0x00060E00 File Offset: 0x0005F000
	// (set) Token: 0x060010F3 RID: 4339 RVA: 0x00060DF0 File Offset: 0x0005EFF0
	public bool NotRangeOut
	{
		get
		{
			return this.m_flag.Test(1);
		}
		set
		{
			this.m_flag.Set(1, value);
		}
	}

	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x060010F6 RID: 4342 RVA: 0x00060E20 File Offset: 0x0005F020
	// (set) Token: 0x060010F5 RID: 4341 RVA: 0x00060E10 File Offset: 0x0005F010
	public bool RequestDestroy
	{
		get
		{
			return this.m_flag.Test(2);
		}
		set
		{
			this.m_flag.Set(2, value);
		}
	}

	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00060E40 File Offset: 0x0005F040
	// (set) Token: 0x060010F7 RID: 4343 RVA: 0x00060E30 File Offset: 0x0005F030
	public bool Sleep
	{
		get
		{
			return this.m_flag.Test(3);
		}
		set
		{
			this.m_flag.Set(3, value);
		}
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x00060E50 File Offset: 0x0005F050
	public void SpawnedObject(SpawnableObject spawnObject)
	{
		this.m_object = spawnObject;
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x00060E5C File Offset: 0x0005F05C
	public void DestroyedObject()
	{
		if (this.m_object)
		{
			this.m_object = null;
			this.m_flag.Set(0, true);
		}
	}

	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x060010FC RID: 4348 RVA: 0x00060E94 File Offset: 0x0005F094
	// (set) Token: 0x060010FB RID: 4347 RVA: 0x00060E84 File Offset: 0x0005F084
	public bool AttributeOnlyOne
	{
		get
		{
			return this.m_attribute.Test(0);
		}
		set
		{
			this.m_attribute.Set(0, value);
		}
	}

	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x060010FD RID: 4349 RVA: 0x00060EA4 File Offset: 0x0005F0A4
	public ObjectSpawnManager Manager
	{
		get
		{
			return this.m_manager;
		}
	}

	// Token: 0x04000F51 RID: 3921
	public int m_block;

	// Token: 0x04000F52 RID: 3922
	public int m_blockActivateID;

	// Token: 0x04000F53 RID: 3923
	public Vector3 m_position;

	// Token: 0x04000F54 RID: 3924
	public Quaternion m_rotation;

	// Token: 0x04000F55 RID: 3925
	public SpawnableParameter m_parameters;

	// Token: 0x04000F56 RID: 3926
	private Bitset32 m_flag;

	// Token: 0x04000F57 RID: 3927
	private Bitset32 m_attribute;

	// Token: 0x04000F58 RID: 3928
	public SpawnableObject m_object;

	// Token: 0x04000F59 RID: 3929
	public ObjectSpawnManager m_manager;

	// Token: 0x0200026C RID: 620
	private enum Flags
	{
		// Token: 0x04000F5B RID: 3931
		DESTROY,
		// Token: 0x04000F5C RID: 3932
		NOTRANGEOUT,
		// Token: 0x04000F5D RID: 3933
		REQUESTDESTROY,
		// Token: 0x04000F5E RID: 3934
		SLEEP
	}

	// Token: 0x0200026D RID: 621
	private enum Attribute
	{
		// Token: 0x04000F60 RID: 3936
		ONLYONE_OBJECT
	}
}
