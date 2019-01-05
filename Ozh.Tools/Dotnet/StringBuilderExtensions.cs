using System.Text;

namespace Ozh.Tools.Dotnet {
    public static class StringBuilderExtensions {

        public static void AppendLineTabbed(this StringBuilder stringBuilder, int tabCount, string line ) {
            string tabStr = string.Empty;
            for(int i = 0; i < tabCount; i++ ) {
                tabStr += '\t';
            }
            stringBuilder.AppendLine(tabStr + line);
        }
    }
}
