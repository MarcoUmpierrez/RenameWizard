using RenameWizard;
using RenameWizard.Handlers;

namespace RenameWizardTest
{
    public class HandlerTest
    {
        [Fact]
        public void AddTextHandlerTest()
        {
            var handler = new AddTextHandler(true, true, "world");
            Assert.Equal("hello_world.txt", handler.Rename("hello_", ".txt"));
        }

        [Fact]
        public void NumberHandlerTest()
        {
            var handler = new NumberHandler(
                addNumber: true,
                type: NumerationType.Simple,
                order: Order.TextNumber,
                totalFiles: 10,
                count: 0);
            Assert.Equal("hello_0.txt", handler.Rename("hello_", ".txt"));

            handler = new NumberHandler(
                addNumber: true,
                type: NumerationType.Simple,
                order: Order.NumberText,
                totalFiles: 10,
                count: 0);
            Assert.Equal("0_hello.txt", handler.Rename("_hello", ".txt"));

            handler = new NumberHandler(
                addNumber: true,
                type: NumerationType.Simple,
                order: Order.TextNumber,
                totalFiles: 10,
                count: 1);
            for (int i = 0; i <= 10; i++)
            {
                Assert.Equal($"hello_{i+1}.txt", handler.Rename("hello_", ".txt"));
            }


            handler = new NumberHandler(
                addNumber: true,
                type: NumerationType.Smart,
                order: Order.TextNumber,
                totalFiles: 10,
                count: 0);
            Assert.Equal("hello_00.txt", handler.Rename("hello_", ".txt"));

            handler = new NumberHandler(
                addNumber: true,
                type: NumerationType.Smart,
                order: Order.TextNumber,
                totalFiles: 100,
                count: 0);

            for (int i = 0; i < 100; i++)
            {
                if (i < 10)
                {
                    Assert.Equal($"hello_00{i}.txt", handler.Rename("hello_", ".txt"));                
                }
                else
                {
                    Assert.Equal($"hello_0{i}.txt", handler.Rename("hello_", ".txt"));
                }
            }

            Assert.Equal("hello_100.txt", handler.Rename("hello_", ".txt"));
        }

        [Fact]
        public void ReplaceTextHandler()
        {
            var handler = new ReplaceTextHandler(
                doReplacement: true,
                str1: "world",
                str2: "house");
            Assert.Equal("hello_house.txt", handler.Rename("hello_world", ".txt"));
        }
    }
}