using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
[AddComponentMenu("NGUI/Examples/Equip Items")]
public class EquipItems : MonoBehaviour
{
	// Token: 0x06000284 RID: 644 RVA: 0x0000AC94 File Offset: 0x00008E94
	private void Start()
	{
		if (this.itemIDs != null && this.itemIDs.Length > 0)
		{
			InvEquipment invEquipment = base.GetComponent<InvEquipment>();
			if (invEquipment == null)
			{
				invEquipment = base.gameObject.AddComponent<InvEquipment>();
			}
			int max = 12;
			int i = 0;
			int num = this.itemIDs.Length;
			while (i < num)
			{
				int num2 = this.itemIDs[i];
				InvBaseItem invBaseItem = InvDatabase.FindByID(num2);
				if (invBaseItem != null)
				{
					invEquipment.Equip(new InvGameItem(num2, invBaseItem)
					{
						quality = (InvGameItem.Quality)UnityEngine.Random.Range(0, max),
						itemLevel = NGUITools.RandomRange(invBaseItem.minItemLevel, invBaseItem.maxItemLevel)
					});
				}
				else
				{
					global::Debug.LogWarning("Can't resolve the item ID of " + num2);
				}
				i++;
			}
		}
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x04000119 RID: 281
	public int[] itemIDs;
}
