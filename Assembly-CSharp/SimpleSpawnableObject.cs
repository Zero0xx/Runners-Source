using System;
using UnityEngine;

// Token: 0x02000273 RID: 627
[AddComponentMenu("Scripts/Runners/Object/Game/Level/SimpleSpawnableObject")]
public class SimpleSpawnableObject : SpawnableBehavior
{
	// Token: 0x06001125 RID: 4389 RVA: 0x00062164 File Offset: 0x00060364
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x00062170 File Offset: 0x00060370
	public override SpawnableParameter GetParameter()
	{
		return this.m_paramter;
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x00062178 File Offset: 0x00060378
	public override SpawnableParameter GetParameterForExport()
	{
		return this.m_paramter;
	}

	// Token: 0x04000F83 RID: 3971
	public SpawnableParameter m_paramter;
}
