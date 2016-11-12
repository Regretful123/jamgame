using UnityEngine;
using System.Collections;

public class RandomNumber : MonoBehaviour {

    private enum Side { Top = 0, Bottom, Left, Right, Front, Back }
    private Side selectedSide;
    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;
    public GameObject front;
    public GameObject back;
    private GameObject[] numObj = new GameObject[5];
    private Transform[] sidePos = new Transform[5];


    void Enabled()
    {
        Start();
    }

    private string GetRandomRange
    {
        get { return ((int)Random.Range(1, 10)).ToString(); }
    }

    // Use this for initialization
    void Start ()
    {
        sidePos[(int)Side.Top] = top.transform;
        sidePos[(int)Side.Bottom] = bottom.transform;
        sidePos[(int)Side.Left] = left.transform;
        sidePos[(int)Side.Right] = right.transform;
        sidePos[(int)Side.Front] = front.transform;
        sidePos[(int)Side.Back] = back.transform;

        for (int i = 0; i < 5; i++)
        {
            if (numObj[i] != null)
            {
                Destroy(numObj[i]);
            }

            numObj[i] = (GameObject)Instantiate(Resources.Load(GetRandomRange),sidePos[i].position, 
        }


        numObj[(int)Side.Top] = (GameObject)Instantiate(Resources.Load(GetRandomRange), top.transform.position, top.transform.rotation);
        numObj[(int)Side.Bottom] = (GameObject)Instantiate(Resources.Load(GetRandomRange), bottom.transform.position, bottom.transform.rotation);
        numObj[(int)Side.Left] = (GameObject)Instantiate(Resources.Load(GetRandomRange), left.transform.position, left.transform.rotation);
        numObj[(int)Side.Right] = (GameObject)Instantiate(Resources.Load(GetRandomRange), right.transform.position, right.transform.rotation);
        numObj[(int)Side.Front] = (GameObject)Instantiate(Resources.Load(GetRandomRange), front.transform.position, front.transform.rotation);
        numObj[(int)Side.Back] = (GameObject)Instantiate(Resources.Load(GetRandomRange), back.transform.position, back.transform.rotation);

        numObj[(int)Side.Top].transform.parent = top.transform;
        numObj[(int)Side.Bottom].transform.parent = bottom.transform;

    }
    
    public int GetSelectedNumber()
    {
        // first thing first find the object closest to the camera.
        float maxDist = Mathf.Infinity;
        int objSel = -1;
        for (int i = 0; i < numObj.Length; i++)
        {
            float dist = Vector3.Distance(Camera.main.transform.position, numObj[i].transform.position);
            if ( dist < maxDist )
            {
                objSel = i;
                maxDist = dist;
            }
        }

        return int.Parse(numObj[objSel].name ?? "0");
    }
}
