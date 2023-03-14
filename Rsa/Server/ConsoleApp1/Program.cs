using ConsoleApp1;

Console.ForegroundColor = ConsoleColor.Magenta;

Console.WriteLine("*************** Server UDP RSA ***************");

RsaKey key = default!;

while (true)
{
    Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine("Selecione uma opção:");

    Console.WriteLine("1 - Gerar chaves.");
    Console.WriteLine("2 - Habilitar servidor UDP.");
    Console.WriteLine("3 - Sair");

    Console.WriteLine("Escolha uma opção: ");
    int opt = Convert.ToInt32(Console.ReadLine());

    if (opt == 1)
    {
        key = new KeyGenerator().GenerateKey();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n\rKey: ");
        Console.WriteLine("P: " + key.P);
        Console.WriteLine("Q: " + key.Q);
        Console.WriteLine("N: " + key.N);
        Console.WriteLine("E: " + key.E);
    }
    else if (opt == 2)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        
        new SecureUdpServer(key).StartAsync(cancellationToken);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Pressione qualquer tecla para interromper o servidor UDP.");
        Console.ReadKey();
        cancellationTokenSource.Cancel();
    }
    else
    {
        break;
    }
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Saind...");
Thread.Sleep(2000);