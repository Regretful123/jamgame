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
