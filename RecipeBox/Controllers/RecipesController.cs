using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecipeBox.Controllers
{
  [Authorize]
  public class RecipesController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public RecipesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
    {
      _db = db;
      _userManager = userManager;
    }

    public async Task<ActionResult> Index()
    {
      string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Recipe> masterList = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return Index(masterList);
    }

    [NonAction]
    public ActionResult Index(List<Recipe> masterList)
    {
      ViewBag.fiveStar = masterList.Where(entry => entry.StarRating == 5).ToList();
      ViewBag.fourStar = masterList.Where(entry => entry.StarRating == 4).ToList();
      ViewBag.threeStar = masterList.Where(entry => entry.StarRating == 3).ToList();
      ViewBag.twoStar = masterList.Where(entry => entry.StarRating == 2).ToList();
      ViewBag.oneStar = masterList.Where(entry => entry.StarRating == 1).ToList();
      ViewBag.zeroStar = masterList.Where(entry => entry.StarRating == 0).ToList();
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Index(string search)
    {
      string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Recipe> masterList = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id).ToList();
      if(search != null)
      {
        for(int i = 0; i < masterList.Count; i++)
        {
          if(!(masterList[i].IngredientsString.Contains(search)))
          {
            masterList.RemoveAt(i);
            i--;
          }
        }
      }
      return Index(masterList);
    }

    public ActionResult Create(int ingredientNumber, int instructionNumber)
    {
      Recipe recipeCreate = new Recipe();
      recipeCreate.IngredientsList = Enumerable.Repeat("", ingredientNumber).ToList();
      recipeCreate.InstructionsList = Enumerable.Repeat("", instructionNumber).ToList();
      return Create(recipeCreate, ingredientNumber, instructionNumber);
    }

    [NonAction]
    public ActionResult Create(Recipe recipe, int ingredientNumber, int instructionNumber)
    {
      ViewBag.IngredientsLength = ingredientNumber;
      ViewBag.InstructionsLength = instructionNumber;
      return View(recipe);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Recipe recipe)
    {
      int ingredientNumber = recipe.IngredientsList.Count;
      int instructionNumber = recipe.InstructionsList.Count;

      if(recipe.IngredientsGrow && recipe.InstructionsGrow)
      {
        ingredientNumber = recipe.GrowIngredients();
        instructionNumber = recipe.GrowInstructions();
        return Create(recipe, ingredientNumber, instructionNumber);
      }
      else if (recipe.IngredientsGrow)
      {
        ingredientNumber = recipe.GrowIngredients();
        return Create(recipe, ingredientNumber, instructionNumber);
      }
      else if (recipe.InstructionsGrow)
      {
        instructionNumber = recipe.GrowInstructions();
        return Create(recipe, ingredientNumber, instructionNumber);
      }
      else
      {
        string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        recipe.User = currentUser;
        recipe.CleanLists();
        recipe.TranslateToString();
        _db.Recipes.Add(recipe);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      thisRecipe.TranslateToList();
      return View(thisRecipe);
    }

    public ActionResult Edit(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      thisRecipe.TranslateToList();
      int ingredientNumber = thisRecipe.IngredientsList.Count;
      int instructionNumber = thisRecipe.InstructionsList.Count;
      return Edit(thisRecipe, ingredientNumber, instructionNumber);
    }

    [NonAction]
    public ActionResult Edit(Recipe recipe, int ingredientNumber, int instructionNumber)
    {
      ViewBag.IngredientsLength = ingredientNumber;
      ViewBag.InstructionsLength = instructionNumber;
      return View(recipe);
    }

    [HttpPost]
    public ActionResult Edit(Recipe recipe)
    {
      int ingredientNumber = recipe.IngredientsList.Count;
      int instructionNumber = recipe.InstructionsList.Count;

      if (recipe.IngredientsGrow && recipe.InstructionsGrow)
      {
        ingredientNumber = recipe.GrowIngredients();
        instructionNumber = recipe.GrowInstructions();
        return Edit(recipe, ingredientNumber, instructionNumber);
      }
      else if (recipe.IngredientsGrow)
      {
        ingredientNumber = recipe.GrowIngredients();
        return Edit(recipe, ingredientNumber, instructionNumber);
      }
      else if (recipe.InstructionsGrow)
      {
        instructionNumber = recipe.GrowInstructions();
        return Edit(recipe, ingredientNumber, instructionNumber);
      }
      else
      {
        recipe.CleanLists();
        recipe.TranslateToString();
        _db.Entry(recipe).State = EntityState.Modified;
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Delete(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}