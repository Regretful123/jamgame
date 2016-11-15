using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultChecker : MonoBehaviour {

    public 
    public List<GameObject> dices = new List<GameObject>();

    public void CheckResult()
    {
        int result = 0;
        // this will be called in such ways that we wanted to make sure the result of the dices is the sum of number 10.
        for ( int i = 0; i < dices.Count; i++)
        {
            if (dices[i].GetComponent<RandomNumber>() != null)
                result += dices[i].GetComponent<RandomNumber>().GetSelectedNumber;
        }

        Debug.Log(result);

        if ( result == 10 )
        {
            // make the dice fade out because the player has solved it.
            // todo job for the future once we get our design done.  
            this.gameObject.SetActive(false);
        }
    }



    public void Validate()
    {
        // this would be the most difficult part where we need to run a loop function for each dices in the set. 
        // this means that for each dice we have in our array collection, we would have to run a loop function to run against multiple of possible answer case scenario. 
        // I will have to check online and see if we can optimize the code, but for now, we need to do a for each side of the faces, we need to know that we can get to number 10
    }
}
