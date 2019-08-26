Shader "Custom/WrapLit"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Radius ("Radius", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		float _Radius;

        struct Input
        {
            float2 uv_MainTex;
        };
		
		void vert (inout appdata_full v)
		{
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
		}

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
