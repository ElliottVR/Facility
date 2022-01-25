// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit alpha-cutout shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "DoubleSided_IF/Unlit/Transparent Waving" {
Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	_Speed ("Speed", Range(0, 5.0)) = 1 
	_Frequency ("Frequency", Range(0, 1.3)) = 1 
	_Amplitude ("Amplitude", Range(0, 5.0)) = 1

}
SubShader {
	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	LOD 100
  	Cull Off

	Lighting Off

	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				
			};

			struct v2f {
				//float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				float4 pos : SV_POSITION; 
				float2 uv : TEXCOORD0;

			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _Cutoff;
			float _Speed; 
			float _Frequency; 
			float _Amplitude;


			v2f vert (appdata_t v)
			{
				v2f o;
				//o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				v.vertex.y += cos((v.vertex.x + _Time.y * _Speed) * _Frequency) * _Amplitude * (v.vertex.x - 5); 
				o.pos = UnityObjectToClipPos(v.vertex); 
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				clip(col.a - _Cutoff);
return tex2D(_MainTex, i.uv);

				return col;
			}
		ENDCG
	}
}

}