#version 330

// shader input
in vec2 P;						// fragment position in screen space
in vec2 uv;						// interpolated texture coordinates
uniform sampler2D pixels;		// input texture (1st pass render target)

// shader output
out vec3 outputColor;

void main()
{
	// retrieve input pixel
	outputColor = texture( pixels, uv ).rgb;
	// apply postprocessing effect
	float dx = P.x - 0.5, dy = P.y - 0.5;
	float distance = sqrt( dx * dx + dy * dy );
	outputColor *= sin( distance * 7.0f ) * 0.3f + 0.75f;
	//outputColor = texture( pixels, uv - vec2(sin(distance),0)).rgb ;
}

// EOF