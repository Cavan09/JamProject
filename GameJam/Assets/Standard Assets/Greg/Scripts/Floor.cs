using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public Tower m_Tower;
    public GameObject m_Overlay;
    public GameObject m_Entrance;
    public GameObject m_Exit;
    public Collider m_HalfwayPoint;

    public bool m_IsActive;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

  public void ActivateFloor()
    {
        m_IsActive = true;
        m_Overlay.SetActive(false);
        //TODO: Remove any overlays hiding the contents of this floor
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object has entered");
        //Enable the next floor within the list

        m_Tower.m_ListOfFloors[m_Tower.m_ListOfFloors.IndexOf(this) + 1].ActivateFloor();
    }

private void OnTriggerExit(Collider other)
    {
        Debug.Log("Object has left");
    }
}
