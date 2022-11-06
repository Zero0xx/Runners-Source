using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using UnityEngine;

// Token: 0x02000278 RID: 632
public class SpawnableParser : MonoBehaviour
{
	// Token: 0x06001148 RID: 4424 RVA: 0x00062428 File Offset: 0x00060628
	public IEnumerator CreateSetData(ResourceManager resManager, TextAsset xmlFile, StageSpawnableParameterContainer stageDataList)
	{
		TimeProfiler.StartCountTime("CreateSetData:CreateSetData");
		if (xmlFile != null)
		{
			TimeProfiler.StartCountTime("CreateSetData:AESCrypt.Decrypt");
			string text_data = AESCrypt.Decrypt(xmlFile.text);
			TimeProfiler.EndCountTime("CreateSetData:AESCrypt.Decrypt");
			XmlReader reader = XmlReader.Create(new StringReader(text_data));
			if (reader != null)
			{
				reader.Read();
				if (reader.ReadToFollowing("Stage") && reader.ReadToDescendant("Block"))
				{
					do
					{
						if (reader.MoveToAttribute("ID"))
						{
							int blockID = int.Parse(reader.Value);
							reader.MoveToElement();
							if (reader.ReadToDescendant("Layer"))
							{
								yield return base.StartCoroutine(this.ReadLayer(resManager, reader, blockID, stageDataList));
							}
						}
					}
					while (reader.ReadToNextSibling("Block"));
				}
				reader.Close();
			}
		}
		TimeProfiler.EndCountTime("CreateSetData:CreateSetData");
		yield break;
	}

	// Token: 0x06001149 RID: 4425 RVA: 0x00062470 File Offset: 0x00060670
	private static Vector3 ReadVector3(XmlReader reader)
	{
		Vector3 zero = Vector3.zero;
		reader.MoveToFirstAttribute();
		do
		{
			string name = reader.Name;
			switch (name)
			{
			case "X":
				zero.x = float.Parse(reader.Value);
				break;
			case "Y":
				zero.y = float.Parse(reader.Value);
				break;
			case "Z":
				zero.z = float.Parse(reader.Value);
				break;
			}
		}
		while (reader.MoveToNextAttribute());
		reader.MoveToElement();
		return zero;
	}

