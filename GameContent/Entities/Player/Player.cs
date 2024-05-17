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
        }

        public override void FixedTick(object? sender, EventArgs e)
        {
           base.FixedTick(sender, e);
           AddMovementInput(0);

           if(InputManager.IsKeyDown(Keys.A) == true)
           {

                AddMovementInput(-1);
           }
           if (InputManager.IsKeyDown(Keys.D) == true)
           {
                AddMovementInput(1);
           }

        }
    }
}