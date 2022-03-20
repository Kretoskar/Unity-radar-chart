using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RadarChart
{
    [CreateAssetMenu(menuName = "Radar chart/new style", fileName = "new style")]
    public class RadarStyle : ScriptableObject
    {
        [SerializeField] private Material material;
        [SerializeField] private Texture2D texture;
        [SerializeField] private float radius = 1;
        [SerializeField] private Vector2 textureTiling = Vector2.one;
        [SerializeField] private Vector2 textureOffset;
        [SerializeField] private bool isGradient;
        [SerializeField] private float startRot;
        [SerializeField] private bool scaleBounds = false;

        public Material Material => material;
        public Texture2D Texture => texture;
        public float Radius => radius;
        public Vector2 TextureTiling => textureTiling;
        public Vector2 TextureOffset => textureOffset;
        public bool IsGradient => isGradient;
        public float StartRot => startRot;
        public bool ScaleBounds => scaleBounds;
    }
}