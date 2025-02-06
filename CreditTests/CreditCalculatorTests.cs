using Xunit;
using CreditLib.Models;
using System;

namespace CreditTests
{
    public class CreditCalculatorTests
    {
        [Fact]
        public void TestCalculerMensualite()
        {
            var calculator = new CreditCalculator(175000, 25, 1.5m, 0.3m);
            decimal mensualite = calculator.CalculerMensualite();
            Assert.True(mensualite > 0);
        }

        [Fact]
        public void TestCalculerCotisationAssurance()
        {
            var calculator = new CreditCalculator(175000, 25, 1.5m, 0.3m);
            decimal cotisation = calculator.CalculerCotisationAssurance();
            Assert.Equal(43.75m, cotisation, 2);
        }

        [Fact]
        public void TestCalculerTotalInterets()
        {
            var calculator = new CreditCalculator(175000, 25, 1.5m, 0.3m);
            decimal totalInterets = calculator.CalculerTotalInterets();
            Assert.True(totalInterets > 0);
        }

        [Fact]
        public void TestCapitalRembourseApres10Ans()
        {
            var calculator = new CreditCalculator(175000, 25, 1.5m, 0.3m);
            decimal capitalRembourse = calculator.CapitalRembourseApresAnnees(10);
            Assert.True(capitalRembourse > 0);
        }

        [Theory]
        [InlineData(50000, 9, 1.5, 0.3)]
        [InlineData(1000000, 25, 5.0, 0.5)]
        public void Constructor_WithValidParameters_ShouldCreateInstance(decimal capital, int dureeAnnees, decimal tauxNominal, decimal tauxAssurance)
        {
            var calculator = new CreditCalculator(capital, dureeAnnees, tauxNominal, tauxAssurance);
            
            Assert.Equal(capital, calculator.Capital);
            Assert.Equal(dureeAnnees * 12, calculator.DureeMois);
            Assert.Equal(tauxNominal / 100, calculator.TauxNominal);
            Assert.Equal(tauxAssurance / 100, calculator.TauxAssurance);
        }

        [Theory]
        [InlineData(49999, 15, 1.5, 0.3, "Le capital doit être supérieur à 50.000€.")]
        [InlineData(175000, 8, 1.5, 0.3, "La durée doit être entre 9 et 25 ans.")]
        [InlineData(175000, 26, 1.5, 0.3, "La durée doit être entre 9 et 25 ans.")]
        public void Constructor_WithInvalidParameters_ShouldThrowException(decimal capital, int dureeAnnees, decimal tauxNominal, decimal tauxAssurance, string expectedMessage)
        {
            var exception = Assert.Throws<ArgumentException>(() => 
                new CreditCalculator(capital, dureeAnnees, tauxNominal, tauxAssurance));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(175000, 25, 1.5, 0.3, 699.89)]
        [InlineData(200000, 20, 2.0, 0.4, 1011.77)]
        public void CalculerMensualite_ShouldReturnExpectedAmount(decimal capital, int dureeAnnees, decimal tauxNominal, decimal tauxAssurance, decimal expectedMensualite)
        {
            var calculator = new CreditCalculator(capital, dureeAnnees, tauxNominal, tauxAssurance);
            decimal mensualite = calculator.CalculerMensualite();
            Assert.Equal(expectedMensualite, mensualite, 2);
        }

        [Fact]
        public void CalculerTotalAssurance_ShouldReturnCorrectTotal()
        {
            var calculator = new CreditCalculator(175000, 25, 1.5m, 0.3m);
            decimal totalAssurance = calculator.CalculerTotalAssurance();
            decimal expectedTotal = 43.75m * 300; // 300 months (25 years)
            Assert.Equal(expectedTotal, totalAssurance, 2);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(15)]
        [InlineData(20)]
        public void CapitalRembourseApresAnnees_ShouldBeIncreasing(int annees)
        {
            var calculator = new CreditCalculator(175000, 25, 1.5m, 0.3m);
            decimal capitalRembourse1 = calculator.CapitalRembourseApresAnnees(annees);
            decimal capitalRembourse2 = calculator.CapitalRembourseApresAnnees(annees + 1);
            Assert.True(capitalRembourse2 > capitalRembourse1);
        }

        [Fact]
        public void CapitalRembourseApres25Ans_ShouldEqualInitialCapital()
        {
            var calculator = new CreditCalculator(175000, 25, 1.5m, 0.3m);
            decimal capitalRembourse = calculator.CapitalRembourseApresAnnees(25);
            Assert.Equal(calculator.Capital, capitalRembourse, 2);
        }
    }
}
