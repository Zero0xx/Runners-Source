using System;

namespace Facebook
{
	// Token: 0x02000019 RID: 25
	public class IOSFacebookLoader : FB.CompiledFacebookLoader
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000045FC File Offset: 0x000027FC
		protected override IFacebook fb
		{
			get
			{
				return FBComponentFactory.GetComponent<IOSFacebook>(IfNotExist.AddNew);
			}
		}
	}
}
