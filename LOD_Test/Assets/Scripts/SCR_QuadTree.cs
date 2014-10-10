using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCR_QuadTree : MonoBehaviour {

	SCR_QuadNode m_TopNode;
	public int m_MaxDepth;
	float m_Size = 10;
	public List<Vector3> m_StartPoints = new List<Vector3>();
	// Use this for initialization
	void Start ()
	{
		m_TopNode = new SCR_QuadNode(0,m_StartPoints,m_Size);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetAllPoints(ref List<List<Vector3>> pointList)
	{
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
	
	public void FuzeBottomNodes()// for testing 
	{
		m_TopNode.FuzeBottomNode();
	}
	
	
}
