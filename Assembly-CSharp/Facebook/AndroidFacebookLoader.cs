using System;

namespace Facebook
{
	// Token: 0x02000003 RID: 3
	public class AndroidFacebookLoader : FB.CompiledFacebookLoader
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002E08 File Offset: 0x00001008
		protected override IFacebook fb
		{
			get
			{
				return FBComponentFactory.GetComponent<AndroidFacebook>(IfNotExist.AddNew);
			}
		}
	}
}
