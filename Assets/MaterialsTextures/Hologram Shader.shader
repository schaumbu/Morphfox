Shader "Unlit/Hologram Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

		ZWrite OFF
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha

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
				float4 world : TEXTCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
				o.world = v.vertex;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				// sample the texture
				
				float t = _Time + i.world.y*.1;
				float v = sin(t * 50) * cos(t * 112) * sin(t * 13);
				float m = smoothstep(.1, .2, v * 0.5 + 0.5);
				float m2 = smoothstep(.0, .3, v * 0.5 + 0.5);


				i.uv.x += sin(m2);
				fixed4 col = tex2D(_MainTex, i.uv) * m;
				col.a *= sin(i.world.y*100 - _Time*20) * .5 + .5 + .3;
                return col;
            }
            ENDCG
        }
    }
}
