using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class TUIMeshCircle : MonoBehaviour
{
	public bool Static = true;

	public Material material;

	public string frameName;

	public Color color = Color.white;

	public float radius = 50f;

	public int sectors = 15;

	public Vector2 textureOffset;

	protected Material materialClone;

	protected MeshFilter meshFilter;

	protected MeshRenderer meshRender;

	public void Awake()
	{
		if ((bool)material)
		{
			materialClone = UnityEngine.Object.Instantiate(material) as Material;
			materialClone.hideFlags = HideFlags.DontSave;
		}
		else
		{
			materialClone = null;
		}
		meshFilter = base.gameObject.GetComponent<MeshFilter>();
		meshRender = base.gameObject.GetComponent<MeshRenderer>();
		meshFilter.sharedMesh = new Mesh();
		meshFilter.sharedMesh.hideFlags = HideFlags.DontSave;
		meshRender.castShadows = false;
		meshRender.receiveShadows = false;
	}

	public void Start()
	{
		UpdateMesh();
	}

	private void OnDestroy()
	{
		if ((bool)meshFilter && (bool)meshFilter.sharedMesh)
		{
			UnityEngine.Object.DestroyImmediate(meshFilter.sharedMesh);
		}
		if ((bool)materialClone)
		{
			UnityEngine.Object.DestroyImmediate(materialClone);
		}
	}

	public void Update()
	{
		if (!Static)
		{
			UpdateMesh();
		}
	}

	public virtual void UpdateMesh()
	{
		if (meshFilter == null || meshRender == null)
		{
			return;
		}
		TUITextureManager.Frame frame = TUITextureManager.Instance().GetFrame(frameName);
		Vector2 vector = new Vector2((frame.uv.z - frame.uv.x) / frame.size.x, (frame.uv.w - frame.uv.y) / frame.size.y);
		Vector2 vector2 = new Vector2(frame.uv.x, frame.uv.y) + new Vector2(vector.x * textureOffset.x, vector.y * textureOffset.y);
		if ((bool)materialClone)
		{
			materialClone.mainTexture = frame.texture;
			meshRender.sharedMaterial = materialClone;
		}
		else if ((bool)frame.material)
		{
			meshRender.sharedMaterial = frame.material;
		}
		List<Vector3> list = new List<Vector3>();
		List<Vector2> list2 = new List<Vector2>();
		List<Color> list3 = new List<Color>();
		List<int> list4 = new List<int>();
		list.Add(new Vector3(0f, 0f, 0f));
		list2.Add(vector2);
		list3.Add(color);
		float num = 360f / (float)sectors * ((float)Math.PI / 180f);
		for (int i = 0; i < sectors; i++)
		{
			float f = num * (float)i;
			float num2 = Mathf.Cos(f) * radius;
			float num3 = Mathf.Sin(f) * radius;
			list.Add(new Vector3(num2, num3, 0f));
			list2.Add(vector2 + new Vector2(vector.x * num2, vector.y * (0f - num3)));
			list3.Add(color);
			if (i != 0)
			{
				list4.Add(0);
				list4.Add(list.Count - 1);
				list4.Add(list.Count - 2);
				if (i == sectors - 1)
				{
					list4.Add(0);
					list4.Add(1);
					list4.Add(list.Count - 1);
				}
			}
		}
		meshFilter.sharedMesh.Clear();
		meshFilter.sharedMesh.vertices = list.ToArray();
		meshFilter.sharedMesh.uv = list2.ToArray();
		meshFilter.sharedMesh.colors = list3.ToArray();
		meshFilter.sharedMesh.triangles = list4.ToArray();
	}
}
