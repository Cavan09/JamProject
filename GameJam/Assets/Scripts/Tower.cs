using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private const string FloorPrefabPath = @"LevelPrefabs/";

    /*  Tower class needs a list of all floors
     *  The "Bottom" floor and "Top" floors that are active (we dont need a list of all active floors
     *  
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * */

    public List<GameObject> m_ListOfFloors;
    public List<GameObject> m_ActiveFloors;

    public int m_MaxActiveFloors = 2;
    private int m_TopFloor; //Top and bottom floor will track the index of those floors within the list of floors
    private int m_BottomFloor;


    // Use this for initialization
    void Start()
    {
        CreateFloors();
        GenerateFloors();
    }

    private void CreateFloors()
    {
        var floors = GetAllFloorPrefabs();
        for(int i = 0; i < floors.Count; i++)
        {
            m_ListOfFloors.Add(GameObject.Instantiate(floors[i], new Vector3(0, 0, -1), Quaternion.identity));
            m_ListOfFloors[i].GetComponent<Floor>().AddTower(this);
        }
    }

    public void CheckFloorStatus()
    {
    }

    void GenerateFloors()   //Called at runtime, generates all floors in the buildings
    {
        for (int i = 0; i < m_MaxActiveFloors; i++)
        {
            AddFloor();
        }
    }

    public Transform UpdateCameraTarget()
    {
        return m_ActiveFloors.LastOrDefault().transform;
    }

    public GameObject NextFloor(Floor currentFloor) //Next Floor returns the index of the floor a player will move to after reaching the end of their current floor
    {
        return m_ListOfFloors.ElementAt(m_ActiveFloors.IndexOf(currentFloor.gameObject) + 1);
    }

    private void ActivateFloor(GameObject floor)
    {
        floor.SetActive(true);
        Floor currentFloor = floor.GetComponent<Floor>();
        floor.transform.parent = this.gameObject.transform;
        currentFloor.transform.localPosition = Vector3.zero;
        var lastPosition = Vector3.zero;
        if(m_ActiveFloors.Count < 2)
        {
            float offset = floor.GetComponent<SpriteRenderer>().bounds.size.y;
            lastPosition = m_ActiveFloors.LastOrDefault().gameObject.transform.position;
            lastPosition = new Vector3(lastPosition.x, lastPosition.y - offset, lastPosition.z);
        }
        else
        {
            lastPosition = m_ActiveFloors[m_ActiveFloors.Count() - 2].gameObject.transform.position;
        }
        currentFloor.ActivateFloor(lastPosition);
    }

    private void AddFloor()
    {
        var inActiveFloors = m_ListOfFloors.Where(x => !x.GetComponent<Floor>().m_IsActive);
        int randomIndex = UnityEngine.Random.Range(0, inActiveFloors.Count());
        m_ActiveFloors.Add(inActiveFloors.ElementAt(randomIndex));
        ActivateFloor(m_ActiveFloors.LastOrDefault());
    }
    private void RemoveFloor()
    {
        int index = m_ListOfFloors.IndexOf(m_ActiveFloors.FirstOrDefault());
        m_ListOfFloors.ElementAt(index).GetComponent<Floor>().DeactivateFloor();
        m_ActiveFloors.RemoveAt(0);
    }

    private void UpdateFloors()
    {
        AddFloor();
        RemoveFloor();
    }

    public void CheckIsLastFloor(Floor currentFloor)
    {
        if (currentFloor.gameObject.Equals(m_ActiveFloors.LastOrDefault()))
        {
            UpdateFloors();
        }
    }

    private List<GameObject> GetAllFloorPrefabs()
    {
        List<GameObject> retval = new List<GameObject>();
        var levels = Resources.LoadAll(FloorPrefabPath);
        foreach (var level in levels)
        {
            retval.Add(level as GameObject);
        }
        return retval;
    }
}

public enum FloorDirection
{
    Right,
    Left
}
