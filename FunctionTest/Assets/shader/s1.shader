Shader "Custom/s1" {
	Properties {

		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_FlowSpeed("Flow Speed", Range(0,1)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		sampler2D _MainTex;
		float _FlowSpeed;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex + _Time.y) ; u와v 방향 둘다로 더함
			//fixed4 c = tex2D(_MainTex, float2(IN.uv_MainTex.x + _Time.y, IN.uv_MainTex.y)); // x방향으로만 더하기 
			fixed4 c = tex2D(_MainTex, float2(IN.uv_MainTex.x + _Time.y*_FlowSpeed, IN.uv_MainTex.y)); // x방향으로만 더하기+ 시간제어
			o.Albedo = c.rgb;
			
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
