Shader "Custom/WaveShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        //_Amplitude ("Amplitude", Float) = 1
        //_Steepness ("Steepness", Range(0, 1)) = 0.5
        //_Wavelength ("WaveLength", Float) = 10
        //_Direction("Direction", Vector) = (1,0,0,0)
        _WaveA ("Wave A (dir, steepness, wavelength)", Vector) = (1,0,0.5,10)
        _WaveB ("Wave B", Vector) = (0, 1, 0.25, 20)
        //_Speed ("Speed", Float) = 1


    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard  vertex:vert fullformwardshadows addshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)


        //float _Steepness, _Wavelength; //, _Speed;
        //float2 _Direction;
        float4 _WaveA, _WaveB;

        float3 GerstnerWave (
			float4 wave, float3 p, inout float3 tangent, inout float3 binormal
		) {

            float steepness = wave.z;
            float wavelength = wave.w;

            //Calculate the wave count because I have no clue how to use it as an input so let's divide directly in the shader
            float k = 2 * UNITY_PI /wavelength;
            //Speed of wave based on wave count
            float c = sqrt(9.8 / k);

            //Normalize _Direction vector to make it of unit length
            float2 d = normalize(wave.xy);
            //Wave function
            float f = k * (dot(d,p.xz) - c * _Time.y );
            //Amplitude
            float a = steepness / k;

            //Normalized derivative of the function
            tangent += float3(
				-d.x * d.x * (steepness * sin(f)),
				d.x * (steepness * cos(f)),
				-d.x * d.y * (steepness * sin(f))
			);
			binormal += float3(
				-d.x * d.y * (steepness * sin(f)),
				d.y * (steepness * cos(f)),
				-d.y * d.y * (steepness * sin(f))
			);
            
            return float3 (
                d.x * (a * cos(f)),
                a * sin(f),
                d.y * (a * cos(f))
            );
        }


        void vert (inout appdata_full vertexData) {

            //Get world position of vertex
            float4 worldPos = mul(unity_ObjectToWorld, vertexData.vertex);
            float3 gridPoint = vertexData.vertex.xyz;

            float3 tangent = float3(1, 0, 0);
            float3 binormal = float3(0, 0, 1);
            float3 p = gridPoint;
            
            //Vertex Transformations
            p += GerstnerWave(_WaveA, worldPos.xyz, tangent, binormal);
            p += GerstnerWave(_WaveB, worldPos.xyz, tangent, binormal);
           
            
            

            //Calculate normal for correct lighting
            float3 normal = normalize(cross(binormal, tangent));

            //Apply vertex transformations
			vertexData.vertex.xyz = p;
            //Apply modified normal on the mesh.
            vertexData.normal = normal;
        }

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
