

using System.Data.Entity.Infrastructure;
using EntityFramework.Testing;
using MainBackend.Databases.BowlingDb.Context;
using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.BowlingDb.Repositories.Classes;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace MainBackendTests;

[TestClass]
    public class InvoiceRepositoryTests
    {
        private BowlingDb _context;
        private InvoiceRepository _invoiceRepo;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<BowlingDb>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
    
            _context = new BowlingDb(options);
            
            _context.Invoices.AddRange(GetTestInvoices());
            _context.SaveChanges();

            _invoiceRepo = new InvoiceRepository(_context);
        }

        [TestMethod]
        public async Task GetClientInvoices_ReturnsCorrectInvoices()
        {
            var clientId = 1;

            var returnedInvoices = await _invoiceRepo.GetClientInvoices(clientId);

            Assert.AreEqual(GetTestInvoices().Count(), returnedInvoices.Count);
        }

        private IEnumerable<Invoice> GetTestInvoices()
        {
            return new List<Invoice> 
            {
                new Invoice { Id = 1, ClientId = 1},
                new Invoice { Id = 2, ClientId = 1},
            }.AsEnumerable();
        }
    }