using UnityEngine;
using System.Collections;

public class SimpleRotate : MonoBehaviour {

    private enum State { Rotating, Idle }
    private State state = State.Idle;

    public float time = 2f;
    public float magnitude = 2f;
    public AnimationCurve animOffset;

    /// <summary>
    /// Tells whether the cube are still in transition or not.
    /// </summary>
    public bool IsTransition { get { return state == State.Rotating; } }
    private float offsetX = 0;
    private float offsetY = 0;
    private float i = 0;
    private Vector3 orgpos;
    private Quaternion orgrot;

    void Awake()
    {
        orgpos = transform.position;
        orgrot = transform.rotation;
        offsetX = orgpos.x;
        offsetY = orgpos.y;
    }

    public void BeginRotate()
    {
        // call the function to begin rotating the object.
        state = State.Rotating;
    }

    private void RotateThis()
    {

    }

    // Update is called once per frame
    void Update ()
    {
	    switch( state )
        {
            case State.Idle: break;
            case State.Rotating: RotateThis(); break;
        }
	}
}
