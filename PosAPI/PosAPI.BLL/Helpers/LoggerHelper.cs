namespace PosAPI.BLL.Helpers
{
    public static class LoggerHelper
    {
        public static string LoggerMessage(string? methodName, string? value, int type)
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
                    message = $"{methodName} method has an error: \n {value}";
                    break;
                case 5:
                    message = $"{methodName} method has a warning: \n {value}";
                    break;
                default:
                    break;
            }

            return message;
        }
    }
}
