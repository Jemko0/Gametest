using Game.Global;

namespace Object.Entity;

public class EPlayer : ECharacter
{
    public GameInput InputManager = new GameInput();
    public float _lr = 0;
    public override void Init()
    {
        base.Init();
        ticking = true;
        collidable = false;
    }

    public override void Tick(float delta)
    {
        base.Tick(delta);
        _lr = 0;
        if (InputManager.IsKeyDown(Keys.A) == true)
        {
            _lr = -1;
        }
        if (InputManager.IsKeyDown(Keys.D) == true)
        {
            _lr = 1;
        }
        AddMovementInput(_lr);

        if (InputManager.IsKeyDown(Keys.Space) == true)
        {
            Jump();
        }

        if(Position.Y > 1000000)
        {
            velocity.Y = 0;
            Position = new System.Numerics.Vector2(400, -400);
        }
    }
}