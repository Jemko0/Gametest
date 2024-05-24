using Engine;
using Game.Global;
using Gametest;
using Object.Entity.Character;

namespace Entity
{
    public class EPlayer : ECharacter
    {
        public GameInput InputManager = new GameInput();
        public EPlayer()
        {
        InitializeEntity(new System.Numerics.Vector2(400,400), "player");
            collidable = false;
        }

        public override void Tick(float delta)
        {
            base.Tick(delta);



            AddMovementInput(0);
            if (InputManager.IsKeyDown(Keys.A) == true)
            {

                AddMovementInput(-1);
            }
            if (InputManager.IsKeyDown(Keys.D) == true)
            {
                AddMovementInput(1);
            }
            if (InputManager.IsKeyDown(Keys.Space) == true)
            {
                Jump();
            }

            if(Position.Y > 1000)
            {
                velocity.Y = 0;
                Position = new System.Numerics.Vector2(400, 400);
            }
        }
    }
}