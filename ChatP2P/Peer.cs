using System.Net;
using System.Net.Sockets;
using System.Text;
namespace ChatP2P;

public class Peer
{
    private readonly TcpListener _tcpListener;
    private TcpClient? _tcpClient;
    private const int Port = 8080;
    public Peer() => _tcpListener = new TcpListener(IPAddress.Any, Port);


    public async Task ConnectToPeer(string ipAddress, string port)
    {
        try
        {
            _tcpClient = new TcpClient(ipAddress, Convert.ToInt32(port));
            Console.WriteLine($"Connection established");
            var receiveTask = RecieveMessage();
            await SendMessage();
            await receiveTask;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error connecting to peer: " + ex.Message);
        }
    }
    public async Task StartListening()
    {
        try
        {
            //intento de ejecutar logica 
            //alguna excepcion se ejecuta el cathc y el finaly

            _tcpListener.Start();
            Console.WriteLine("Listening for incoming connections...");
            _tcpClient = await _tcpListener.AcceptTcpClientAsync();
            Console.WriteLine("connection established.");
            var receiveTask = RecieveMessage();
            await SendMessage();
            await receiveTask;

        }
        catch (Exception ex)
        {
            //log del error
            Console.WriteLine("Conexion fallida: " + ex.Message);
        }

    }

    public async Task RecieveMessage()
    {
        try
        {
            var stream = _tcpClient!.GetStream();
            var reader = new StreamReader(stream, Encoding.UTF8);
            while (true)
            {
                var message = await reader.ReadLineAsync();
                if (message == null || message.ToLower() == "exit")
                {
                    Console.WriteLine("Disconnected.");
                    break;
                }
                Console.WriteLine($"Peer message: {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error receiving message: " + ex.Message);
        }
        finally
        {
            Close();
        }
    }

    public void Close()
    {
        _tcpClient?.Close();
        _tcpListener.Stop();

    }

    public async Task SendMessage()
    {
         try
        {
            var stream = _tcpClient!.GetStream();
            var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            while (true)
            {
                var message = Console.ReadLine();
                if (message == null || message.ToLower() == "exit")
                {
                    await writer.WriteLineAsync("exit");
                    break;
                }
                await writer.WriteLineAsync(message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending message: " + ex.Message);
        }
    }
}