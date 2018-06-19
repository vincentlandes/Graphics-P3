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
        public static Node camera = new Node("Camera", null, null, Matrix4.Identity, null);

        public static Node teapot = new Node("Teapot", Game.GetShader, Game.GetTeapot, Matrix4.Identity, Game.GetTexture);
        public static Node floor = new Node("Plane", Game.GetShader, Game.GetFloor, Matrix4.Identity, Game.GetTexture);
        public static Node dummy = new Node("Dummy", Game.GetShader, Game.GetDummy, Matrix4.Identity, Game.GetTexture);
        

        public void Init()
        {
            teapot.transformlocal = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), 0);
            teapot.transformlocal *= Matrix4.CreateTranslation(0, -4, -15);
            teapot.transformlocal *= Matrix4.CreatePerspectiveFieldOfView(1.2f, 1.3f, .1f, 1000);

            floor.transformlocal = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), 0);
            floor.transformlocal *= Matrix4.CreateTranslation(0, -4, -15);
            floor.transformlocal *= Matrix4.CreatePerspectiveFieldOfView(1.2f, 1.3f, .1f, 1000);

            dummy.transformlocal = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), 0);
            dummy.transformlocal *= Matrix4.CreateTranslation(-4, -4, -15);
            dummy.transformlocal = Matrix4.CreateScale(0.1f, 0.1f, 0.1f);
            dummy.transformlocal *= Matrix4.CreatePerspectiveFieldOfView(1.2f, 1.3f, .1f, 1000);


            camera.children.Add(worldSpace);
            worldSpace.children.Add(teapot);
            worldSpace.children.Add(floor);
            worldSpace.children.Add(dummy);
        }

        public void SceneGraph()
        {
            Render(camera, null, Matrix4.Identity);
        }

        public static void Render(Node self, Node parent, Matrix4 transform)
        {
            transform *= self.transformlocal;
            if (self.mesh != null)
            {
                self.mesh.Render(self.shader, transform, self.texture);
            }
                
            foreach (Node child in self.children)
            {
                Render(child, self, transform);
            }
        }
        //transform all method
        //root
    }
}
