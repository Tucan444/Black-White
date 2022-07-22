Shader "Unlit/DL.v0.1"
{
    Properties
    {
        _Color ("Color", Color) = (0, 0, 0, 0)
        _LightPos ("LightPos", Vector) = (0, 0, 0, 0)
        _Power ("Power", Float) = 6
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

            fixed4 frag (v2f i) : SV_Target
            {
                float4 position = float4(i.worldPos, 0) - _LightPos;
                float m = sqrt(pow(position[0], 2) + pow(position[1], 2) + pow(position[2], 2));
                float inverse_m = 1 / m;
                position = float4(position[0] * inverse_m, position[1] * inverse_m, position[2] * inverse_m, 0);

                // normalize above value
                float d = min(1, dot(i.normal, position) + 1 + (1 - max(0, (_Power - m) / _Power)));

                float4 color = float4(_Color[0] * d, _Color[1] * d, _Color[2] * d, 0);
                float4 color_ = float4(m, m, m, 0);
                return color;
            }
            ENDCG
        }
    }
}
