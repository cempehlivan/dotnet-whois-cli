using DotnetWhoisCLI;

namespace DotnetWhoisCLI.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestWhois()
        {
            DotnetWhoisCLI.Program.Main(new string[] { "github.com" });
        }
    }
}