using System;
using UnityEngine;

// Token: 0x020008F0 RID: 2288
[AddComponentMenu("Scripts/Runners/Object/Common/ObjItemPoint")]
public class ObjItemPoint : SpawnableObject
{
	// Token: 0x06003C84 RID: 15492 RVA: 0x0013E524 File Offset: 0x0013C724
	protected override void OnSpawned()
	{
	}

	// Token: 0x06003C85 RID: 15493 RVA: 0x0013E528 File Offset: 0x0013C728
	private void Update()
	{
		if (!this.m_createItemBox)
		{
			ItemType itemType = this.GetItemType();
			if (itemType == ItemType.REDSTAR_RING && StageModeManager.Instance != null && StageModeManager.Instance.FirstTutorial)
			{
				itemType = ItemType.TRAMPOLINE;
			}
			ItemType itemType2 = itemType;
			GameObject x;
			switch (itemType2 + 5)
			{
			case ItemType.INVINCIBLE:
				x = this.CreateObjTimerObj(TimerType.GOLD);
				break;
			case ItemType.BARRIER:
				x = this.CreateObjTimerObj(TimerType.SILVER);
				break;
			case ItemType.MAGNET:
				x = this.CreateObjTimerObj(TimerType.BRONZE);
				break;
			case ItemType.TRAMPOLINE:
				x = this.CreateObjRedStarRing();
				break;
			default:
				x = this.CreateObjItemBox(itemType);
				break;
			}
			this.m_createItemBox = true;
			if (x == null)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x06003C86 RID: 15494 RVA: 0x0013E5F0 File Offset: 0x0013C7F0
	public void SetID(int tbl_id)
	{
		if (this.m_tbl_id == -1)
		{
			this.m_tbl_id = tbl_id;
		}
	}

	// Token: 0x06003C87 RID: 15495 RVA: 0x0013E608 File Offset: 0x0013C808
	public bool IsCreateItemBox()
	{
		return this.m_createItemBox;
	}

	// Token: 0x06003C88 RID: 15496 RVA: 0x0013E610 File Offset: 0x0013C810
	private GameObject CreateObjItemBox(ItemType item_type)
	{
		if (item_type < ItemType.NUM)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, "ObjItemBox");
			if (gameObject)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, base.transform.position, base.transform.rotation) as GameObject;
				if (gameObject2)
				{
					gameObject2.SetActive(true);
					gameObject2.transform.parent = base.transform;
					SpawnableObject component = gameObject2.GetComponent<SpawnableObject>();
					if (component)
					{
						component.AttachModelObject();
					}
					ObjItemBox objItemBox = GameObjectUtil.FindChildGameObjectComponent<ObjItemBox>(base.gameObject, gameObject2.name);
					if (objItemBox)
					{
						objItemBox.CreateItem(item_type);
					}
					return gameObject2;
				}
			}
		}
		return null;
	}

	// Token: 0x06003C89 RID: 15497 RVA: 0x0013E6C8 File Offset: 0x0013C8C8
	private GameObject CreateObjRedStarRing()
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, "ObjRedStarRing");
		if (gameObject)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, base.transform.position, base.transform.rotation) as GameObject;
			if (gameObject2)
			{
				gameObject2.SetActive(true);
				gameObject2.transform.parent = base.transform;
				SpawnableObject component = gameObject2.GetComponent<SpawnableObject>();
				if (component)
				{
					component.AttachModelObject();
				}
				SphereCollider component2 = gameObject2.GetComponent<SphereCollider>();
				if (component2)
				{
					gameObject2.transform.localPosition = new Vector3(0f, -component2.center.y, 0f);
				}
				return gameObject2;
			}
		}
		return null;
	}

	// Token: 0x06003C8A RID: 15498 RVA: 0x0013E790 File Offset: 0x0013C990
	private GameObject CreateObjTimerObj(TimerType type)
	{
		string objectName = ObjTimerUtil.GetObjectName(type);
		if (!string.IsNullOrEmpty(objectName))
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, objectName);
			if (gameObject)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, base.transform.position, base.transform.rotation) as GameObject;
				if (gameObject2)
				{
					gameObject2.SetActive(true);
					gameObject2.transform.parent = base.transform;
					SpawnableObject component = gameObject2.GetComponent<SpawnableObject>();
					if (component)
					{
						component.AttachModelObject();
					}
					ObjTimerBase component2 = gameObject2.GetComponent<ObjTimerBase>();
					if (component2 != null)
					{
						component2.SetMoveType(ObjTimerBase.MoveType.Still);
					}
					SphereCollider component3 = gameObject2.GetComponent<SphereCollider>();
					if (component3)
					{
						gameObject2.transform.localPosition = new Vector3(0f, -component3.center.y, 0f);
					}
					return gameObject2;
				}
			}
		}
		return null;
	}

	// Token: 0x06003C8B RID: 15499 RVA: 0x0013E884 File Offset: 0x0013CA84
	private ItemType GetItemType()
	{
		if (StageItemManager.Instance != null)
		{
			ItemTable itemTable = StageItemManager.Instance.GetItemTable();
			if (itemTable != null)
			{
				return itemTable.GetItemType(this.m_tbl_id);
			}
		}
		return ItemType.TRAMPOLINE;
	}

	// Token: 0x040034AF RID: 13487
	public int m_debugItemID = -1;

	// Token: 0x040034B0 RID: 13488
	private int m_tbl_id = -1;

	// Token: 0x040034B1 RID: 13489
	private bool m_createItemBox;
}
