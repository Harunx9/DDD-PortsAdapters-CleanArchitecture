using System.Linq;
using System.Runtime.CompilerServices;
using Xunit;

namespace Test.UT.Base
{
    public class AssertAttribute : FactAttribute
    {
        public AssertAttribute(
            string separatorReplacement = " ",
            string separator = "_",
            [CallerFilePath] string classFile = "",
            [CallerMemberName] string methodName = "")
        {
            var testClassName = classFile.Split('\\')
                .Last()
                .Split('.')
                .First()
                .Replace(separator, separatorReplacement);
            var testMethodName = methodName.Replace(separator, separatorReplacement);
            DisplayName = string.Concat(testClassName, " ", testMethodName);
        }
    }
}
