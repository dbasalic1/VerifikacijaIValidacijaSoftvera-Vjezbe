using Hypo_Banka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Unit_Testovi
{
    [TestClass]
    public class Zadatak1Testovi
    {
        [TestMethod]
        public void TestDavanjaUkupnogNovcaNaSvimRačunima()
        {
            Racun r = new Racun(1000);
            Racun r2 = new Racun(500);
            Klijent k = new Klijent();
            k.Racuni.Add(r);
            k.Racuni.Add(r2);
            Assert.AreEqual(k.DajUkupanIznosNovcaNaSvimRačunima(), 1500);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDavanjaUkupnogNovcaNaSvimRačunima2()
        {
            Racun r = new Racun(0);
            r.Blokiran = true;
            Racun r2 = new Racun(0);
            r2.Blokiran = true;
            Klijent k = new Klijent();
            k.Racuni.Add(r);
            k.Racuni.Add(r2);
            Assert.AreEqual(k.DajUkupanIznosNovcaNaSvimRačunima(), 1500);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDavanjaUkupnogNovcaNaSvimRačunima3()
        {
            Klijent k = new Klijent();
            Assert.AreEqual(k.DajUkupanIznosNovcaNaSvimRačunima(), 1500);
        }

        [TestMethod]
        public void TestAutomatskogGenerisanjaPodataka()
        {
            Klijent k = new Klijent();
            k.Ime = "Mujo";
            k.Prezime = "Mujić";
            Tuple<string, string> s = k.AutomatskoGenerisanjePodataka();
            Assert.AreEqual(s.Item1, "mmujić1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAutomatskogGenerisanjaPodataka2()
        {
            Klijent k = new Klijent();
            Tuple<string, string> s = k.AutomatskoGenerisanjePodataka();
            
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestProvjereStanjaOtplate()
        {
            Klijent x = new Klijent();
            Kredit k = new Kredit(x, -5000, 100, 0.05, new DateTime());
            string s = k.ProvjeriStanjeOtplate();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestProvjereStanjaOtplate2()
        {
            Klijent x = new Klijent();
            Kredit k = new Kredit(x, 5000, 100, -0.05, new DateTime());
            string s = k.ProvjeriStanjeOtplate();
        }

        [TestMethod]
        public void TestProvjereStanjaOtplate3()
        {
            Klijent x = new Klijent();
            Kredit k = new Kredit(x, 5000, 100, 0.05, new DateTime(2021, 1, 1));
            string s = k.ProvjeriStanjeOtplate();
            Assert.AreEqual(s, "Kredit koji se treba vratiti najkasnije na dan 01.01.2021. godine ima preostali iznos od 5000 KM. Iznos rate je 100 KM, po stopi od 5 %.");
        }

        [TestMethod]
        public void TestKlijenataSaBlokiranimRacunima1()
        {
            Banka b = new Banka();
            Racun r = new Racun(200);
            r.PromijeniStanjeRačuna("BANKAR12345", -200);
            r.Blokiran = true;
            Racun r2 = new Racun(500);
            Klijent k = new Klijent();
            k.Racuni.Add(r);
            k.Racuni.Add(r2);
            b.Klijenti.Add(k);
            Assert.AreEqual(b.KlijentiSBlokiranimRačunima().Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestKlijenataSaBlokiranimRacunima2()
        {
            Banka b = new Banka();
            Racun r = new Racun(1000);
            r.Blokiran = false;
            Racun r2 = new Racun(500);
            r2.Blokiran = false;
            Klijent k = new Klijent();
            k.Racuni.Add(r);
            k.Racuni.Add(r2);
            b.Klijenti.Add(k);
            Assert.AreEqual(b.KlijentiSBlokiranimRačunima().Count, 0);
        }

    }
}
