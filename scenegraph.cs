using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace Template_P3
{
    public class scenegraph
    {
        Shader shader;
        public static Node worldSpace = new Node("Worldspace", null, null, Matrix4.Identity, null);
        public static Node camera = new Node("Camera", null, null, Matrix4.Identity, null);

        public static Node teapot = new Node("Teapot", Game.GetShader, Game.GetTeapot, Matrix4.Identity, Game.GetTextureGlass);
        public static Node floor = new Node("Plane", Game.GetShader, Game.GetFloor, Matrix4.Identity, Game.GetTexture);
        public static Node dummy = new Node("Dummy", Game.GetShader, Game.GetDummy, Matrix4.Identity, Game.GetTexture);

        public static light light1 = new light(new Vector3(0, 5, -5), new Vector4(1, 1, 1, 1));
        

        public static float a = 0;
        private Stopwatch timer;
        const float PI = 3.1415926535f;

        public void Init(Shader shader)
        {
            this.shader = shader;
            // initialize stopwatch
            timer = new Stopwatch();
            timer.Reset();
            timer.Start();

            camera.transformlocal = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), 0);
            camera.transformlocal *= Matrix4.CreateTranslation(0, -4, -15);
            
            worldSpace.children.Add(camera);
            worldSpace.children.Add(teapot);
            worldSpace.children.Add(floor);
            teapot.children.Add(dummy);

            int lightLoc = GL.GetUniformLocation(shader.programID, "lightInfo");
            GL.UseProgram(shader.programID);
            Matrix4 lightMatrix = new Matrix4(new Vector4(light1.lightPos, 1.0f), light1.ambiantlightColor, new Vector4(0), new Vector4(0));
            GL.UniformMatrix4(lightLoc, false, ref lightMatrix);
        }

        public void SceneGraph()
        {
            // measure frame duration
            float frameDuration = timer.ElapsedMilliseconds;
            timer.Reset();
            timer.Start();

            // update rotation
            a += 0.001f * frameDuration;
            if (a > 2 * PI) a -= 2 * PI;

            teapot.transformlocal = Matrix4.CreateRotationY(a);
            dummy.transformlocal = Matrix4.CreateScale(0.05f);
            dummy.transformlocal *= Matrix4.CreateRotationY(-2*a);
            dummy.transformlocal *= Matrix4.CreateTranslation(8.0f, 6.3f, 0);

            Render(worldSpace, null, camera.transformlocal * Matrix4.CreatePerspectiveFieldOfView(1.2f, 1.3f, .1f, 1000));
        }

        public static void Render(Node self, Node parent, Matrix4 transform)
        {
            var transform2 = self.transformlocal * transform;
            if (self.mesh != null)
            {
                self.mesh.Render(self.shader, transform2, self.texture);
            }
                
            foreach (Node child in self.children)
            {
                Render(child, self, transform2);
            }
        }
        //transform all method
        //root
    }
}
