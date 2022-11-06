using System;
using Message;
using UnityEngine;

// Token: 0x0200094E RID: 2382
public class MultiSetParaloopCircle : MultiSetBase
{
	// Token: 0x06003E15 RID: 15893 RVA: 0x00143060 File Offset: 0x00141260
	protected override void OnSpawned()
	{
		base.OnSpawned();
	}

	// Token: 0x06003E16 RID: 15894 RVA: 0x00143068 File Offset: 0x00141268
	protected override void OnCreateSetup()
	{
		this.SetActiveParaloopComponent(false);
	}

	// Token: 0x06003E17 RID: 15895 RVA: 0x00143074 File Offset: 0x00141274
	public void SucceedParaloop()
	{
		if (MultiSetParaloopCircle.IsNowParaloop())
		{
			this.SetActiveParaloopComponent(true);
			this.SetStartMagnet();
		}
	}

	// Token: 0x06003E18 RID: 15896 RVA: 0x00143090 File Offset: 0x00141290
	private void OnTriggerEnter(Collider other)
	{
		if (other)
		{
			GameObject gameObject = other.gameObject;
			if (gameObject)
			{
				string a = LayerMask.LayerToName(gameObject.layer);
				if (a == "Player")
				{
					this.SucceedParaloop();
					ObjUtil.PopCamera(CameraType.LOOP_TERRAIN, 2.5f);
				}
				else if (a == "Magnet")
				{
				}
			}
		}
	}

	// Token: 0x06003E19 RID: 15897 RVA: 0x001430FC File Offset: 0x001412FC
	private void SetActiveParaloopComponent(bool flag)
	{
		for (int i = 0; i < this.m_dataList.Count; i++)
		{
			GameObject obj = this.m_dataList[i].m_obj;
			if (obj)
			{
				MagnetControl component = obj.GetComponent<MagnetControl>();
				if (component)
				{
					component.enabled = flag;
				}
				SphereCollider component2 = obj.GetComponent<SphereCollider>();
				if (component2)
				{
					component2.enabled = flag;
				}
				BoxCollider component3 = obj.GetComponent<BoxCollider>();
				if (component3)
				{
					component3.enabled = flag;
				}
			}
		}
	}

	// Token: 0x06003E1A RID: 15898 RVA: 0x00143190 File Offset: 0x00141390
	private void SetStartMagnet()
	{
		for (int i = 0; i < this.m_dataList.Count; i++)
		{
			GameObject obj = this.m_dataList[i].m_obj;
			if (obj)
			{
				MsgOnDrawingRings value = new MsgOnDrawingRings();
				obj.SendMessage("OnDrawingRings", value, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x06003E1B RID: 15899 RVA: 0x001431EC File Offset: 0x001413EC
	public static bool IsNowParaloop()
	{
		PlayerInformation playerInformation = ObjUtil.GetPlayerInformation();
		return playerInformation != null && playerInformation.IsNowParaloop();
	}
}
