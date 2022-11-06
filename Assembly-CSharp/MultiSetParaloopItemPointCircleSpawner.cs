using System;
using UnityEngine;

// Token: 0x02000953 RID: 2387
public class MultiSetParaloopItemPointCircleSpawner : SpawnableBehavior
{
	// Token: 0x06003E37 RID: 15927 RVA: 0x00143974 File Offset: 0x00141B74
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		MultiSetParaloopItemPointCircleParameter multiSetParaloopItemPointCircleParameter = srcParameter as MultiSetParaloopItemPointCircleParameter;
		if (multiSetParaloopItemPointCircleParameter != null)
		{
			this.m_parameter = multiSetParaloopItemPointCircleParameter;
			GameObject @object = multiSetParaloopItemPointCircleParameter.m_object;
			MultiSetParaloopItemPointCircle component = base.GetComponent<MultiSetParaloopItemPointCircle>();
			if (@object != null && component != null)
			{
				if (!ObjUtil.IsUseTemporarySet())
				{
					BoxCollider component2 = base.GetComponent<BoxCollider>();
					if (component2)
					{
						component2.size = multiSetParaloopItemPointCircleParameter.GetSize();
						component2.center = multiSetParaloopItemPointCircleParameter.GetCenter();
					}
				}
				component.Setup();
				component.SetID(multiSetParaloopItemPointCircleParameter.m_tblID);
				component.AddObject(@object, base.transform.position, Quaternion.identity);
			}
		}
	}

	// Token: 0x06003E38 RID: 15928 RVA: 0x00143A18 File Offset: 0x00141C18
	private void OnDrawGizmos()
	{
		BoxCollider component = base.GetComponent<BoxCollider>();
		if (component)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(base.transform.position + component.center, component.size);
		}
		if (ObjUtil.IsUseTemporarySet())
		{
			Gizmos.DrawWireSphere(base.transform.position, 0.5f);
		}
	}

	// Token: 0x06003E39 RID: 15929 RVA: 0x00143A84 File Offset: 0x00141C84
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x06003E3A RID: 15930 RVA: 0x00143A8C File Offset: 0x00141C8C
	public override SpawnableParameter GetParameterForExport()
	{
		BoxCollider component = base.GetComponent<BoxCollider>();
		if (component)
		{
			this.m_parameter.SetSize(component.size);
			this.m_parameter.SetCenter(component.center);
		}
		return this.m_parameter;
	}

	// Token: 0x04003591 RID: 13713
	[SerializeField]
	private MultiSetParaloopItemPointCircleParameter m_parameter;
}
