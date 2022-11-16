using System.Collections.Generic;

namespace Full_GRASP_And_SOLID
{
    public class TimedRecipe : IRecipeContent, TimerClient // Modificado por DIP
    {
        // Cambiado por OCP
        private IList<BaseStep> steps = new List<BaseStep>();

        public Product FinalProduct { get; set; }

        private bool cooked = false;

        private CountdownTimer countdownTimer;

        public bool Cooked
        {
            get
            {
                return cooked;
            }
        }

        // Agregado por Creator
        public void AddStep(Product input, double quantity, Equipment equipment, int time)
        {
            Step step = new Step(input, quantity, equipment, time);
            this.steps.Add(step);
            countdownTimer.Register(GetCookTime(), this);
        }

        // Agregado por OCP y Creator
        public void AddStep(string description, int time)
        {
            WaitStep step = new WaitStep(description, time);
            this.steps.Add(step);
            countdownTimer.Register(GetCookTime(), this);
        }

        public virtual void RemoveStep(BaseStep step)
        {
            this.steps.Remove(step);
            countdownTimer.Register(GetCookTime(), this);
        }

        // Agregado por SRP
        public string GetTextToPrint()
        {
            string result = $"Receta de {this.FinalProduct.Description}:\n";
            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetTextToPrint() + "\n";
            }

            // Agregado por Expert
            result = result + $"Costo de producción: {this.GetProductionCost()}";

            return result;
        }

        // Agregado por Expert
        public double GetProductionCost()
        {
            double result = 0;

            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetStepCost();
            }

            return result;
        }

        public int GetCookTime()
        {
            int totalTime = 0;

            foreach (BaseStep step in this.steps)
            {
                totalTime += step.Time;
            }

            return totalTime;
        }

        public void Cook()
        {
            cooked = true;
        }

        public void TimeOut()
        {
            Cook();
        }
    }
}