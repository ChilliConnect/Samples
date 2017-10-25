namespace UnrealBuildTool.Rules
{
    public class ChilliConnect : ModuleRules
    {
        public ChilliConnect(ReadOnlyTargetRules ROTargetRules) : base(ROTargetRules)
        {
            PrivateIncludePaths.AddRange(
                new string[] {"ChilliConnect/Private"}
            );

            PublicDependencyModuleNames.AddRange(
                new string[]
                {
                    "Core",
                    "CoreUObject",
                    "Engine",
                    "HTTP",
                    "Json"
                }
            );
        }
    }
}
