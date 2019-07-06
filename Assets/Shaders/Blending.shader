Shader "Custom/Blending"
{
    Properties
    {
        _MainTex ("Main Tex", 2D) = "white" {}
        _ShadowTex ("Shadow Tex", 2D) = "white" {}

    }
    SubShader
    {
        // 在所有不透明对象之后绘制自己，更加靠近屏幕
        Tags{ "Queue" = "Transparent" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _ShadowTex;
            float4 _ShadowTex_ST;

            struct v2f
            {
                float4 pos : POSITION;
                half2 uv : TEXCOORD0;
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

                return o;
            }

            float4 frag (v2f i) : COLOR
            {
                half blending_rate = tex2D(_ShadowTex, i.uv).r;
                half3 col = tex2D(_MainTex, i.uv);
                return float4(col * blending_rate + (col * 0.2) * (1 - blending_rate), 1.0f);
            }

            ENDCG
        }

    }
    FallBack "Diffuse"
}
