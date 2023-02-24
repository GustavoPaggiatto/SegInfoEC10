using System;
using System.Threading;
using CesarCifer;

/*
 * Nomes e RAs:
 * Gustavo Henrique Cechini Paggiatto - 082180006
 * Julio Cesar Rodrigues Lima         - 082180012
 * Gustavo Ribeiro                    - 082180026
 * Eleni Oliveira                     - 082180021
 * 
 */

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
{
    await criptoService.CriptoAsync(filePath, cancellationToken);
    Console.WriteLine("AGORA PAGUE!!!");
}
else
    await criptoService.DecriptAsync(filePath, cancellationToken);

