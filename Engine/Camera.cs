using Gametest;
using System.Numerics;
using Object.Entity;
using Engine;

namespace Engine.Camera
{
    public class Camera
    {
        public EEntity trackedEntity;
        public Vector2 position;
        public float zoom;
        public Camera()
        {
            zoom = 1;
        }
        
        public void Update()
        {
            if (trackedEntity != null)
            {
                position.X = EngineFunctions.Lerp(position.X, trackedEntity.Position.X, 0.02f);
                position.Y = EngineFunctions.Lerp(position.Y, trackedEntity.Position.Y, 0.02f);
            }
        }
    }
}