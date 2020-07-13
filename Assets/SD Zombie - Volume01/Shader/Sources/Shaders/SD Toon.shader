Shader "SD Toon" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Main Texture", 2D) = "white" {}
		_Ramp ("ToonRamp", 2D) = "gray" {}
		_Outline_Bold("Outline Bold", Range(0, 1)) = 0.1
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

	cull front
	Pass
	{
		CGPROGRAM
		#pragma vertex _VertexFuc
		#pragma fragment _FragmentFuc
		#include "UnityCG.cginc"

			struct ST_VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct ST_VertexOutput
			{
				float4 vertex : SV_POSITION;
			};

			float _Outline_Bold;

			ST_VertexOutput _VertexFuc(ST_VertexInput stInput)
			{
				ST_VertexOutput stOutput;

				float3 fNormalized_Normal = normalize(stInput.normal);
				float3 fOutline_Position = stInput.vertex + fNormalized_Normal * (_Outline_Bold * 0.1f);

				stOutput.vertex = UnityObjectToClipPos(fOutline_Position);
				return stOutput;
			}


			float4 _FragmentFuc(ST_VertexOutput i) : SV_Target
			{
				return 0.0f;
			}

		ENDCG
	}

	cull back
		CGPROGRAM
		#pragma surface surf ToonRamp

		sampler2D _Ramp;

		#pragma lighting ToonRamp exclude_path:prepass

			inline half4 LightingToonRamp (SurfaceOutput s, half3 lightDir, half atten)
			{
				#ifndef USING_DIRECTIONAL_LIGHT
				lightDir = normalize(lightDir);
				#endif
				
				half d = dot (s.Normal, lightDir)*0.5 + 0.5;
				half3 ramp = tex2D (_Ramp, float2(d,d)).rgb;
				
				half4 c;
				c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
				c.a = 0;
				return c;
			}

			sampler2D _MainTex;
			float4 _Color;

			struct Input
			{
				float2 uv_MainTex : TEXCOORD0;
			};

			void surf (Input IN, inout SurfaceOutput o)
			{
				half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}

		ENDCG

	}

	Fallback "Diffuse"
}