#version 330
 
// shader input
in vec2 uv;						// interpolated texture coordinates
in vec4 normal;					// interpolated normal
in vec3 Position;				// untransformed vertex position
uniform sampler2D pixels;		// texture sampler
uniform vec3 color;				// ambient color
uniform vec3 lightPos = vec3(0,15,-10);
float angle;
float L;
float B = 500;
								//hoe groter hoek tussen normal en invallende straal

// shader output
out vec4 outputColor;

// fragment shader
void main()
{
	L = length(lightPos - Position);
	angle = dot(normalize(lightPos - Position), normal.xyz);


    outputColor = texture( pixels, uv ) * max(0, angle) * (1.0f/(L*L)) * B;
	// + 0.5f * vec4( normal.xyz, 1 );
}