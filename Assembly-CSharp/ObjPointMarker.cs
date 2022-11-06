using System;
using Message;
using UnityEngine;

// Token: 0x020008FB RID: 2299
public class ObjPointMarker : SpawnableObject
{
	// Token: 0x06003CAD RID: 15533 RVA: 0x0013EE38 File Offset: 0x0013D038
	protected override string GetModelName()
	{
		return "obj_cmn_pointmarker";
	}

	// Token: 0x06003CAE RID: 15534 RVA: 0x0013EE40 File Offset: 0x0013D040
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003CAF RID: 15535 RVA: 0x0013EE44 File Offset: 0x0013D044
	protected override void OnSpawned()
	{
		ObjUtil.StopAnimation(base.gameObject);
	}

	// Token: 0x06003CB0 RID: 15536 RVA: 0x0013EE54 File Offset: 0x0013D054
	public override void OnCreate()
	{
		this.SetActivePointMarker(false);
	}

	// Token: 0x06003CB1 RID: 15537 RVA: 0x0013EE60 File Offset: 0x0013D060
	private void Update()
	{
		if (this.m_end && this.m_chaoItemTime > 0f)
		{
			this.m_chaoItemTime -= Time.deltaTime;
			if (this.m_chaoItemTime <= 0f)
			{
				ObjUtil.RequestStartAbilityToChao(ChaoAbility.CHECK_POINT_ITEM_BOX, false);
				this.m_chaoItemTime = 0f;
			}
		}
	}

	// Token: 0x06003CB2 RID: 15538 RVA: 0x0013EEC0 File Offset: 0x0013D0C0
	public void SetType(PointMarkerType type)
	{
		this.m_type = (int)type;
	}

	// Token: 0x06003CB3 RID: 15539 RVA: 0x0013EECC File Offset: 0x0013D0CC
	public void OnActivePointMarker(MsgActivePointMarker msg)
	{
		if (msg.m_type == (PointMarkerType)this.m_type)
		{
			this.SetActivePointMarker(true);
			msg.m_activated = true;
		}
	}

	// Token: 0x06003CB4 RID: 15540 RVA: 0x0013EEF0 File Offset: 0x0013D0F0
	private void SetActivePointMarker(bool flag)
	{
		base.enabled = flag;
		BoxCollider component = base.GetComponent<BoxCollider>();
		if (component)
		{
			component.enabled = flag;
		}
		Component[] componentsInChildren = base.GetComponentsInChildren<MeshRenderer>(true);
		foreach (MeshRenderer meshRenderer in componentsInChildren)
		{
			if (meshRenderer)
			{
				meshRenderer.enabled = flag;
			}
		}
	}

	// Token: 0x06003CB5 RID: 15541 RVA: 0x0013EF5C File Offset: 0x0013D15C
	private void OnTriggerEnter(Collider other)
	{
		if (this.m_end)
		{
			return;
		}
		if (other)
		{
			GameObject gameObject = other.gameObject;
			if (gameObject)
			{
				string a = LayerMask.LayerToName(gameObject.layer);
				if (a == "Player")
				{
					PlayerInformation playerInformation = GameObjectUtil.FindGameObjectComponent<PlayerInformation>("PlayerInformation");
					if (playerInformation != null)
					{
						int numRings = playerInformation.NumRings;
						GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnAddStockRing", new MsgAddStockRing(numRings), SendMessageOptions.DontRequireReceiver);
					}
					ObjUtil.SendMessageTransferRing();
					this.PassPointMarker();
					GameObjectUtil.SendMessageFindGameObject("StageComboManager", "OnPassPointMarker", null, SendMessageOptions.DontRequireReceiver);
					if (StageAbilityManager.Instance != null && StageAbilityManager.Instance.HasChaoAbility(ChaoAbility.CHECK_POINT_ITEM_BOX))
					{
						int num = (int)StageAbilityManager.Instance.GetChaoAbilityValue(ChaoAbility.CHECK_POINT_ITEM_BOX);
						if (num >= ObjUtil.GetRandomRange100())
						{
							if (this.m_type == 0)
							{
								this.m_chaoItemTime = 0.3f;
							}
							else
							{
								this.m_chaoItemTime = 0.001f;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06003CB6 RID: 15542 RVA: 0x0013F064 File Offset: 0x0013D264
	private void PassPointMarker()
	{
		ObjUtil.PlaySE("obj_checkpoint", "SE");
		Component[] componentsInChildren = base.GetComponentsInChildren<MeshRenderer>(true);
		foreach (MeshRenderer meshRenderer in componentsInChildren)
		{
			if (meshRenderer.name == "obj_cmn_pointmarkerbar")
			{
				ObjUtil.PlayEffectChild(meshRenderer.gameObject, "ef_ob_pass_pointmarker01", ObjPointMarker.EffectLocalPosition, Quaternion.identity, 1f, true);
				break;
			}
		}
		Animation componentInChildren = base.GetComponentInChildren<Animation>();
		if (componentInChildren)
		{
			componentInChildren.wrapMode = WrapMode.Once;
			componentInChildren.Play("obj_pointmarker_bar");
		}
		this.m_end = true;
	}

	// Token: 0x040034D3 RID: 13523
	private const string ModelName = "obj_cmn_pointmarker";

	// Token: 0x040034D4 RID: 13524
	private const float ChaoItemTime = 0.3f;

	// Token: 0x040034D5 RID: 13525
	private static Vector3 EffectLocalPosition = new Vector3(0f, 0.8f, 0f);

	// Token: 0x040034D6 RID: 13526
	private int m_type;

	// Token: 0x040034D7 RID: 13527
	private bool m_end;

	// Token: 0x040034D8 RID: 13528
	private float m_chaoItemTime;
}
