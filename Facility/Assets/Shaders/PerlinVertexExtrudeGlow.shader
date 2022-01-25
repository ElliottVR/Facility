Shader "Glow Normal Extrusion " {
    Properties {
    	 _MainColor ("Main Color", Color) = (1,1,1,1)
	 _MainTex ("Texture", 2D) = "white" {}
	 _BumpMap ("Bumpmap", 2D) = "bump" {}
     _GlowColor ("Glow Color", Color) = (1,0.58,0,1)
      _GlowPower ("Glow Power", Range(10,0.5)) = 10
      _Albedo ("Albedo", Range(0,1)) = 0
      _Emission("Emission", Range(1,2)) = 1.3
	 _OcclusionPower ("Occlusion power", Range(0, 3)) = 0.4
      _Occlusion ("Occlusion", 2D) = "white" {}
      _Amount ("Extrusion Amount", Range(-1,1)) = 0.5
      _PerlinScale ("Perlin Scale", Vector) = (1, 1, 1)
      _PerlinOffset ("Perlin Offset", Vector) = (0, 0, 0)
      _PerlinSpeed ("Perlin Speed", Vector) = (0, 0, 0)
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }


      CGPROGRAM
      #pragma surface surf Lambert vertex:vert
      #include "noiseSimplex.cginc"
      
		float3 _PerlinScale;
		float3 _PerlinOffset;
		float3 _PerlinSpeed;
      
      struct Input {
          float2 uv_MainTex;
		float2 uv_BumpMap;
          float2 uv_Occlusion;
          float3 viewDir;
      };

      float _Amount;
	float4 _MainColor;
      float _Albedo;
      float _Emission;
      sampler2D _BumpMap;
      float4 _GlowColor;
      float _GlowPower;
      uniform sampler2D _Occlusion;
      float _OcclusionPower;

      
      void vert (inout appdata_full v)
      {
          v.vertex.xyz += v.normal * _Amount * snoise(v.vertex.xyz * _PerlinScale + _PerlinOffset + _Time*_PerlinSpeed);
      }
      
      sampler2D _MainTex;
      
      
void surf (Input IN, inout SurfaceOutput o)
      {
          half4 occ = tex2D(_Occlusion, IN.uv_Occlusion);
      	  float4 texture0 = tex2D (_MainTex, IN.uv_MainTex) * _MainColor.rgba;
      	  float4 texture1 = tex2D (_MainTex, IN.uv_MainTex).rgba * _Albedo + texture0.rgba;
          o.Albedo = lerp(texture1.rgb, texture1.rgb * occ.rgb, _OcclusionPower);
          o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
          half rimLight = _Emission - saturate(dot (normalize(IN.viewDir), o.Normal));
          o.Emission = _GlowColor.rgb * pow (rimLight, _GlowPower);
          o.Alpha = texture0.a + o.Emission.rgb;

      }
      ENDCG
    } 
    Fallback "Diffuse"
  }