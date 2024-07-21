Shader "Custom/OutlineHighlight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,0,1)
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade

        sampler2D _MainTex;
        fixed4 _OutlineColor;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;

            float outline = 1 - c.a;
            
            o.Emission = lerp(c.rgb, _OutlineColor, step(0.5, outline));
            
            o.Alpha = max(c.a, _OutlineColor.a);
        }
        ENDCG
    }
    FallBack "Diffuse"
}