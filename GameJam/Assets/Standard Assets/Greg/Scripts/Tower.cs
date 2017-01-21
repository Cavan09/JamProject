using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

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

    public List<Floor> m_ListOfFloors;


    private int m_TopFloor; //Top and bottom floor will track the index of those floors within the list of floors
    private int m_BottomFloor;

    public Floor m_Floor0;
    public Floor m_Floor1;
    public Floor m_Floor2;


	// Use this for initialization
	void Start ()
    {
        m_ListOfFloors.Add(m_Floor0);
        m_ListOfFloors.Add(m_Floor1);
        m_ListOfFloors.Add(m_Floor2);
	}
	
	// Update is called once per frame
	void Update ()
    {
		/*  To determine the Top and Bottom floors, we need to query all active floors within the list.
         *  The top and bottom floors can be the same if all players are on the same level.
         *  Each player will have a current floor variable that can be compared to the list to determine what floors have players
         *  Since we will be creating floors from bottom to top, 
         * 
         * 
         * 
         * */


	}

   public void CheckFloorStatus()
    {
        Debug.Log(m_ListOfFloors[0].m_IsActive);
        Debug.Log(m_ListOfFloors[1].m_IsActive);
    }

    void GenerateFloors()   //Called at runtime, generates all floors in the buildings
    {

    }

    int NextFloor() //Next Floor returns the index of the floor a player will move to after reaching the end of their current floor
    {
        return 0;
    }
}
