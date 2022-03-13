using System;
using System.Collections;
using System.Collections.Generic;
using RadarChart;
using Unity.VisualScripting;
using UnityEngine;

public class RadarDrawer
{
    private Material material;
    private CanvasRenderer canvasRenderer;
    private List<RadarItem> radarItems;
    private float radius;

    public RadarDrawer(CanvasRenderer canvasRenderer, List<RadarItem> radarItems, float radius, Material material)
    {
        this.material = material;
        this.canvasRenderer = canvasRenderer;
        this.radarItems = radarItems;
        this.radius = radius;
    }
    
    public void Draw()
    {
        float radarItemsMaxValue = Mathf.NegativeInfinity;
        
        foreach (var radarItem in radarItems)
        {
            if (radarItem.Value > radarItemsMaxValue)
                radarItemsMaxValue = radarItem.Value;
        }
        
        int count = radarItems.Count;
        
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[count + 1];
        Vector2[] uv = new Vector2[count + 1];
        int[] triangles = new int[3 * count];

        float angle = 2f * Mathf.PI/count;

        vertices[0] = Vector3.zero;
        
        for (int i = 0; i < count; i++)
        {
            float newAngle = angle * (i);
            float newRadius = radius * (radarItems[i].Value / radarItemsMaxValue);
            vertices[i + 1] = new Vector3(newRadius * Mathf.Cos(newAngle), newRadius * Mathf.Sin(newAngle));
        }

        for (int i = 0; i < count - 1; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }
        
        triangles[3 * count - 3] = 0;
        triangles[3 * count - 2] = count;
        triangles[3 * count - 1] = 1;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        canvasRenderer.SetMesh(mesh);
        canvasRenderer.SetMaterial(material, null);
    }
}
