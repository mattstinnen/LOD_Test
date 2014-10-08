using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCR_QuadTree : MonoBehaviour {

	SCR_QuadNode m_TopNode;
	public int m_MaxDepth;
	float m_Size = 10;
	public Vector3[] m_StartPoints = new Vector3[4];
	// Use this for initialization
	void Start ()
	{
		m_TopNode = new SCR_QuadNode(0,m_StartPoints,m_Size);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetAllPoints(ref List<Vector3[]> pointList)
	{
		Debug.Log("QUADTREE GETALLPOINTS");
		m_TopNode.GetAllPoints(ref pointList);
	}
	
	public void ResetTree()
	{
		m_TopNode.Clear();
	}
	public void SplitTopNode()//fortesting
	{
		m_TopNode.SplitAll();
	}
	
	
}
