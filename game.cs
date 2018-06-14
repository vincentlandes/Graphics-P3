﻿using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

// minimal OpenTK rendering framework for UU/INFOGR
// Jacco Bikker, 2016

namespace Template_P3 {

class Game
{
	// member variables
	public Surface screen;					// background surface for printing etc.
	public static Mesh teapot, floor;						// a mesh to draw using OpenGL
	const float PI = 3.1415926535f;			// PI
	float a = 0;							// teapot rotation angle
	Stopwatch timer;						// timer for measuring frame duration
	public static Shader shader;							// shader to use for rendering
	Shader postproc;						// shader to use for post processing
	public static Texture wood;							// texture to use for rendering
	RenderTarget target;					// intermediate render target
	ScreenQuad quad;						// screen filling quad for post processing
	bool useRenderTarget = true;

	// initialize
	public void Init()
	{
		// load teapot
		teapot = new Mesh( "../../assets/teapot.obj" );
		floor = new Mesh( "../../assets/floor.obj" );
		// initialize stopwatch
		timer = new Stopwatch();
		timer.Reset();
		timer.Start();
		// create shaders
		shader = new Shader( "../../shaders/vs.glsl", "../../shaders/fs.glsl" );
		postproc = new Shader( "../../shaders/vs_post.glsl", "../../shaders/fs_post.glsl" );
		// load a texture
		wood = new Texture( "../../assets/wood.jpg" );
		// create the render target
		target = new RenderTarget( screen.width, screen.height );
		quad = new ScreenQuad();
	}

        public static Mesh GetMesh => teapot;
        public static Shader GetShader => shader;
        public static Texture GetTexture => wood;


	// tick for background surface
	public void Tick()
	{
		screen.Clear( 0 );
		screen.Print( "hello world", 2, 2, 0xffff00 );
	}

	// tick for OpenGL rendering code
	public void RenderGL()
	{
		// measure frame duration
		float frameDuration = timer.ElapsedMilliseconds;
		timer.Reset();
		timer.Start();
	
		// prepare matrix for vertex shader
		Matrix4 transform = Matrix4.CreateFromAxisAngle( new Vector3( 0, 1, 0 ), a );
            Matrix4 toWorld = transform;
		transform *= Matrix4.CreateTranslation( 0, -4, -15 );
		transform *= Matrix4.CreatePerspectiveFieldOfView( 1.2f, 1.3f, .1f, 1000 );

		// update rotation
		a += 0.001f * frameDuration; 
		if (a > 2 * PI) a -= 2 * PI;

		if (useRenderTarget)
		{
			// enable render target
			target.Bind();

			// render scene to render target
			//teapot.Render( shader, transform, wood ); //vervangen voor render method in scenegraph
			//floor.Render( shader, transform, wood );

			// render quad
			target.Unbind(); //gwn laten
			quad.Render( postproc, target.GetTextureID() );
		}
		else
		{
			// render scene directly to the screen
			teapot.Render( shader, transform, wood ); //wel verneuken
			floor.Render( shader, transform, wood );
		}
	}
}

} // namespace Template_P3