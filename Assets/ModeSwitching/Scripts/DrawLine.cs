using Meta;
using Meta.HandInput;
using UnityEngine;

namespace ModeSwitching
{
    public class DrawLine : MonoBehaviour
    {
        public GameObject penMarker;
        public GameObject lr;

        private GameObject clone;
        private bool draw;
        private float penDownTime, penUpTime, penTime;

        // Use this for initialization
        void Start()
        {
            penMarker.GetComponent<Renderer>().material.color = Color.red;
            penDownTime = 0f; penUpTime = 0f; penTime = 0f; draw = false;
        }

        // Update is called once per frame
        void Update () {
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Board")
            {
                penMarker.GetComponent<Renderer>().material.color = Color.green;
                penDownTime = Time.time;

                clone = Instantiate(lr);
                clone.tag = "clone";
                clone.GetComponent<LineRenderer>().positionCount = 0;

                draw = true;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if(collision.gameObject.name == "Board" && draw)
            {
                Draw();
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.name == "Board")
            {
                penMarker.GetComponent<Renderer>().material.color = Color.red;
                penUpTime = Time.time;
                draw = false;
            }
        }

        private void Draw()
        {
            Vector3 pos = transform.position;
            pos.z = 0.975f;
            clone.GetComponent<LineRenderer>().positionCount++;
            clone.GetComponent<LineRenderer>().SetPosition(clone.GetComponent<LineRenderer>().positionCount - 1, pos);
        }

        public float PenTime { get { return penDownTime - penUpTime; } }
    }
}
