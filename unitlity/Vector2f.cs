using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphC_.unitlity
{
    internal class Vector2f
    {
        Vector2f() 
        {
            this.x = 0;
            this.y = 0;
        }
        Vector2f(float x, float y)
        {
            this.x = x; 
            this.y = y;
        }

        Vector2f(Vector2f v) 
        {
            x = v.x;
            y = v.y;
        }

        float x;
        float y;
    }
}
