#region Using
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion // Using

#region Description
/*
 * BookControl 
 * writen by Electric Wolf
 * Copyright 2014
 * 
 */
#endregion // Description

public class BookControl : MonoBehaviour
{
    #region CONST
    static int TurnedAnimationHash_Turn = Animator.StringToHash("Base Layer.Turn_Wait");
    static int TurnedAnimationHash_BackWait = Animator.StringToHash("Base Layer.Wait_End");
    #endregion CONST

    #region Variables
    public BOOK_DATA Book;

    private int pageIndex { get; set; }
    // The Book data
    private GameObject mainBook { get; set; }
    private Animator bookAnimator { get; set; }

    // The Page data
    private GameObject page { get; set; }
    private GameObject PageArt { get; set; }
    private Animator pageAnimator { get; set; }
    private Material pageFrontMaterial { get; set; }
    private Material pageBackMaterial { get; set; }
    private Material bookInSideFrontPage { get; set; }
    private Material bookInSideBackPage { get; set; }
    bool isPageTurning { get; set; }
    bool isPageBackTurning { get; set; }
    bool bookOpen { get; set; }

    #endregion // Variables

    #region Debug
    // We can turn off any errors and warnings.
    public void TraceMessage_Info(string _sMessage)
    {
        Debug.Log("OpenBook Info [" + gameObject.name + "] " + _sMessage);
    }
    public void TraceMessage_Warning(string _sMessage)
    {
        Debug.Log("OpenBook Warning [" + gameObject.name + "] " + _sMessage);
    }
    public void TraceMessage_Error(string _sMessage)
    {
        Debug.Log("OpenBook Error [" + gameObject.name + "] " + _sMessage);
    }
    public void TraceMessage_CodeError(string _sMessage)
    {
        Debug.Log("OpenBook Code Error [" + gameObject.name + "] " + _sMessage);
    }
    #endregion // Debug

