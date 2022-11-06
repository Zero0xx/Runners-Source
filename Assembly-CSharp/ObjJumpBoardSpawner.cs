using System;
using UnityEngine;

// Token: 0x020008F6 RID: 2294
public class ObjJumpBoardSpawner : SpawnableBehavior
{
	// Token: 0x06003C9A RID: 15514 RVA: 0x0013EB4C File Offset: 0x0013CD4C
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjJumpBoardParameter objJumpBoardParameter = srcParameter as ObjJumpBoardParameter;
		if (objJumpBoardParameter != null)
		{
			ObjJumpBoard component = base.GetComponent<ObjJumpBoard>();
			if (component)
			{
				component.SetObjJumpBoardParameter(objJumpBoardParameter);
			}
		}
	}

	// Token: 0x06003C9B RID: 15515 RVA: 0x0013EB80 File Offset: 0x0013CD80
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x040034C1 RID: 13505
	[SerializeField]
	private ObjJumpBoardParameter m_parameter;
}
