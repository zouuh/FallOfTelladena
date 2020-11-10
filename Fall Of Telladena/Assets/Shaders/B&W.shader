// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/B&W"
{
    Properties
    {
        [NoScaleOffset] _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (0.5, 0.65, 1, 1)

        //_OutlineColor("Outline Color", Color) = (0,0,0,1)
        //_Outline("Outline width", Range(0.0, 0.03)) = .005
    }
    SubShader
    {
        Pass
        {
            Tags {
            
                "LightMode" = "ForwardBase"

                "Queue" = "Transparent"
                "RenderType" = "Transparent"
                "IgnoreProjector" = "True"
            }
            //Zwrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            //Blend One One

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

        // compile shader into multiple variants, with and without shadows
        // (we don't care about any lightmaps yet, so skip these variants)
        #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
        // shadow helper functions and macros
        #include "AutoLight.cginc"

        struct v2f
        {
            float2 uv : TEXCOORD0;
            SHADOW_COORDS(1) // put shadows data into TEXCOORD1
            fixed3 diff : COLOR0;
            fixed3 ambient : COLOR1;
            float4 pos : SV_POSITION;
        };

        v2f vert(appdata_base v)
        {
            v2f o;
            
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            half3 worldNormal = UnityObjectToWorldNormal(v.normal);
            half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
            o.diff = nl * _LightColor0.rgb;
            o.ambient = ShadeSH9(half4(worldNormal,1));

            // compute shadows data
            TRANSFER_SHADOW(o)
            
            //o.vertex = UnityObjectToClipPos(v.vertex);
            return o;
        }

        sampler2D _MainTex;
        float4 _Color;
        //float _Outline;
        //float4 _OutlineColor;

        fixed4 frag(v2f i) : SV_Target
        {
            fixed4 col = tex2D(_MainTex, i.uv);
        // compute shadow attenuation (1.0 = fully lit, 0.0 = fully shadowed)
        fixed shadow = SHADOW_ATTENUATION(i);
        // darken light's illumination with shadow, keep ambient intact
        fixed3 lighting = i.diff * shadow + i.ambient;
        col.rgb *= lighting;
        float moy = (col.r + col.b + col.g) / 3;
        if (moy <= 0.1) {
            moy = 0;
        }
        else if(moy <= 0.2) {
            moy = int(moy * 1000) % 2;
            //moy = moy<0.5?0:1;
            //Random.Range(0, 1);
        }
        else {
            moy = 1;
        }
        float4 result = _Color;
        result.rgb = moy;
        result.a = _Color.a <= 0 ? _Color.a + 0.2 : _Color.a;
        return result;
    }
    ENDCG
}

// shadow casting support
UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}