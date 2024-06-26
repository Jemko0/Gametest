using Game;
using Game.Global;
using Gametest;
using Gametest.GameContent.Gameplay;
using System.Numerics;

namespace Object.Entity;

public class EPlayer : ECharacter
{
    public GameInput InputManager = new GameInput();
    public Inventory inv = new Inventory();
    private int selecteditem;
    public Item helditem;
    private bool canUse;
    private float _lr = 0;
    public override void Init()
    {
        base.Init();
        ticking = true;
        collidable = false;
        gravitymult = 3.0f;
        UpdateHeld();
    }

    public void UpdateHeld()
    {
        if(helditem != null)
        {
            helditem.Destroy();
        }
        if(inv.items.Count > selecteditem)
        {
            helditem = ID.ItemID.GetItem(inv.items[selecteditem].id)._class;
            helditem.pickedup = true;
            helditem.parent = this;
        }
    }

    public override void Tick(float delta)
    {
        //Renderer.DrawDebugPoint(new System.Numerics.Vector2((float)EngineFunctions.TileSnap(Position.X - EDescription.HSize.X * Math.Clamp(-velocity.X, -1, 1)), (float)EngineFunctions.TileSnap(Position.Y + EDescription.HSize.Y / 2)));

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

        if (InputManager.IsKeyDown(Keys.G))
        {
            GameClient.RegisterObject(new Item("basepick", ID.ItemID.GetItem("basepick"), Position + new Vector2(100, 0)));
        }

        if (InputManager.IsKeyDown(Keys.Space) == true)
        {
            Jump();
        }
        base.Tick(delta);

    }
}