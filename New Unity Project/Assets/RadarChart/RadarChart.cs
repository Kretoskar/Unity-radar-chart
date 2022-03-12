using System;
using System.Collections.Generic;
using UnityEngine;

namespace RadarChart
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class RadarChart : MonoBehaviour
    {
        [SerializeField] private Material material;
        [SerializeField] private List<RadarItem> radarItems;
        [SerializeField] private float radius = 1;

        [SerializeField, HideInInspector] private CanvasRenderer canvasRenderer;

        private void OnValidate()
        {
            canvasRenderer = GetComponent<CanvasRenderer>();
        }

        private void Start()
        {
            RadarDrawer radarDrawer = new RadarDrawer(canvasRenderer, radarItems, radius, material);
            radarDrawer.Draw();
        }
    }
}