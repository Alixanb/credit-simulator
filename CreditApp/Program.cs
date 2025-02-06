using System;
using System.IO;
using System.Reflection;
using CreditLib.Models;

class Program
{
    static void Main()
    {
        Console.WriteLine("Simulation de crédit immobilier");
        
        decimal capital = 175000;
        int duree = 25;
        decimal tauxNominal = 1.5m;
        decimal tauxAssurance = 3.5m;
        
        var credit = new CreditCalculator(capital, duree, tauxNominal, tauxAssurance);
        var exporter = new CreditExporter(credit);

        // Console TODO: delete ?
        Console.WriteLine($"Mensualité: {credit.CalculerMensualite():F2}€");
        Console.WriteLine($"Cotisation assurance: {credit.CalculerCotisationAssurance():F2}€");
        Console.WriteLine($"Total intérêts: {credit.CalculerTotalInterets():F2}€");
        Console.WriteLine($"Total assurance: {credit.CalculerTotalAssurance():F2}€");
        Console.WriteLine($"Capital remboursé après 10 ans: {credit.CapitalRembourseApresAnnees(10):F2}€");

        string executingPath = Assembly.GetExecutingAssembly().Location;
        string projectRoot = Path.GetDirectoryName(executingPath);
        
        for (int i = 0; i < 3; i++)
        {
            projectRoot = Directory.GetParent(projectRoot).FullName;
        }
        
        projectRoot = Directory.GetParent(projectRoot).FullName;
        
        string csvPath = Path.Combine(projectRoot, "simulation_credit.csv");
        
        try
        {
            exporter.ExportToCsv(csvPath);
            Console.WriteLine($"\nLes résultats ont été exportés dans : {csvPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErreur lors de l'export du fichier CSV : {ex.Message}");
        }
    }
}
