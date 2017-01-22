using System;
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


    public FloorDirection ExitDirection = FloorDirection.Right;
    public bool m_IsActive;

    private static float xPosition = 0.0f;
    private bool m_HasPlayer = false;
    public bool GetPlayer
    {
        get
        {
            return m_HasPlayer;
        }
    }

    private readonly Dictionary<Enum, Func<GameObject, bool>> CheckLevelUpdate = new Dictionary<Enum, Func<GameObject, bool>>()
    {
        {FloorDirection.Right, HasPassedRight },
        {FloorDirection.Left, HasPassedLeft }

    };

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateFloor(Vector3 position)
    {
        m_IsActive = true;
        //m_Overlay.SetActive(false);
        //TODO: Remove any overlays hiding the contents of this floor
        transform.position = new Vector3(position.x, position.y + GetComponent<SpriteRenderer>().bounds.size.y, position.z);
    }

    internal void AddTower(Tower tower)
    {
        this.m_Tower = tower;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(collision.tag, "Player", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
        {
            if (CheckLevelUpdate[ExitDirection](collision.gameObject))
            {
                m_Tower.CheckIsLastFloor(this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object has entered");
        //Enable the next floor within the list

        //m_Tower.m_ListOfFloors[m_Tower.m_ListOfFloors.IndexOf(this) + 1].ActivateFloor();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Object has left");
        if (System.Text.RegularExpressions.Regex.IsMatch(other.tag, "Player", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
        {
            var nextFloor = m_Tower.NextFloor(this);
            //other.GetComponent<Player>().MoveToNextFloor(nextFloor);
        }
    }

    public static bool HasPassedRight(GameObject player)
    {
        if (player.transform.position.x < xPosition)
        {
            return true;
        }

        return false;
    }

    public static bool HasPassedLeft(GameObject player)
    {
        if (player.transform.position.x > xPosition)
        {
            return true;
        }

        return false;
    }

    internal void DeactivateFloor()
    {
        throw new NotImplementedException();
    }
}
