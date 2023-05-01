Shader "RoyalBirb/Ground Grid"
{
	Properties
	{
		[HDR]_Color1("High Color", Color) = (1, .5, .5, 1)
		[HDR]_Color2("Low Color", Color) = (.5, 0,  0, 1)
		_Density("Grid Density", Float) = 1
		_Width("Grid Width", Range(.05, .25)) = 1
		_Offset("Offset", Float) = 1
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="TransparentCutout" }
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
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
				float3 norm : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 norm : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
			};
			
			float _Density;

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.pos);
				o.uv = 0;
				o.norm = UnityObjectToWorldNormal(v.norm);
				o.worldPos = mul(unity_ObjectToWorld, v.pos) * _Density;
				return o;
			}
			
			float _Width;
			float _Offset;
			float4 _Color1;
			float4 _Color2;

			fixed4 frag (v2f i) : SV_Target
			{
				i.uv = frac(i.worldPos.xz - float2(_Offset, 0));
				float grid = saturate(
					step(i.uv.x, _Width) + step(1 - i.uv.x, _Width) +
					step(i.uv.y, _Width) + step(1 - i.uv.y, _Width));
				clip(grid-1);

				return lerp(_Color2, _Color1, dot(i.norm, _WorldSpaceLightPos0) * .5 + .5);
			}
			ENDCG
		}
	}
}
