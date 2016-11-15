using UnityEngine;
using System.Collections.Generic;

public class RandomNumber : MonoBehaviour {

    private enum Side { Top = 0, Bottom=1, Left = 2, Right = 3, Front = 4, Back = 5 }
    private Side selectedSide;
    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;
    public GameObject front;
    public GameObject back;
    private GameObject[] numObj = new GameObject[6];
    private Transform[] sidePos = new Transform[6];
    private List<int> numDef = new List<int>();
    public List<int> NumberDeclared { get { return numDef; } }
    private int TryFail = 0;
    private const int MaxTryBeforeFail = 10;
    
    /// <summary>
    /// Generate a random number range between 0 and 10
    /// </summary>
    private int GetRandomRange
    {
        get { return (int)Random.Range(0, 10); }
    }

    void Start ()
    {
        sidePos[(int)Side.Top] = top.transform;
        sidePos[(int)Side.Bottom] = bottom.transform;
        sidePos[(int)Side.Left] = left.transform;
        sidePos[(int)Side.Right] = right.transform;
        sidePos[(int)Side.Front] = front.transform;
        sidePos[(int)Side.Back] = back.transform;

        GenerateNumbers();
    }

    /// <summary>
    ///  this may be used to help regenerate number in case of something goes wrong or there's unsolvable solution occurred.
    /// </summary>
    public void GenerateNumbers()
    {
        numDef.Clear(); // clear the current list of active numbers
        for (int i = 0; i < numObj.Length; i++)
        {
            if (numObj[i] != null) { Destroy(numObj[i]); }
            int r = GetRandomRange;

            // seems dangerous but as long as I have the try fail exception. 
            while (numDef.IndexOf(r) != -1)
            {
                if (MaxTryBeforeFail < TryFail) { break; }
                TryFail++;
                r = GetRandomRange;
            }

            TryFail = 0;
            numDef.Add(r);
            numObj[i] = (GameObject)Instantiate(Resources.Load(r.ToString()), sidePos[i].position, sidePos[i].rotation);
            numObj[i].transform.parent = sidePos[i];
            numObj[i].tag = r.ToString();
        }
    }
    
    /// <summary>
    /// Returns the number that is the closest to the camera. (Selected number if possible)
    /// </summary>
    public int GetSelectedNumber
    {
        get
        {   // first thing first find the object closest to the camera.
            float maxDist = Mathf.Infinity;
            int objSel = -1;
            for (int i = 0; i < numObj.Length; i++)
            {
                // don't know why, don't care, wanted to make sure it works, either way it'll roll back to zero.
                if (numObj[i] == null) continue;
                float dist = Vector3.Distance(Camera.main.transform.position, numObj[i].transform.position);
                if (dist < maxDist)
                {
                    objSel = i;
                    maxDist = dist;
                }
            }
            return objSel == -1 ? 0 : (int.Parse(numObj[objSel].tag ?? "0"));
        }
    }
}
