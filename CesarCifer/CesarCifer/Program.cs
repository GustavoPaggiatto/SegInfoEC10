using System;
using System.Threading;
using CesarCifer;

Console.WriteLine("********** Cesar Cifer **********");

Console.WriteLine("Options:");
Console.WriteLine("1 - Encript");
Console.WriteLine("2 - Decript");
Console.WriteLine("Informe uma opção: ");

int option = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Informe o arquivo: ");
string filePath = Console.ReadLine();

var criptoService = new CesarCriptoService();
var cancellationToken = new CancellationToken();

if (option == 1)
    await criptoService.CriptoAsync(filePath, cancellationToken);
else
    await criptoService.DecriptAsync(filePath, cancellationToken);

