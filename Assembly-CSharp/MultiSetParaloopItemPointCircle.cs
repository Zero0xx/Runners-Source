using System;
using UnityEngine;

// Token: 0x02000951 RID: 2385
public class MultiSetParaloopItemPointCircle : MultiSetBase
{
	// Token: 0x06003E27 RID: 15911 RVA: 0x001435F8 File Offset: 0x001417F8
	protected override void OnSpawned()
	{
		base.OnSpawned();
	}

	// Token: 0x06003E28 RID: 15912 RVA: 0x00143600 File Offset: 0x00141800
	protected override void OnCreateSetup()
	{
		ObjItemPoint objItemPoint = this.GetObjItemPoint();
		if (objItemPoint != null)
		{
			objItemPoint.SetID(this.m_tblID);
		}
	}

	// Token: 0x06003E29 RID: 15913 RVA: 0x0014362C File Offset: 0x0014182C
	protected override void UpdateLocal()
	{
		if (!this.m_setup)
		{
			ObjItemPoint objItemPoint = this.GetObjItemPoint();
			if (objItemPoint != null && objItemPoint.IsCreateItemBox())
			{
				this.SetActiveParaloopComponent(false);
				this.m_setup = true;
			}
		}
	}

	// Token: 0x06003E2A RID: 15914 RVA: 0x00143670 File Offset: 0x00141870
	public void SetID(int id)
	{
		this.m_tblID = id;
	}

	// Token: 0x06003E2B RID: 15915 RVA: 0x0014367C File Offset: 0x0014187C
	public void SucceedParaloop()
	{
		if (MultiSetParaloopCircle.IsNowParaloop())
		{
			this.SetActiveParaloopComponent(true);
			this.SetStartMagnet();
		}
	}

	// Token: 0x06003E2C RID: 15916 RVA: 0x00143698 File Offset: 0x00141898
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
					ObjUtil.PopCamera(CameraType.LOOP_TERRAIN, 5.5f);
				}
				else if (a == "Magnet")
				{
				}
			}
		}
	}

	// Token: 0x06003E2D RID: 15917 RVA: 0x00143704 File Offset: 0x00141904
	private void SetActiveParaloopComponent(bool flag)
	{
		ObjItemBox objItemBox = this.GetObjItemBox();
		if (objItemBox != null)
		{
			MagnetControl magnetControl = objItemBox.gameObject.GetComponent<MagnetControl>();
			if (magnetControl == null)
			{
				magnetControl = objItemBox.gameObject.AddComponent<MagnetControl>();
			}
			if (magnetControl != null)
			{
				magnetControl.enabled = flag;
			}
			SphereCollider component = objItemBox.gameObject.GetComponent<SphereCollider>();
			if (component != null)
			{
				component.enabled = flag;
			}
		}
	}

	// Token: 0x06003E2E RID: 15918 RVA: 0x0014377C File Offset: 0x0014197C
	private void SetStartMagnet()
	{
		ObjItemBox objItemBox = this.GetObjItemBox();
		if (objItemBox != null)
		{
			ObjUtil.StartMagnetControl(objItemBox.gameObject, 0.5f);
		}
	}

	// Token: 0x06003E2F RID: 15919 RVA: 0x001437AC File Offset: 0x001419AC
	private ObjItemPoint GetObjItemPoint()
	{
		if (this.m_dataList.Count > 0)
		{
			GameObject obj = this.m_dataList[0].m_obj;
			if (obj != null)
			{
				return obj.GetComponent<ObjItemPoint>();
			}
		}
		return null;
	}

	// Token: 0x06003E30 RID: 15920 RVA: 0x001437F0 File Offset: 0x001419F0
	private ObjItemBox GetObjItemBox()
	{
		ObjItemPoint objItemPoint = this.GetObjItemPoint();
		if (objItemPoint != null)
		{
			GameObject gameObject = objItemPoint.gameObject;
			if (gameObject != null)
			{
				for (int i = 0; i < gameObject.transform.childCount; i++)
				{
					Transform child = gameObject.transform.GetChild(i);
					if (child != null)
					{
						ObjItemBox component = child.gameObject.GetComponent<ObjItemBox>();
						if (component != null)
						{
							return component;
						}
					}
				}
			}
		}
		return null;
	}

	// Token: 0x04003587 RID: 13703
	private int m_tblID;

	// Token: 0x04003588 RID: 13704
	private bool m_setup;
}
