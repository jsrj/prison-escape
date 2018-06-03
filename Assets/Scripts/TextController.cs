using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    // Game Object references
    public Text           gameText;
    public AudioClip Terminal_Beep;
    public AudioClip       BGMusic;
    public AudioSource   bgmSource;
    public AudioSource    beepFile;

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
        // Load audio Files
        bgmSource.clip = BGMusic;
        beepFile.clip  = Terminal_Beep;

        bgmSource.Play();

        dialog = "";
        StartCoroutine(GetDialog(1, 0));
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(string.Format("Dialog Done: {0} | queNum: {1}", dialogDone, queNum));

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
            StartCoroutine(GetDialog(2, 1));
         }

         if (queNum == 2 && dialogDone)
         {
            StartCoroutine(GetDialog(3, 1));
         }


        // With each frame, ensure that the UI text matches the dialog's current
        // state, and increment the frame tick counter.
        gameText.text = dialog;
        tick = (tick < 9999)? 0 : tick+1;
	}

    IEnumerator GetDialog(int queNumber, int delay) {

        // Waits for n amount of seconds before changing dialog where n = delay.
        // As such, this method must be called within StartCoroutine()
        yield return new WaitForSeconds(delay);
        dialog = "";
        messageArr = "".ToCharArray();
        currentIndex = 0;

        /* -- Que groups -- */

        // -- Group 1: Greeting --
        string que1 = "Welcome to The Hyperion!";
        string que2 = "The Hyperion is a prison transport ship. \n" +
        " I don't know what you did to end up on it, but I am hoping that doesn't matter now.";
        string que3 = "I am The Hyperion Artifical Intelligence Lexicon. You can call me HAL.";

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

            case 3:
                messageArr = que3.ToCharArray();
                DiagFinalLen = que3.Length;
                break;
        }
        queNum = queNumber;
    }
}
