using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCR_Tess_Cube : MonoBehaviour 
{
	public Vector3 m_Position = new Vector3(0,0,0); // position of center of cube
	public float m_Radius = 1; //size of cube L/W/H
	public int m_LOD = 1; // number of quads per dimention
	
	public void Start()
	{
		GenerateCube();
	}
	
	public void GenerateCube()
	{
		SCR_MeshBuilder meshBuilder = new SCR_MeshBuilder();
		float m_Width = m_Radius/m_LOD;
		for (int i = 0; i <= m_LOD; i++)
		{
			float z = (m_Width * i) - (m_Radius * 0.5f);
			float v = (1.0f / m_LOD) * i;
			
			for (int j = 0; j <= m_LOD; j++)
			{
				float x = (m_Width * j) - (m_Radius * 0.5f);
				float u = (1.0f / m_LOD) * j;
				
				Vector3 offset = new Vector3(x, m_Position.y + m_Radius, z);
				Vector2 uv = new Vector2(u, v);
				bool buildTriangles = i > 0 && j > 0;
				BuildQuadForGrid(meshBuilder, offset, uv, buildTriangles, m_LOD + 1);
				
			}
		}
		
		//Create the mesh:
		MeshFilter filter = GetComponent<MeshFilter>();
		
		if (filter != null)
		{
			filter.sharedMesh = meshBuilder.CreateMesh();
			filter.sharedMesh.RecalculateNormals();
		}
	}
	
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