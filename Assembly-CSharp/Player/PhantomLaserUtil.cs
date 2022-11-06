using System;

namespace Player
{
	// Token: 0x020009B4 RID: 2484
	public class PhantomLaserUtil
	{
		// Token: 0x060040F5 RID: 16629 RVA: 0x00151B90 File Offset: 0x0014FD90
		public static void ChangeVisualOnEnter(CharacterState context)
		{
			StateUtil.EnableChildObject(context, context.BodyModelName, false);
			StateUtil.EnableChildObject(context, CharacterDefs.PhantomBodyName[0], true);
			StateUtil.EnablePlayerCollision(context, false);
			StateUtil.SetActiveEffect(context, "ef_ph_laser_lp01", true);
			StateUtil.SetShadowActive(context, false);
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x00151BD4 File Offset: 0x0014FDD4
		public static void ChangeVisualOnLeave(CharacterState context)
		{
			StateUtil.SetShadowActive(context, true);
			StateUtil.EnableChildObject(context, context.BodyModelName, true);
			StateUtil.EnableChildObject(context, CharacterDefs.PhantomBodyName[0], false);
			StateUtil.EnablePlayerCollision(context, true);
			StateUtil.SetActiveEffect(context, "ef_ph_laser_lp01", false);
		}

		// Token: 0x040037A9 RID: 14249
		public const string EffectName = "ef_ph_laser_lp01";

		// Token: 0x040037AA RID: 14250
		public const string SEName = "phantom_laser_shoot";
	}
}
