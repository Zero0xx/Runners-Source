using System;
using Message;
using UnityEngine;

// Token: 0x020008FE RID: 2302
[AddComponentMenu("Scripts/Runners/Object/Common/ObjPrism")]
public class ObjPrism : SpawnableObject
{
	// Token: 0x06003CBC RID: 15548 RVA: 0x0013F178 File Offset: 0x0013D378
	protected override string GetModelName()
	{
		return "obj_cmn_prism";
	}

	// Token: 0x06003CBD RID: 15549 RVA: 0x0013F180 File Offset: 0x0013D380
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003CBE RID: 15550 RVA: 0x0013F184 File Offset: 0x0013D384
	protected override void OnSpawned()
	{
		ObjUtil.StopAnimation(base.gameObject);
	}

	// Token: 0x06003CBF RID: 15551 RVA: 0x0013F194 File Offset: 0x0013D394
	public void OnReturnFromPhantom(MsgReturnFromPhantom msg)
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06003CC0 RID: 15552 RVA: 0x0013F1A4 File Offset: 0x0013D3A4
	private void OnTriggerEnter(Collider other)
	{
		ObjUtil.PlayEffectCollisionCenter(base.gameObject, "ef_ph_laser_reflect01", 1f, false);
		Animation componentInChildren = base.GetComponentInChildren<Animation>();
		if (componentInChildren)
		{
			componentInChildren.wrapMode = WrapMode.Once;
			componentInChildren.Play("obj_prism_anim");
		}
		ObjUtil.PlaySE("phantom_laser_prism", "SE");
	}

	// Token: 0x040034DB RID: 13531
	private const string ModelName = "obj_cmn_prism";
}
