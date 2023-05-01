Shader "RoyalBirb/Pillar"
{
	Properties
	{
		[HDR]_Color1("High Color", Color) = (1, .5, .5, 1)
		[HDR]_Color2("Low Color", Color) = (.5, 0,  0, 1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 norm : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 norm : TEXCOORD0;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.norm = UnityObjectToWorldNormal(v.norm);
				return o;
			}

			float4 _Color1;
			float4 _Color2;

			fixed4 frag (v2f i) : SV_Target
			{
				return lerp(_Color2, _Color1, dot(i.norm, _WorldSpaceLightPos0) * .5 + .5);
			}
			ENDCG
		}
	}
}
