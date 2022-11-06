using System;
using Message;
using UnityEngine;

// Token: 0x020008EF RID: 2287
[AddComponentMenu("Scripts/Runners/Object/Common/ObjItemBox")]
public class ObjItemBox : SpawnableObject
{
	// Token: 0x06003C7D RID: 15485 RVA: 0x0013E3E0 File Offset: 0x0013C5E0
	protected override string GetModelName()
	{
		return "obj_cmn_itembox";
	}

	// Token: 0x06003C7E RID: 15486 RVA: 0x0013E3E8 File Offset: 0x0013C5E8
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003C7F RID: 15487 RVA: 0x0013E3EC File Offset: 0x0013C5EC
	protected override void OnSpawned()
	{
	}

	// Token: 0x06003C80 RID: 15488 RVA: 0x0013E3F0 File Offset: 0x0013C5F0
	public void CreateItem(ItemType item_type)
	{
		this.m_item_type = (uint)item_type;
		if (this.m_item_type < 8U)
		{
			string itemFileName = ItemTypeName.GetItemFileName((ItemType)this.m_item_type);
			if (itemFileName.Length > 0)
			{
				this.m_item_obj = base.AttachObject(ResourceCategory.OBJECT_RESOURCE, itemFileName, Vector3.zero, Quaternion.Euler(Vector3.zero));
			}
		}
	}

	// Token: 0x06003C81 RID: 15489 RVA: 0x0013E450 File Offset: 0x0013C650
	private void OnTriggerEnter(Collider other)
	{
		if (this.m_end)
		{
			return;
		}
		if (other)
		{
			if (StageItemManager.Instance != null)
			{
				StageItemManager.Instance.OnAddItem(new MsgAddItemToManager((ItemType)this.m_item_type));
			}
			this.TakeItemBox();
		}
	}

	// Token: 0x06003C82 RID: 15490 RVA: 0x0013E4A0 File Offset: 0x0013C6A0
	private void TakeItemBox()
	{
		this.m_end = true;
		ObjUtil.PlayEffectCollisionCenter(base.gameObject, "ef_com_itembox_open01", 1f, false);
		ObjUtil.PlaySE("obj_itembox", "SE");
		ObjUtil.SendGetItemIcon((ItemType)this.m_item_type);
		if (this.m_item_obj)
		{
			UnityEngine.Object.Destroy(this.m_item_obj);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040034AB RID: 13483
	private const string ModelName = "obj_cmn_itembox";

	// Token: 0x040034AC RID: 13484
	private uint m_item_type;

	// Token: 0x040034AD RID: 13485
	private GameObject m_item_obj;

	// Token: 0x040034AE RID: 13486
	private bool m_end;
}
