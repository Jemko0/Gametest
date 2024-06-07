using Object;

namespace Gametest
{
    internal class SceneManager
    {
        private GameClient _client;
        public SceneManager(GameClient gc)
        {
            _client = gc;
        }
        public void SaveScene(string filedest = "null_scene")
        {
            if(filedest != "null_scene")
            {
                if (File.Exists(filedest))
                {
                    
                }
                else
                {
                    File.Create(filedest);
                }

                //write data
                StreamWriter writer = new StreamWriter(filedest);

                foreach (EObject obj in GameClient.objs.Values)
                {
                    writer.Write("t=" + obj.GetType().ToString(), "r=" + obj.ticking.ToString());
                }
            }
        }
        public bool LoadScene(Engine.EngineStructs.Scene sdata)
        {
            return true;
        }
    }
}
