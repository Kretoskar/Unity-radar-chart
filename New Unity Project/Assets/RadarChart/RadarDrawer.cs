using System;
using System.Collections;
using System.Collections.Generic;
using RadarChart;
using Unity.VisualScripting;
using UnityEngine;

public class RadarDrawer
{
    private Material material;
    private Texture2D texture;
    private CanvasRenderer canvasRenderer;
    private List<RadarItem> radarItems;
    private float radius;
    private Vector2 textureTiling;
    private Vector2 textureOffset;
    private bool isGradient;
    private float startRot;
    private bool scaleBounds;

    //TODO: Gównianie wielki 
    public RadarDrawer(CanvasRenderer canvasRenderer, List<RadarItem> radarItems, float radius, Material material,
        Texture2D texture, Vector2 textureTiling, Vector2 textureOffset, bool isGradient, float startRot, bool scaleBounds)
    {
        this.material = material;
        this.canvasRenderer = canvasRenderer;
        this.radarItems = radarItems;
        this.radius = radius;
        this.texture = texture;
        this.textureTiling = textureTiling;
        this.textureOffset = textureOffset;
        this.isGradient = isGradient;
        this.startRot = startRot;
        this.scaleBounds = scaleBounds;
    }
    
    public void Draw()
    {
        int count = radarItems.Count;
        float radarItemsMaxValue = GetRadarItemsMaxValue();
        float angle = 2f * Mathf.PI/count;
        
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        
        float minY = Mathf.Infinity;
        float maxY = Mathf.NegativeInfinity;

        Vector3[] vertices = new Vector3[count + 1];
        Vector2[] uvs = new Vector2[count + 1];
        int[] triangles = new int[3 * count];

        //vertices
        vertices[0] = Vector3.zero;

        startRot *= Mathf.Deg2Rad;
        
        for (int i = 0; i < count; i++)
        {
            float newAngle = angle * i + startRot;
            float newRadius = radius * (radarItems[i].Value / radarItemsMaxValue);
            
            float x = newRadius * Mathf.Cos(newAngle);
            float y = newRadius * Mathf.Sin(newAngle);

            if (x > maxX) maxX = x;
            if (x < minX) minX = x;

            if (y > maxY) maxY = y;
            if (y < minY) minY = y;
            
            vertices[i + 1] = new Vector3(x, y);
        }

        //triangles
        for (int i = 0; i < count - 1; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }
        
        triangles[3 * count - 3] = 0;
        triangles[3 * count - 2] = count;
        triangles[3 * count - 1] = 1;

        float boundsX = Mathf.Abs(minX) + Mathf.Abs(maxX);
        float boundsY = Mathf.Abs(minY) + Mathf.Abs(maxY);

        //UVs
        if (isGradient)
        {
            uvs[0] = Vector2.zero;
            for (int i = 1; i < uvs.Length; i++)
            {
                uvs[i] = Vector2.one;
            }
        }
        
        //tutaj te boundsy trzeba ogarnąć
        else if(scaleBounds)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                uvs[i] = new Vector2(vertices[i].x / boundsX * textureTiling.x - (textureOffset.x),
                    vertices[i].y / boundsY * textureTiling.y - (textureOffset.y));
            }
        }
        else
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                uvs[i] = new Vector2(vertices[i].x / radius * (textureTiling.x - .5f) - .5f + textureOffset.x,
                    vertices[i].y / radius * (textureTiling.y - .5f) - .5f + textureOffset.y);
            }
        }

        Mesh mesh = new Mesh
        {
            vertices = vertices,
            uv = uvs,
            triangles = triangles
        };
        
        canvasRenderer.SetMesh(mesh);
        canvasRenderer.SetMaterial(material, texture);
    }

    private float GetRadarItemsMaxValue()
    {
        float radarItemsMaxValue = Mathf.NegativeInfinity;

        for (var i = 0; i < radarItems.Count; i++)
        {
            var radarItem = radarItems[i];
            if (radarItem.Value > radarItemsMaxValue)
                radarItemsMaxValue = radarItem.Value;
        }

        return radarItemsMaxValue;
    }
}
