namespace CreditLib.Models
{
    public class CreditCalculator
    {
        public decimal Capital { get; }
        public int DureeMois { get; }
        public decimal TauxNominal { get; }
        public decimal TauxAssurance { get; }

        public CreditCalculator(decimal capital, int dureeAnnees, decimal tauxNominal, decimal tauxAssurance)
        {
            if (capital < 50000) throw new ArgumentException("Le capital doit être supérieur à 50.000€.");
            if (dureeAnnees < 9 || dureeAnnees > 25) throw new ArgumentException("La durée doit être entre 9 et 25 ans.");

            Capital = capital;
            DureeMois = dureeAnnees * 12;
            TauxNominal = tauxNominal / 100;
            TauxAssurance = tauxAssurance / 100;
        }

        public decimal CalculerMensualite()
        {
            decimal tauxMensuel = TauxNominal / 12;
            return Capital * tauxMensuel / (1 - (decimal)Math.Pow(1 + (double)tauxMensuel, -DureeMois));
        }

        public decimal CalculerCotisationAssurance()
        {
            return Capital * TauxAssurance / 12;
        }

        public decimal CalculerTotalInterets()
        {
            return (CalculerMensualite() * DureeMois) - Capital;
        }

        public decimal CalculerTotalAssurance()
        {
            return CalculerCotisationAssurance() * DureeMois;
        }

        public decimal CapitalRembourseApresAnnees(int annees)
        {
            int mois = annees * 12;
            decimal capitalRestant = Capital;
            decimal mensualite = CalculerMensualite();

            for (int i = 0; i < mois; i++)
            {
                decimal interet = capitalRestant * (TauxNominal / 12);
                decimal principal = mensualite - interet;
                capitalRestant -= principal;
            }
            return Capital - capitalRestant;
        }
    }
}
