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

        /// <summary>
        /// Returns a centered rect.
        /// </summary>
        /// <param name="y">Y value</param>
        /// <returns>The centered rect</returns>
        public static Rect CreateCenteredRect(int y)
        {
            return new Rect(Screen.width / 2 - Screen.width / 10, Screen.height / 2 - Screen.height / 20 - y, Screen.width / 5,
                Screen.height / 20);
        }
    }
}