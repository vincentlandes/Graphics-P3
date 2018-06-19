using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template_P3;
using OpenTK;

namespace template_P3
{
    class Node
    {
        public String Name;
        public Mesh mesh;
        public Shader shader;
        public List<Node> children;
        public Texture texture;
        public Matrix4 transformlocal;
        //eig ff per node n shader & texture etc meegevon joe xoxo

        public Node(String _Name, Shader _shader, Mesh _mesh, Matrix4 _transformlocal, Texture _texture)
        {
            Name = _Name;
            mesh = _mesh;
            texture = _texture;
            shader = _shader;
            transformlocal = _transformlocal;
            children = new List<Node>();
            
        }

    }
}
