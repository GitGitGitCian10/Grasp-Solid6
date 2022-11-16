using System.Collections.Generic;

namespace Full_GRASP_And_SOLID
{
    public class TimedRecipe : IRecipeContent, TimerClient
    {
        public Recipe recipe;

        public CountdownTimer countdownTimer;

        public TimedRecipe(Recipe recipe)
        {
            this.recipe = recipe;
            countdownTimer.Register(recipe.GetCookTime(), this);
        }

        public string GetTextToPrint()
        {
            return recipe.GetTextToPrint();
        }

        public void TimeOut()
        {
            recipe.Cook();
        }
    }
}