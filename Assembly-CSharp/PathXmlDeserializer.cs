using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x020002A1 RID: 673
public class PathXmlDeserializer
{
	// Token: 0x060012A9 RID: 4777 RVA: 0x000675A4 File Offset: 0x000657A4
	public static IEnumerator CreatePathObjectData(TextAsset xmlData, Dictionary<string, ResPathObjectData> dictonary)
	{
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(xmlData.text);
		yield return null;
		XmlNodeList pathObjDataNode = doc.GetElementsByTagName("ResPathObjectData");
		foreach (object obj in pathObjDataNode)
		{
			XmlNode rootNode = (XmlNode)obj;
			ResPathObjectData pathObj = new ResPathObjectData();
			pathObj.name = rootNode.Attributes["Name"].Value;
			int indexAt = pathObj.name.IndexOf("@");
			if (indexAt >= 0)
			{
				pathObj.name = pathObj.name.Substring(0, indexAt);
			}
			pathObj.name = pathObj.name.ToLower();
			int value = int.Parse(rootNode.Attributes["PlaybackType"].Value);
			pathObj.playbackType = (byte)value;
			int value2 = int.Parse(rootNode.Attributes["Flags"].Value);
			pathObj.flags = (byte)value2;
			pathObj.numKeys = ushort.Parse(rootNode.Attributes["NumKeys"].Value);
			pathObj.length = float.Parse(rootNode.Attributes["Length"].Value);
			pathObj.distance = new float[(int)pathObj.numKeys];
			pathObj.position = new Vector3[(int)pathObj.numKeys];
			pathObj.normal = new Vector3[(int)pathObj.numKeys];
			pathObj.tangent = new Vector3[(int)pathObj.numKeys];
			XmlNode distance = rootNode.SelectSingleNode("Distance");
			PathXmlDeserializer.ParseFloatArray(distance, pathObj.distance, (int)pathObj.numKeys);
			XmlNode position = rootNode.SelectSingleNode("Position");
			PathXmlDeserializer.ParseVector3Array(position, pathObj.position, (int)pathObj.numKeys);
			XmlNode normal = rootNode.SelectSingleNode("Normal");
			PathXmlDeserializer.ParseVector3Array(normal, pathObj.normal, (int)pathObj.numKeys);
			XmlNode tangent = rootNode.SelectSingleNode("Tangent");
			PathXmlDeserializer.ParseVector3Array(tangent, pathObj.tangent, (int)pathObj.numKeys);
			XmlNode vertNode = rootNode.SelectSingleNode("Vertices");
			pathObj.numVertices = uint.Parse(vertNode.Attributes["value"].Value);
			if (pathObj.numVertices > 0U)
			{
				pathObj.vertices = new Vector3[pathObj.numVertices];
				PathXmlDeserializer.ParseVector3Array(vertNode, pathObj.vertices, (int)pathObj.numVertices);
			}
			Vector3 vec = default(Vector3);
			XmlNode minNode = rootNode.SelectSingleNode("Min");
			XmlNode item = minNode.SelectSingleNode("item");
			PathXmlDeserializer.ParseVector3(item, ref vec);
			pathObj.min = vec;
			vec = default(Vector3);
			XmlNode maxNode = rootNode.SelectSingleNode("Max");
			item = maxNode.SelectSingleNode("item");
			PathXmlDeserializer.ParseVector3(item, ref vec);
			pathObj.max = vec;
			pathObj.uid = (ulong)uint.Parse(rootNode.Attributes["UniqueID"].Value);
			dictonary.Add(pathObj.name, pathObj);
		}
		yield return null;
		yield break;
	}

	// Token: 0x060012AA RID: 4778 RVA: 0x000675D4 File Offset: 0x000657D4
	private static void ParseFloatArray(XmlNode node, float[] array, int numOfArray)
	{
		if (node != null)
		{
			XmlNodeList xmlNodeList = node.SelectNodes("item");
			if (xmlNodeList.Count == numOfArray && xmlNodeList.Count > 0)
			{
				int num = 0;
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					float.TryParse(xmlNode.InnerText, out array[num]);
					num++;
				}
			}
			else
			{
				global::Debug.Log("Array Num is not Equal keyNum");
			}
		}
	}

	// Token: 0x060012AB RID: 4779 RVA: 0x0006768C File Offset: 0x0006588C
	private static void ParseVector3Array(XmlNode node, Vector3[] array, int numOfArray)
	{
		if (node != null)
		{
			XmlNodeList xmlNodeList = node.SelectNodes("item");
			if (xmlNodeList.Count == numOfArray && xmlNodeList.Count > 0)
			{
				int num = 0;
				foreach (object obj in xmlNodeList)
				{
					XmlNode node2 = (XmlNode)obj;
					PathXmlDeserializer.ParseVector3(node2, ref array[num]);
					num++;
				}
			}
			else
			{
				global::Debug.Log("Array Num is not Equal keyNum");
			}
		}
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x00067740 File Offset: 0x00065940
	private static void ParseVector3(XmlNode node, ref Vector3 vec)
	{
		if (node != null)
		{
			float.TryParse(node.Attributes["X"].Value, out vec.x);
			float.TryParse(node.Attributes["Y"].Value, out vec.y);
			float.TryParse(node.Attributes["Z"].Value, out vec.z);
		}
	}
}
