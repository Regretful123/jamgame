using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private int scaleRatio = 4;
    public GameObject obj_Background;

    // find a way to spawn objects into array of list. 
    // preferrably withhin the distance of the camera viewport. 


    void Awake()
    {
        //if (obj_Background == null )
        Camera cam = Camera.main;
        Vector3 TopRight = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        Vector3 Bottomleft = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));
        
        // shows the boundary of top left corner, and the width/height of the dimension
        Vector4 bound = new Vector4( );

        float sizeset = obj_Background.transform.localScale.magnitude * scaleRatio;

        for ( int i = 0; i <  )
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
