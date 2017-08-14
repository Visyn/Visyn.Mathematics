using System.Collections.Generic;
using Visyn.Public.Mathematics;

namespace Visyn.Mathematics
{
    public static class ColorStructExtensions
    {
        public static ColorStruct Average(this IEnumerable<IColor> colors )
        {
            int red = 0;
            int green = 0;
            int blue = 0;
            int alpha = 0;
            int count = 0;
            foreach(var color in colors)
            {
                red += color.R;
                green += color.G;
                blue += color.B;
                alpha += color.Alpha;
                count++;
            }
            return new ColorStruct((byte)(red/count), (byte)(green /count), (byte)(blue /count), (byte)(alpha /count));
        }
    }
}
