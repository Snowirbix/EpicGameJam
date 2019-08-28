Shader "Custom/FresnelShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
        _Intensity ("Fresnel Intensity", Range(0,9)) = 1
        _Exposure ("Fresnel Exposure", Range(0.25,5)) = 1
        _Cutoff ("Fresnel Cutoff", Range(0, 1)) = 0.6
        _PulseRange ("Pulse Range", Range(0.1, 0.5)) = 0.25
        _PulseSpeed ("Pulse Speed", Range(0.5, 4)) = 2
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 viewDir;
            INTERNAL_DATA
        };

        half _Glossiness;
        half _Metallic;
        half _Intensity;
        half _Exposure;
        half _Cutoff;
        half _PulseRange;
        half _PulseSpeed;
        fixed4 _Color;
        fixed4 _FresnelColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float fresnel = dot(IN.worldNormal, IN.viewDir);
            fresnel = saturate(1 - fresnel);
            fresnel = pow(fresnel, _Exposure + lerp(-_PulseRange/2, _PulseRange/2, sin(_Time.w*_PulseSpeed)));
            if (fresnel < _Cutoff)
            {
                fresnel = 0;
            }
            else
            {
                fresnel = 1;
            }
            o.Emission = _FresnelColor * fresnel * pow(2, _Intensity);
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb * (1 - fresnel) + _FresnelColor * fresnel;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
