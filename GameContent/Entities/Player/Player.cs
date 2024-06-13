using Game;
using Game.Global;
using Gametest;
using Engine.Data;
using Gametest.GameContent.Gameplay;
using Engine;

namespace Object.Entity;

public class EPlayer : ECharacter
{
    public GameInput InputManager = new GameInput();
    public Inventory inv = new Inventory();
    public int selecteditem;
    public Item helditem;
    public float _lr = 0;
    public override void Init()
    {
        base.Init();
        ticking = true;
        collidable = false;
        gravitymult = 3.0f;
        inv.AddItem("basepick", 1, string.Empty);
        UpdateHeld();
    }

    public void UpdateHeld()
    {
        if(helditem != null)
        {
            helditem.Destroy();
        }

        helditem = ID.ItemID.GetItem(inv.items[selecteditem].id)._class;
        helditem.pickedup = true;
        helditem.parent = this;
    }

    public override void Tick(float delta)
    {
        System.Diagnostics.Debug.WriteLine(Traces.isTraversible(EngineFunctions.TileSnap(Position.X), EngineFunctions.TileSnap(Position.Y)).ToString());
        Renderer.DrawDebugPoint(new System.Numerics.Vector2((float)EngineFunctions.TileSnap(Position.X), (float)EngineFunctions.TileSnap(Position.Y + EDescription.HSize.Y)));

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

        
        if(InputManager.IsKeyDown(Keys.G))
        {
            GameClient.RegisterObject(new Item("basepick", ID.ItemID.GetItem("basepick"), Position));
        }

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