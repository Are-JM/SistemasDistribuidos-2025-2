namespace ChatP2P;

public class Program
{
    private static async Task Main(string[] args)
    {
        var peer = new Peer();
        if (args.Length > 0 && args[0] == "connect" && !string.IsNullOrEmpty(args[1]) && !string.IsNullOrEmpty(args[2]))
        {
            await peer.ConnectToPeer(args[1], args[2]);
        }
        else
        {
            await peer.StartListening();
        }
    }
}