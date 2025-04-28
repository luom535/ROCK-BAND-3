Shader "Custom/SpriteOutline"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (1,0,0,1)
        _OutlineThickness ("Outline Thickness", Range(0, 10)) = 0
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                half2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            fixed4 _Color;
            fixed4 _OutlineColor;
            float _OutlineThickness;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, IN.texcoord);
                float alpha = col.a;

                if (alpha == 0)
                {
                    float outlineAlpha = 0;
                    float2 offset = _OutlineThickness * _MainTex_TexelSize.xy;

                    outlineAlpha += tex2D(_MainTex, IN.texcoord + float2(offset.x, 0)).a;
                    outlineAlpha += tex2D(_MainTex, IN.texcoord + float2(-offset.x, 0)).a;
                    outlineAlpha += tex2D(_MainTex, IN.texcoord + float2(0, offset.y)).a;
                    outlineAlpha += tex2D(_MainTex, IN.texcoord + float2(0, -offset.y)).a;

                    outlineAlpha = step(0.01, outlineAlpha);

                    return _OutlineColor * outlineAlpha;
                }

                return col * _Color;
            }
            ENDCG
        }
    }
}
