using System;
using System.Collections.Generic;
using UnityEngine;

namespace RadarChart
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class RadarChart : MonoBehaviour
    {
        [SerializeField] private Material material;
        [SerializeField] private Texture2D texture;
        [SerializeField] private float radius = 1;
        [SerializeField] private Vector2 textureTiling = Vector2.one;
        [SerializeField] private Vector2 textureOffset;
        [SerializeField] private bool isGradient;
        [SerializeField] private List<RadarItem> radarItems;

        [SerializeField, HideInInspector] private CanvasRenderer canvasRenderer;

        private void OnValidate()
        {
            canvasRenderer = GetComponent<CanvasRenderer>();
        }

        private void Update()
        {
            RadarDrawer radarDrawer = new RadarDrawer(canvasRenderer, radarItems, radius, material, texture, textureTiling, textureOffset, isGradient);
            radarDrawer.Draw();
        }
    }
}