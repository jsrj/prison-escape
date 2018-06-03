using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public Text      gameText;

    private string     dialog = "";
    private char[] messageArr = new char[2048];
    private int        queNum = 0;
    private int          tick = 0;
    private int  currentIndex = 0;

	// Use this for initialization
	void Start () {
        queNum = 1;
        GetDialog(queNum);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Tick: "+tick);
        Debug.Log("dialog length: "+dialog.Length);
        Debug.Log("messageArr length: "+messageArr.Length);

        // For every even frame tick, while dialog length is shorter than the 
        // full message length, add a character from messageArr to dialog.
        if (tick % 2 == 0 && dialog.Length < messageArr.Length) {
            dialog += messageArr[currentIndex];
            currentIndex++;
        }

        /* Handle dialog changes triggered by player */

        /* Handle dialog changes triggered automatically 
         * (e.g. sentence continuation)
         */

         /* Ensures this block is only triggered when a previous dialog trigger
          * has completed. */

         // Chain que1 to que2 since they are in the same group...
        if (dialog.Length == currentIndex+1 && queNum == 1)
         {

             queNum = 2;
             GetDialog(queNum);
         }
         // Set dialog to idle state
         if (queNum == 3)
         {

             queNum = 0;
             GetDialog(queNum);
         }


        // With each frame, ensure that the UI text matches the dialog's current
        // state, and increment the frame tick counter.
        gameText.text = dialog;
        tick = (tick >= 1001)? 0 : tick+1;
	}

    void GetDialog(int queNumber) {
        
        currentIndex = 0;

        /* -- Que groups -- */

        // -- Group 1: Greeting --
        string que1 = "Welcome to Prison Escape!";
        string que2 = "A classic choose your own adventure style game.";

        // -- Group 2: Rules --

        switch (queNumber)
        {
            case 0:
                messageArr = "".ToCharArray();
                break;

            case 1:
                messageArr = que1.ToCharArray();
                break;

            case 2:
                messageArr = que2.ToCharArray();
                break;
        }
    }
}
