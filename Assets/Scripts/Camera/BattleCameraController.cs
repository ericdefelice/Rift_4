using System;
using UnityEngine;

namespace Rift4
{
    public class BattleCameraController : MonoBehaviour
    {
        //
        // Enumerations
        //
        public enum CameraState
        {
            FREE_MOVE   = 0,
            ROTATING    = 1,
            ZOOMING     = 2,
            RECENTERING = 3
        }


        //
        // Public Members
        //
        [Range(0f, 90f)]
        public float m_camAngle = 45f;            // The angle at which the camera faces downward towards the scene
        public float m_maxDistance;               // The radius around a line coming up from where the camera is facing, in which the camera rotates around
        public float m_camStepSize;               // The distance to move when the camera is zoomed in/out
        public float m_rotateTime;                // The time to take the slerp when rotating camera
        public float m_zoomTime;                  // The time to take when lerping the camera zoom
        [Range(0, 3)]
        public int m_camRotationPos = 0;

        //
        // Private Members
        //
        [SerializeField] private GameObject m_camRig;     // A reference to the main camera rig, this is for camera movement, collision component on this object
        [SerializeField] private GameObject m_camRotObj;  // A reference to the camera rotation object, Y-axis to spin around and X-axis for camera angle
        [SerializeField] private GameObject m_camPosObj;  // A reference to the camera object, to set the local position for camera offset
        
        //private GameObject m_cameraAimObj;                // A reference to the collider for where the camera is aiming, used to collide with level bounds
        //private GameObject m_selectedChar;                // A reference to the currently selected character, this is to be able to re-center on him/her
        
        private int m_currCamLevel;                       // The current cam level of zoom : 0 is furthest away
        private CameraState m_camState;                   // The current camera state
        private Quaternion m_camTargetRotation;           // The target camera rotation
        private Quaternion m_camCurrentRotation;          // The current camera rotation
        private Vector3 m_camTargetPosition;              // The target camera position
        private Vector3 m_camCurrentPosition;             // The current camera position

        //
        // Unity MonoBehavior Functions
        //
        private void Awake()
        {
            // Set the camera to the furthest position by default
            m_camTargetRotation = Quaternion.Euler(new Vector3(m_camAngle, 45f, 0f));
            m_camCurrentRotation = m_camTargetRotation;

            // Set the initial camera position
            m_camTargetPosition = new Vector3(0f, 0f, -m_maxDistance);
            m_camCurrentPosition = m_camTargetPosition;
            
            // Set camera into free move state
            m_camState = CameraState.FREE_MOVE;

            // Call the update functions with no lerping
            UpdateRotation(false);
            UpdatePosition(false);
        }

        private void Update()
        {
            SetTargetRotation();
            SetTargetPosition();

            UpdateRotation(true);
            UpdatePosition(true);
        }

        // TODO(ebd): Move this to InputManager later
        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                m_camRotationPos++;

            if (Input.GetKeyDown(KeyCode.E))
                m_camRotationPos--;

            if (m_camRotationPos < 0)
                m_camRotationPos = 3;
            else if (m_camRotationPos > 3)
                m_camRotationPos = 0;
        }


        //
        // Private Functions
        //
        private void SetTargetRotation()
        {
            m_camTargetRotation = Quaternion.Euler(m_camAngle, 45f + m_camRotationPos*90f, 0f);
        }

        private void SetTargetPosition()
        {
            m_camTargetPosition = new Vector3(0f, 0f, -m_maxDistance);
        }

        private void UpdateRotation(bool useLerp)
        {
            if (useLerp)
            {
                m_camCurrentRotation = m_camTargetRotation;
                m_camRotObj.transform.rotation = m_camCurrentRotation;
            }
            else
            {
                m_camCurrentRotation = m_camTargetRotation;
                m_camRotObj.transform.rotation = m_camCurrentRotation;
            }
        }

        private void UpdatePosition(bool useLerp)
        {
            if (useLerp)
            {
                m_camCurrentPosition = m_camTargetPosition;
                m_camPosObj.transform.localPosition = m_camCurrentPosition;
            }
            else
            {
                m_camCurrentPosition = m_camTargetPosition;
                m_camPosObj.transform.localPosition = m_camCurrentPosition;
            }
        }

    }
}
