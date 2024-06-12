using Engine.Data;
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
            base.Tick(delta);
            Move(_input, 0);
        }

        public void AddMovementInput(float lr_input)
        {
            _input = lr_input;
        }

        private void Move(float inputX, float inputY)
        {
            grounded = false;

            velocity.X += (accel * inputX) * odelta;
            velocity.X = Math.Clamp(velocity.X, -speed, speed);

            if (inputX == 0)
            {
                    velocity.X /= 1 + odelta * 6;
            }

            EngineStructs.ECollisionResult collisionResult = CheckCollisions();

            if (!collisionResult.collision)
            {
                velocity.Y += Game.GameProperties.gravity * gravitymult * odelta;
                return;

            }
            else
            {
                if (collisionResult.collision)
                {
                    grounded = true;

                    //boonce with fallback restitution
                    if(collisionResult.hitobject != null)
                    {
                        velocity.Y = -Math.Abs(velocity.Y * collisionResult.hitobject.restitution);
                    }
                    else
                    {
                        velocity.Y = -Math.Abs(velocity.Y * 0.25f); ;
                    }
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