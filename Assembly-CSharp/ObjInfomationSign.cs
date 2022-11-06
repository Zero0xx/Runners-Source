using System;
using UnityEngine;

// Token: 0x020008EC RID: 2284
[AddComponentMenu("Scripts/Runners/Object/Common/ObjInfomationSign")]
public class ObjInfomationSign : SpawnableObject
{
	// Token: 0x06003C73 RID: 15475 RVA: 0x0013E2BC File Offset: 0x0013C4BC
	protected override string GetModelName()
	{
		return "obj_cmn_infomationsign";
	}

	// Token: 0x06003C74 RID: 15476 RVA: 0x0013E2C4 File Offset: 0x0013C4C4
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x170008BF RID: 2239
	// (get) Token: 0x06003C76 RID: 15478 RVA: 0x0013E2FC File Offset: 0x0013C4FC
	// (set) Token: 0x06003C75 RID: 15477 RVA: 0x0013E2C8 File Offset: 0x0013C4C8
	public GameObject InfomationObject
	{
		get
		{
			return this.m_infomationObject;
		}
		set
		{
			if (this.m_infomationObject == null)
			{
				this.m_infomationObject = value;
				this.m_infomationObject.SetActive(true);
			}
		}
	}

	// Token: 0x06003C77 RID: 15479 RVA: 0x0013E304 File Offset: 0x0013C504
	protected override void OnSpawned()
	{
	}

	// Token: 0x040034A7 RID: 13479
	private const string ModelName = "obj_cmn_infomationsign";

	// Token: 0x040034A8 RID: 13480
	private GameObject m_infomationObject;
}
