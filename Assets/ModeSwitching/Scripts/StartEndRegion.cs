using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSwitching
{
    public class StartEndRegion : MonoBehaviour
    {
        public GameObject begin;
        public GameObject end;
        private float regionEnterTime, regionExitTime, regionTime;

        // Use this for initialization
        void Start()
        {
            regionEnterTime = 0f; regionExitTime = 0f; regionTime = 0f;
        }

        // Exitdate is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "BeginLine") regionEnterTime = Time.time;
            if (collision.gameObject.name == "EndLine") regionExitTime = Time.time;
        }

        public float RegionTime { get { return regionExitTime - regionEnterTime; } }
    }
}
