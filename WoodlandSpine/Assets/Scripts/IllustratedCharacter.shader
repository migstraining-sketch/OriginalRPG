Shader "Woodland/Illustrated Character"
{
    Properties
    {
        _MainTex ("Illustrated character on green", 2D) = "white" {}
        _Color ("Painted art tint", Color) = (1,1,1,1)
        _KeyLow ("Green edge start", Range(0,1)) = 0.035
        _KeyHigh ("Green removal", Range(0,1)) = 0.16
    }
    SubShader
    {
        Tags { "Queue"="AlphaTest" "RenderType"="TransparentCutout" }
        Cull Off ZWrite On ZTest LEqual
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float _KeyLow, _KeyHigh;
            struct appdata { float4 vertex:POSITION; float2 uv:TEXCOORD0; };
            struct v2f { float4 position:SV_POSITION; float2 uv:TEXCOORD0; };
            v2f vert(appdata v)
            { v2f o;o.position=UnityObjectToClipPos(v.vertex);o.uv=TRANSFORM_TEX(v.uv,_MainTex);return o; }
            fixed4 frag(v2f i):SV_Target
            {
                fixed4 c=tex2D(_MainTex,i.uv);
                float excess=c.g-max(c.r,c.b);
                c.a*=1-smoothstep(_KeyLow,_KeyHigh,excess);
                clip(c.a-0.05);
                // Suppress key-color spill on the antialiased silhouette only.
                c.g=lerp(min(c.g,max(c.r,c.b)),c.g,c.a);
                return c*_Color;
            }
            ENDCG
        }
    }
    Fallback Off
}
