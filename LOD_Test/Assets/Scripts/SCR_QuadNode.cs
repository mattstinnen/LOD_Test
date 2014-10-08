using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SCR_QuadNode // each node holds 4 subnodes and 4 verts basically each node is a quad to be rendered higher depth higher resolution
 {

	SCR_QuadNode[] m_Nodes = new SCR_QuadNode[4];
	int m_Depth;
	Vector3[] m_Points = new Vector3[4];
	float m_Size;
	public int GetDepth()
	{
		return m_Depth;
	}
	
	public SCR_QuadNode()
	{
		m_Depth = -1;
	}
	
	public SCR_QuadNode(int depth, Vector3[] points,float size)
	{
		m_Depth 	= depth;
		m_Points 	= points;
		m_Size 		= size;
		for(int i = 0; i < m_Nodes.Length; ++i)
		{
			m_Nodes[i] = null;
		}
	}
	
	public void Clear() // clears the tree recursivly 
	{
		for(int i = 0; i < m_Nodes.Length; ++i)
		{
			if(m_Nodes[i]!= null)
			{
				m_Nodes[i].Clear();
				m_Nodes[i] = null;
			}
		}
	}
	
	
	
	public void Split() // splits node into 4 nodes/ calculate new positions 
	{
		if(m_Nodes[0] == null) // if we know the node has not been split
		{
			float newSize = m_Size *0.5f;
			Vector3[] newPoints = new Vector3[4];
			
			//BOTTOM LEFT
			newPoints[1] 	= m_Points[1];//TL
			newPoints[1].z 	= m_Points[1].z - newSize;
			
			newPoints[0] 	= m_Points[0];//BL
			
			newPoints[2] 	= m_Points[2];//TR
			newPoints[2].z 	= m_Points[2].z - newSize;
			newPoints[2].x 	= m_Points[2].x - newSize;
			
			newPoints[3] 	= m_Points[3];//BR
			newPoints[3].x 	= m_Points[3].x - newSize;
			
			
			
			m_Nodes[0] = new SCR_QuadNode(m_Depth+1,newPoints,newSize);
			newPoints = new Vector3[4];
			
			
			// TOP LEFT
			newPoints[1] 	= m_Points[1];//TL
			
			newPoints[0] 	= m_Points[0];//BL
			newPoints[0].z 	= m_Points[0].z + newSize;
			
			newPoints[2] 	= m_Points[2];//TR
			newPoints[2].x 	= m_Points[2].x - newSize;
			
			newPoints[3] 	= m_Points[3];//BR
			newPoints[3].z 	= m_Points[3].z + newSize;
			newPoints[3].x 	= m_Points[3].x - newSize;
			
		
			m_Nodes[1] = new SCR_QuadNode(m_Depth+1,newPoints,newSize);
			newPoints = new Vector3[4];
			//TOP RIGHT
			newPoints[1] 	= m_Points[1];//TL
			newPoints[1].x 	= m_Points[1].x + newSize;
			
			newPoints[0] 	= m_Points[0];//BL
			newPoints[0].x 	= m_Points[0].x + newSize;
			newPoints[0].z 	= m_Points[0].z + newSize;
			
			newPoints[2] 	= m_Points[2];//TR
			
			newPoints[3] 	= m_Points[3];//BR
			newPoints[3].z 	= m_Points[3].z + newSize;
		
			m_Nodes[2] = new SCR_QuadNode(m_Depth+1,newPoints,newSize);
			newPoints = new Vector3[4];
			//BOTTOM RIGHT
			newPoints[1] 	= m_Points[1];//TL
			newPoints[1].x 	= m_Points[1].x + newSize;
			newPoints[1].z 	= m_Points[1].z - newSize;
			
			newPoints[0] 	= m_Points[0];//BL
			newPoints[0].x 	= m_Points[0].x + newSize;
			
			newPoints[2] 	= m_Points[2];//TR
			newPoints[2].z 	= m_Points[2].z - newSize;
			
			newPoints[3] 	= m_Points[3];//BR
			
			m_Nodes[3] = new SCR_QuadNode(m_Depth+1,newPoints,newSize);
		}
	}
	
	public void SplitAll() //TODO:: THIS WORKS FOR FIRST ITERATION BUT NOT SECOND * UNITY LOCKS UP*/stack overflows .... think its calling itself too manytimes, problem with null check?
	{
		// if we dont have anything lower we should split
		if(m_Nodes[0] == null)
		{
			Split();
		}
		else // go a layer deeper
		{
			SplitAll();
		}
	}
	
	public Vector3[] GetPoints()
	{
		return m_Points;
	}

	public void GetAllPoints(ref List<Vector3[]> pointList) // recursivly gathers all available points
	{
		Debug.Log("GET ALL POINTS NODE Depth-" + m_Depth);
		bool gatherPoints = true;
		for(int i = 0; i < m_Nodes.Length; ++i)
		{
			Debug.Log("null check" + (m_Nodes[i] == null));
			if(m_Nodes[i] != null) // if we are not at the bottom of the tree go deeper
			{
				gatherPoints = false;
				m_Nodes[i].GetAllPoints(ref pointList);
			}
		}
		
		if(gatherPoints == true) // if this is as far as we can go we should add to the list
		{
			
			pointList.Add(m_Points);
		}
	}
}
