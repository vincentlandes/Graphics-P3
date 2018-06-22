using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Template_P3
{
    public class light
    {
        public Vector3 lightPos;
        public Vector4 ambiantlightColor;

        public light(Vector3 pos, Vector4 color)
        {
            lightPos = pos;
            ambiantlightColor = color;
        }
        
    }
}
