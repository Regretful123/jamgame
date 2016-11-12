using UnityEngine;

namespace Jordan
{
    public class InteractionBehavior : MonoBehaviour
    {
        public bool IsTouching;
        private Vector3 orgScale;
        private Quaternion orgRotation;
        private Quaternion targetRotation;
        public float ScaleFactor = 3f;
        public float duration = 0.5f;
        public AnimationCurve AnimCurve = new AnimationCurve();
        private float i;
        public bool returnToOriginalRotation;

        // Use this for initialization
        void Start()
        {
            orgScale = transform.localScale;
            orgRotation = transform.rotation;
        }

        /// <summary>
        /// Return a better linear interpolate between two vectors without having to clamp between zero and one
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private Vector3 Interpolate(Vector3 a, Vector3 b, float t)
        {
            return t * b + (1 - t) * a;
        }

        public void OnStartSelect()
        {
            // do nothing right now.
        }

        private float AngleSnap(float a )
        {
            return Mathf.Round(a/90) * 90;
        }

        public void OnEndSelect()
        {
            // make the cube faced correctly or face towards the world snapped.
            Vector3 currRot = transform.localEulerAngles;
            currRot.x = AngleSnap(currRot.x);
            currRot.y = AngleSnap(currRot.y);
            currRot.z = AngleSnap(currRot.z);
            targetRotation = Quaternion.Euler(currRot);
        }

        // rotate the object based from the camera orientation.. How??? Local space??? The code below only works on local orientation!!! FUCK
        // what about angle forces? 
        public void UpdateRotation()
        {
            Vector3 currMousePos = Input.mousePosition;
            float xOrt = currMousePos.x - GameManager.PrevMousePosition.x;
            float yOrt = currMousePos.y - GameManager.PrevMousePosition.y;
            transform.Rotate(-Vector3.up, xOrt, Space.World);
            transform.Rotate(Vector3.right, yOrt, Space.World);
            GameManager.PrevMousePosition = Input.mousePosition;
        }

        // Update is called once per frame

        // if we want the cube to return to it's original rotation then we need to add something in here that will help us utilize that.
        void FixedUpdate()
        {
            IsTouching = gameObject == GameManager.SelectedObject;

            // we would have to do a switch statement here. 
            if (IsTouching)
            {
                i += Time.fixedDeltaTime / duration;
            }
            else
            {
                // return back to normal if possible
                i -= Time.fixedDeltaTime / duration;
                if (returnToOriginalRotation)
                {
                    if (transform.rotation != orgRotation)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, orgRotation, Time.deltaTime * 10f);
                    }
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
                }
            }

            i = Mathf.Clamp01(i);
            transform.localScale = Interpolate(orgScale, orgScale * ScaleFactor, AnimCurve.Evaluate(i));
        }
    }
}