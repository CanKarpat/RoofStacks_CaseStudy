Shader "Paint in 3D/Opaque"
{
	Properties
	{
		_MainTex("Main Tex", 2D) = "white" {}
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Color1("Color 1", Color) = (0.686, 0.686, 0.686, 1.0)
		_Color2("Color 2", Color) = (1.0, 1.0, 1.0, 1.0)
		_Rim("Rim", Float) = 0.65
		_Shift("Shift", Float) = 2.56
		[Toggle(P3D_A)]_Clamp("Clamp", Float) = 0
	}

	SubShader
	{
		Cull Off

		Tags
		{
			"Queue" = "Geometry" "DisableBatching" = "True"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag
			// Clamp
			#pragma multi_compile __ P3D_A

			sampler2D _MainTex;
			float4    _Color;
			float4    _Color1;
			float4    _Color2;
			float     _Rim;
			float     _Shift;

			struct a2v
			{
				float4 vertex    : POSITION;
				float2 texcoord0 : TEXCOORD0;
				float3 normal    : NORMAL;
				float4 color     : COLOR;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv     : TEXCOORD0;
				float3 normal : TEXCOORD1;
				float3 direct : TEXCOORD2;
				float4 color  : COLOR;
			};

			void Vert(a2v i, out v2f o)
			{
				o.vertex = UnityObjectToClipPos(i.vertex);
				o.uv     = i.texcoord0;
				o.normal = mul((float3x3)unity_ObjectToWorld, i.normal.xyz).xyz;
				o.direct = _WorldSpaceCameraPos - mul(unity_ObjectToWorld, i.vertex).xyz;
				o.color  = i.color * _Color;
			}

			void Frag(v2f i, out float4 o:COLOR0)
			{
				float d = abs(dot(normalize(i.normal), normalize(i.direct)));
#if P3D_A // Clamp
				d = saturate(d);
#endif
				float r = _Shift - pow(1.0f - d, _Rim);

				o = tex2D(_MainTex, i.uv) * lerp(_Color1, _Color2, r) * i.color;
			}
			ENDCG
		} // Pass
	} // SubShader
} // Shader