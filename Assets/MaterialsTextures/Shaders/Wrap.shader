Shader "Unlit/Wrap"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius ("Radius", float) = 0
        _Mag ("Magnetude", float) = 0
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Radius;
            float _Mag;
            float4 _MainTex_ST;

/*
			float f(float v) {
				return _Radius-sqrt(pow(_Radius,2)-pow(v,2));
			}
			v.vertex.y = f(length(v.vertex.xz));
*/

            v2f vert (appdata v)
            {
                v2f o;
				v.vertex = mul(unity_ObjectToWorld, v.vertex);

				v.vertex.xz -= _WorldSpaceCameraPos.xz;
				float alpha = atan2(v.vertex.x, v.vertex.z);
				float phi = length(v.vertex.xz) / _Radius;
				float3 pos = float3(0, v.vertex.y - _Radius, 0);
				float2x2 rot = float2x2(cos(phi), sin(phi), -sin(phi), cos(phi));
				float2 npos = mul(rot, pos.yz);
				pos.yz = npos.xy;

				rot = float2x2(cos(alpha), sin(alpha), -sin(alpha), cos(alpha));
				pos.xz = mul(rot, pos.xz);

				pos.y += _Radius;
			    v.vertex.xyz = pos.xyz;


				v.vertex.xz += _WorldSpaceCameraPos.xz;
				
				v.vertex = mul(unity_WorldToObject, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
