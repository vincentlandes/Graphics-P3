using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template_P3;


namespace template_P3
{
    class scenegraph
    {
        public static Node worldSpace = new Node(null, null, null);
        public static Node teapot = new Node(Game.GetMesh, Game.GetShader, Game.GetTexture);
        
        static void SceneGraph()
        {
            worldSpace.children.Add(teapot);
            Render(worldSpace, null);
        }

        static void Render(Node self, Node parent)
        {
            if (self.mesh != null)
            {
                Matrix4 parenttransformcamera = parent.mesh.transformlocal;
                Matrix4 parenttransformworld = parent.mesh.transformlocal;
            //parent.mesh.Render(parent.shader, , parent.texture);
            }

            if (self.children != null)
            {
                foreach (Node child in parent.children)
                {
                    Matrix4 childtransformworld = parent.mesh.transformlocal * child.mesh.transformlocal;
                    //hier transform berekenen, meegeven in n.mesh.render
                    if (child.children != null)
                        Render(child, self);
                }
            }
            //voor iedere node camera matrix * root trnsform * child transform

            //hierna render methode aanroepen 
        }
        //transform all method
        //root
    }
}
