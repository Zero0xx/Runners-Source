using System;
using UnityEngine;

// Token: 0x0200034D RID: 845
public abstract class ChaoGetPartsBase : MonoBehaviour
{
	// Token: 0x0600190D RID: 6413 RVA: 0x00090F04 File Offset: 0x0008F104
	public void SetCallback(ChaoGetPartsBase.AnimationEndCallback callback)
	{
		this.m_callback = callback;
	}

	// Token: 0x170003C5 RID: 965
	// (get) Token: 0x0600190E RID: 6414 RVA: 0x00090F10 File Offset: 0x0008F110
	public int ChaoId
	{
		get
		{
			return this.m_chaoId;
		}
	}

	// Token: 0x0600190F RID: 6415
	public abstract void Setup(GameObject chaoGetObjectRoot);

	// Token: 0x06001910 RID: 6416
	public abstract void PlayGetAnimation(Animation anim);

	// Token: 0x06001911 RID: 6417
	public abstract ChaoGetPartsBase.BtnType GetButtonType();

	// Token: 0x06001912 RID: 6418
	public abstract void PlayEndAnimation(Animation anim);

	// Token: 0x06001913 RID: 6419
	public abstract void PlaySE(string seType);

	// Token: 0x06001914 RID: 6420
	public abstract EasySnsFeed CreateEasySnsFeed(GameObject rootObject);

	// Token: 0x0400168A RID: 5770
	protected ChaoGetPartsBase.AnimationEndCallback m_callback;

	// Token: 0x0400168B RID: 5771
	protected int m_chaoId = -1;

	// Token: 0x0200034E RID: 846
	public enum AnimType
	{
		// Token: 0x0400168D RID: 5773
		NONE = -1,
		// Token: 0x0400168E RID: 5774
		GET_ANIM_CONTINUE,
		// Token: 0x0400168F RID: 5775
		GET_ANIM_FINISH,
		// Token: 0x04001690 RID: 5776
		OUT_ANIM,
		// Token: 0x04001691 RID: 5777
		NUM
	}

	// Token: 0x0200034F RID: 847
	public enum BtnType
	{
		// Token: 0x04001693 RID: 5779
		NONE = -1,
		// Token: 0x04001694 RID: 5780
		OK,
		// Token: 0x04001695 RID: 5781
		NEXT,
		// Token: 0x04001696 RID: 5782
		EQUIP_OK,
		// Token: 0x04001697 RID: 5783
		NUM
	}

	// Token: 0x02000A80 RID: 2688
	// (Invoke) Token: 0x0600483A RID: 18490
	public delegate void AnimationEndCallback(ChaoGetPartsBase.AnimType animType);
}
