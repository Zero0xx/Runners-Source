using System;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames.Android
{
	// Token: 0x02000048 RID: 72
	internal class NoopProxy : AndroidJavaProxy
	{
		// Token: 0x06000278 RID: 632 RVA: 0x0000A824 File Offset: 0x00008A24
		internal NoopProxy(string javaInterfaceClass) : base(javaInterfaceClass)
		{
			this.mInterfaceClass = javaInterfaceClass;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A834 File Offset: 0x00008A34
		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			Logger.d("NoopProxy for " + this.mInterfaceClass + " got call to " + methodName);
			return null;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A854 File Offset: 0x00008A54
		public override AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] javaArgs)
		{
			Logger.d("NoopProxy for " + this.mInterfaceClass + " got call to " + methodName);
			return null;
		}

		// Token: 0x04000113 RID: 275
		private string mInterfaceClass;
	}
}
