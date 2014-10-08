using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SCR_QuadTreePlane : MonoBehaviour {

	public SCR_QuadTree m_Tree;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			m_Tree.SplitTopNode();
			Redraw();
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			m_Tree.ResetTree();
			Redraw();
		}
	}
	
	public void Redraw()
	{
		List<List<Vector3>> pointList = new List<List<Vector3>>();
		m_Tree.GetAllPoints(ref pointList); // get all the points from tree
		
		SCR_MeshBuilder meshBuilder = new SCR_MeshBuilder();
		int indexMath = 0;// using this to jump for the start of each quad's indicies
		for(int i = 0; i < pointList.Count; ++i)
		{
			meshBuilder.Vertices.AddRange(pointList[i]);
			meshBuilder.AddTriangle(indexMath, indexMath + 1, indexMath + 2);
			meshBuilder.AddTriangle(indexMath, indexMath + 2, indexMath + 3);
			indexMath += 4;
		}
		
		//Create the mesh:
		MeshFilter filter = GetComponent<MeshFilter>();
		
		if (filter != null)
		{
			filter.sharedMesh = meshBuilder.CreateMesh();
			filter.sharedMesh.RecalculateNormals();
		}
	}
}
