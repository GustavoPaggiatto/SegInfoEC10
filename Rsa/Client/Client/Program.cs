using System.Text;
using Client;

Console.ForegroundColor = ConsoleColor.Magenta;

Console.WriteLine("*************** Client UDP RSA ***************");

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Iniciando recepção da chave RSA...");

var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;
var rsaKey = await (new KeyReceiver().ReceiveAsync(cancellationToken));

Console.WriteLine("Finalizando a recepção da chave RSA...");
Console.WriteLine("Chave:");
Console.WriteLine("P:" + rsaKey.P);
Console.WriteLine("Q:" + rsaKey.Q);
Console.WriteLine("N:" + rsaKey.N);
Console.WriteLine("E:" + rsaKey.E);

while (true)
{
    Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine("Informe uma frase para criptografar:");
    string msg = Console.ReadLine()!;
    
    var stream = new RsaEncriptor().Encript(rsaKey, Encoding.Default.GetBytes(msg));
    var lenBytesSent = await new MessageSender().SendAsync(stream);

    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("Lenght of bytes sent: " + lenBytesSent);
    
    Thread.Sleep(3000);
    
    Console.WriteLine("Deseja continuar (S/N)?");
    var @continue = Console.ReadLine()!.ToUpper();

    if (@continue.Equals("N"))
        break;
}

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("Finalizando...");

Thread.Sleep(3000);