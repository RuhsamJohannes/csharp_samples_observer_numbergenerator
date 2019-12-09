using System;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher einfache Statistiken bereit stellt (Min, Max, Sum, Avg).
    /// </summary>
    public class StatisticsObserver : BaseObserver
    {
        #region Fields

        private int _countOfNumbersToWaitFor;
        private int _countOfNumbersReceived = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Enthält das Minimum der generierten Zahlen.
        /// </summary>
        public int Min { get; private set; }

        /// <summary>
        /// Enthält das Maximum der generierten Zahlen.
        /// </summary>
        public int Max { get; private set; }

        /// <summary>
        /// Enthält die Summe der generierten Zahlen.
        /// </summary>
        public int Sum { get; private set; }

        /// <summary>
        /// Enthält den Durchschnitt der generierten Zahlen.
        /// </summary>
        public int Avg => Sum / _countOfNumbersReceived;

        #endregion

        #region Constructors

        public StatisticsObserver(IObservable numberGenerator, int countOfNumbersToWaitFor) : base(numberGenerator, countOfNumbersToWaitFor)
        {
            _countOfNumbersToWaitFor = countOfNumbersToWaitFor;
            Min = 999;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return ($"BaseObserver [CountOfNumbersReceived='{_countOfNumbersReceived}', CountOfNumbersToWaitFor='{_countOfNumbersToWaitFor}'] " +
                $"=> StatisticsObserver [Min='{Min}', Max='{Max}', Sum='{Sum}', Avg='{Avg}']");
        }

        public override void OnNextNumber(int number)
        {
            _countOfNumbersReceived++;

            if (Min > number)
            {
                Min = number;
            }
            if (Max < number)
            {
                Max = number;
            }

            Sum += number;

            if (_countOfNumbersReceived >= _countOfNumbersToWaitFor)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"   >> {this.GetType().Name}: Received '{CountOfNumbersReceived}' of '{CountOfNumbersToWaitFor}' => I am not interested in new numbers anymore => Detach().");
                Console.ResetColor();
                DetachFromNumberGenerator();
            }
        }

        #endregion
    }
}
