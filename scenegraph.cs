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
        public static Node worldSpace = new Node("Worldspace", null, null, Matrix4.Identity, null);
        public static Node teapot = new Node("Teapot", Game.GetShader, Game.GetMesh, Matrix4.Identity, Game.GetTexture);

        public void Init()
        {
            teapot.transformlocal = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), 0);
            teapot.transformlocal *= Matrix4.CreateTranslation(0, -4, -15);
            teapot.transformlocal *= Matrix4.CreatePerspectiveFieldOfView(1.2f, 1.3f, .1f, 1000);

            worldSpace.children.Add(teapot);
        }

        public void SceneGraph()
        {
            Render(worldSpace, null);
        }

        public static void Render(Node self, Node parent)
        {
            if (self.mesh != null && parent != null)
            {
                //self.transformlocal *= parent.transformlocal;
                self.mesh.Render(self.shader, self.transformlocal * parent.transformlocal, self.texture);
            }

            foreach (Node child in self.children)
            {
                Render(child, self);
            }

            //voor iedere node camera matrix * root trnsform * child transform

            //hierna render methode aanroepen 
        }
        //transform all method
        //root
    }
}
