using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Film_Webshop;
using Film_Webshop.Context.MSSQL;
using Film_Webshop.Helpers;
using Film_Webshop.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;

namespace Film_Webshop_Test
{
    [TestClass]
    public class IntegrationTests
    {
        private readonly MssqlAccountContext _accountContext = new MssqlAccountContext();
        private readonly MssqlWinkelmandContext _winkelmandContext = new MssqlWinkelmandContext();
        private readonly MssqlCreditContext _creditContext = new MssqlCreditContext();
        private readonly MssqlFilmContext _filmContext = new MssqlFilmContext();
        private TransactionScope scope;

        [TestInitialize]
        public void Initialize()
        {
            this.scope = new TransactionScope();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.scope.Dispose();
        }

        [TestMethod]
        public void TestInsertAccount()
        {
            //Arrange
            //goed account
            Account accountGoed = new Account("integration@test.com", PasswordManager.Hash("Wachtwoord123"));
            //leeg account
            Account accountLeeg = new Account();

            //Act
            bool goedAccount = _accountContext.Insert(accountGoed);
            bool leegAccount = _accountContext.Insert(accountLeeg);

            //Assert
            Assert.IsTrue(goedAccount);
            Assert.IsFalse(leegAccount);
        }

        [TestMethod]
        public void TestInsertWinkelmand()
        {
            //Arrange
            //goed winkelmandID
            int winkelmandIdGoed = 1;
            //Niet bestaand winkelmandID in database
            int winkelmandIdLeeg = 0;
            // goed filmID
            int filmIdGoed = 1;
            //Niet bestaand filmID in database
            int filmIdLeeg = 0;

            //Act
            bool goedWinkelmandGoedFilm = _winkelmandContext.Insert(winkelmandIdGoed, filmIdGoed);
            bool goedWinkelmandLeegFilm = _winkelmandContext.Insert(winkelmandIdGoed, filmIdLeeg);
            bool leegWinkelmandGoedFilm = _winkelmandContext.Insert(winkelmandIdLeeg, filmIdGoed);
            bool leegWinkelmandLeegFilm = _winkelmandContext.Insert(winkelmandIdLeeg, filmIdLeeg);


            //Assert
            Assert.IsTrue(goedWinkelmandGoedFilm);
            Assert.IsFalse(goedWinkelmandLeegFilm);
            Assert.IsFalse(leegWinkelmandGoedFilm);
            Assert.IsFalse(leegWinkelmandLeegFilm);
        }

        [TestMethod]
        public void TestUpdateCredit()
        {
            //Arrange
            //goed Account
            Account accountGoed = new Account(1, 100, "simon@account.com", PasswordManager.Hash("Wachtwoord123"), false);
            //Niet bestaand accountID in database
            Account accountLeeg = new Account();
            //goed credits
            int creditsGoed = 100;
            //Negatieve credits
            int creditsNeg = -100;

            //Act
            bool goedAccountGoedCredits = _creditContext.Update(accountGoed, creditsGoed);
            bool goedAccountNegCredits = _creditContext.Update(accountGoed, creditsNeg);
            bool leegAccountGoedCredits = _creditContext.Update(accountLeeg, creditsGoed);
            bool leegAccountNegCredits = _creditContext.Update(accountLeeg, creditsNeg);


            //Assert
            Assert.IsTrue(goedAccountGoedCredits);
            Assert.IsFalse(goedAccountNegCredits);
            Assert.IsFalse(leegAccountGoedCredits);
            Assert.IsFalse(leegAccountNegCredits);
        }

        [TestMethod]
        public void TestSelectFilm()
        {
            //Arrange
            //Goede films
            Film KingKong = new Film(1, "King Kong", "Peter Jacksons versie van het beroemde verhaal over de gigantische gorilla King Kong die op een mysterieus eiland ontdekt wordt door een filmcrew.", new List<Genre> { new Genre { Naam = "Actie ", Aantal = 0, Checked = false } }, 201, 30, 7.2, null, 2005);
            KingKong.Image = _filmContext.GetById(1).Image;
            //Niet bestaande Film
            Film Leeg = new Film();

            //Act
            Film KK = _filmContext.GetById(1);
            Film empty = _filmContext.GetById(0);

            //Assert
            //king kong
            Assert.AreEqual(KingKong.Id, KK.Id);
            Assert.AreEqual(KingKong.Date, KK.Date);
            CollectionAssert.AreEqual(KingKong.Image, KK.Image);
            Assert.AreEqual(KingKong.ListGenres.Count, KK.ListGenres.Count);
            Assert.AreEqual(KingKong.ListGenres[0].Naam, KK.ListGenres[0].Naam);
            Assert.AreEqual(KingKong.ListGenres[0].Checked, KK.ListGenres[0].Checked);
            Assert.AreEqual(KingKong.ListGenres[0].Aantal, KK.ListGenres[0].Aantal);
            Assert.AreEqual(KingKong.Naam, KK.Naam);
            Assert.AreEqual(KingKong.Prijs, KK.Prijs);
            Assert.AreEqual(KingKong.Beschrijving, KK.Beschrijving);
            Assert.AreEqual(KingKong.GenresString, KK.GenresString);
            Assert.AreEqual(KingKong.Jaar, KK.Jaar);
            Assert.AreEqual(KingKong.Lengte, KK.Lengte);
            Assert.AreEqual(KingKong.Rating, KK.Rating);
            Assert.AreEqual(KingKong.Time, KK.Time);

            //lege film
            Assert.AreEqual(Leeg.Id, empty.Id);
            Assert.AreEqual(Leeg.Date, empty.Date);
            CollectionAssert.AreEqual(Leeg.Image, empty.Image);
            Assert.AreEqual(Leeg.ListGenres, empty.ListGenres);
            Assert.AreEqual(Leeg.Naam, empty.Naam);
            Assert.AreEqual(Leeg.Prijs, empty.Prijs);
            Assert.AreEqual(Leeg.Beschrijving, empty.Beschrijving);
            Assert.AreEqual(Leeg.GenresString, empty.GenresString);
            Assert.AreEqual(Leeg.Jaar, empty.Jaar);
            Assert.AreEqual(Leeg.Lengte, empty.Lengte);
            Assert.AreEqual(Leeg.Rating, empty.Rating);
            Assert.AreEqual(Leeg.Time, empty.Time);
        }

        [TestMethod]
        public void TestFilmInsert()
        {
            //Arrange
            //Goede film
            //haal king kong van db
            Film KK = _filmContext.GetById(1);
            Film filmGoed = new Film(1, "King Kong", "Peter Jacksons versie van het beroemde verhaal over de gigantische gorilla King Kong die op een mysterieus eiland ontdekt wordt door een filmcrew.", KK.ListGenres, 201, 30, 7.2, KK.Image, 2005);
            //Lege film
            Film filmLeeg = new Film();

            //Act
            bool goed = _filmContext.Insert(filmGoed);
            bool leeg = _filmContext.Insert(filmLeeg);

            //Assert
            Assert.IsTrue(goed);
            Assert.IsFalse(leeg);
        }
    }
}
