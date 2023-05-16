using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace statistics
{
    internal class Program
    {
        static void Main(string[] args)
        {
                Console.Write("Enter the number of items: ");
                int n = int.Parse(Console.ReadLine());

                List<double> items = new List<double>();
                for (int i = 0; i < n; i++)
                {
                    Console.Write($"Enter item {i + 1}: ");
                    items.Add(double.Parse(Console.ReadLine()));
                }

                double[] sortedItems = items.OrderBy(x => x).ToArray();

                double median = GetMedian(sortedItems);
                double mode = GetMode(sortedItems);
                double range = GetRange(sortedItems);
                double quartile1 = GetQuartile(sortedItems, 0.25);
                double quartile3 = GetQuartile(sortedItems, 0.75);
                double p90 = GetPercentile(sortedItems, 0.9);
                double interquartile = quartile3 - quartile1;
                double lowerOutlierBoundary = quartile1 - 1.5 * interquartile;
                double upperOutlierBoundary = quartile3 + 1.5 * interquartile;

                Console.WriteLine($"Median: {median}");
                Console.WriteLine($"Mode: {mode}");
                Console.WriteLine($"Range: {range}");
                Console.WriteLine($"First Quartile: {quartile1}");
                Console.WriteLine($"Third Quartile: {quartile3}");
                Console.WriteLine($"P90: {p90}");
                Console.WriteLine($"Interquartile: {interquartile}");
                Console.WriteLine($"Outlier Region: ({lowerOutlierBoundary}, {upperOutlierBoundary})");

                Console.Write("Enter a value to check if it's an outlier: ");
                double inputValue = double.Parse(Console.ReadLine());
                if (inputValue < lowerOutlierBoundary || inputValue > upperOutlierBoundary)
                {
                    Console.WriteLine($"{inputValue} is an outlier.");
                }
                else
                {
                    Console.WriteLine($"{inputValue} is not an outlier.");
                }
            }

            static double GetMedian(double[] sortedItems)
            {
                int n = sortedItems.Length;
                if (n % 2 == 0)
                {
                    return (sortedItems[n / 2 - 1] + sortedItems[n / 2]) / 2;
                }
                else
                {
                    return sortedItems[n / 2];
                }
            }

            static double GetMode(double[] sortedItems)
            {
                Dictionary<double, int> counts = new Dictionary<double, int>();
                foreach (double item in sortedItems)
                {
                    if (counts.ContainsKey(item))
                    {
                        counts[item]++;
                    }
                    else
                    {
                        counts[item] = 1;
                    }
                }

                int maxCount = counts.Values.Max();
                double mode = counts.First(x => x.Value == maxCount).Key;
                return mode;
            }

            static double GetRange(double[] sortedItems)
            {
                return sortedItems.Last() - sortedItems.First();
            }

            static double GetQuartile(double[] sortedItems, double percentile)
            {
                int n = sortedItems.Length;
                int index = (int)Math.Round(percentile * (n - 1));
                if (n % 2 == 0)
                {
                    return (sortedItems[index] + sortedItems[index + 1]) / 2;
                }
                else
                {
                    return sortedItems[index];
                }
            }
        static double GetPercentile(double[] sortedItems, double percentile)
        {
            int n = sortedItems.Length;
            double index = percentile * (n - 1);
            int lowerIndex = (int)Math.Floor(index);
            int upperIndex = (int)Math.Ceiling(index);
            if (lowerIndex == upperIndex)
            {
                return sortedItems[lowerIndex];
            }
            else
            {
                return sortedItems[lowerIndex] * (upperIndex - index) + sortedItems[upperIndex] * (index - lowerIndex);
            }
        }
    }
}
