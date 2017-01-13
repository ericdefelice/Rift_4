using System;
using UnityEngine;

namespace Rift4
{
    public class BattleCameraController : MonoBehaviour
    {
        //
        // Public Members
        //
        public float m_camAngle;                  // The angle at which the camera faces downward towards the scene
        public float m_maxRadius;                 // The radius around a line coming up from where the camera is facing, in which the camera rotates around
        public float m_camStepSize;               // The distance to move when the camera is zoomed in/out

        //
        // Private Members
        //
        private GameObject m_cameraRig;           // A reference to the main camera rig, this is for camera movement and rotation
        private GameObject m_cameraObj;           // A reference to the camera object, to rotate to look downwards
        private GameObject m_cameraAimObj;        // A reference to the collider for where the camera is aiming, used to collide with level bounds
        private GameObject m_selectedChar;        // A reference to the currently selected character, this is to be able to re-center on him/her
        private int m_currCamLevel;               // The current cam level of zoom : 0 is furthest away

        //
        // Unity MonoBehavior Functions
        //
        private void Awake()
        {

        }

    }
}
