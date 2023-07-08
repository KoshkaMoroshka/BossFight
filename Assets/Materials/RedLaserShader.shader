Shader "Custom/RedLaserShader"
{
    Properties
    {
        _Color ("Color", Color) = (1, 0, 0, 1)
        _SparkleTexture ("Sparkle Texture", 2D) = "white" {}
        _SparkleSize ("Sparkle Size", Range(0, 1)) = 0.6
        _SparkleSpeed ("Sparkle Speed", Range(0, 10)) = 1
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            float4 _Color;
            sampler2D _SparkleTexture;
            float _SparkleSize;
            float _SparkleSpeed;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color * _Color;
                o.uv = v.vertex.xy * _SparkleSize + _Time.y * _SparkleSpeed;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 sparkleColor = tex2D(_SparkleTexture, i.uv);
                return i.color * sparkleColor;
            }
            ENDCG
        }
    }
}
