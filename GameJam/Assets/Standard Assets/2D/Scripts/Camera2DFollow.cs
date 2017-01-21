using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
        public float m_yOffset = 0.1f;
        public List<GameObject> players = null;
        public float ScaleMultiplyer = 0.5f;

        private float m_OffsetZ;
        private float m_Scale;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
        private Camera m_CameraComponent;
        private Vector3 target;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = players.FirstOrDefault().transform.position;
            m_OffsetZ = (transform.position - players.FirstOrDefault().transform.position).z;
            transform.parent = null;
            m_CameraComponent = GetComponent<Camera>();
            m_Scale = m_CameraComponent.orthographicSize;
        }


        // Update is called once per frame
        private void Update()
        {
            //Update target Postion
            target = UpdateStartPosition();
            Debug.DrawLine(transform.position, target, Color.red);
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = target;

            m_CameraComponent.orthographicSize = ScaleBasedOnPlayerPos() + m_Scale;
        }

        private Vector3 UpdateStartPosition()
        {
            Vector3 retval = Vector3.zero;
            GameObject furthestPlayer = GetFurthestPlayerPos();
            GameObject FurthestFromPlayer = null;
            Vector3 diff = Vector3.zero;

            foreach (var player in players)
            {
                diff = player.transform.position - furthestPlayer.transform.position;
                if (diff.sqrMagnitude > retval.sqrMagnitude)
                {
                    FurthestFromPlayer = player;
                    retval = diff;
                }
            }

            return (furthestPlayer.transform.localPosition - FurthestFromPlayer.transform.localPosition) * 0.5f;
        }

        private float ScaleBasedOnPlayerPos()
        {
            float retval = 0;
            GameObject furthest = GetFurthestPlayerPos();
            Vector3 position = transform.position;

            Vector3 diff = furthest.transform.position.normalized - position.normalized;
            float distance = diff.sqrMagnitude;
            retval = distance;

            return retval * ScaleMultiplyer;
        }

        private GameObject GetFurthestPlayerPos()
        {
            GameObject retval = players.First();
            float currDist = 0.0f;

            foreach(var player in players)
            {
                Vector3 diff = player.transform.position.normalized - transform.position.normalized;
                float distance = diff.magnitude;
                if (distance > currDist)
                {
                    currDist = distance;
                    retval = player;
                }
            }

            return retval;
        }
    }
}
