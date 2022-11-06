using System;
using UnityEngine;

// Token: 0x0200008C RID: 140
[AddComponentMenu("NGUI/Interaction/Input Validator")]
[RequireComponent(typeof(UIInput))]
public class UIInputValidator : MonoBehaviour
{
	// Token: 0x0600039B RID: 923 RVA: 0x000117EC File Offset: 0x0000F9EC
	private void Start()
	{
		base.GetComponent<UIInput>().validator = new UIInput.Validator(this.Validate);
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00011808 File Offset: 0x0000FA08
	private char Validate(string text, char ch)
	{
		if (this.logic == UIInputValidator.Validation.None || !base.enabled)
		{
			return ch;
		}
		if (this.logic == UIInputValidator.Validation.Integer)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && text.Length == 0)
			{
				return ch;
			}
		}
		else if (this.logic == UIInputValidator.Validation.Float)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && text.Length == 0)
			{
				return ch;
			}
			if (ch == '.' && !text.Contains("."))
			{
				return ch;
			}
		}
		else if (this.logic == UIInputValidator.Validation.Alphanumeric)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch;
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.logic == UIInputValidator.Validation.Username)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch - 'A' + 'a';
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.logic == UIInputValidator.Validation.Name)
		{
			char c = (text.Length <= 0) ? ' ' : text[text.Length - 1];
			if (ch >= 'a' && ch <= 'z')
			{
				if (c == ' ')
				{
					return ch - 'a' + 'A';
				}
				return ch;
			}
			else if (ch >= 'A' && ch <= 'Z')
			{
				if (c != ' ' && c != '\'')
				{
					return ch - 'A' + 'a';
				}
				return ch;
			}
			else if (ch == '\'')
			{
				if (c != ' ' && c != '\'' && !text.Contains("'"))
				{
					return ch;
				}
			}
			else if (ch == ' ' && c != ' ' && c != '\'')
			{
				return ch;
			}
		}
		return '\0';
	}

	// Token: 0x04000266 RID: 614
	public UIInputValidator.Validation logic;

	// Token: 0x0200008D RID: 141
	public enum Validation
	{
		// Token: 0x04000268 RID: 616
		None,
		// Token: 0x04000269 RID: 617
		Integer,
		// Token: 0x0400026A RID: 618
		Float,
		// Token: 0x0400026B RID: 619
		Alphanumeric,
		// Token: 0x0400026C RID: 620
		Username,
		// Token: 0x0400026D RID: 621
		Name
	}
}
