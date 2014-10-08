using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SCR_QuadNode // each node holds 4 subnodes and 4 verts basically each node is a quad to be rendered higher depth higher resolution
 {

	List<SCR_QuadNode> m_Nodes = new List<SCR_QuadNode>();
	int m_Depth;
	List<Vector3> m_Points = new List<Vector3>();
	float m_Size;
	public int GetDepth()
	{
		return m_Depth;
	}
	
	public SCR_QuadNode()
	{
		m_Depth = -1;
	}
	
	public SCR_QuadNode(int depth, List<Vector3> points,float size)
	{
		m_Depth 	= depth;
		m_Points 	= points;
		m_Size 		= size;
		for(int i = 0; i < m_Nodes.Count; ++i)
		{
			m_Nodes[i] = null;
		}
	}
	
	public void Clear() // clears the tree recursivly 
	{
		for(int i = 0; i < m_Nodes.Count; ++i)
		{
			if(m_Nodes[i]!= null)
			{
				m_Nodes.Clear();
			}
		}
	}
	
	List<Vector3> GenQuadPoints(float newSize)
	{
		Debug.Log(newSize);
		List<Vector3> newPoints = new List<Vector3>();
		
		//BOTTOM LEFT 
		newPoints.Add(new Vector3(m_Points[0].x, m_Points[0].y, m_Points[0].z));//BL
		newPoints.Add(new Vector3(m_Points[1].x, m_Points[1].y, m_Points[1].z - newSize));//TL
		newPoints.Add(new Vector3(m_Points[2].x - newSize, m_Points[2].y, m_Points[2].z - newSize));//TR
		newPoints.Add(new Vector3(m_Points[3].x - newSize, m_Points[3].y, m_Points[3].z));//BR
		
		Debug.Log(newPoints[newPoints.Count-1]);
		
		// TOP LEFT
		newPoints.Add(new Vector3(m_Points[0].x, m_Points[0].y, m_Points[0].z + newSize));//BL
		newPoints.Add(new Vector3(m_Points[1].x, m_Points[1].y, m_Points[1].z));//TL
		newPoints.Add(new Vector3(m_Points[2].x - newSize, m_Points[2].y, m_Points[2].z));//TR
		newPoints.Add(new Vector3(m_Points[3].x - newSize, m_Points[3].y, m_Points[3].z + newSize));//BR
		
		Debug.Log(newPoints[newPoints.Count-1]);
		
		//TOP RIGHT
		newPoints.Add(new Vector3(m_Points[0].x + newSize, m_Points[0].y, m_Points[0].z + newSize));//BL
		newPoints.Add(new Vector3(m_Points[1].x + newSize, m_Points[1].y, m_Points[1].z));//TL
		newPoints.Add(new Vector3(m_Points[2].x, m_Points[2].y, m_Points[2].z));//TR
		newPoints.Add(new Vector3(m_Points[3].x, m_Points[3].y, m_Points[3].z + newSize));//BR
		
		Debug.Log(newPoints[newPoints.Count-1]);
		
		//BOTTOM RIGHT
		newPoints.Add(new Vector3(m_Points[0].x + newSize, m_Points[0].y, m_Points[0].z));//BL
		newPoints.Add(new Vector3(m_Points[1].x + newSize, m_Points[1].y, m_Points[1].z - newSize));//TL
		newPoints.Add(new Vector3(m_Points[2].x, m_Points[2].y, m_Points[2].z - newSize));//TR
		newPoints.Add(new Vector3(m_Points[3].x, m_Points[3].y, m_Points[3].z));//BR
		
		Debug.Log(newPoints[newPoints.Count-1]);
		
		return newPoints;
	}
	
	public void Split() // splits node into 4 nodes/ calculate new positions 
	{
		if(m_Nodes.Count == 0) // if we know the node has not been split
		{
			float newSize = m_Size *0.5f;
			List<Vector3> newPoints = GenQuadPoints(newSize);
			
			for(int i = 0; i < 4; ++i)
				m_Nodes.Add(new SCR_QuadNode(m_Depth + 1, newPoints.GetRange(i * 4, 4), newSize));
		}
	}
	
	public void SplitAll() //TODO:: THIS WORKS FOR FIRST ITERATION BUT NOT SECOND * UNITY LOCKS UP*/stack overflows .... think its calling itself too manytimes, problem with null check?
	{
		// if we dont have anything lower we should split
		if(m_Nodes.Count == 0)
		{
			Split();
		}
		else // go a layer deeper
		{
			foreach(SCR_QuadNode node in m_Nodes)
			{
				node.SplitAll();
			}
		}
	}
	
	public List<Vector3> GetPoints()
	{
		return m_Points;
	}

	public void GetAllPoints(ref List<List<Vector3>> pointList) // recursivly gathers all available points
	{
		bool gatherPoints = true;
		for(int i = 0; i < m_Nodes.Count; ++i)
		{
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
