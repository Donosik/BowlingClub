using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Classes;
using Moq;

namespace MainBackendTests;

[TestClass]
public class UserServiceTests
{
    private Mock<IUserRepository> _mockUserRepo;
    private Mock<IRepositoryWrapper> _mockRepoWrapper;
    private UserService _service;
    
    [TestInitialize]
    public void TestInit()
    {
        _mockRepoWrapper = new Mock<IRepositoryWrapper>();
            
        _mockRepoWrapper.Setup(r => r.normalDbWrapper.user.GetAll()).ReturnsAsync(GetTestUsers);

        _service = new UserService(_mockRepoWrapper.Object, null);
    }
    
    [TestMethod]
    public async Task GetUsers_WithPaging_ReturnsCorrectPageSize()
    {
        var usersPerPage = 2;
        var currentPage = 1;

        var returnedUsers = await _service.GetUsers(usersPerPage, currentPage);

        Assert.IsNotNull(returnedUsers);
        Assert.AreEqual(usersPerPage, returnedUsers.Count);
    }

    [TestMethod]
    public async Task GetUsers_WithPaging_ReturnsCorrectPage()
    {
        var usersPerPage = 2;
        var currentPage = 2;

        var returnedUsers = await _service.GetUsers(usersPerPage, currentPage);

        Assert.IsNotNull(returnedUsers);
        Assert.AreEqual(3, returnedUsers.First().Id); 
    }
    
    private List<User> GetTestUsers()
    {
        return new List<User> 
        {
            new User { Id = 1 },
            new User { Id = 2 },
            new User { Id = 3},
            new User { Id = 4 },
        };
    }
}