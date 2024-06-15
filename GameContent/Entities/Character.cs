using Engine.Data;
using Gametest;
using System.Numerics;
using Game;
namespace Object.Entity
{
    /// <summary>
    /// A Character derives from EEntity and has the capabililty of handling movement input
    /// </summary>
    public class ECharacter : EEntity
    {
        public float accel = 2048f;
        public float speed = 300f;
        public float gravitymult = 1.0f;
        public bool grounded = false;
        public float air_decel = 0.99f;
        public float grounded_decel = 0.89f;
        public float _input;
        public short Health;

        public override void Init()
        {
            base.Init();
        }

        public override void Tick(float delta)
        {
            Move(_input, 0);
            base.Tick(delta);
        }

        public void AddMovementInput(float lr_input)
        {
            _input = lr_input;
        }

        private void Move(float inputX, float inputY)
        {
            grounded = true;

            velocity.X += (accel * inputX) * odelta;
            velocity.X = Math.Clamp(velocity.X, -speed, speed);

            if (inputX == 0)
            {
                velocity.X /= 1 + odelta * 6;
            }
            
            EngineStructs.ECollisionResult cresult = CheckCollisions();
            velocity.Y += Game.GameProperties.gravity * gravitymult * odelta;
            if (cresult.collided)
            {
                grounded = true;
                velocity += cresult.normal * new Vector2(Math.Abs(velocity.X), Math.Abs(velocity.Y)) * (1 - cresult.time);
                return;
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