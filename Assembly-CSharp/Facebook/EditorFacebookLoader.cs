using System;

namespace Facebook
{
	// Token: 0x02000007 RID: 7
	public class EditorFacebookLoader : FB.CompiledFacebookLoader
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003580 File Offset: 0x00001780
		protected override IFacebook fb
		{
			get
			{
				return FBComponentFactory.GetComponent<EditorFacebook>(IfNotExist.AddNew);
			}
		}
	}
}
