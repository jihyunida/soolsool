Shader "Custom/glitch"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Speed ("Speed", Range(0,5)) = 1
        _Amount ("Amount", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Opaque" }
        LOD 100

        Pass
        {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata 
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _Speed;
            float _Amount;
                
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
      
            float2 distortion(float2 p, float t)
            {
                float2 frequency = float2(10.0, 30.0);
                float2 amplitude = float2(0.02, 0.05);
                float2 speed = float2(0.1, 0.2);
                float2 offset = float2(-0.1, 0.1);
                float2 distortion = 0.5 * sin(frequency * p * t + offset);
                distortion += amplitude * (1.0 - smoothstep(0.0, 1.0, p.y));
                distortion *= 1.0 - pow(p.y, 3.0);
                distortion *= step(p.y, 1.0) * step(0.0, p.y);
                
                return distortion;

            }

            float4 frag(v2f i) : SV_Target
            {
                float t = _Time.y * _Speed;
                float2 distortionAmount = distortion(i.uv, t);
                float2 offset = distortionAmount * _Amount;
                float4 color = tex2D(_MainTex, i.uv + offset);

                color *= _Color;

                return color;
            }


            ENDCG
        }
    }
   FallBack "Diffuse"
}
