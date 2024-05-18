using Engine;
using Gametest;
using System.ComponentModel;
using System.Numerics;
using System.Windows;
namespace Object.Entity.Character
{
    /// <summary>
    /// A Character derives from EEntity and has the capabililty of handling movement input
    /// </summary>
    public class ECharacter : EEntity
    {
        public float accel = 3f;
        public float speed = 1f;
        public bool grounded = false;
        public float gravity = 2f;
        public ECharacter()
        {
            return;
        }

        public override void Tick(double delta)
        {
            base.Tick(delta);
            EngineStructs.ECollisionResult collisionResult = CheckCollisions();
        }

        public void AddMovementInput(float lr_input)
        {
            Move(lr_input, 0);
        }

        private void Move(float inputX, float inputY)
        {
            grounded = false;
            velocity.Y += gravity;
            velocity.X = accel * inputX;

            if (true)
            {
                grounded = true;
            }
            
        }

        public void Jump()
        {
            if(grounded)
            { 
                velocity.Y = -9;
            }
            
        }
    }
}