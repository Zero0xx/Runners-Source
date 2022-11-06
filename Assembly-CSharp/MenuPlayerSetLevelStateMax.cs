using System;
using Text;
using UnityEngine;

// Token: 0x020004C5 RID: 1221
public class MenuPlayerSetLevelStateMax : MenuPlayerSetLevelState
{
	// Token: 0x06002416 RID: 9238 RVA: 0x000D8410 File Offset: 0x000D6610
	private void Start()
	{
	}

	// Token: 0x06002417 RID: 9239 RVA: 0x000D8414 File Offset: 0x000D6614
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
			gameObject.SetActive(false);
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "Lbl_word_max");
		if (gameObject2 != null)
		{
			gameObject2.SetActive(true);
		}
	}

	// Token: 0x06002418 RID: 9240 RVA: 0x000D84B4 File Offset: 0x000D66B4
	private void LateUpdate()
	{
	}
}
