using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RenameWizard.Strategies
{
    public class NumberStrategy : IRenameStrategy
    {
        private readonly bool addNumber;
        private readonly NumerationType type;
        private readonly Order order;
        private readonly int totalFiles;
        private int count;

        public NumberStrategy(bool addNumber, NumerationType type, Order order, int totalFiles, int count)
        {
            this.addNumber = addNumber;
            this.type = type;
            this.order = order;
            this.totalFiles = totalFiles;
            this.count = count;
        }
        public string Rename(string fileName)
        {
            string name = fileName;
            if (addNumber)
            {
                string number = string.Empty;
                switch (type)
                {
                    case NumerationType.Simple:
                        number = $"{count++}";
                        break;

                    case NumerationType.Smart:
                        number = FindSmartNumber(count++, totalFiles);
                        break;

                    case NumerationType.FindInFile:
                        number = FindNumberInFile(name);
                        break;
                }

                switch (order)
                {
                    case Order.TextNumber:
                        name = $"{number}{name}";
                        break;
                    case Order.NumberText:
                        name = $"{name}{number}";
                        break;
                }
            }

            return name;
        }

        private string FindSmartNumber(int count, int total)
        {
            int positions = total.ToString().Length;
            return count.ToString("D" + positions);
        }

        private string FindNumberInFile(string input)
        {
            // Use Regex.Match to find the first occurrence
            // of a number in the input text
            string pattern = @"\d+";
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
