Shader "Unlit/DL.v0.3"
{
    Properties
    {
        _Color ("Color", Color) = (0, 0, 0, 0)
        _LightPos ("LightPos", Vector) = (0, 0, 0, 0)
        _Power ("Power", Float) = 6

        FlashPower ("FlashPower", Float) = 12
        FlashPos ("FlashPos", Vector) = (0, 0, 0, 0)
        FlashDirection ("FlashDirection", Vector) = (0, 0, 0, 0)
        FlashLimit ("FlashLimit", Float) = 0.86

        WaveHalfWidthInversed ("WaveHalfWidthInversed", Float) = 0.5
        WaveCenter ("WaveCenter", Vector) = (0, 0, 0, 0)
        WaveRadius ("WaveRadius", Float) = 0
        WaveActive ("WaveActive", Int) = 0
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
                  float4 normal : NORMAL;
                  
              };
  
              struct v2f {
                  float4 vertex : SV_POSITION;
                  float3 worldPos : TEXCOORD0;
                  float4 normal : NORMAL;
              };
              
  
              v2f vert(appdata v) {
                  v2f o;
  
                  o.vertex = UnityObjectToClipPos(v.vertex);
                  o.worldPos = mul (unity_ObjectToWorld, v.vertex);
                  o.normal = v.normal;
  
                  return o;
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

            fixed4 frag (v2f i) : SV_Target
            {
                // point lights

                float4 position = float4(i.worldPos, 0) - _LightPos;
                float m = sqrt(pow(position[0], 2) + pow(position[1], 2) + pow(position[2], 2));
                float inverse_m = 1 / m;
                position = float4(position[0] * inverse_m, position[1] * inverse_m, position[2] * inverse_m, 0);

                float a = min(1, dot(i.normal, position) + 1 + (1 - max(0, (_Power - m) / _Power)));

                float4 color = float4(_Color[0] * a, _Color[1] * a, _Color[2] * a, 0);
                
                // flash light

                position = float4(i.worldPos, 0) - FlashPos;
                m = sqrt(pow(position[0], 2) + pow(position[1], 2) + pow(position[2], 2));
                inverse_m = 1 / m;
                position = float4(position[0] * inverse_m, position[1] * inverse_m, position[2] * inverse_m, 0);

                if (dot(position, FlashDirection) > FlashLimit) {
                    a = min(1, dot(i.normal, position) + 1 + (1 - max(0, (FlashPower - m) / FlashPower)));
                    color = float4(color[0] * a, color[1] * a, color[2] * a, 0);
                }

                // light wave

                if (WaveActive == 1) {
                    position = float4(i.worldPos, 0) - WaveCenter;
                    m = sqrt(pow(position[0], 2) + pow(position[1], 2) + pow(position[2], 2));

                    a = min(1, abs(m - WaveRadius) * WaveHalfWidthInversed);
                    color = float4(color[0] * a, color[1] * a, color[2] * a, 0);
                }

                return color;
            }
            ENDCG
        }
    }
}
