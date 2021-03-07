using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfVlad.Tools
{
    public struct Size
    {
        public float Width { get; set; }
        public float Height { get; set; }
        public Size(float width, float height)
        {
            Width = width;
            Height = height;
        }
    }
}
