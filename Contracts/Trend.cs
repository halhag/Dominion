using System;
using System.Collections.Generic;

namespace Contracts
{
    public class Trend
    {
        private Queue<double> _queue = new Queue<double>();

        public void Add(double previous, double current)
        {
            double change = current - previous;
            _queue.Enqueue(change);
            if (_queue.Count > 5)
            {
                _queue.Dequeue();
            }
        } 

        public string GetEndring()
        {
            var endring = string.Empty;
            while (_queue.Count > 0)
            {
                var change = _queue.Dequeue();
                if (change > 0)
                {
                    endring += "+";
                }
                endring += Math.Round(change,0);
                if (_queue.Count > 0)
                    endring += " ";
            }

            return endring;
        }
    }
}
