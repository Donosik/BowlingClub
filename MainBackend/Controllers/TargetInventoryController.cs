using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

[Authorize(Policy = "Worker")]
[ApiController]
[Route("[controller]")]
public class TargetInventoryController : ControllerBase
{
    private readonly IServiceWrapper serviceWrapper;

    public TargetInventoryController(IServiceWrapper serviceWrapper)
    {
        this.serviceWrapper = serviceWrapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTargetInventoryItems()
    {
        var result = await serviceWrapper.targetInventory.GetTargetInventoryItems();
        if (result != null)
            return Ok(result);
        return NotFound();
    }

    [HttpGet("MagazineStatus")]
    public async Task<IActionResult> GetMagazineStatus()
    {
        var result = await serviceWrapper.targetInventory.GetMagazineStatus();
        if (result != null)
            return Ok(result);
        return NotFound();
    }
    
    [HttpGet("MagazineStatus/{usersPerPage}/{currentPage}")]
    public async Task<IActionResult> GetMagazineStatusPaginated(int usersPerPage, int currentPage)
    {
        var result = await serviceWrapper.targetInventory.GetMagazineStatus(usersPerPage, currentPage);
        if (result != null)
            return Ok(result);
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddTargetInventoryItem(TargetInventory targetInventory)
    {
        if (await serviceWrapper.targetInventory.AddTargetInventoryItem(targetInventory))
            return Ok();
        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ChangeTargetInventoryItem(int id, TargetInventory targetInventory)
    {
        if (await serviceWrapper.targetInventory.UpdateTargetInventoryItem(id, targetInventory))
            return Ok();
        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTargetInventoryITem(int id)
    {
        if (await serviceWrapper.targetInventory.DeleteTargetInventoryItem(id))
            return Ok();
        return NotFound();
    }
}