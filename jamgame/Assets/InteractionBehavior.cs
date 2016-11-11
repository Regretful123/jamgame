using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class InteractionBehavior : MonoBehaviour {

    public enum State { Increasing, Amass, Decreasing, Original }
    public State state = State.Original;
    public bool IsTouching;
    private Vector3 orgScale;
    private Quaternion orgRotation;
    public float ScaleFactor = 3f;
    public float duration = 0.5f;
    public AnimationCurve AnimCurve = new AnimationCurve();
    private float i;
    private Rigidbody rb;

    private float x;
    private float y;

    public bool returnToOriginalRotation;

	// Use this for initialization
	void Start ()
    {
        orgScale = transform.localScale;
        orgRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    public void OnStartTouch()
    {
        IsTouching = true;
    }

    public void OnEndTouch()
    {
        IsTouching = false;
    }

    // rotate the object based from the camera orientation.. How??? Local space?
    // what about angle forces? 
    public void UpdateRotation()
    {
        Vector3 currMousePos = Input.mousePosition;
        Vector3 OrgRotEul = orgRotation.eulerAngles;
        Vector3 DeltaPos = new Vector3( OrgRotEul.x + currMousePos.y - GameManager.PrevMousePosition.y,
                                        OrgRotEul.y + currMousePos.x - GameManager.PrevMousePosition.x,
                                        OrgRotEul.z + currMousePos.z - GameManager.PrevMousePosition.z);
        transform.rotation = Quaternion.Euler(DeltaPos);

    }

	// Update is called once per frame

        // if we want the cube to return to it's original rotation then we need to add something in here that will help us utilize that.
	void FixedUpdate ()
    {
        IsTouching = this.gameObject == GameManager.SelectedObject;
        
        // we would have to do a switch statement here. 
        if( IsTouching )
        {
            i += Time.fixedDeltaTime / duration;
        }   
        else
        {
            // return back to normal if possible
            i -= Time.fixedDeltaTime / duration;
            if ( returnToOriginalRotation )
            {
                if( transform.rotation != orgRotation )
                {
                    rb.angularVelocity = Vector3.zero;
                    transform.rotation = Quaternion.Slerp(transform.rotation, orgRotation, Time.fixedDeltaTime * 10f);
                }
            }
        }     
        
        i = Mathf.Clamp01(i);
        transform.localScale = Vector3.Lerp(orgScale, orgScale * ScaleFactor, AnimCurve.Evaluate(i));
    }
}
