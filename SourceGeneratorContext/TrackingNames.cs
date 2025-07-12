namespace Datacute.SourceGeneratorContext;

public static class TrackingNames
{
    public const string InitialExtraction = nameof(InitialExtraction);
    public const string Combine = nameof(Combine);
    
    public const int Generator_Initialize = 0;
    public const int AttributeContext_Transform = 1; // too noisy to include
    public const int AnalyzerConfigOptionsDescription_Select = 2;
    public const int CompilationDescription_Select = 3;
    public const int ParseOptionsDescription_Select = 4;
    public const int AdditionalTextDescription_Select = 5;
    public const int MetadataReferenceDescription_Select = 6;
    public const int Generator_Action = 7;
    public const int DiagnosticLog_Written = 8;

    public static readonly Dictionary<int, string> TracingNames = new()
    {
        { Generator_Initialize, nameof(Generator_Initialize) },
        { AttributeContext_Transform, nameof(AttributeContext_Transform) },
        { AnalyzerConfigOptionsDescription_Select, nameof(AnalyzerConfigOptionsDescription_Select) },
        { CompilationDescription_Select, nameof(CompilationDescription_Select) },
        { ParseOptionsDescription_Select, nameof(ParseOptionsDescription_Select) },
        { AdditionalTextDescription_Select, nameof(AdditionalTextDescription_Select) },
        { MetadataReferenceDescription_Select, nameof(MetadataReferenceDescription_Select) },
        { Generator_Action, nameof(Generator_Action) },
        { DiagnosticLog_Written, nameof(DiagnosticLog_Written) },
    };
}