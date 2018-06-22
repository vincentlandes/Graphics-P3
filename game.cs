using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


// minimal OpenTK rendering framework for UU/INFOGR
// Jacco Bikker, 2016

namespace Template_P3
{

    class Game
    {
        // member variables
        public Surface screen;                  // background surface for printing etc.
        public static Mesh teapot, floor, dummy;// a mesh to draw using OpenGL
        public static Shader shader;            // shader to use for rendering
        Shader postproc;                        // shader to use for post processing
        public static Texture wood, glass;      // texture to use for rendering
        RenderTarget target;                    // intermediate render target
        ScreenQuad quad;                        // screen filling quad for post processing
        bool useRenderTarget = true;
        public scenegraph scenegraph;

        // initialize
        public void Init()
        {
            // load teapot
            teapot = new Mesh("../../assets/teapot.obj");
            floor = new Mesh("../../assets/floor.obj");
            dummy = new Mesh("../../assets/dummy_obj.obj");
            // create shaders
            shader = new Shader("../../shaders/vs.glsl", "../../shaders/fs.glsl");
            postproc = new Shader("../../shaders/vs_post.glsl", "../../shaders/fs_post.glsl");
            // load a texture
            wood = new Texture("../../assets/wood.jpg");
            glass = new Texture("../../assets/glass.jpg");
            // create the render target
            target = new RenderTarget(screen.width, screen.height);
            quad = new ScreenQuad();
            scenegraph = new scenegraph();
            scenegraph.Init(shader);
        }

        public static Mesh GetTeapot => teapot;
        public static Mesh GetFloor => floor;
        public static Shader GetShader => shader;
        public static Texture GetTexture => wood;
        public static Texture GetTextureGlass => glass;
        public static Mesh GetDummy => dummy;


        // tick for background surface ///////////////////////////update methode
        public void Tick()
        {
            screen.Clear(0);
            screen.Print("hello world", 2, 2, 0xffff00);
            var keyboard = OpenTK.Input.Keyboard.GetState();

            if (keyboard[OpenTK.Input.Key.Up])
                scenegraph.camera.transformlocal *= Matrix4.CreateTranslation(0, -0.5f, 0);
            if (keyboard[OpenTK.Input.Key.Down])
                scenegraph.camera.transformlocal *= Matrix4.CreateTranslation(0, 0.5f, 0);
            if (keyboard[OpenTK.Input.Key.Left])
                scenegraph.camera.transformlocal *= Matrix4.CreateTranslation(0.5f, 0, 0);
            if (keyboard[OpenTK.Input.Key.Right])
                scenegraph.camera.transformlocal *= Matrix4.CreateTranslation(-0.5f, 0, 0);
            if (keyboard[OpenTK.Input.Key.J])
                scenegraph.camera.transformlocal *= Matrix4.CreateTranslation(0, 0, 0.5f);
            if (keyboard[OpenTK.Input.Key.K])
                scenegraph.camera.transformlocal *= Matrix4.CreateTranslation(0, 0, -0.5f);
            if (keyboard[OpenTK.Input.Key.D])
                scenegraph.camera.transformlocal *= Matrix4.CreateRotationY(-0.01f);
            if (keyboard[OpenTK.Input.Key.A])
                scenegraph.camera.transformlocal *= Matrix4.CreateRotationY(0.01f);
        }

        // tick for OpenGL rendering code
        public void RenderGL()
        {

            if (useRenderTarget)
            {
                // enable render target
                target.Bind();
                scenegraph.SceneGraph();

                // render quad
                target.Unbind(); //gwn laten
                quad.Render(postproc, target.GetTextureID());
            }
        }
    }

} // namespace Template_P3