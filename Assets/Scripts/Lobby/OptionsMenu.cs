 using UnityEngine;
 using System.Collections;
 using UnityEngine.UI;


public class OptionsMenu
{
    public string GetCurrentResolution()
    {
        return Screen.width + "x" + Screen.height;
    }
    public string GetCurrentGraphicsQuality()
    {
        var quality = QualitySettings.GetQualityLevel();
        switch (quality){
            case 0:
                return "Fastest";
            case 1:
                return "Fast";
            case 2:
                return "Simple";
            case 3:
                return "Good";
            case 4:
                return "Beautiful";
            case 5:
                return "Fantastic";
            case 6:
                return "Berserk";
            default:
                Debug.LogError("Index out of range");
                return "";
        }
    }

    public bool IsWindowMode()
    {
        return Screen.fullScreen;
    }

    public void SetResolution(string resolution)
    {
        var splittedResolution = resolution.Split('x');
        var width = System.Convert.ToInt16(splittedResolution[0]);
        var height = System.Convert.ToInt16(splittedResolution[1]);
        Screen.SetResolution(width, height, Screen.fullScreen);
    }

    public void SetGraphics(string graphicQuality)
    {
        var qualityIndex = 0;
        switch (graphicQuality)
        {
            case "Fastest":
                qualityIndex = 0;
                break;
            case "Fast":
                qualityIndex = 1;
                break;
            case "Simple":
                qualityIndex = 2;
                break;
            case "Good":
                qualityIndex = 3;
                break;
            case "Beautiful":
                qualityIndex = 4;
                break;
            case "Fantastic":
                qualityIndex = 5;
                break;
            case "Berserk":
                qualityIndex = 6;
                break;
            default:
                Debug.LogError("Index out of range");
                break;
        }
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ToggleWindowMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
     