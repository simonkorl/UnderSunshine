Shader "Custom/Rays"
{
    Properties
    {
        _MainTex ("Main Tex", 2D) = "white" {}
        _Angle ("Angle", Range(0, 180)) = 90
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

            half3 _BackgroundColor;

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
                half3 col = tex2D(_MainTex, i.uv);
                if(col.r == 0.0f && col.g == 1.0f && col.b == 0.0f)
                    return fixed4(1.0f, 1.0f, 1.0f, 1.0f);
                else
                    return fixed4(0.0f, 0.0f, 0.0f, 1.0f);
            }

            ENDCG
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Angle;

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
                int cnt = 20;
                float stepLength = 0.002;
                half2 step = half2(cos(_Angle / 180 * 3.14159265), sin(_Angle / 180 * 3.14159265)) * stepLength;

                [unroll(40)]
                for(int it = 0; it < cnt; it++)
                {
                    half2 pos = i.uv + step * it;
                    if(pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1)
                        break;
                    if(tex2D(_MainTex, pos).r < 0.5)
                        return fixed4(0.0f, 0.0f, 0.0f, 1.0f);
                }

                return fixed4(1.0f, 1.0f, 1.0f, 1.0f);

            }

            ENDCG
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Angle;

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
                int cnt = 20;
                float stepLength = 0.002;
                half2 step = half2(cos(_Angle / 180 * 3.14159265), sin(_Angle / 180 * 3.14159265)) * cnt * stepLength;

                [unroll(40)]
                for(int it = 0; it < cnt; it++)
                {
                    half2 pos = i.uv + step * it;
                    if(pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1)
                        break;
                    if(tex2D(_MainTex, pos).r < 0.5)
                        return fixed4(0.0f, 0.0f, 0.0f, 1.0f);
                }

                return fixed4(1.0f, 1.0f, 1.0f, 1.0f);

            }

            ENDCG
        }

    }
    FallBack "Diffuse"
}