	// Token: 0x0600114A RID: 4426 RVA: 0x00062554 File Offset: 0x00060754
	private static void ReadUserParameters(ResourceManager resManager, XmlReader reader, object o, Type t, string parameterName)
	{
		if (!(o is SpawnableParameter))
		{
			return;
		}
		if (reader.ReadToDescendant("It"))
		{
			do
			{
				reader.MoveToFirstAttribute();
				string name = null;
				string text = null;
				string text2 = null;
				do
				{
					string text3 = reader.Name;
					switch (text3)
					{
					case "N":
						name = reader.Value;
						break;
					case "V":
						text = reader.Value;
						break;
					case "T":
						text2 = reader.Value;
						break;
					}
				}
				while (reader.MoveToNextAttribute());
				reader.MoveToElement();
				FieldInfo field = t.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (field != null)
				{
					string text3 = text2;
					switch (text3)
					{
					case "g":
					{
						GameObject spawnableGameObject = resManager.GetSpawnableGameObject(text);
						if (spawnableGameObject != null)
						{
							field.SetValue(o, spawnableGameObject);
						}
						break;
					}
					case "f":
					{
						float num2;
						if (float.TryParse(text, out num2))
						{
							field.SetValue(o, num2);
						}
						break;
					}
					case "i":
					{
						int num3;
						if (int.TryParse(text, out num3))
						{
							field.SetValue(o, num3);
						}
						break;
					}
					case "u":
					{
						uint num4;
						if (uint.TryParse(text, out num4))
						{
							num4 = uint.Parse(text, NumberStyles.AllowHexSpecifier);
							field.SetValue(o, num4);
						}
						break;
					}
					case "s":
					{
						string text4 = text;
						if (text4 != null)
						{
							field.SetValue(o, text4);
						}
						break;
					}
					}
				}
			}
			while (reader.ReadToNextSibling("It"));
		}
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x000627A4 File Offset: 0x000609A4
	private void ReadObjects(ResourceManager resManager, XmlReader reader, BlockSpawnableParameterContainer blockData)
	{
		do
		{
			string text = null;
			uint id = 0U;
			string text2 = null;
			int num = 20;
			int num2 = 30;
			reader.MoveToFirstAttribute();
			do
			{
				string name = reader.Name;
				switch (name)
				{
				case "C":
					text = reader.Value;
					break;
				case "ID":
					id = uint.Parse(reader.Value, NumberStyles.AllowHexSpecifier);
					break;
				case "P":
					text2 = reader.Value;
					break;
				case "I":
					num = int.Parse(reader.Value);
					break;
				case "O":
					num2 = int.Parse(reader.Value);
					break;
				}
			}
			while (reader.MoveToNextAttribute());
			reader.MoveToElement();
			if (!string.IsNullOrEmpty(text))
			{
				Type type = null;
				object obj = null;
				SpawnableParameter spawnableParameter = null;
				if (!string.IsNullOrEmpty(text2))
				{
					type = Type.GetType(text2);
					if (type != null)
					{
						obj = Activator.CreateInstance(type);
						spawnableParameter = (obj as SpawnableParameter);
					}
					if (spawnableParameter == null)
					{
						type = null;
						obj = null;
						spawnableParameter = new SpawnableParameter();
						text2 = null;
					}
				}
				else
				{
					spawnableParameter = new SpawnableParameter();
				}
				spawnableParameter.ObjectName = text;
				spawnableParameter.ID = id;
				spawnableParameter.RangeIn = (float)num;
				spawnableParameter.RangeOut = (float)num2;
				XmlReader xmlReader = reader.ReadSubtree();
				while (xmlReader.Read())
				{
					string name = xmlReader.Name;
					switch (name)
					{
					case "P":
						spawnableParameter.Position = SpawnableParser.ReadVector3(xmlReader);
						break;
					case "A":
					{
						Vector3 euler = SpawnableParser.ReadVector3(xmlReader);
						spawnableParameter.Rotation = Quaternion.Euler(euler);
						break;
					}
					case "PS":
						if (!string.IsNullOrEmpty(text2))
						{
							SpawnableParser.ReadUserParameters(resManager, xmlReader, obj, type, text2);
						}
						else
						{
							xmlReader.Skip();
						}
						break;
					}
				}
				xmlReader.Close();
				blockData.AddParameter(spawnableParameter);
			}
		}
		while (reader.ReadToNextSibling("Obj"));
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x00062A5C File Offset: 0x00060C5C
	private IEnumerator ReadLayer(ResourceManager resManager, XmlReader reader, int blockID, StageSpawnableParameterContainer stageDataList)
	{
		do
		{
			if (reader.MoveToAttribute("ID"))
			{
				int layerID = int.Parse(reader.Value);
				reader.MoveToElement();
				if (reader.ReadToDescendant("Obj"))
				{
					BlockSpawnableParameterContainer blockData = new BlockSpawnableParameterContainer(blockID, layerID);
					this.ReadObjects(resManager, reader, blockData);
					stageDataList.AddData(blockID, layerID, blockData);
				}
			}
			yield return null;
		}
		while (reader.ReadToNextSibling("Layer"));
		yield break;
	}

	// Token: 0x04000F90 RID: 3984
	public static readonly string[] ObjectKeyTable = new string[]
	{
		"Obj",
		"ID",
		"C",
		"P",
		"I",
		"O",
		"P",
		"A"
	};

	// Token: 0x04000F91 RID: 3985
	public static readonly string[] ParameterKeyTable = new string[]
	{
		"PS",
		"It",
		"N",
		"T",
		"V"
	};

	// Token: 0x02000279 RID: 633
	public enum Object
	{
		// Token: 0x04000F98 RID: 3992
		Object,
		// Token: 0x04000F99 RID: 3993
		ID,
		// Token: 0x04000F9A RID: 3994
		ClassName,
		// Token: 0x04000F9B RID: 3995
		ParameterName,
		// Token: 0x04000F9C RID: 3996
		RangeIn,
		// Token: 0x04000F9D RID: 3997
		RangeOut,
		// Token: 0x04000F9E RID: 3998
		Position,
		// Token: 0x04000F9F RID: 3999
		Angle,
		// Token: 0x04000FA0 RID: 4000
		NUM
	}

	// Token: 0x0200027A RID: 634
	public enum Parameter
	{
		// Token: 0x04000FA2 RID: 4002
		Parameters,
		// Token: 0x04000FA3 RID: 4003
		Item,
		// Token: 0x04000FA4 RID: 4004
		Name,
		// Token: 0x04000FA5 RID: 4005
		Type,
		// Token: 0x04000FA6 RID: 4006
		Value,
		// Token: 0x04000FA7 RID: 4007
		NUM
	}
}
