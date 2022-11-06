using System;
using UnityEngine;

namespace Player
{
	// Token: 0x020009AC RID: 2476
	public class PhantomAsteroidUtil
	{
		// Token: 0x060040C9 RID: 16585 RVA: 0x001506A8 File Offset: 0x0014E8A8
		public static GameObject ChangeVisualOnEnter(CharacterState context)
		{
			string name = CharacterDefs.PhantomBodyName[2];
			StateUtil.EnableChildObject(context, context.BodyModelName, false);
			StateUtil.EnableChildObject(context, name, true);
			StateUtil.EnablePlayerCollision(context, false);
			GameObject result = null;
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, name);
			if (gameObject != null)
			{
				result = StateUtil.CreateEffectOnTransform(context, gameObject.transform, "ef_ph_aste_lp01", false);
			}
			SoundManager.SePlay("phantom_asteroid", "SE");
			return result;
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x00150718 File Offset: 0x0014E918
		public static void ChangeVisualOnLeave(CharacterState context, GameObject effect)
		{
			StateUtil.EnableChildObject(context, context.BodyModelName, true);
			StateUtil.EnableChildObject(context, CharacterDefs.PhantomBodyName[2], false);
			StateUtil.EnablePlayerCollision(context, true);
			if (effect != null)
			{
				UnityEngine.Object.Destroy(effect);
			}
			SoundManager.SeStop("phantom_asteroid", "SE");
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x00150768 File Offset: 0x0014E968
		public static GameObject GetModelObject(CharacterState context)
		{
			return GameObjectUtil.FindChildGameObject(context.gameObject, CharacterDefs.PhantomBodyName[2]);
		}

		// Token: 0x04003775 RID: 14197
		public const string EffectName = "ef_ph_aste_lp01";

		// Token: 0x04003776 RID: 14198
		public const string SEName = "phantom_asteroid";
	}
}
