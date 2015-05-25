using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay
{
    public int Index { get; set; }
    public bool CabbageMan { get; set; }
    public double Timer { get; set; }

    public List<GameObject> Images { get; set; }

    public HowToPlay()
    {
        Index = 0;
        CabbageMan = false;
        Timer = 0;

        Images = new List<GameObject>();
    }

    public void EnableCurrentImage()
    {
        Images[Index].SetActive(true);
    }

    public void DisableCurrentImage()
    {
        Images[Index].SetActive(false);    
    }

    public bool IsShowingCabbageMan()
    {
        if (CabbageMan)
        {   
            CabbageMan = false;
            return true;
        }
        return false;
    }

    public bool ShouldDisplayCabbageMan()
    {
        if (Index == (Images.Count - 2))
        {
            CabbageMan = true;
            EnableCurrentImage();
            return true;
        }

        return false;
    }

    public bool IsShowingFirstImage()
    {
        return Index == 0;
    }

    public void DisableAllImages()
    {
        foreach (var image in Images)
        {
            image.SetActive(false);
        }
    }
}