using System;
using UnityEngine;

// Token: 0x02000275 RID: 629
public class SpawnableBehavior : MonoBehaviour
{
	// Token: 0x06001137 RID: 4407 RVA: 0x00062248 File Offset: 0x00060448
	public virtual void SetParameters(SpawnableParameter srcParameter)
	{
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x0006224C File Offset: 0x0006044C
	public virtual SpawnableParameter GetParameter()
	{
		return null;
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x00062250 File Offset: 0x00060450
	public virtual SpawnableParameter GetParameterForExport()
	{
		return this.GetParameter();
	}
}
