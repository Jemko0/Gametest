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
        public float accel = 0.5f;
        public float speed = 1f;
        public ECharacter()
        {
            return;
        }

        public void AddMovementInput(float lr_input)
        {
            Move(lr_input);
        }

        private void Move(float input)
        {
            if(input == 0)
            {
                velocity.X /= 1.2f;
            }


            velocity.Y += 0.1f;

            EngineStructs.ECollisionResult collisionResult = CheckCollisions();
            if (collisionResult.collision)
            {
                velocity.Y = 0;
            }

            velocity.X += accel * input;


        }
    }
}