    #region Set-up
    void Start()
    {
        GetObjects();

        SetBookArt();

        if (Book.PageList.Length % 2 != 0)
        {
            TraceMessage_Error("EOP0007 : You need to have an even number of pages in a book.");
        }
    }
    private void GetObjects()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in allChildren)
        {
            if (child.name == "Book_Holder")
            {
                bookAnimator = child.GetComponent<Animator>();
            }
            if (child.name == "Book_Mesh")
            {
                mainBook = child.gameObject;
            }
            if (child.name == "Page_Obj")
            {
                page = child.gameObject;
                pageAnimator = page.GetComponent<Animator>();
            }
        }

        // validation
        if (bookAnimator == null)
        {
            TraceMessage_Error("EOP0001 : Failed to find the animator object on the main book named 'Book_Holder'");
        }
        if (mainBook == null)
        {
            TraceMessage_Error("EOP0002 : Failed to find the main book object mesh named 'Book_Mesh'");
        }
        if (page == null)
        {
            TraceMessage_Error("EOP0003 : Failed to find the page object where the page should be found.");
        }


        // Get the page
        allChildren = page.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in allChildren)
        {
            if (child.name == "Page")
            {
                PageArt = child.gameObject;
                pageFrontMaterial = PageArt.GetComponent<Renderer>().materials[0];
                pageBackMaterial = PageArt.GetComponent<Renderer>().materials[1];
            }
        }

        if (PageArt == null)
        {
            TraceMessage_Error("EOP0004 : Failed to find the page object");
        }
        if (pageFrontMaterial == null)
        {
            TraceMessage_Error("EOP0005 : Failed to find the page front material");
        }
        if (pageBackMaterial == null)
        {
            TraceMessage_Error("EOP0006 : Failed to find the page back material");
        }

        HidePage();
    }
    private void SetBookArt()
    {
        if (mainBook == null)
        {
            return;
        }
        Material MainCover = null;
        foreach (Material mat in mainBook.GetComponent<Renderer>().materials)
        {
            if (MainCover == null)
            {
                if (mat.name == "Cover (Instance)")
                {
                    MainCover = mat;
                }
            }
            if (bookInSideFrontPage == null)
            {
                if (mat.name == "InSidePageA - Copy (Instance)")
                {
                    bookInSideFrontPage = mat;
                }
            }
            if (bookInSideBackPage == null)
            {
                if (mat.name == "InSidePageB - Copy (Instance)")
                {
                    bookInSideBackPage = mat;
                }
            }
        }

        // Set the cover of the new book.
        if (MainCover != null)
        {
            MainCover.mainTexture = Book.GetCoverTexture();
        }
        if (bookInSideFrontPage != null)
        {
            bookInSideFrontPage.mainTexture = Book.GetPage(pageIndex);
        }
        pageIndex++;

        if (bookInSideBackPage != null)
        {
            bookInSideBackPage.mainTexture = Book.GetPage(pageIndex);
        }
    }
    private void HidePage()
    {
        this.PageArt.GetComponent<Renderer>().enabled = false;
    }
    #endregion // Set-up

    #region Access Functions
    public bool IsOpen()
    {
        return bookOpen;
    }
    public void Open_Book()
    {
        Set_Book_Open(true);
    }
    public void Close_Book()
    {
        Set_Book_Open(false);
        HidePage();
    }
    public void Turn_Page()
    {
        Set_TurnPage();
    }
    public void Turn_BackPage()
    {
        Set_TurnBackPage();
    }
    public bool CanTurnPage()
    {
        if (isPageTurning)
            return false;   // no, we are current turning the page

        if (isPageBackTurning)
            return false;   // no, we are current turning the page

        if (pageIndex == Book.PageTotal())
            return false;   // no, end of the page list

        return true; // Yes
    }
    public bool CanGoBackAPage()
    {
        if (isPageTurning)
            return false;   // no, we are current turning the page

        if (isPageBackTurning)
            return false;   // no, we are current turning the page

        if (pageIndex < 2)
            return false;   // No, we are on the fist page.

        return true; // Yes
    }
    #endregion // Access Functions

    #region Page Turn (normal)
    private void Set_TurnPage()
    {
        PageArt.GetComponent<Renderer>().enabled = true;

        SetPageFront(pageIndex);
        SetPageBack(pageIndex + 1);
        bookInSideBackPage.mainTexture = Book.GetPage(pageIndex + 2);

        pageAnimator.SetBool("OpenPage", true);

        isPageTurning = true;
    }
    private void PageTurnFinished()
    {
        pageIndex++;
        bookInSideFrontPage.mainTexture = Book.GetPage(pageIndex);
        pageIndex++;

        // hide the page
        PageArt.GetComponent<Renderer>().enabled = false;

        // re set it to the starting page.
        pageAnimator.SetBool("OpenPage", false);
        isPageTurning = false;
    }
    #endregion Page Turn (normal)

    #region Page Back Turn
    private void Set_TurnBackPage()
    {
        PageArt.GetComponent<Renderer>().enabled = true;
        pageAnimator.SetBool("ClosePage", true);

        SetPageFront(pageIndex - 2); // the page before
        SetPageBack(pageIndex - 1); // the current page.

        bookInSideFrontPage.mainTexture = Book.GetPage(pageIndex - 3); // back two

        isPageBackTurning = true;
    }
    private void PageBackTurnFinished()
    {
        pageIndex--;
        pageIndex--;
        bookInSideBackPage.mainTexture = Book.GetPage(pageIndex);

        // hide the page
        PageArt.GetComponent<Renderer>().enabled = false;
        // re set it to the starting page.
        pageAnimator.SetBool("ClosePage", false);
        isPageBackTurning = false;
    }
    private void SetPageFront(int _index)
    {
        if (pageFrontMaterial != null)
        {
            pageFrontMaterial.mainTexture = Book.GetPage(_index);
        }
    }
    private void SetPageBack(int _index)
    {
        if (pageBackMaterial != null)
        {
            pageBackMaterial.mainTexture = Book.GetPage(_index);
        }
    }
    private void Set_Book_Open(bool bVal)
    {
        bookAnimator.SetBool("Open", bVal);
        bookOpen = bVal;
    }

    #endregion // Page Back Turn

    #region Updates
    void Update()
    {
        if (isPageTurning)
        {
            // check to see if the page has finsihed turning..
            AnimatorStateInfo currentBaseState = pageAnimator.GetCurrentAnimatorStateInfo(0);
            if (currentBaseState.fullPathHash == TurnedAnimationHash_Turn)
            {
                PageTurnFinished();
            }
        }

        if (isPageBackTurning)
        {
            // Check to see if the page has finsihed turning.
            AnimatorStateInfo currentBaseState = pageAnimator.GetCurrentAnimatorStateInfo(0);
            if (currentBaseState.fullPathHash == TurnedAnimationHash_BackWait)
            {
                PageBackTurnFinished();
            }
        }
    }
    #endregion // Updates

}

[Serializable]
public class BOOK_DATA
{
    #region Variables
    public Texture covers = null;
    public Texture bookEdge = null;
    public Texture[] PageList;
    #endregion // Variables

    #region Set-up
    protected BOOK_DATA()
    {
    }
    #endregion Set-up

    #region Functions
    public Texture GetCoverTexture()
    {
        return covers;
    }
    public Texture GetPage(int Id)
    {
        if (PageList.Length < Id)
        {
            return null;
        }
        return PageList[Id];
    }
    public int PageTotal()
    {
        return PageList.Length - 1;
    }
    #endregion // Functions
}
