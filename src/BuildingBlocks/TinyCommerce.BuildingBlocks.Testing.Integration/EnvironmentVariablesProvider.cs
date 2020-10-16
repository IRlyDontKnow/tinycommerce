using System;

namespace TinyCommerce.BuildingBlocks.Testing.Integration
{
    public static class EnvironmentVariablesProvider
    {
        public static string GetVariable(string variableName)
        {
            var envVariable = Environment.GetEnvironmentVariable(variableName);

            if (!string.IsNullOrEmpty(envVariable))
            {
                return envVariable;
            }

            envVariable = Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.User);

            if (!string.IsNullOrEmpty(envVariable))
            {
                return envVariable;
            }

            return Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Machine);
        }
    }
}