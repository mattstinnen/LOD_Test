using UnityEngine;
using System.Collections;

public class SCR_MovePointTest : MonoBehaviour {

	public Vector3[] m_Points = new Vector3[4];
	// Use this for initialization
	void Start () 
	{
		m_Points[0] = new Vector3(0.0f,0.0f,0.0f);
		m_Points[1] = new Vector3(0.0f,0.0f,1.0f);
		m_Points[2] = new Vector3(1.0f,0.0f,1.0f);
		m_Points[3] = new Vector3(1.0f,0.0f,0.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Redraw();
		}
	}
	
	public void Redraw()
	{
		SCR_MeshBuilder meshBuilder = new SCR_MeshBuilder();
		meshBuilder.Vertices.AddRange(m_Points);
		meshBuilder.AddTriangle(0,1,2);
		meshBuilder.AddTriangle(0,2,3);
		
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
