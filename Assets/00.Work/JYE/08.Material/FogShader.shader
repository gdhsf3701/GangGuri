// Upgrade NOTE: commented out 'float3 _WorldSpaceCameraPos', a built-in variable

Shader "JYE/FogMeshShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _SurfaceColor ("Surface Color", Color) = (1,1,1,1)
        _InsideFogColor ("Inside Fog Color", Color) = (0.4, 0.6, 0.9, 1)
        _BoxSize ("Box Size (World Units)", Vector) = (1,1,1)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            sampler2D _MainTex;
            float4 _SurfaceColor;
            float4 _InsideFogColor;
            float3 _BoxSize;

            // float3 _WorldSpaceCameraPos;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float3 objCenter = mul(unity_ObjectToWorld, float4(0,0,0,1)).xyz;
                float3 halfSize = _BoxSize * 0.5;

                float3 delta = abs(_WorldSpaceCameraPos - objCenter);
                bool inside = all(delta < halfSize);

                if (inside)
                {
                    return _InsideFogColor;
                }
                else
                {
                    float4 tex = tex2D(_MainTex, i.uv);
                    return tex * _SurfaceColor;
                }
            }
            ENDCG
        }
    }
}
