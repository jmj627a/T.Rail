Shader "Custom/fire" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex2("Albedo (RGB)", 2d) = "white" {}
	}
	SubShader {
		// 알파는 실제 게임에서 일반 오브젝트와 매우 다르게 처리되며, 상당히 무거운 연산 중 하나이다.
		// 일단 임시로 작동하도록 만든다고 하지만(예제에서) 매우 무거운 연산
		Tags { "RenderType"="Transparent" "Queue"="Transparent"}
		//Tags { "RenderType" = "Opaque"}
		LOD 200
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alpha:fade
		//#pragma surface surf Standard fullforwardshadows

		sampler2D _MainTex;
		sampler2D _MainTex2;
		struct Input {
			float2 uv_MainTex;
			float2 uv_MainTex2;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) ;
			fixed4 d = tex2D(_MainTex2, float2(IN.uv_MainTex2.x, IN.uv_MainTex2.y-_Time.y));
			o.Emission = c.rgb * d.rgb;
			o.Alpha = d.a * c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
