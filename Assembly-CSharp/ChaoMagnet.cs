using System;
using Message;
using UnityEngine;

// Token: 0x02000160 RID: 352
public class ChaoMagnet : MonoBehaviour
{
	// Token: 0x06000A3F RID: 2623 RVA: 0x0003D66C File Offset: 0x0003B86C
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x0003D678 File Offset: 0x0003B878
	public void Setup(float colliRadius, string hitLayer)
	{
		this.m_hitLayer = hitLayer;
		this.m_collider = base.gameObject.GetComponent<SphereCollider>();
		if (this.m_collider == null)
		{
			this.m_collider = base.gameObject.AddComponent<SphereCollider>();
		}
		if (this.m_collider != null)
		{
			this.m_collider.radius = colliRadius;
			this.m_collider.isTrigger = true;
			this.m_collider.enabled = false;
		}
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x0003D6F4 File Offset: 0x0003B8F4
	public void SetEnable(bool flag)
	{
		if (this.m_collider != null)
		{
			this.m_collider.enabled = flag;
		}
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x0003D714 File Offset: 0x0003B914
	public void OnTriggerEnter(Collider other)
	{
		if (!string.IsNullOrEmpty(this.m_hitLayer) && other.gameObject.layer == LayerMask.NameToLayer(this.m_hitLayer))
		{
			MsgOnDrawingRings value = new MsgOnDrawingRings(base.gameObject);
			other.gameObject.SendMessage("OnDrawingRingsToChao", value, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x040007EC RID: 2028
	private SphereCollider m_collider;

	// Token: 0x040007ED RID: 2029
	private string m_hitLayer = string.Empty;
}
