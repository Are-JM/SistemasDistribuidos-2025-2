using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FileDownload;

public class Peer
{
    private readonly TcpListener _listener;
    private TcpClient _client;
    private const int Port = 8080;

    public Peer() => _listener = new TcpListener(IPAddress.Any, Port);

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _listener.Start();
        while (true)
        {
            _client = await _listener.AcceptTcpClientAsync(cancellationToken);
            await HandleClientAsync(cancellationToken);
        }
    }

    public async Task DownloadFileAsync(string peerIP, int peerPort, string fileName, string savePath, CancellationToken cancellationToken)
    {
        _client = new TcpClient(peerIP, peerPort);
        await using var stream = _client.GetStream();
        var request = Encoding.UTF8.GetBytes(fileName);
        await stream.WriteAsync(request, cancellationToken);

        await using var fs = new FileStream(savePath, FileMode.Create, FileAccess.Write);
        var buffer = new byte[1024];
        int bytesRead;
        while ((bytesRead = await stream.ReadAsync(buffer, cancellationToken)) > 0)
        {
            await fs.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
        }

        Console.WriteLine($"File '{fileName}' downloaded succesfully to '{savePath}'.");

    }

    private async Task HandleClientAsync(CancellationToken cancellationToken)
    {
        await using var stream = _client.GetStream();
        var buffer = new byte[1024];
        var bytesRead = await stream.ReadAsync(buffer, cancellationToken);
        var fileName = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        if (File.Exists(fileName))
        {
            var fileData = await File.ReadAllBytesAsync(fileName, cancellationToken);
            await stream.WriteAsync(fileData, cancellationToken);
            Console.WriteLine($"File '{fileName}' sent successfully.");
        }
        else
        {
            var errorMessage = Encoding.UTF8.GetBytes("File not found");
            await stream.WriteAsync(errorMessage, cancellationToken);
            Console.WriteLine($"File '{fileName}' not found.");
        }
    }
}