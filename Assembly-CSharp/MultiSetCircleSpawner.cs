using System;
using UnityEngine;

// Token: 0x0200094A RID: 2378
public class MultiSetCircleSpawner : SpawnableBehavior
{
	// Token: 0x06003E08 RID: 15880 RVA: 0x00142B20 File Offset: 0x00140D20
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		MultiSetCircleParameter multiSetCircleParameter = srcParameter as MultiSetCircleParameter;
		if (multiSetCircleParameter != null)
		{
			this.m_parameter = multiSetCircleParameter;
			GameObject @object = multiSetCircleParameter.m_object;
			MultiSetCircle component = base.GetComponent<MultiSetCircle>();
			if (@object != null && component != null)
			{
				component.Setup();
				int count = multiSetCircleParameter.m_count;
				float radius = multiSetCircleParameter.m_radius;
				float num = 360f / (float)count;
				for (int i = 0; i < count; i++)
				{
					float f = 0.017453292f * (num * (float)i);
					float x = radius * Mathf.Cos(f);
					float y = radius * Mathf.Sin(f);
					Vector3 b = new Vector3(x, y, 0f);
					Vector3 pos = base.transform.position + b;
					component.AddObject(@object, pos, Quaternion.identity);
				}
			}
		}
	}

	// Token: 0x06003E09 RID: 15881 RVA: 0x00142BF4 File Offset: 0x00140DF4
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(base.transform.position, this.m_parameter.m_radius);
		if (ObjUtil.IsUseTemporarySet())
		{
			GameObject @object = this.m_parameter.m_object;
			if (@object != null)
			{
				SphereCollider component = @object.GetComponent<SphereCollider>();
				BoxCollider component2 = @object.GetComponent<BoxCollider>();
				int count = this.m_parameter.m_count;
				float radius = this.m_parameter.m_radius;
				float num = 360f / (float)count;
				for (int i = 0; i < count; i++)
				{
					float f = 0.017453292f * (num * (float)i);
					float x = radius * Mathf.Cos(f);
					float y = radius * Mathf.Sin(f);
					Vector3 b = new Vector3(x, y, 0f);
					Vector3 a = base.transform.position + b;
					if (component != null)
					{
						Gizmos.DrawWireSphere(a + component.center, component.radius);
					}
					else if (component2 != null)
					{
						Gizmos.DrawWireCube(a + component2.center, component2.size);
					}
				}
			}
		}
	}

	// Token: 0x06003E0A RID: 15882 RVA: 0x00142D28 File Offset: 0x00140F28
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x04003575 RID: 13685
	[SerializeField]
	private MultiSetCircleParameter m_parameter;
}
