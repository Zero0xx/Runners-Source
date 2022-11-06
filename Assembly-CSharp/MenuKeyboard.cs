using System;
using UnityEngine;

// Token: 0x0200046B RID: 1131
public class MenuKeyboard : MonoBehaviour
{
	// Token: 0x060021DA RID: 8666 RVA: 0x000CBBEC File Offset: 0x000C9DEC
	public void Open()
	{
		this.m_keyboard = TouchScreenKeyboard.Open(string.Empty, TouchScreenKeyboardType.Default);
		this.m_isOpen = true;
		this.m_isDone = false;
		this.m_isCanceled = false;
	}

	// Token: 0x1700048B RID: 1163
	// (get) Token: 0x060021DB RID: 8667 RVA: 0x000CBC20 File Offset: 0x000C9E20
	// (set) Token: 0x060021DC RID: 8668 RVA: 0x000CBC28 File Offset: 0x000C9E28
	public bool IsDone
	{
		get
		{
			return this.m_isDone;
		}
		private set
		{
			this.m_isDone = value;
		}
	}

	// Token: 0x1700048C RID: 1164
	// (get) Token: 0x060021DD RID: 8669 RVA: 0x000CBC34 File Offset: 0x000C9E34
	// (set) Token: 0x060021DE RID: 8670 RVA: 0x000CBC3C File Offset: 0x000C9E3C
	public bool IsCanceled
	{
		get
		{
			return this.m_isCanceled;
		}
		private set
		{
			this.m_isCanceled = value;
		}
	}

	// Token: 0x1700048D RID: 1165
	// (get) Token: 0x060021DF RID: 8671 RVA: 0x000CBC48 File Offset: 0x000C9E48
	// (set) Token: 0x060021E0 RID: 8672 RVA: 0x000CBC50 File Offset: 0x000C9E50
	public string InputText
	{
		get
		{
			return this.m_inputText;
		}
		private set
		{
			this.m_inputText = value;
		}
	}

	// Token: 0x060021E1 RID: 8673 RVA: 0x000CBC5C File Offset: 0x000C9E5C
	private void Start()
	{
	}

	// Token: 0x060021E2 RID: 8674 RVA: 0x000CBC60 File Offset: 0x000C9E60
	private void Update()
	{
	}

	// Token: 0x060021E3 RID: 8675 RVA: 0x000CBC64 File Offset: 0x000C9E64
	private void OnGUI()
	{
		if (this.m_keyboard != null && this.m_isOpen)
		{
			this.m_inputText = this.m_keyboard.text;
			if (this.m_keyboard.done)
			{
				this.m_isDone = true;
				this.m_keyboard = null;
				this.m_isOpen = false;
			}
			else if (this.m_keyboard.wasCanceled)
			{
				this.m_isCanceled = true;
				this.m_keyboard = null;
				this.m_isOpen = false;
			}
		}
	}

	// Token: 0x04001E97 RID: 7831
	private TouchScreenKeyboard m_keyboard;

	// Token: 0x04001E98 RID: 7832
	private string m_inputText = string.Empty;

	// Token: 0x04001E99 RID: 7833
	private bool m_isOpen;

	// Token: 0x04001E9A RID: 7834
	private bool m_isDone;

	// Token: 0x04001E9B RID: 7835
	private bool m_isCanceled;
}
