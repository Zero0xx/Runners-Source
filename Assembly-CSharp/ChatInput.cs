using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
[RequireComponent(typeof(UIInput))]
[AddComponentMenu("NGUI/Examples/Chat Input")]
public class ChatInput : MonoBehaviour
{
	// Token: 0x060002C6 RID: 710 RVA: 0x0000C3B4 File Offset: 0x0000A5B4
	private void Start()
	{
		this.mInput = base.GetComponent<UIInput>();
		if (this.fillWithDummyData && this.textList != null)
		{
			for (int i = 0; i < 30; i++)
			{
				this.textList.Add(string.Concat(new object[]
				{
					(i % 2 != 0) ? "[AAAAAA]" : "[FFFFFF]",
					"This is an example paragraph for the text list, testing line ",
					i,
					"[-]"
				}));
			}
		}
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x0000C448 File Offset: 0x0000A648
	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Return))
		{
			if (!this.mIgnoreNextEnter && !this.mInput.selected)
			{
				this.mInput.label.maxLineCount = 1;
				this.mInput.selected = true;
			}
			this.mIgnoreNextEnter = false;
		}
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x0000C4A0 File Offset: 0x0000A6A0
	public void OnSubmit()
	{
		if (this.textList != null)
		{
			string text = NGUITools.StripSymbols(this.mInput.value);
			if (!string.IsNullOrEmpty(text))
			{
				this.textList.Add(text);
				this.mInput.value = string.Empty;
				this.mInput.selected = false;
			}
		}
		this.mIgnoreNextEnter = true;
	}

	// Token: 0x04000178 RID: 376
	public UITextList textList;

	// Token: 0x04000179 RID: 377
	public bool fillWithDummyData;

	// Token: 0x0400017A RID: 378
	private UIInput mInput;

	// Token: 0x0400017B RID: 379
	private bool mIgnoreNextEnter;
}
