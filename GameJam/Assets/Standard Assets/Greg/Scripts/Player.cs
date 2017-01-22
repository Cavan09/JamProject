using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator m_Animator;

    public Vector3 m_JumpVector;
    public float m_JumpForce = 1.0f;

    public bool m_IsGrounded;

    Rigidbody m_RigidBody;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_JumpVector = new Vector3(0.0f, 2.0f, 0.0f);
    }       
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-0.05f, 0.0f, 0.0f);
            m_Animator.SetBool("IsMoving", true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.05f, 0.0f, 0.0f);
            m_Animator.SetBool("IsMoving", true);
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_Animator.SetBool("IsCrouching", true);
        }
        else
        {
            m_Animator.SetBool("IsCrouching", false);
        }

            if (Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.A) == false)
        {
            m_Animator.SetBool("IsMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && m_IsGrounded)
        {

            m_RigidBody.AddForce(m_JumpVector * m_JumpForce, ForceMode.Impulse);
            m_Animator.SetBool("IsGrounded", false);
            m_IsGrounded = false;
        } 
    }
    
    private void OnCollisionStay(Collision collision)
    {
        m_IsGrounded = true;
        m_Animator.SetBool("IsGrounded", true);
    }
}
