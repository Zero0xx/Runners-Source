using System;

namespace Message
{
	// Token: 0x0200058B RID: 1419
	public class MsgAssetBundleResponseFailed : MessageBase
	{
		// Token: 0x06002AEF RID: 10991 RVA: 0x00109804 File Offset: 0x00107A04
		public MsgAssetBundleResponseFailed(AssetBundleRequest request, AssetBundleResult result) : base(61519)
		{
			this.m_request = request;
			this.m_result = result;
		}

		// Token: 0x04002757 RID: 10071
		public AssetBundleRequest m_request;

		// Token: 0x04002758 RID: 10072
		public AssetBundleResult m_result;
	}
}
