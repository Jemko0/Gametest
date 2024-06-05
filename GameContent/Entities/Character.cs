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
        public float accel = 2048f;
        public float speed = 300f;
        public bool grounded = false;
        public float gravity = 500f;
        public ECharacter()
        {
            return;
        }

        public override void Tick(float delta)
        {
            base.Tick(delta);
        }

        public void AddMovementInput(float lr_input)
        {
            Move(lr_input, 0);
        }

        private void Move(float inputX, float inputY)
        {
            grounded = false;

            velocity.X += (accel * inputX) * odelta;
            velocity.X = Math.Clamp(velocity.X, -speed, speed);

            if (inputX == 0)
            {
                velocity.X *= 0.98f;
            }

            EngineStructs.ECollisionResult collisionResult = CheckCollisions();

            if (!collisionResult.collision)
            {
                velocity.Y += gravity * odelta;
                return;

            }
            else
            {
                if (collisionResult.collision)
                {
                    grounded = true;
                    velocity.Y = 0;
                }
            }
        }

        public void Jump()
        {
            if(grounded)
            { 
                velocity.Y = -250;
            }
            
        }
    }
}