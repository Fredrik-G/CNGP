using UnityEngine;
using System.Collections;

namespace Engine
{
    public class UIFormat : MonoBehaviour
    {
        public enum FontSize
        {
            ExtraSmall = 1,
            Small = 2,
            Medium = 3,
            Large = 4,
            ExtraLarge = 5
        }

        public static GUIStyle FormatGuiStyle(TextAnchor anchor, FontSize fontSize, Color color)
        {
            var style = new GUIStyle
            {
                alignment = anchor,
                fontSize = Screen.height*(int) fontSize/100,
                normal = {textColor = color}
            };

            return style;
        }

    }
}