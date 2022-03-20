using System;
using System.Collections.Generic;
using UnityEngine;

namespace RadarChart
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class RadarChart : MonoBehaviour
    {
        [SerializeField] private RadarStyle style;
        [SerializeField] private List<RadarItem> radarItems;

        [SerializeField, HideInInspector] private CanvasRenderer canvasRenderer;

        private void OnValidate()
        {
            canvasRenderer = GetComponent<CanvasRenderer>();
        }

        private void Update()
        {
            RadarDrawer radarDrawer = new RadarDrawer(canvasRenderer, radarItems, style);
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