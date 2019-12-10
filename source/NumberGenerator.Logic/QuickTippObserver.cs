using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher auf einen vollständigen Quick-Tipp wartet: 6 unterschiedliche Zahlen zw. 1 und 45.
    /// </summary>
    public class QuickTippObserver : IObserver
    {
        #region Fields

        private IObservable _numberGenerator;

        #endregion

        #region Properties

        public List<int> QuickTippNumbers { get; private set; }
        public int CountOfNumbersReceived { get; private set; }

        #endregion

        #region Constructor

        public QuickTippObserver(IObservable numberGenerator)
        {
            _numberGenerator = numberGenerator;
            QuickTippNumbers = new List<int>();
            _numberGenerator.Attach(this);
        }

        #endregion

        #region Methods

        public void OnNextNumber(int number)
        {
            if (number >= 1 && number <= 45 && !QuickTippNumbers.Contains(number))
            {
                QuickTippNumbers.Add(number);
            }
            
            if (QuickTippNumbers.Count >= 6)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"   >> {this.GetType().Name}: Got full a Quick-Tipp => I am not interested in new numbers anymore => Detach().");
                Console.ResetColor();
                DetachFromNumberGenerator();
            }

            CountOfNumbersReceived++;
        }

        public override string ToString()
        {
            string nums = string.Empty;
            QuickTippNumbers.Sort();
            foreach (int num in QuickTippNumbers)
            {
                nums += Convert.ToString(num) + ", ";
            }
            return $"   >> {GetType().Name}: Recieved {CountOfNumbersReceived} numbers ==> Quick-Tipp is {nums.Remove(nums.Length-2)}.";
        }

        private void DetachFromNumberGenerator()
        {
            _numberGenerator.Detach(this);
        }

        #endregion
    }
}
