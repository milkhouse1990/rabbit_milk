using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(AvgEngine))]
    public class AvgEngineInput : MonoBehaviour {

    private AvgEngine m_avg;
    private void Awake()
    {
        m_avg = GetComponent<AvgEngine>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (CrossPlatformInputManager.GetButtonDown("A"))
            m_avg.NextPage();

	}
}
}