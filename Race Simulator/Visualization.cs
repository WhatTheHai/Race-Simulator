namespace Race_Simulator
{
    public static class Visualization
    {
        public static void Initalize()
        {
            System.Console.WriteLine(_finishHorizontal);
        }
        #region graphics
        private static string[] _finishHorizontal =
        {
            "----",
            "  # ",
            "  # ",
            "----"
        };
        private static string[] _straightHorizontal =
{
            "----",
            "    ",
            "    ",
            "----"
        };
        private static string[] _straightVertical =
{
            "|  |",
            "|  |",
            "|  |",
            "|  |"
        };
        private static string[] _bendSW =
        {
            "--\\ ",
            "   \\",
            "\\  |",
            "|  |"
        };
        private static string[] _bendSE =
        {
            " /--",
            "/   ",
            "|   /",
            "|   |"
        };
        private static string[] _bendNW =
        {
            "   ",
            "",
            "",
            ""
        };

        #endregion
    }
}
