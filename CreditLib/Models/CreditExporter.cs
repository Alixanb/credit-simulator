using System.Text;

namespace CreditLib.Models
{
    public class CreditExporter
    {
        private readonly CreditCalculator _calculator;

        public CreditExporter(CreditCalculator calculator)
        {
            _calculator = calculator;
        }

        public string GenerateCsvContent()
        {
            var sb = new StringBuilder();
            decimal mensualite = _calculator.CalculerMensualite();
            decimal assurance = _calculator.CalculerCotisationAssurance();
            
            sb.AppendLine("Mois,Capital Restant,Intérêts,Principal,Assurance,Mensualité Totale");

            decimal capitalRestant = _calculator.Capital;
            
            for (int mois = 1; mois <= _calculator.DureeMois; mois++)
            {
                decimal interets = capitalRestant * (_calculator.TauxNominal / 12);
                decimal principal = mensualite - interets;
                decimal mensualiteTotale = mensualite + assurance;
                
                sb.AppendLine($"{mois},{capitalRestant:F2},{interets:F2},{principal:F2},{assurance:F2},{mensualiteTotale:F2}");
                
                capitalRestant -= principal;
            }

            sb.AppendLine();
            sb.AppendLine("Résumé,Montant");
            sb.AppendLine($"Capital initial,{_calculator.Capital:F2}");
            sb.AppendLine($"Total intérêts,{_calculator.CalculerTotalInterets():F2}");
            sb.AppendLine($"Total assurance,{_calculator.CalculerTotalAssurance():F2}");
            sb.AppendLine($"Coût total du crédit,{_calculator.CalculerTotalInterets() + _calculator.CalculerTotalAssurance():F2}");

            return sb.ToString();
        }

        public void ExportToCsv(string filePath)
        {
            File.WriteAllText(filePath, GenerateCsvContent());
        }
    }
} 