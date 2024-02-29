using System.Text.RegularExpressions;

namespace RenameWizard.Handlers
{
    public class NumberHandler : IRenameHandler
    {
        private readonly bool addNumber;
        private readonly NumerationType type;
        private readonly Order order;
        private readonly int totalFiles;
        private int count;
        private IRenameHandler handler;

        public NumberHandler(bool addNumber, NumerationType type, Order order, int totalFiles, int count)
        {
            this.addNumber = addNumber;
            this.type = type;
            this.order = order;
            this.totalFiles = totalFiles;
            this.count = count;
        }
        public string Rename(string name, string ext)
        {
            string fileName = name;
            if (this.addNumber)
            {
                string number = string.Empty;
                switch (this.type)
                {
                    case NumerationType.Simple:
                        number = $"{this.count++}";
                        break;

                    case NumerationType.Smart:
                        number = this.FindSmartNumber(this.count++, this.totalFiles);
                        break;

                    case NumerationType.FindInFile:
                        number = this.FindNumberInFile(fileName);
                        break;
                }

                switch (this.order)
                {
                    case Order.TextNumber:
                        fileName = $"{fileName}{number}";
                        break;
                    case Order.NumberText:
                        fileName = $"{number}{fileName}";
                        break;
                }
            }

            return this.handler?.Rename(fileName, ext) ?? $"{fileName}{ext}";
        }

        public void SetHandler(IRenameHandler handler)
        {
            this.handler = handler;
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
            return match.Success ? match.Value : string.Empty;
        }
    }
}
