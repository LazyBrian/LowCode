using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LowCode.Models;
using Microsoft.EntityFrameworkCore;
using Attribute = LowCode.Models.Attribute;

namespace LowCode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly LowCodeDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, LowCodeDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        Entity entity = _dbContext.Entities.Include(c => c.Attributes)
            .FirstOrDefault(c => c.LogicalName == "Books");

        if (entity != null)
        {
            string attributes = string.Join(",", entity.Attributes.Select(c => c.LogicalName));

            string tableName = entity.LogicalName;

            string tableId = entity.EntityId.ToString();

            string sql = $"select {attributes} from {tableName} ";
            Console.WriteLine(sql);
            var dic = new Dictionary<string, object>();

            IList<dynamic> list = _dbContext.CollectionSql(sql, dic).ToList();

            foreach (var item in list)
            {
                Console.WriteLine(item.BookId);

                Console.WriteLine(item.BookName);
            }
        }


        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public IActionResult QueryEntities(string logicalName)
    {

        Entity entity = _dbContext.Entities.Include(c => c.Attributes)
                    .FirstOrDefault(c => c.LogicalName == "Books");

        if (entity != null)
        {
            string attributes = string.Join(",", entity.Attributes.Select(c => c.LogicalName));

            string tableName = entity.LogicalName;

            string tableId = entity.EntityId.ToString();

            string sql = $"select {attributes} from {tableName} ";
            Console.WriteLine(sql);
            var dic = new Dictionary<string, object>();

            IList<dynamic> list = _dbContext.CollectionSql(sql, dic).ToList();

            return View(list);
        }
        return View();
    }


    public IActionResult AddEntity()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddEntity([Bind("LogicalName,DisplayName,Description")] Entity entity)
    {
        if (ModelState.IsValid)
        {


            if (!_dbContext.Entities.Any(c => c.LogicalName == entity.LogicalName))
            {
                DatabaseHelper.CreateTable(entity);

                _dbContext.Entities.Add(entity);

                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Has a same Entity {entity.LogicalName}");
            }
        }
        return View();
    }

    public IActionResult AddAttribute()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddAttribute([Bind("LogicalName,DisplayName,Description")] Attribute attribute)
    {
        string entityLogicalName = "test";
        attribute.EntityId = 5;
        // if (ModelState.IsValid)
        // {
            if (!_dbContext.Attributes.Any(c => c.LogicalName == entityLogicalName && c.LogicalName == attribute.LogicalName))
            {
                DatabaseHelper.AddAttribute(entityLogicalName, attribute);
                _dbContext.Attributes.Add(attribute);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Has a same Attribute {attribute.LogicalName}");
            }
        // }

        return View();
    }
}
