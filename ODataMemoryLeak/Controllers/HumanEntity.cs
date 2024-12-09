using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataMemoryLeak.Services;
using ODataMemoryLeak.Services.Entities;

namespace ODataMemoryLeak.Controllers;

public class HumanEntityController : ODataController
{
    private readonly IDataService _dataService;
    public HumanEntityController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, PageSize = 1_000_000_000)]
    public IQueryable<HumanEntity> Get()
    {
        Request.GetWriterSettings().Validations = Microsoft.OData.ValidationKinds.None;
        return _dataService.Get<HumanEntity>();
    }

    [HttpGet("" + nameof(HumanEntity) + "({id})")]
    [ProducesResponseType(typeof(HumanEntity), 200)]
    public async Task<IActionResult> GetEntityByKey(
        [FromRoute]
        [FromODataUri] int id)
    {
        var result = _dataService.Get<HumanEntity>().FirstOrDefault(x => x.Id == id);
        return Ok(result);
    }

}