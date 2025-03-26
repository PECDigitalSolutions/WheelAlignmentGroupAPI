using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HjulinstallningAPI.Controllers;
using HjulinstallningAPI.Data;
using HjulinstallningAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class WowCharacterControllerTests
{
    private readonly WowCharacterController _controller;
    private readonly VehicleDbContext _dbContext;

    public WowCharacterControllerTests()
    {
        // ✅ Setup InMemory Database
        var options = new DbContextOptionsBuilder<VehicleDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new VehicleDbContext(options);
        _controller = new WowCharacterController(_dbContext);

        // ❌ REMOVE seeding from constructor (causes duplicates)
        SeedDatabase();
    }

    // ✅ Run this before each test to clear & re-seed database
    private void SeedDatabase()
    {
        _dbContext.Database.EnsureDeleted();  // ❗ Resets the DB before each test
        _dbContext.Database.EnsureCreated();  // ❗ Creates a fresh DB

        _dbContext.WowCharacters.Add(new WowCharacter { Id = 1, Name = "Thrall", Race = "Orc", Class = "Shaman", ArmorType = "Mail" });
        _dbContext.WowCharacters.Add(new WowCharacter { Id = 2, Name = "Jaina", Race = "Human", Class = "Mage", ArmorType = "Cloth" });
        _dbContext.SaveChanges();
    }

    // ✅ Test: Get all characters
    [Fact]
    public async Task GetCharacters_ReturnsAllCharacters()
    {
        // Act
        var result = await _controller.GetCharacters(null, null, null);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var characters = Assert.IsType<List<WowCharacter>>(okResult.Value);
        Assert.Equal(2, characters.Count);
    }



    // ✅ Test: Get a single character (valid ID)
    [Fact]
    public async Task GetCharacter_ReturnsCharacter_WhenCharacterExists()
    {
        // Act
        var result = await _controller.GetCharacter(1);

        // Assert
        var okResult = Assert.IsType<WowCharacter>(result.Value);
        Assert.Equal("Thrall", okResult.Name);
    }

    // ✅ Test: Get a character (invalid ID)
    [Fact]
    public async Task GetCharacter_ReturnsNotFound_WhenCharacterDoesNotExist()
    {
        // Act
        var result = await _controller.GetCharacter(99);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    // ✅ Test: Add a new character
    [Fact]
    public async Task AddCharacter_ReturnsCreatedCharacter()
    {
        // Arrange (ArmorType added)
        var newCharacter = new WowCharacter { Id = 3, Name = "Sylvanas", Race = "Undead", Class = "Hunter", ArmorType = "Leather" };

        // Act
        var result = await _controller.AddCharacter(newCharacter);

        // Assert
        var createdAtAction = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedCharacter = Assert.IsType<WowCharacter>(createdAtAction.Value);
        Assert.Equal("Sylvanas", returnedCharacter.Name);
    }

    // ✅ Test: Ensure `400 BadRequest` when `ArmorType` is missing
    [Fact]
    public async Task AddCharacter_ReturnsBadRequest_WhenArmorTypeIsMissing()
    {
        // Arrange
        var invalidCharacter = new WowCharacter { Id = 4, Name = "Gul'dan", Race = "Orc", Class = "Warlock" };

        // Act
        var result = await _controller.AddCharacter(invalidCharacter);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("ArmorType is required.", badRequestResult.Value); // ✅ Check error message
    }


    // ✅ Test: Update an existing character
    [Fact]
    public async Task UpdateCharacter_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // ✅ Retrieve entity from DB before modifying
        var existingCharacter = await _dbContext.WowCharacters.FindAsync(1);
        existingCharacter.Class = "Warrior"; // Modify the property

        // Act
        var result = await _controller.UpdateCharacter(1, existingCharacter);

        // Assert
        Assert.IsType<NoContentResult>(result);

        var modifiedCharacter = await _dbContext.WowCharacters.FindAsync(1);
        Assert.Equal("Warrior", modifiedCharacter.Class);
    }

    // ✅ Test: Update a non-existing character
    [Fact]
    public async Task UpdateCharacter_ReturnsNotFound_WhenCharacterDoesNotExist()
    {
        // Arrange (ArmorType added)
        var updatedCharacter = new WowCharacter { Id = 99, Name = "Illidan", Race = "Night Elf", Class = "Demon Hunter", ArmorType = "Leather" };

        // Act
        var result = await _controller.UpdateCharacter(99, updatedCharacter);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    // ✅ Test: Delete an existing character
    [Fact]
    public async Task DeleteCharacter_ReturnsNoContent_WhenCharacterExists()
    {
        // Act
        var result = await _controller.DeleteCharacter(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Null(await _dbContext.WowCharacters.FindAsync(1));
    }

    // ✅ Test: Delete a non-existing character
    [Fact]
    public async Task DeleteCharacter_ReturnsNotFound_WhenCharacterDoesNotExist()
    {
        // Act
        var result = await _controller.DeleteCharacter(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
