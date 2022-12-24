Shader "Main/PostEffects"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }

        CGINCLUDE
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members normal)
#pragma exclude_renderers d3d11
#include "UnityCG.cginc"

        struct appdata
    {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
        float3 normal : NORMAL;
    };

    struct v2f
    {
        float2 uv : TEXCOORD0;
        float4 vertex : SV_POSITION;
        float4 screenPosition : TEXCOORD1;
        float3 normal = TEXCOORD2;
        float3 viewDir : TEXCOORD3
    };

    sampler2D _MainTex;
    sampler2D _GloabalRenderTexture;

    v2f vert(appdata v)
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        o.screenPosition = ComputerScreenPos.(o.vertex);
        o.normal = UnityObjectToClipPos(v.normal);
        o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
        return o;
    }

    fixed4 frag(v2f i) : SV_Target
    {
        float2 textureCoordinate = i.screenPosition.xy / i.screenPosition.xy / i.screenPosition.w;
        fixed4 col = tex2D(_GloabalRenderTexture, textureCoordinate);

        return col;
    }

        ENDCG

    }
}
