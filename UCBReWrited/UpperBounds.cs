using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCBReWrited
{
    struct UpperBounds
    {
        double bounds ;
        int index;

        public UpperBounds(int index)
        {
            this.index = index;
            bounds = 0;
        }

        public double Bounds { get => bounds; set => bounds = value; }
        public int Index { get => index; set => index = value; }
    }
}
