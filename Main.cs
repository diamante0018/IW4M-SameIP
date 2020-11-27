using SharedLibraryCore;
using SharedLibraryCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IW4M_Plugin
{
    public class Main : IPlugin
    {
        public string Name => "Same IP Different HWID";

        public float Version => (float)Utilities.GetVersionAsDouble();

        public string Author => "Diavolo";

        public HashSet<string> IPList = new HashSet<string>();

        public Task OnEventAsync(GameEvent E, Server S)
        {
            if (S.GameName != Server.Game.IW5) return Task.CompletedTask;

            if (E.Type == GameEvent.EventType.Join)
            {
                var origin = E.Origin;
                if (IPList.Contains(origin.IPAddressString))
                {
                    var sender = Utilities.IW4MAdminClient(E.Owner);
                    origin.Kick("Same IP, different HWID. Is the cat stepping on the keyboard?", sender);
                }
                else
                    IPList.Add(origin.IPAddressString);
            }
            else if (E.Type == GameEvent.EventType.Disconnect)
                IPList.Remove(E.Origin.IPAddressString);

            return Task.CompletedTask;
        }

        public Task OnLoadAsync(IManager manager) => Task.CompletedTask;

        public Task OnTickAsync(Server S) => Task.CompletedTask;

        public Task OnUnloadAsync() => Task.CompletedTask;
    }
}
