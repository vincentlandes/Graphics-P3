using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template_P3;

namespace template_P3
{
    class Node
    {
        public Mesh mesh;
        public List<Node> children;
        public Shader shader;
        public Texture texture;
        //eig ff per node n shader & texture etc meegevon joe xoxo

        public Node(Mesh _mesh, Shader _shader, Texture _texture)
        {
            mesh = _mesh;
            texture = _texture;
            shader = _shader;
            
        }

    }
}
