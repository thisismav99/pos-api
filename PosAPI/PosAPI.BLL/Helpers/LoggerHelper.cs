namespace PosAPI.BLL.Helpers
{
    public static class LoggerHelper
    {
        public static string LoggerMessage(string? methodName, string? error, int type)
        {
            string message = string.Empty;

            switch (type)
            {
                case 1:
                    message = $"{methodName} method initiated";
                    break;
                case 2:
                    message = $"Running the logic";
                    break;
                case 3:
                    message = $"{methodName} method run successfully";
                    break;
                case 4:
                    message = $"{methodName} method has an error: \n {error}";
                    break;
                default:
                    break;
            }

            return message;
        }
    }
}
