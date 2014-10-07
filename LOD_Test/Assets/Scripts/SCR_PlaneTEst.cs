using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCR_PlaneTEst : MonoBehaviour {
	public float m_Size = 1;
	public int m_Resolution = 10;
	public int m_CurrentResolution = 10;
	public Vector3[,] m_Verts;
	void Start()
	{
		m_Verts = new Vector3[m_Resolution+1,m_Resolution+1];
		//generate highest resolution verticies
		for (int i = 0; i <= m_Resolution; i++)
		{
			float z = m_Size * i;
			for (int j = 0; j <= m_Resolution; j++)
			{
				float x = m_Size * j;
				Vector3 offset = new Vector3(x, m_Size, z);	
				m_Verts[j,i] = offset;
			}
		}
		
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Redraw();
		}
	}
	
	void Redraw() // this currently works for the entire face
	{
		SCR_MeshBuilder meshBuilder = new SCR_MeshBuilder();
		
		int resMath = m_Resolution/m_CurrentResolution; // use this to skip over unused verts
		
		
		for (int i = 0; i <= m_Resolution;)
		{
			//float z = m_Size * i;
			float v = i%2;//(1.0f / m_SegmentCount) * i;
			
			for (int j = 0; j <= m_Resolution;)
			{
				//float x = m_Size * j;
				float u = j%2;//(1.0f / m_SegmentCount) * j;
				
				//Vector3 offset = new Vector3(x, m_Size, z);
				Vector2 uv = new Vector2(u, v);
				bool buildTriangles = i > 0 && j > 0;
				BuildQuadForGrid(meshBuilder, m_Verts[j,i], uv, buildTriangles, m_CurrentResolution + 1);
				j+= resMath;
			}
			i+= resMath;
		}
		
		//Create the mesh:
		MeshFilter filter = GetComponent<MeshFilter>();
		
		if (filter != null)
		{
			filter.sharedMesh = meshBuilder.CreateMesh();
			filter.sharedMesh.RecalculateNormals();
		}
	}
/*	
	void BuildQuad(SCR_MeshBuilder meshBuilder, Vector3 offset)
	{
		meshBuilder.Vertices.Add(new Vector3(0.0f, 0.0f, 0.0f) + offset);
		meshBuilder.UVs.Add(new Vector2(0.0f, 0.0f));
		meshBuilder.Normals.Add(Vector3.up);
		
		meshBuilder.Vertices.Add(new Vector3(0.0f, 0.0f, m_Length) + offset);
		meshBuilder.UVs.Add(new Vector2(0.0f, 1.0f));
		meshBuilder.Normals.Add(Vector3.up);
		
		meshBuilder.Vertices.Add(new Vector3(m_Width, 0.0f, m_Length) + offset);
		meshBuilder.UVs.Add(new Vector2(1.0f, 1.0f));
		meshBuilder.Normals.Add(Vector3.up);
		
		meshBuilder.Vertices.Add(new Vector3(m_Width, 0.0f, 0.0f) + offset);
		meshBuilder.UVs.Add(new Vector2(1.0f, 0.0f));
		meshBuilder.Normals.Add(Vector3.up);
		
		int baseIndex = meshBuilder.Vertices.Count - 4;
		
		meshBuilder.AddTriangle(baseIndex, baseIndex + 1, baseIndex + 2);
		meshBuilder.AddTriangle(baseIndex, baseIndex + 2, baseIndex + 3);
	}
	*/
	void BuildQuadForGrid(SCR_MeshBuilder meshBuilder, Vector3 position, Vector2 uv, 
	                      bool buildTriangles, int vertsPerRow)
	{
		meshBuilder.Vertices.Add(position);
		meshBuilder.UVs.Add(uv);
		
		if (buildTriangles)
		{
			int baseIndex = meshBuilder.Vertices.Count - 1;
			
			int index0 = baseIndex;
			int index1 = baseIndex - 1;
			int index2 = baseIndex - vertsPerRow;
			int index3 = baseIndex - vertsPerRow - 1;
			
			meshBuilder.AddTriangle(index0, index2, index1);
			meshBuilder.AddTriangle(index2, index3, index1);
		}
	}
}
