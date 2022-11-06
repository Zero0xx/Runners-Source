using System;
using System.Xml;
using UnityEngine;

// Token: 0x0200028C RID: 652
internal class ImportTerrains
{
	// Token: 0x060011E2 RID: 4578 RVA: 0x00064A00 File Offset: 0x00062C00
	public static TerrainList Import(TextAsset textAsset)
	{
		if (textAsset == null)
		{
			return null;
		}
		string xml = AESCrypt.Decrypt(textAsset.text);
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(xml);
		return ImportTerrains.Import(xmlDocument);
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x00064A3C File Offset: 0x00062C3C
	public static TerrainList Import(XmlDocument document)
	{
		if (document == null)
		{
			return null;
		}
		XmlNode documentElement = document.DocumentElement;
		if (documentElement == null)
		{
			return null;
		}
		XmlNode namedItem = documentElement.Attributes.GetNamedItem("name");
		if (namedItem == null)
		{
			return null;
		}
		TerrainList terrainList = new TerrainList(namedItem.Value);
		XmlNodeList childNodes = documentElement.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlNode terrainNode = (XmlNode)obj;
			Terrain terrain = ImportTerrains.GetTerrain(terrainNode);
			terrainList.AddTerrain(terrain);
		}
		return terrainList;
	}

	// Token: 0x060011E4 RID: 4580 RVA: 0x00064AFC File Offset: 0x00062CFC
	private static Terrain GetTerrain(XmlNode terrainNode)
	{
		XmlNode namedItem = terrainNode.Attributes.GetNamedItem("name");
		if (namedItem == null)
		{
			return null;
		}
		XmlNode xmlNode = terrainNode.SelectSingleNode("Meter");
		if (xmlNode == null)
		{
			return null;
		}
		string value = namedItem.Value;
		string value2 = xmlNode.Attributes.GetNamedItem("value").Value;
		Terrain terrain = new Terrain(value, float.Parse(value2));
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (object obj in childNodes)
		{
			XmlNode blockNode = (XmlNode)obj;
			TerrainBlock terrainBlock = ImportTerrains.GetTerrainBlock(blockNode);
			if (terrainBlock != null)
			{
				terrain.AddTerrainBlock(terrainBlock);
			}
		}
		return terrain;
	}

	// Token: 0x060011E5 RID: 4581 RVA: 0x00064BE8 File Offset: 0x00062DE8
	private static TerrainBlock GetTerrainBlock(XmlNode blockNode)
	{
		if (blockNode == null)
		{
			return null;
		}
		XmlNode namedItem = blockNode.Attributes.GetNamedItem("name");
		if (namedItem == null)
		{
			return null;
		}
		string value = namedItem.Value;
		if (value == null)
		{
			return null;
		}
		TransformParam transformParam = ImportTerrains.GetTransformParam(blockNode);
		if (transformParam == null)
		{
			return null;
		}
		return new TerrainBlock(value, transformParam);
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x00064C40 File Offset: 0x00062E40
	private static TransformParam GetTransformParam(XmlNode blockNode)
	{
		XmlNode xmlNode = blockNode.SelectSingleNode("Position");
		if (xmlNode == null)
		{
			return null;
		}
		Vector3 pos = default(Vector3);
		float floatValue = ImportTerrains.GetFloatValue(xmlNode, "posX");
		float floatValue2 = ImportTerrains.GetFloatValue(xmlNode, "posY");
		float floatValue3 = ImportTerrains.GetFloatValue(xmlNode, "posZ");
		pos.Set(floatValue, floatValue2, floatValue3);
		XmlNode xmlNode2 = blockNode.SelectSingleNode("Rotation");
		if (xmlNode2 == null)
		{
			return null;
		}
		Vector3 rot = default(Vector3);
		float floatValue4 = ImportTerrains.GetFloatValue(xmlNode2, "rotX");
		float floatValue5 = ImportTerrains.GetFloatValue(xmlNode2, "rotY");
		float floatValue6 = ImportTerrains.GetFloatValue(xmlNode2, "rotZ");
		rot.Set(floatValue4, floatValue5, floatValue6);
		return new TransformParam(pos, rot);
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x00064CFC File Offset: 0x00062EFC
	private static float GetFloatValue(XmlNode node, string attributeName)
	{
		if (node == null)
		{
			return 0f;
		}
		XmlNode namedItem = node.Attributes.GetNamedItem(attributeName);
		if (namedItem == null)
		{
			return 0f;
		}
		string value = namedItem.Value;
		if (value == null)
		{
			return 0f;
		}
		return float.Parse(value);
	}
}
