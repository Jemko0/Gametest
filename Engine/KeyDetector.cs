using System.Runtime.InteropServices;

namespace Game.Global
{
public class GameInput
{
    public const int IS_DOWN = 0x8000;

    [DllImport("user32.dll")]
    static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

    public bool IsKeyDown(System.Windows.Forms.Keys vKey)
    {
        var result = GetAsyncKeyState(vKey);
        return (result & IS_DOWN) > 0;
    }

    [DllImport("user32.dll")]
    static extern bool GetKeyboardState(System.Windows.Forms.Keys vKey);

    public bool GetCurrentKey(System.Windows.Forms.Keys vKey)
        {
            return GetKeyboardState(vKey);
        }
    }
}