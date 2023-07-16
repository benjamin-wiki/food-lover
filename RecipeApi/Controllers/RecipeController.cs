// Controllers/RecipeController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApi.Data;
using RecipeApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly RecipeContext _context;
    private readonly UserManager<User> _userManager;

    public RecipeController(RecipeContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Recipe>>> Get()
    // {
    //     return await _context.Recipes.Include(r => r.User).ToListAsync();
    // }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<Recipe>> Get(int id)
    // {
    //     var recipe = await _context.Recipes.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);

    //     if (recipe == null)
    //         return NotFound();

    //     return recipe;
    // }

    [HttpGet]
    public IEnumerable<Recipe> Get()
    {
        return _context.Recipes.Include(r => r.User).ToList();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var recipe = await _context.Recipes.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);

        if (recipe == null)
            return NotFound();

        return Ok(recipe);
    }

    [HttpPost]
    public async Task<IActionResult> Create(RecipeCreateDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Console.WriteLine(User.Identity.Name);

        var recipe = new Recipe 
        {
            Title = model.Title,
            Ingredients = model.Ingredients,
            Instructions = model.Instructions,
            UserId = _userManager.GetUserId(User)
        };

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = recipe.Id }, recipe);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RecipeUpdateDto model)
    {
        var recipe = await _context.Recipes.FindAsync(id);

        if (recipe == null)
            return NotFound();

        if (recipe.UserId != _userManager.GetUserId(User))
            return Unauthorized();

        // Update the properties of the recipe...
        recipe.Title = model.Title;
        recipe.Instructions = model.Instructions;
        recipe.Ingredients = model.Ingredients;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var recipe = await _context.Recipes.FindAsync(id);

        if (recipe == null)
            return NotFound();

        if (recipe.UserId != _userManager.GetUserId(User))
            return Unauthorized();

        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
