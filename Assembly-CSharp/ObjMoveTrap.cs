using System;
using GameScore;
using Tutorial;
using UnityEngine;

// Token: 0x02000915 RID: 2325
[AddComponentMenu("Scripts/Runners/Object/Common/ObjMoveTrap")]
public class ObjMoveTrap : ObjTrapBase
{
	// Token: 0x06003D33 RID: 15667 RVA: 0x001411F4 File Offset: 0x0013F3F4
	protected override string GetModelName()
	{
		this.SetupParam();
		return this.m_modelParam.m_modelName;
	}

	// Token: 0x06003D34 RID: 15668 RVA: 0x00141208 File Offset: 0x0013F408
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003D35 RID: 15669 RVA: 0x0014120C File Offset: 0x0013F40C
	protected override int GetScore()
	{
		return Data.MoveTrap;
	}

	// Token: 0x06003D36 RID: 15670 RVA: 0x00141214 File Offset: 0x0013F414
	public override bool IsValid()
	{
		return !(StageModeManager.Instance != null) || !StageModeManager.Instance.IsQuickMode();
	}

	// Token: 0x06003D37 RID: 15671 RVA: 0x00141238 File Offset: 0x0013F438
	protected override void OnSpawned()
	{
		if (this.IsCreateCheck())
		{
			base.enabled = false;
			this.SetupParam();
			base.OnSpawned();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06003D38 RID: 15672 RVA: 0x00141274 File Offset: 0x0013F474
	public void SetObjMoveTrapParameter(ObjMoveTrapParameter param)
	{
		if (this.IsCreateCheck())
		{
			this.SetupParam();
			this.m_param = param;
			MotorConstant component = base.GetComponent<MotorConstant>();
			if (component)
			{
				component.SetParam(this.m_param.moveSpeed, this.m_param.moveDistance, this.m_param.startMoveDistance, -base.transform.right, "SE", this.m_modelParam.m_seName);
			}
		}
	}

	// Token: 0x06003D39 RID: 15673 RVA: 0x001412F4 File Offset: 0x0013F4F4
	protected override void PlayEffect()
	{
		ObjUtil.PlayEffectCollisionCenter(base.gameObject, this.m_modelParam.m_effectName, 1f, false);
	}

	// Token: 0x06003D3A RID: 15674 RVA: 0x00141314 File Offset: 0x0013F514
	protected override void TrapDamageHit()
	{
		base.SetBroken();
	}

	// Token: 0x06003D3B RID: 15675 RVA: 0x0014131C File Offset: 0x0013F51C
	private bool IsTutorialCheck()
	{
		return StageTutorialManager.Instance != null && !StageTutorialManager.Instance.IsCompletedTutorial();
	}

	// Token: 0x06003D3C RID: 15676 RVA: 0x0014134C File Offset: 0x0013F54C
	private bool IsCreateCheck()
	{
		if (!ObjUtil.IsUseTemporarySet() && StageTutorialManager.Instance != null)
		{
			if (StageTutorialManager.Instance.IsCompletedTutorial())
			{
				return true;
			}
			EventID currentEventID = StageTutorialManager.Instance.CurrentEventID;
			if (currentEventID != EventID.DAMAGE)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003D3D RID: 15677 RVA: 0x0014139C File Offset: 0x0013F59C
	private void SetupParam()
	{
		if (this.m_levelInformation == null)
		{
			this.m_levelInformation = ObjUtil.GetLevelInformation();
		}
		if (this.m_modelParam == null)
		{
			this.m_modelParam = ObjMoveTrap.MODEL_PARAMS[0];
			if (this.m_levelInformation != null && !this.IsTutorialCheck())
			{
				int randomRange = ObjUtil.GetRandomRange100();
				if (randomRange < this.m_levelInformation.MoveTrapBooRand)
				{
					this.m_modelParam = ObjMoveTrap.MODEL_PARAMS[1];
				}
			}
		}
	}

	// Token: 0x04003535 RID: 13621
	private static readonly ObjMoveTrap.ModelParam[] MODEL_PARAMS = new ObjMoveTrap.ModelParam[]
	{
		new ObjMoveTrap.ModelParam("obj_cmn_movetrap", "obj_missile_shoot", "ef_com_explosion_m01"),
		new ObjMoveTrap.ModelParam("obj_cmn_boomboo", "obj_ghost_s", "ef_com_explosion_m01")
	};

	// Token: 0x04003536 RID: 13622
	private ObjMoveTrapParameter m_param;

	// Token: 0x04003537 RID: 13623
	private LevelInformation m_levelInformation;

	// Token: 0x04003538 RID: 13624
	private ObjMoveTrap.ModelParam m_modelParam;

	// Token: 0x02000916 RID: 2326
	private enum ModelType
	{
		// Token: 0x0400353A RID: 13626
		Missile,
		// Token: 0x0400353B RID: 13627
		Boo,
		// Token: 0x0400353C RID: 13628
		NUM,
		// Token: 0x0400353D RID: 13629
		NONE
	}

	// Token: 0x02000917 RID: 2327
	private class ModelParam
	{
		// Token: 0x06003D3E RID: 15678 RVA: 0x00141420 File Offset: 0x0013F620
		public ModelParam(string modelName, string seName, string effectName)
		{
			this.m_modelName = modelName;
			this.m_seName = seName;
			this.m_effectName = effectName;
		}

		// Token: 0x0400353E RID: 13630
		public string m_modelName;

		// Token: 0x0400353F RID: 13631
		public string m_seName;

		// Token: 0x04003540 RID: 13632
		public string m_effectName;
	}
}
