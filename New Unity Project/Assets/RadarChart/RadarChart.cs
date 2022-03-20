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
        [SerializeField] private float startRot;
        [SerializeField] private bool scaleBounds = false;
        [SerializeField] private List<RadarItem> radarItems;

        [SerializeField, HideInInspector] private CanvasRenderer canvasRenderer;

        private void OnValidate()
        {
            canvasRenderer = GetComponent<CanvasRenderer>();
        }

        private void Update()
        {
            RadarDrawer radarDrawer = new RadarDrawer(canvasRenderer, radarItems, radius, material, 
                texture, textureTiling, textureOffset, isGradient, startRot, scaleBounds);
            radarDrawer.Draw();
        }

        public void SetStat(string id, int val)
        {
            for (var i = 0; i < radarItems.Count; i++)
            {
                var radarItem = radarItems[i];
                if (radarItem.Name.Equals(id))
                    radarItem.Value = val;
            }
        }
    }
}