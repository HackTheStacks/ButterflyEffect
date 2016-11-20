#region Using
using UnityEngine;
using System.Collections;
#endregion // Using

#region Description
/*
 * UI_Control 
 * writen by Electric Wolf
 * Copyright 2014
 * 
 * This give you an example of how to call the book to do the effects.
 * just set the book object you need to control in the inspector
 */
#endregion // Description

public class UI_Control : MonoBehaviour
{
    #region Variables
    public GameObject bookObject = null;
    private BookControl bookControl = null;
    #endregion // Variables

    #region Set-up
    void Start()
    {
        // We need to find the book game object
        if (bookObject != null)
        {
            // Just store the book control so we can work with it directly.
            bookControl = (BookControl)bookObject.GetComponent<BookControl>();
        }
        else
        {

        }
    }
    #endregion // Set-up

    #region Access Functions
    #endregion // Access Functions

    #region Updates
    void Update()
    {
    }
    #endregion // Updates

    #region GUI
    void OnGUI()
    {
        if (bookControl == null)
            return; // we don't have the book set.

        // is the book open ?
        if (!bookControl.IsOpen())
        {
            if (GUI.Button(new Rect(0, 5, 164, 90), "Open"))
            {
                // Open the book
                bookControl.Open_Book();
            }
        }
        else
        {
            if (GUI.Button(new Rect(0, 50, 164, 90), "Close"))
            {
                // Close the book
                bookControl.Close_Book();
            }

            // Check to see if we can turn the page
            if (bookControl.CanTurnPage())
            {
                if (GUI.Button(new Rect(0, 200, 164, 90), "Turn page"))
                {
                    // Turn the page
                    bookControl.Turn_Page();
                }
            }

            // Check to see if we can turn back the page.
            if (bookControl.CanGoBackAPage())
            {
                // Turn back the page.
                if (GUI.Button(new Rect(0, 300, 164, 90), "Turn page back"))
                {
                    bookControl.Turn_BackPage();
                }
            }
        }
    }
    #endregion // GUI
}
