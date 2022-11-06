using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000142 RID: 322
	public class ChaoUtility
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x0003A81C File Offset: 0x00038A1C
		public static ChaoType GetChaoType(GameObject obj)
		{
			if (obj != null)
			{
				if (obj.name == "MainChao")
				{
					return ChaoType.MAIN;
				}
				if (obj.name == "SubChao")
				{
					return ChaoType.SUB;
				}
			}
			return ChaoType.MAIN;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0003A864 File Offset: 0x00038A64
		public static ShaderType GetChaoShaderType(GameObject parentObj, ChaoType type)
		{
			if (parentObj != null)
			{
				string name = (type != ChaoType.MAIN) ? "SubChao" : "MainChao";
				ChaoState chaoState = GameObjectUtil.FindChildGameObjectComponent<ChaoState>(parentObj, name);
				if (chaoState != null)
				{
					return chaoState.ShaderOffset;
				}
			}
			return ShaderType.NORMAL;
		}
	}
}
