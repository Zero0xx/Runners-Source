using System;
using GameScore;
using UnityEngine;

namespace Player
{
	// Token: 0x02000968 RID: 2408
	public class CharacterDefs
	{
		// Token: 0x040035EF RID: 13807
		public const int NumMaxTrick = 5;

		// Token: 0x040035F0 RID: 13808
		public static readonly string[] PhantomBodyName = new string[]
		{
			"pha_laser",
			"pha_spin",
			"pha_asteroid"
		};

		// Token: 0x040035F1 RID: 13809
		public static readonly Vector3 BaseFrontTangent = new Vector3(1f, 0f, 0f);

		// Token: 0x040035F2 RID: 13810
		public static readonly Vector3 BaseRightTangent = new Vector3(0f, 0f, -1f);

		// Token: 0x040035F3 RID: 13811
		public static readonly int[] TrickScore = new int[]
		{
			Data.Trick1,
			Data.Trick2,
			Data.Trick3,
			Data.Trick4,
			Data.Trick5
		};
	}
}
