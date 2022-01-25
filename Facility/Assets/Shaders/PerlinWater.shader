// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "PerlinWater" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
	//_MainTex ("Base (RGB) RefStrGloss (A)", 2D) = "white" {}
	_Cube ("Reflection Cubemap", Cube) = "" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_MainTex ("Texture", 2D) = "white" {}
      _Amount ("Extrusion Amount", Range(-1,1)) = 0.5
      _PerlinScale ("Perlin Scale", Vector) = (1, 1, 1)
      _PerlinOffset ("Perlin Offset", Vector) = (0, 0, 0)
      _PerlinSpeed ("Perlin Speed", Vector) = (0, 0, 0)
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 400
CGPROGRAM
//#pragma surface surf BlinnPhong
#pragma target 3.0
#pragma surface surf Lambert vertex:vert

 #include "noiseSimplex.cginc"

	float3 _PerlinScale;
	float3 _PerlinOffset;
	float3 _PerlinSpeed;

	sampler2D _MainTex;
	sampler2D _BumpMap;
	samplerCUBE _Cube;

	fixed4 _Color;
	fixed4 _ReflectColor;
	half _Shininess;

struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
	float3 worldRefl;
	INTERNAL_DATA
};

float _Amount;


void vert (inout appdata_full v)
      {
          v.vertex.xyz += v.normal * _Amount * snoise(v.vertex.xyz * _PerlinScale + _PerlinOffset + _Time*_PerlinSpeed);
      }


void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	fixed4 c = tex * _Color;
	o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
	
	o.Gloss = tex.a;
	o.Specular = _Shininess;
	
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	
	float3 worldRefl = WorldReflectionVector (IN, o.Normal);
	fixed4 reflcol = texCUBE (_Cube, worldRefl);
	reflcol *= tex.a;
	o.Emission = reflcol.rgb * _ReflectColor.rgb;
	o.Alpha = reflcol.a * _ReflectColor.a;
}
ENDCG
}

FallBack "Legacy Shaders/Reflective/Bumped Diffuse"
}
