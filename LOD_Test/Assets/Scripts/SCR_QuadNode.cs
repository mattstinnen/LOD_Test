using UnityEngine;
using System.Collections;

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
	
	public SCR_QuadNode(int depth, Vector3[] points,float size)
	{
		m_Depth 	= depth;
		m_Points 	= points;
		m_Size 		= size;
	}
	
	public void Clear() // clears the tree recursivly 
	{
		for(int i = 0; i < m_Nodes.Length; ++i)
		{
			m_Nodes[i].Clear(); //?? not pointers so i dont need this?
			m_Nodes[i] = null;
		}
	}
	
	public void Split() // splits node into 4 nodes/ calculate new positions 
	{
		float newSize = m_Size *0.5f;
		Vector3[] newPoints = new Vector3[4];
		
		// TOP LEFT
		newPoints[0] 	= m_Points[0];//TL
		newPoints[1] 	= m_Points[1];//BL
		newPoints[1].z 	= m_Points[1].z + newSize;
		newPoints[2] 	= m_Points[2];//TR
		newPoints[2].x 	= m_Points[2].x - newSize;
		newPoints[3] 	= m_Points[3];//BR
		newPoints[3].z 	= m_Points[3].z + newSize;
		newPoints[3].x 	= m_Points[3].x - newSize;
		m_Nodes[0] = new SCR_QuadNode(m_Depth+1,newPoints,newSize);
		
		//BOTTOM LEFT
		newPoints[0] 	= m_Points[0];//TL
		newPoints[1].z 	= m_Points[1].z - newSize;
		newPoints[1] 	= m_Points[1];//BL
		newPoints[2] 	= m_Points[2];//TR
		newPoints[2].z 	= m_Points[2].z - newSize;
		newPoints[2].x 	= m_Points[2].x - newSize;
		newPoints[3] 	= m_Points[3];//BR
		newPoints[3].x 	= m_Points[3].x - newSize;
		m_Nodes[1] = new SCR_QuadNode(m_Depth+1,newPoints,newSize);
		
		//TOP RIGHT
		newPoints[0] 	= m_Points[0];//TL
		newPoints[0].x 	= m_Points[0].x + newSize;
		newPoints[1] 	= m_Points[1];//BL
		newPoints[1].x 	= m_Points[1].x + newSize;
		newPoints[1].z 	= m_Points[1].z + newSize;
		newPoints[2] 	= m_Points[2];//TR
		newPoints[3] 	= m_Points[3];//BR
		newPoints[3].z 	= m_Points[3].z + newSize;
		m_Nodes[2] = new SCR_QuadNode(m_Depth+1,newPoints,newSize);
		
		//BOTTOM RIGHT
		newPoints[0] 	= m_Points[0];//TL
		newPoints[0].x 	= m_Points[0].x + newSize;
		newPoints[0].z 	= m_Points[0].z - newSize;
		newPoints[1] 	= m_Points[1];//BL
		newPoints[1].x 	= m_Points[1].x + newSize;
		newPoints[2] 	= m_Points[2];//TR
		newPoints[2].z 	= m_Points[2].z - newSize;
		newPoints[3] 	= m_Points[3];//BR
		m_Nodes[3] = new SCR_QuadNode(m_Depth+1,newPoints,newSize);
	}
	
	public Vector3[] GetPoints()
	{
		return m_Points;
	}
	

}
