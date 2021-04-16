using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public int RecipeId { get; set; }
    public string DishName { get; set; }
    [RangeAttribute(0, 5), DefaultValue(0)]
    public int StarRating { get; set; }
    public string IngredientsString { get; set; }
    [NotMapped]
    public List<string> IngredientsList { get; set;}
    [NotMapped, DefaultValue(false)]
    public bool IngredientsGrow { get; set; }
    public string InstructionsString { get; set; }
    [NotMapped]
    public List<string> InstructionsList { get; set; }
    [NotMapped, DefaultValue(false)]
    public bool InstructionsGrow { get; set; }
    public virtual ApplicationUser User { get; set; }

    public void TranslateToString()
    {
      IngredientsString = string.Join("p__q", IngredientsList);
      InstructionsString = string.Join("p__q", InstructionsList);
    }

    public void TranslateToList()
    {
      IngredientsList = IngredientsString.Split("p__q").ToList();
      InstructionsList = InstructionsString.Split("p__q").ToList();
    }

    public void CleanLists()
    {
      for (int i = 0; i < IngredientsList.Count; i++)
      {
        if (IngredientsList[i] == null)
        {
          IngredientsList.RemoveAt(i);
          i--;
        }
      }
      for (int i = 0; i < InstructionsList.Count; i++)
      {
        if (InstructionsList[i] == null)
        {
          InstructionsList.RemoveAt(i);
          i--;
        }
      }
    }

    public int GrowIngredients()
    {
      IngredientsList.Add("");
      return IngredientsList.Count;
    }

    public int GrowInstructions()
    {
      InstructionsList.Add("");
      return InstructionsList.Count;
    }
  }
}