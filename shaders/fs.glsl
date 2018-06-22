#version 330
 
// shader input
in vec2 uv;						// interpolated texture coordinates
in vec4 normal;					// interpolated normal
in vec3 Position;				// untransformed vertex position
uniform sampler2D pixels;		// texture sampler
uniform vec3 color;				// ambient color
float angle;
float L;
float B = 110;
vec3 posLight;
vec4 colorLight;
uniform mat4 lightInfo;					//hoe groter hoek tussen normal en invallende straal

// shader output
out vec4 outputColor;

// fragment shader
void main()
{
	posLight = vec3(lightInfo[0][0],lightInfo[0][1], lightInfo[0][2]);
	colorLight = vec4(lightInfo[1][0],lightInfo[1][1], lightInfo[1][2], lightInfo[1][3]);
	L = length(posLight - Position);

	angle = dot(normalize(posLight - Position), normalize(normal.xyz)); /////////////////////////is dit phong shading?

    outputColor =  texture( pixels, uv ) * colorLight * max(0, angle) * (1.0f/(L*L)) * B;
}