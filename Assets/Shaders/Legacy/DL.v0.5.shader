Shader "Unlit/DL.v0.5"
{
    Properties
    {
        _Color ("Color", Color) = (0, 0, 0, 0)
        _LightPos ("LightPos", Vector) = (0, 0, 0, 0)
        _Power ("Power", Float) = 6

        FlashPower ("FlashPower", Float) = 24
        FlashPos ("FlashPos", Vector) = (0, 0, 0, 0)
        FlashDirection ("FlashDirection", Vector) = (0, 0, 0, 0)
        FlashLimit ("FlashLimit", Float) = 0.86

        WaveHalfWidthInversed ("WaveHalfWidthInversed", Float) = 0.5
        WaveCenter ("WaveCenter", Vector) = (0, 0, 0, 0)
        WaveRadius ("WaveRadius", Float) = 0
        WaveActive ("WaveActive", Int) = 0

        P0 ("P0", Vector) = (-1, 0, 0, 0)
        P1 ("P1", Vector) = (1, 0, 0, 0)
        P2 ("P2", Vector) = (0, 1, 0, 0)
        P3 ("P3", Vector) = (0, 0, 1, 0)
        N0 ("N0", Vector) = (0, 0, 1, 0)
        N1 ("N1", Vector) = (-0.577, -0.577, -0.577, 0)
        N2 ("N2", Vector) = (0.577, -0.577, -0.577, 0)
        N3 ("N3", Vector) = (0, -1, 0, 0)
        DirectionalLight ("DirectionalLight", Vector) = (0.577, -0.577, 0.577, 0)
        Ambiance ("Ambiance", Float) = 0.6
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
  
              struct appdata {
                  float4 vertex : POSITION;
                  float3 normal : NORMAL;
                  
              };
  
              struct v2f {
                  float4 vertex : SV_POSITION;
                  float3 worldPos : TEXCOORD0;
                  float3 normal : TEXCOORD1;
              };
              
  
              v2f vert(appdata v) {
                    v2f o;

                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.worldPos = mul (unity_ObjectToWorld, v.vertex);
                    o.normal = normalize( mul( float4( v.normal, 0.0 ), unity_WorldToObject ).xyz );
  
                    return o;
              }
            
            float4 normalize(float4 v) {
                float magnitude = sqrt(pow(v[0], 2) + pow(v[1], 2) + pow(v[2], 2));
                float inversed_magnitude = 1 / magnitude;
                v = float4(v[0] * inversed_magnitude, v[1] * inversed_magnitude, v[2] * inversed_magnitude, 0);
                return v;
            }

            float4 _Color;
            float4 _LightPos;
            float _Power;

            float FlashPower;
            float4 FlashPos;
            float4 FlashDirection;
            float FlashLimit;

            float WaveHalfWidthInversed;
            float4 WaveCenter;
            float WaveRadius;
            int WaveActive;

            float4 P0;
            float4 P1;
            float4 P2;
            float4 P3;
            float4 N0;
            float4 N1;
            float4 N2;
            float4 N3;
            float4 DirectionalLight;
            float Ambiance;

            fixed4 frag (v2f i) : SV_Target
            {
                // point lights

                float4 position = float4(i.worldPos, 0) - _LightPos;
                float m = sqrt(pow(position[0], 2) + pow(position[1], 2) + pow(position[2], 2));
                float inverse_m = 1 / m;
                position = float4(position[0] * inverse_m, position[1] * inverse_m, position[2] * inverse_m, 0);

                float a = (min(1, dot(i.normal, position) * 0.6) + 0.6 + (1 - max(0, (_Power - m) / _Power)));

                float4 color = float4(_Color[0] * a, _Color[1] * a, _Color[2] * a, 0);
                
                // flash light

                position = float4(i.worldPos, 0) - FlashPos;
                m = sqrt(pow(position[0], 2) + pow(position[1], 2) + pow(position[2], 2));
                inverse_m = 1 / m;
                position = float4(position[0] * inverse_m, position[1] * inverse_m, position[2] * inverse_m, 0);

                if (dot(position, FlashDirection) > FlashLimit) {
                    a = min(1, (dot(i.normal, position) * 0.6) + 0.6 + (1 - max(0, (FlashPower - m) / FlashPower)));
                    color = float4(color[0] * a, color[1] * a, color[2] * a, 0);
                }

                // light wave

                if (WaveActive == 1) {
                    position = float4(i.worldPos, 0) - WaveCenter;
                    m = sqrt(pow(position[0], 2) + pow(position[1], 2) + pow(position[2], 2));

                    a = min(1, abs(m - WaveRadius) * WaveHalfWidthInversed);
                    color = float4(color[0] * a, color[1] * a, color[2] * a, 0);
                }

                // tetrahedron

                float determinant = 0;
                position = normalize(float4(i.worldPos, 0) - P0);

                determinant += ceil(-dot(position, N0)) * 1;

                //color = float4((dot(position, N0) * 0.5) + 0.5, Ambiance, 0, 0);
                //return color;

                position = normalize(float4(i.worldPos, 0) - P1);

                determinant += ceil(-dot(position, N1)) * 1;

                position = normalize(float4(i.worldPos, 0) - P2);

                determinant += ceil(-dot(position, N2)) * 1;

                position = normalize(float4(i.worldPos, 0) - P3);

                determinant += ceil(-dot(position, N3)) * 1;

                a = min(1, min(Ambiance, min(1, dot(i.normal, DirectionalLight) + 1)) + determinant);

                color = float4(color[0] * a, color[1] * a, color[2] * a, 0);
                //color = float4(determinant / 4, 0, 0, 0);

                return color;
            }
            ENDCG
        }
    }
}
