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

        public string Author => "Diavolo#6969";

        public HashSet<string> IPList = new HashSet<string>(18);

        public Task OnEventAsync(GameEvent E, Server S)
        {
            if (S.GameName != Server.Game.IW5) return Task.CompletedTask;

            switch(E.Type)
            {
                case GameEvent.EventType.Join:
                    if (IPList.Contains(E.Origin.IPAddressString))
                    {
                        var sender = Utilities.IW4MAdminClient(E.Owner);
                        E.Origin.Kick("Same IP, different HWID. Is the cat stepping on the keyboard?", sender);
                    }
                    else
                        IPList.Add(E.Origin.IPAddressString);
                    break;
                case GameEvent.EventType.Disconnect:
                    IPList.Remove(E.Origin.IPAddressString);
                    break;
                default:
                    break;
            }
            return Task.CompletedTask;
        }

        public Task OnLoadAsync(IManager manager) => Task.CompletedTask;

        public Task OnTickAsync(Server S) => Task.CompletedTask;

        public Task OnUnloadAsync() => Task.CompletedTask;
    }
}
