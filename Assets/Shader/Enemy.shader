Shader "Custom/GlowOutlineWithTexture" {
    Properties {
        _Color ("Glow Color", Color) = (1, 0, 0, 1)
        _MainTex ("Main Texture", 2D) = "white" {}  
        _FresnelPower ("Fresnel Intensity", Range(0,5)) = 2  
    }

    SubShader {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 200
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:blend
        sampler2D _MainTex;
        fixed4 _Color;
        float _FresnelPower;
        struct Input {
            float2 uv_MainTex;
            float3 viewDir;
            float3 worldNormal;
        };

    void surf (Input IN, inout SurfaceOutputStandard o) {
    fixed4 baseColor = tex2D(_MainTex, IN.uv_MainTex) * _Color;
    float fresnel = pow(1.0 - saturate(dot(IN.viewDir, IN.worldNormal)), _FresnelPower);
    fixed4 glowColor = fixed4(1, 1, 1, 1) * fresnel;
    o.Albedo = lerp(baseColor.rgb, glowColor.rgb, glowColor.a);
    o.Emission = glowColor.rgb * glowColor.a;
    o.Alpha = glowColor.a;
    }
        ENDCG
    }
}