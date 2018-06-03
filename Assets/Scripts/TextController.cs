using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    // Game Object references
    public Text          gameText;
    public AudioClip Terminal_Beep;
    public AudioSource   beepFile;

    // Local Script properties
    private string      dialog = "";
    private char[]  messageArr = new char[2048];
    private int         queNum = 0;
    private int           tick = 0;
    private int   currentIndex = 0;
    private int   DiagFinalLen = 0;
    private bool    dialogDone = false;

	// Use this for initialization
	void Start () {
        beepFile.clip = Terminal_Beep;

        queNum = 1;
        StartCoroutine(GetDialog(queNum, 0));
	}
	
	// Update is called once per frame
	void Update () {

        // For every even frame tick, while dialog length is shorter than the 
        // full message length, add a character from messageArr to dialog.
        if (tick % 2 == 0 && dialog.Length < messageArr.Length) {
            dialog += messageArr[currentIndex];
            beepFile.Play();
            currentIndex++;
        }

        // Tracks when a current dialog sequence has finished.
        // (i.e.) when current dialog length matches que# length.
        if (dialog.Length == DiagFinalLen) {
            dialogDone = true;
        } else {
            dialogDone = false;
        }

        /* Handle dialog changes triggered by player */

        /* Handle dialog changes triggered automatically 
         * (e.g. sentence continuation)
         */

         /* Ensures this block is only triggered when a previous dialog trigger
          * has completed. */

         // Chain que1 to que2 since they are in the same group...
        if (queNum == 1 && dialogDone) 
         {
            queNum = 2;
            StartCoroutine(GetDialog(queNum, 1));
         }
         // Set dialog to idle state
         if (queNum == 3)
         {

             queNum = 0;
            StartCoroutine(GetDialog(queNum, 0));
         }


        // With each frame, ensure that the UI text matches the dialog's current
        // state, and increment the frame tick counter.
        gameText.text = dialog;
        tick++;
	}

    IEnumerator GetDialog(int queNumber, int delay) {

        // Waits for n amount of seconds before changing dialog where n = delay.
        // As such, this method must be called within StartCoroutine()
        yield return new WaitForSeconds(delay);

        dialog = "";
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
                DiagFinalLen = 0;
                break;

            case 1:
                messageArr = que1.ToCharArray();
                DiagFinalLen = que1.Length;
                break;

            case 2:
                messageArr = que2.ToCharArray();
                DiagFinalLen = que2.Length;
                break;
        }
    }
}
