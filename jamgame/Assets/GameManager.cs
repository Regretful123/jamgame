using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameObject SelectedObject { get; private set; }
    private static InteractionBehavior selectedBehav { get; set; }
    public static Vector3 PrevMousePosition { get; private set; }
    
    public static void AssignSelectedObject( GameObject obj )
    {
        SelectedObject = obj;
        if ( SelectedObject.GetComponent<InteractionBehavior>() != null)
        {
            selectedBehav = SelectedObject.GetComponent<InteractionBehavior>();
        }
    }

    public static void Deselect()
    {
        if( selectedBehav != null )
        {
            selectedBehav = null;
        }
        SelectedObject = null;
    }
	
	// Update is called once per frame
	void Update () {
        if( SelectedObject != null )
        {

            if( selectedBehav != null )
            {
                selectedBehav.UpdateRotation();
            }
        }

        if( Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            PrevMousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(PrevMousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                AssignSelectedObject(hit.transform.gameObject);
            }
        }
        else if ( Input.GetButtonUp("Fire1"))
        {
            Deselect();
        }
	}
}
