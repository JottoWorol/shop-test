using System;
using System.Collections.Generic;
using Core.Interfaces;
using UnityEngine;

namespace Core
{
    public class PlayerData
    {
        public static PlayerData Instance { get; private set; }

        private readonly List<IPlayerResource> resources;

        public PlayerData()
        {
            resources = new List<IPlayerResource>();
            Instance = this;
        }
        
        public bool TryAddResourceData(IPlayerResource resource)
        {
            if (resources.Exists(x => x.Id == resource.Id))
            {
                Debug.Log("Resource with the same ID already exists: " + resource.Id);
                return false;
            }
            
            resources.Add(resource);
            return true;
        }

        public bool TryGetResourceDataById(string resourceId, out IPlayerResource playerResource)
        {
            playerResource = resources.Find(x => x.Id == resourceId);
            return playerResource != null;
        }
    }
}