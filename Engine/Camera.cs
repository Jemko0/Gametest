using Gametest;
using Engine.Data;
using System.Numerics;
using Object.Entity;

namespace Engine.Camera
{
    public class Camera
    {
        public EEntity? trackedEntity;
        public Vector2 position;
        public float zoom;
        public Vector2 viewbounds;
        public Camera()
        {
            zoom = 1f;
            viewbounds = new Vector2 (GameClient.ActiveForm.Width, GameClient.ActiveForm.Height);
        }

        public Camera(Vector2 pos)
        {
            SetCamPos(pos);
        }
        
        void SetCamPos(Vector2 pos)
        {
            position = pos;
        }

        public bool PosInCamBounds(Vector2 pos)
        {
            return pos.X > position.X - 532 - viewbounds.X && pos.X < position.X + 664 + viewbounds.X && pos.Y > position.Y - 300 - viewbounds.Y && pos.Y < position.Y + 300 + viewbounds.Y;
        }

        public void Update()
        {
            if (trackedEntity != null)
            {
                position.X = EngineFunctions.Lerp(position.X, trackedEntity.Position.X,  8f * GameClient.delta);
                position.Y = EngineFunctions.Lerp(position.Y, trackedEntity.Position.Y, 8f * GameClient.delta);
            }
        }
    }
}