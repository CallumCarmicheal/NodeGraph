using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeGraph {
    public static class GlobalExtensions {

        public static CornerRadius ScaleDown(this CornerRadius x, double scale) {
            if (scale == 0)
                return new CornerRadius(0);

            return new CornerRadius(
                x.TopLeft / scale, x.TopRight / scale, x.BottomRight / scale, x.BottomLeft / scale);
        }

    }
}
