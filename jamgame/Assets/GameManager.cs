using UnityEngine;
using System.Collections;
namespace Jordan
{
    public class GameManager : MonoBehaviour
    {
        public static GameObject SelectedObject { get; private set; }
        private static InteractionBehavior selectedBehav { get; set; }
        public static Vector3 PrevMousePosition { get; set; }

        //public delegate void OnEndSelect(object sender, GameObject e);
        //public delegate void OnStartSelect(object sender, GameObject e);
        private static GameManager instance;
        public static GameManager Instance { get { return instance ?? new GameObject("GameManager").AddComponent<GameManager>(); } }
        //public event OnEndSelect EndSelected;
        //public event OnStartSelect StartSelected;

        //protected void NotifyStartSelectedEvent(GameObject obj)
        //{
        //    if (StartSelected != null)
        //    { StartSelected(this, obj); }
        //}

        //protected void NotifyEndSelectedEvent(GameObject obj)
        //{
        //    if (EndSelected != null)
        //    { EndSelected(this, obj); }
        //}

        protected void Start()
        {
            if (instance == null)
            { instance = this; }
        }

        public static void AssignSelectedObject(GameObject obj)
        {
            SelectedObject = obj;
            if (SelectedObject.GetComponent<InteractionBehavior>() != null)
            {
                selectedBehav = SelectedObject.GetComponent<InteractionBehavior>();
                selectedBehav.OnStartSelect();
            }
                
            //Instance.NotifyStartSelectedEvent(SelectedObject);
        }

        public static void Deselect()
        {
            if (selectedBehav != null)
            {
                selectedBehav.OnEndSelect();
                selectedBehav = null;
            }
            //Instance.NotifyEndSelectedEvent(SelectedObject);
            SelectedObject = null;
        }

        // Update is called once per frame
        void Update()
        {
            if (SelectedObject != null)
            {

                if (selectedBehav != null)
                {
                    selectedBehav.UpdateRotation();
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                PrevMousePosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(PrevMousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    AssignSelectedObject(hit.transform.gameObject);
                }
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                Deselect();
            }
        }
    }
}
