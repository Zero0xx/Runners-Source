using System;
using UnityEngine;

// Token: 0x02000274 RID: 628
[Serializable]
public class SpawnableParameter
{
	// Token: 0x06001128 RID: 4392 RVA: 0x00062180 File Offset: 0x00060380
	public SpawnableParameter()
	{
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x000621A0 File Offset: 0x000603A0
	public SpawnableParameter(string name)
	{
		this.objectname = name;
	}

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x0600112B RID: 4395 RVA: 0x000621D4 File Offset: 0x000603D4
	// (set) Token: 0x0600112A RID: 4394 RVA: 0x000621C8 File Offset: 0x000603C8
	public uint ID
	{
		get
		{
			return this.m_id;
		}
		set
		{
			this.m_id = value;
		}
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x0600112D RID: 4397 RVA: 0x000621E8 File Offset: 0x000603E8
	// (set) Token: 0x0600112C RID: 4396 RVA: 0x000621DC File Offset: 0x000603DC
	public string ObjectName
	{
		get
		{
			return this.objectname;
		}
		set
		{
			this.objectname = value;
		}
	}

	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x0600112F RID: 4399 RVA: 0x000621FC File Offset: 0x000603FC
	// (set) Token: 0x0600112E RID: 4398 RVA: 0x000621F0 File Offset: 0x000603F0
	public Vector3 Position
	{
		get
		{
			return this.position;
		}
		set
		{
			this.position = value;
		}
	}

	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x06001131 RID: 4401 RVA: 0x00062210 File Offset: 0x00060410
	// (set) Token: 0x06001130 RID: 4400 RVA: 0x00062204 File Offset: 0x00060404
	public Quaternion Rotation
	{
		get
		{
			return this.rotation;
		}
		set
		{
			this.rotation = value;
		}
	}

	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x06001133 RID: 4403 RVA: 0x00062224 File Offset: 0x00060424
	// (set) Token: 0x06001132 RID: 4402 RVA: 0x00062218 File Offset: 0x00060418
	public float RangeIn
	{
		get
		{
			return this.rangein;
		}
		set
		{
			this.rangein = value;
		}
	}

	// Token: 0x170002BA RID: 698
	// (get) Token: 0x06001135 RID: 4405 RVA: 0x00062238 File Offset: 0x00060438
	// (set) Token: 0x06001134 RID: 4404 RVA: 0x0006222C File Offset: 0x0006042C
	public float RangeOut
	{
		get
		{
			return this.rangeout;
		}
		set
		{
			this.rangeout = value;
		}
	}

	// Token: 0x04000F84 RID: 3972
	public const float DefaultRangeIn = 20f;

	// Token: 0x04000F85 RID: 3973
	public const float DefaultRangeOut = 30f;

	// Token: 0x04000F86 RID: 3974
	private string objectname;

	// Token: 0x04000F87 RID: 3975
	private Vector3 position;

	// Token: 0x04000F88 RID: 3976
	private Quaternion rotation;

	// Token: 0x04000F89 RID: 3977
	private uint m_id;

	// Token: 0x04000F8A RID: 3978
	public float rangein = 20f;

	// Token: 0x04000F8B RID: 3979
	public float rangeout = 30f;
}
