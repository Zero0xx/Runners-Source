using System;
using UnityEngine;

// Token: 0x0200083E RID: 2110
public class GetBonus : MonoBehaviour
{
	// Token: 0x0600390E RID: 14606 RVA: 0x0012E9E4 File Offset: 0x0012CBE4
	public void AddBonusMngObject(GameObject obj)
	{
		this.m_bonus_mng_object = obj;
	}

	// Token: 0x0600390F RID: 14607 RVA: 0x0012E9F0 File Offset: 0x0012CBF0
	public void SetBonusCount(GameObject obj)
	{
		if (this.m_bonus_mng_object)
		{
			this.m_bonus_mng_object.SendMessage("OnTake", obj, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x04002FE9 RID: 12265
	private GameObject m_bonus_mng_object;
}
