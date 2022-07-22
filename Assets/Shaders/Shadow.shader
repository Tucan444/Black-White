Shader "Unlit/Shadow"
{
    Properties
    {
        White ("White", Int) = 0
        PlayerPos ("PlayerPos", Vector) = (0, 0, 0)
        AreaEffect ("AreaEffect", Float) = 2
        Shallow ("Shallow", Int) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 normal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul (unity_ObjectToWorld, v.vertex);
                o.normal = normalize( mul( float4( v.normal, 0.0 ), unity_WorldToObject ).xyz );

                return o;
            }

            int White;
            float3 PlayerPos;
            float AreaEffect;
            int Shallow;

            fixed4 frag (v2f i) : SV_Target
            {
                uint x = distance(i.worldPos, PlayerPos) < AreaEffect;
                uint d = (uint)(White + x + (Shallow * ((White == 0) && (x)))) % 2;
                return float4(0, 0, 0, 1) * d + (float4(1, 1, 1, 1) * d);
            }
            ENDCG
        }
    }
}
