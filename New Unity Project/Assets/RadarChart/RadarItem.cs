using System;
using UnityEngine;

namespace RadarChart
{
    [Serializable]
    public struct RadarItem
    {
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private int value;

        public string Name => name;
        public string Description => description;
        public int Value => value;
    }
}