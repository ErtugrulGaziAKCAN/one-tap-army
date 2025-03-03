using UnityEngine;
namespace QuickTools.Scripts.Extensions
{
    public static class ColorExtensions
    {
        public static Color GetShadedColor(this Color c, float shade)
        {
            var shadedColor = c;
            switch (shade)
            {
                case < 0:
                {
                    shadedColor = Color.Lerp(c, Color.black, -shade / 5f);
                    Color.RGBToHSV(shadedColor, out var h, out var s, out var v);
                    Color.RGBToHSV(c, out var originalH, out var originalS, out var originalV);
                    s -= originalS * .1f * shade;
                    v += originalV * .03f * shade;
                    var rgb = Color.HSVToRGB(h, s, v);
                    shadedColor = rgb;
                    break;
                }
                case > 0:
                {
                    shadedColor = Color.Lerp(c, Color.white, shade / 4f);
                    Color.RGBToHSV(shadedColor, out var h, out var s, out var v);
                    s += .085f * shade;
                    v += .05f * shade;
                    var rgb = Color.HSVToRGB(h, s, v);
                    shadedColor = rgb;
                    break;
                }
            }

            shadedColor.a = 1f;
            return shadedColor;
        }
    }
}