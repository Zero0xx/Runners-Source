using System;

namespace Message
{
	// Token: 0x0200058A RID: 1418
	public class MsgAssetBundleResponseSucceed : MessageBase
	{
		// Token: 0x06002AEE RID: 10990 RVA: 0x001097E8 File Offset: 0x001079E8
		public MsgAssetBundleResponseSucceed(AssetBundleRequest request, AssetBundleResult result) : base(61518)
		{
			this.m_request = request;
			this.m_result = result;
		}

		// Token: 0x04002755 RID: 10069
		public AssetBundleRequest m_request;

		// Token: 0x04002756 RID: 10070
		public AssetBundleResult m_result;
	}
}
