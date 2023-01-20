using SecretNumberGame.Services.Contracts;
using SecretNumberGame.Services.Models;

namespace SecretNumberGame.Services
{
    public class SecretNumberService : ISecretNumberService
    {
        public async Task<SecretNumberViewModel> GetModelAsJson(string nums, int num, int col)
        {
            SecretNumberViewModel modelNumber = new SecretNumberViewModel();
            modelNumber.SecretNum = nums;
            modelNumber.Col = col;
            if (int.Parse(nums.Split(" ").ToArray()[col - 1]) != num && nums.Contains(num.ToString()))
            {
                modelNumber.Num = $"{num}";
                modelNumber.NumFigure = "🐄";
            }
            else if (int.Parse(nums.Split(" ").ToArray()[col - 1]) != num && nums.Contains(num.ToString()) == false)
            {
                modelNumber.Num = $"{num}";
                modelNumber.NumFigure = "💥";
            }
            else if (int.Parse(nums.Split(" ").ToArray()[col - 1]) == num)
            {
                modelNumber.Num = $"{num}";
                modelNumber.NumFigure = "🐂";
            }
            return modelNumber;
        }

        public async Task<SecretNumberViewModel> GetSecretNumberAsync()
        {            
            var firstNumber = new Random().Next(1, 10);
            var secretNumber = new HashSet<int>();
            secretNumber.Add(firstNumber);
            while (secretNumber.Count() < 7)
            {
                secretNumber.Add(new Random().Next(0, 10));
            }
            var model = new SecretNumberViewModel();
            model.SecretNum = string.Join(" ", secretNumber);
            return model;

        }
    }
}