using UnityEngine;
using System.Collections;
namespace Jordan
{
    public class GameManager : MonoBehaviour
    {
        public static GameObject SelectedObject { get; private set; }
        private static InteractionBehavior selectedBehav { get; set; }
        public static Vector3 PrevMousePosition { get; set; }
        private static GameManager instance;
        public static GameManager Instance { get { return instance ?? new GameObject("GameManager").AddComponent<GameManager>(); } }
        public GameObject Dice;
        private const float _SPACE_WIDTH = 5f;

        void Start()
        {
            if (instance == null)
            { instance = this; }
            Spawn();
        }

        // find a way to initial the object and then be able to generate the number to make it sum to number 10

        public void Spawn(int length = 2)
        {
            // fail safe check to ensure that the level has the dice gameobject assigned. 
            if(Dice == null )
            {
                Debug.LogWarning("This GameManager does not have Dice game object assigned! Disabiling!");
                return;
            }

            // must be positive. 
            if (length < 0) length *= -1;
            float start_Length = -( length * _SPACE_WIDTH ) / 2;
            
            // for this script to work, I need to find a place on the screen to spawn the object 
            // possible the center of the screen for this demo
            Vector3 center = ( Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)) 
                            + Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0)) ) / 2;    // and the center isn't correctly centered of the screen???
            center.z = 0;

            // from here, this is our starting root. we can make it so that we can change the orientation to make it sum to number 10.
            // we need to inform the player that we want it to solve either of the direction or which way it needs to be solved. 
            GameObject root = new GameObject("Parent");
            ResultChecker rc = root.AddComponent<ResultChecker>();
            for (int i = 0; i < length; i++)
            {
                // spawn the cube. 
                // find a way to offset the object so that there's space in between to avoid overlapping geometry/objects in the scene
                Vector3 newPos = Vector3.left * (start_Length + ((i + 1) * _SPACE_WIDTH)) + center;
                GameObject dice = (GameObject)Instantiate(Dice, newPos, Quaternion.identity);
                dice.transform.parent = root.transform;
                rc.dices.Add(dice);
                dice.GetComponent<InteractionBehavior>().rc = rc;
            }
            rc.Validate();
            rc.CheckResult();
        }

        public static void AssignSelectedObject(GameObject obj)
        {
            SelectedObject = obj;
            if (SelectedObject.GetComponent<InteractionBehavior>() != null)
            {
                selectedBehav = SelectedObject.GetComponent<InteractionBehavior>();
                selectedBehav.OnStartSelect();
            }
        }

        public static void Deselect()
        {
            if (selectedBehav != null)
            {
                selectedBehav.OnEndSelect();
                selectedBehav = null;
            }
            SelectedObject = null;
        }

        // Update is called once per frame
        void Update()
        {
            if (SelectedObject != null)
            {

                if (selectedBehav != null)
                { selectedBehav.UpdateRotation(); }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                PrevMousePosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(PrevMousePosition);

                if (Physics.Raycast(ray, out hit))
                { AssignSelectedObject(hit.transform.gameObject); }
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                Deselect();
            }
        }
    }
}
