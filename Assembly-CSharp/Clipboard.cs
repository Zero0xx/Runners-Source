using System;

// Token: 0x02000A31 RID: 2609
public class Clipboard
{
	// Token: 0x17000955 RID: 2389
	// (set) Token: 0x06004540 RID: 17728 RVA: 0x00163CE0 File Offset: 0x00161EE0
	public static string text
	{
		set
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (string.IsNullOrEmpty(Clipboard.m_oldText))
				{
					Clipboard.m_oldText = value;
				}
				else
				{
					Clipboard.m_oldText = value;
				}
			}
			if (Binding.Instance != null)
			{
				Binding.Instance.SetClipBoard(value);
			}
		}
	}

	// Token: 0x17000956 RID: 2390
	// (get) Token: 0x06004541 RID: 17729 RVA: 0x00163D30 File Offset: 0x00161F30
	public static string oldText
	{
		get
		{
			return Clipboard.m_oldText;
		}
	}

	// Token: 0x040039DC RID: 14812
	private static string m_oldText;
}
