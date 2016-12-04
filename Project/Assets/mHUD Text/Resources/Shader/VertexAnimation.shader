Shader "Custom/VertexAnimation" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_StartTime("StartTime", Float) = 0
	}
	SubShader {
		Tags {
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma glsl_no_auto_normalization 
			
			#include "UnityCG.cginc"
			
			struct appdata_m 
			{
				float4 vertex : POSITION;
				float3 vertex1 : NORMAL;
				float4 vertex2 : TANGENT;
				float2 vertex3 : TEXCOORD1;

				float2 texcoord : TEXCOORD0;
				
				fixed4 color : COLOR;
			};
			
			struct v2f_m
			{
				float4 pos : SV_POSITION;
				half2 texcoord   : TEXCOORD0;
				fixed4 color : COLOR;
			};

		

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _StartTime;
			float _SamplePoint1;
			
			float _CurTime;
			float _Factor;


			float3 _vertex1;
			float3 _vertex2;
			float3 _vertex3;
			float3 _vec;
			fixed _alpha;
			v2f_m vert (appdata_m v)
			{
				v2f_m o;
				
				_CurTime = _Time.y - _StartTime;
				
				if(_CurTime <= v.vertex1.z) {
					_vertex1 = float3(v.vertex1.x, v.vertex1.y, 0);
					_Factor = _CurTime / v.vertex1.z;
					_vec = lerp(v.vertex.xyz, _vertex1, _Factor);
					_alpha = lerp(v.color.a, v.color.r, _Factor);
				}
				else if (_CurTime <= v.vertex2.z) { 
					
					_vertex1 = float3(v.vertex1.x, v.vertex1.y, 0);
					_vertex2 = float3(v.vertex2.x, v.vertex2.y, 0);

					_Factor =  (_CurTime - v.vertex1.z) / (v.vertex2.z - v.vertex1.z);

					_vec = lerp(_vertex1, _vertex2, _Factor);
					_alpha = lerp(v.color.r, v.color.g, _Factor);
				} 
				else {
					
					_vertex2 = float3(v.vertex2.x, v.vertex2.y, 0);
					_vertex3 = float3(v.vertex2.w, v.vertex3.x, 0);
					
					_Factor =  (_CurTime - v.vertex2.z) / (v.vertex3.y - v.vertex2.z);
					
					_vec = lerp(_vertex2, _vertex3, _Factor);
					_alpha = lerp(v.color.g, v.color.b, _Factor);
				}
					 


				o.pos = mul(UNITY_MATRIX_MVP, float4(_vec, 1) );
				o.texcoord = v.texcoord;
				o.color = fixed4(1,1,1,_alpha);
				return o;
			}

			fixed4 frag (v2f_m i) : COLOR
			{
				fixed4 col;
				col = tex2D(_MainTex, i.texcoord);
				col.a *= i.color.a;
				return col;
			}
			ENDCG
		}


	} 
	FallBack "Diffuse"
}
