using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A4A RID: 2634
public class ObjectUtility
{
	// Token: 0x06004662 RID: 18018 RVA: 0x0016F8EC File Offset: 0x0016DAEC
	public static Dictionary<string, UISprite> GetObjectSprite(GameObject target, List<string> objectName)
	{
		Dictionary<string, UISprite> dictionary = null;
		if (objectName != null && objectName.Count > 0)
		{
			dictionary = new Dictionary<string, UISprite>();
			foreach (string text in objectName)
			{
				UISprite value = GameObjectUtil.FindChildGameObjectComponent<UISprite>(target, text);
				dictionary[text] = value;
			}
		}
		return dictionary;
	}

	// Token: 0x06004663 RID: 18019 RVA: 0x0016F974 File Offset: 0x0016DB74
	public static Dictionary<string, UILabel> GetObjectLabel(GameObject target, List<string> objectName)
	{
		Dictionary<string, UILabel> dictionary = null;
		if (objectName != null && objectName.Count > 0)
		{
			dictionary = new Dictionary<string, UILabel>();
			foreach (string text in objectName)
			{
				UILabel value = GameObjectUtil.FindChildGameObjectComponent<UILabel>(target, text);
				dictionary[text] = value;
			}
		}
		return dictionary;
	}

	// Token: 0x06004664 RID: 18020 RVA: 0x0016F9FC File Offset: 0x0016DBFC
	public static Dictionary<string, UITexture> GetObjectTexture(GameObject target, List<string> objectName)
	{
		Dictionary<string, UITexture> dictionary = null;
		if (objectName != null && objectName.Count > 0)
		{
			dictionary = new Dictionary<string, UITexture>();
			foreach (string text in objectName)
			{
				UITexture value = GameObjectUtil.FindChildGameObjectComponent<UITexture>(target, text);
				dictionary[text] = value;
			}
		}
		return dictionary;
	}

	// Token: 0x06004665 RID: 18021 RVA: 0x0016FA84 File Offset: 0x0016DC84
	public static Dictionary<string, GameObject> GetGameObject(GameObject target, List<string> objectName)
	{
		Dictionary<string, GameObject> dictionary = null;
		if (objectName != null && objectName.Count > 0)
		{
			dictionary = new Dictionary<string, GameObject>();
			foreach (string text in objectName)
			{
				GameObject value = GameObjectUtil.FindChildGameObject(target, text);
				dictionary[text] = value;
			}
		}
		return dictionary;
	}

	// Token: 0x06004666 RID: 18022 RVA: 0x0016FB0C File Offset: 0x0016DD0C
	public static string SetColorString(string org, int r, int g, int b)
	{
		string result = org;
		if (r < 0)
		{
			r = 0;
		}
		else if (r > 255)
		{
			r = 255;
		}
		if (g < 0)
		{
			g = 0;
		}
		else if (g > 255)
		{
			g = 255;
		}
		if (b < 0)
		{
			b = 0;
		}
		else if (b > 255)
		{
			b = 255;
		}
		if (r >= 0 && g >= 0 && b >= 0 && r <= 255 && g <= 255 && b <= 255)
		{
			string colorString = ObjectUtility.GetColorString(r);
			string colorString2 = ObjectUtility.GetColorString(g);
			string colorString3 = ObjectUtility.GetColorString(b);
			string str = string.Concat(new string[]
			{
				"[",
				colorString,
				colorString2,
				colorString3,
				"]"
			});
			result = str + org + "[-]";
		}
		return result;
	}

	// Token: 0x06004667 RID: 18023 RVA: 0x0016FC08 File Offset: 0x0016DE08
	private static string GetColorString(int param)
	{
		string text = "00";
		if (param >= 0 && param <= 255)
		{
			text = string.Empty;
			int num = param / 16;
			int num2 = param % 16;
			switch (num)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
				text = num.ToString();
				break;
			case 10:
				text = "a";
				break;
			case 11:
				text = "b";
				break;
			case 12:
				text = "c";
				break;
			case 13:
				text = "d";
				break;
			case 14:
				text = "e";
				break;
			case 15:
				text = "f";
				break;
			}
			switch (num2)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
				text += num2.ToString();
				break;
			case 10:
				text += "a";
				break;
			case 11:
				text += "b";
				break;
			case 12:
				text += "c";
				break;
			case 13:
				text += "d";
				break;
			case 14:
				text += "e";
				break;
			case 15:
				text += "f";
				break;
			}
		}
		return text;
	}
}
