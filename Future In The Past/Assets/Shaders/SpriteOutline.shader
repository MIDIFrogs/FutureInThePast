Shader "Custom/Sprite Outline" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _OutlineThickness ("Outline Thickness", Range(0.0, 10.0)) = 1.0
    }
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Cull Off
        Blend One OneMinusSrcAlpha
       
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            sampler2D _MainTex;
            fixed4 _Color;
            float4 _MainTex_TexelSize;
            float _OutlineThickness;

            struct v2f {
                float4 pos : SV_POSITION;
                half2 uv : TEXCOORD0;
            };
 
            v2f vert(appdata_base v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }
 
            fixed4 frag(v2f i) : COLOR
            {
                half4 c = tex2D(_MainTex, i.uv);
                c.rgb *= c.a;
                half4 outlineC = _Color;
                outlineC.a *= ceil(c.a);
                outlineC.rgb *= outlineC.a;

                float outlineThickness = _OutlineThickness * _MainTex_TexelSize.x;

                fixed alpha_up = tex2D(_MainTex, i.uv + fixed2(0, outlineThickness)).a;
                fixed alpha_down = tex2D(_MainTex, i.uv - fixed2(0, outlineThickness)).a;
                fixed alpha_right = tex2D(_MainTex, i.uv + fixed2(outlineThickness, 0)).a;
                fixed alpha_left = tex2D(_MainTex, i.uv - fixed2(outlineThickness, 0)).a;

                return lerp(outlineC, c, ceil(alpha_up * alpha_down * alpha_right * alpha_left));
            }  
            ENDCG
        }
    }
    FallBack "Diffuse"
}
