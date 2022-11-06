using System;
using Text;
using UnityEngine;

// Token: 0x020004C4 RID: 1220
public class MenuPlayerSetLevelStateNormal : MenuPlayerSetLevelState
{
	// Token: 0x06002412 RID: 9234 RVA: 0x000D8334 File Offset: 0x000D6534
	private void Start()
	{
	}

	// Token: 0x06002413 RID: 9235 RVA: 0x000D8338 File Offset: 0x000D6538
	public override void ChangeLabels()
	{
		int level = MenuPlayerSetUtil.GetLevel(this.m_params.Character, this.m_params.Ability);
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_item_lv");
		if (uilabel != null)
		{
			uilabel.text = TextUtility.GetTextLevel(level.ToString());
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Lbl_item_price");
		if (gameObject != null)
		{
			gameObject.SetActive(true);
			UILabel component = gameObject.GetComponent<UILabel>();
			component.text = ((float)MenuPlayerSetUtil.GetAbilityCost(this.m_params.Character)).ToString();
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "Lbl_word_max");
		if (gameObject2 != null)
		{
			gameObject2.SetActive(false);
		}
	}

	// Token: 0x06002414 RID: 9236 RVA: 0x000D8404 File Offset: 0x000D6604
	private void LateUpdate()
	{
	}
}
