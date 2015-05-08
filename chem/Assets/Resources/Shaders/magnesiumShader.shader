Shader "Custom/magnesiumShader" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _ColorTint ("Tint", Color) = (.933,.604,.001,.75)
      _Glossiness ("Smoothness", Range(0,1)) = 0.5
	  _Metallic ("Metallic", Range(0,1)) = 1

    }
    SubShader {
      Tags { "RenderType" = "Transparent" }
      CGPROGRAM
      #pragma surface surf Standard finalcolor:mycolor
      struct Input {
          float2 uv_MainTex;
      };
      fixed4 _ColorTint;
      half _Glossiness;
	  half _Metallic;
      void mycolor (Input IN, SurfaceOutputStandard o, inout fixed4 color)
      {
          color *= _ColorTint;
      }
      sampler2D _MainTex;
      void surf (Input IN, inout SurfaceOutputStandard o) {
      	   fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
           o.Albedo = tex.rgb;
           o.Metallic = _Metallic;
		   o.Smoothness = _Glossiness;
    	   o.Alpha = tex.a;
      }
      ENDCG
    } 
    Fallback "Diffuse"
}