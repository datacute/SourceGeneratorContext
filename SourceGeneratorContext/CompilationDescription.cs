using System.Text;
using Microsoft.CodeAnalysis;

namespace Datacute.SourceGeneratorContext;

public readonly struct CompilationDescription
{
    public readonly string DocComments;
    public readonly string OptionsDocComments;
    public readonly string AssemblyDocComments;
    public readonly string ReferencesDocComments;

    public CompilationDescription(Compilation compilation)
    {
        var sb = new StringBuilder();
        sb.AddComment("AssemblyName", compilation.AssemblyName);
        sb.AddComment("Language", compilation.Language);
        sb.AddComment("IsCaseSensitive", compilation.IsCaseSensitive);
        sb.AddComment("DynamicType", compilation.DynamicType);
        sb.AddComment("GlobalNamespace", compilation.GlobalNamespace);
        sb.AddComment("ObjectType", compilation.ObjectType);
        sb.AddComment("ScriptClass", compilation.ScriptClass);
        sb.AddComment("SourceModule", compilation.SourceModule);
        sb.AddComment("ScriptCompilationInfo", compilation.ScriptCompilationInfo);
        sb.AddComment("SyntaxTrees Count", compilation.SyntaxTrees.Count());
        
        DocComments = sb.ToString();

        sb.Clear();
        var options = compilation.Options;
        sb.AddComment("Language", options.Language);
        sb.AddComment("OutputKind", options.OutputKind);
        sb.AddComment("ModuleName", options.ModuleName);
        sb.AddComment("MainTypeName", options.MainTypeName);
        sb.AddComment("ScriptClassName", options.ScriptClassName);
        sb.AddComment("CryptoKeyContainer", options.CryptoKeyContainer);
        sb.AddComment("CryptoKeyFile", options.CryptoKeyFile);
        sb.AddComment("CryptoPublicKey Length", options.CryptoPublicKey.Length);
        sb.AddComment("DelaySign", options.DelaySign);
        sb.AddComment("CheckOverflow", options.CheckOverflow);
        sb.AddComment("Platform", options.Platform);
        sb.AddComment("GeneralDiagnosticOption", options.GeneralDiagnosticOption);
        sb.AddComment("WarningLevel", options.WarningLevel);
        sb.AddComment("ReportSuppressedDiagnostics", options.ReportSuppressedDiagnostics);
        sb.AddComment("OptimizationLevel", options.OptimizationLevel);
        sb.AddComment("ConcurrentBuild", options.ConcurrentBuild);
        sb.AddComment("Deterministic", options.Deterministic);
        //sb.AddComment("XmlReferenceResolver", options.XmlReferenceResolver);
        //sb.AddComment("SourceReferenceResolver", options.SourceReferenceResolver);
        //sb.AddComment("SyntaxTreeOptionsProvider", options.SyntaxTreeOptionsProvider);
        //sb.AddComment("MetadataReferenceResolver", options.MetadataReferenceResolver);
        //sb.AddComment("StrongNameProvider", options.StrongNameProvider);
        //sb.AddComment("AssemblyIdentityComparer", options.AssemblyIdentityComparer);
        sb.AddComment("MetadataImportOptions", options.MetadataImportOptions);
        sb.AddComment("PublicSign", options.PublicSign);
        sb.AddComment("NullableContextOptions", options.NullableContextOptions);
        sb.AddComment("SpecificDiagnosticOptions Count", options.SpecificDiagnosticOptions.Count);
        foreach (var kvp in options.SpecificDiagnosticOptions)
        {
            sb.AddComment($"SpecificDiagnosticOptions '{kvp.Key}'", kvp.Value);
        }
        OptionsDocComments = sb.ToString();

        sb.Clear();
        var assembly = compilation.Assembly;
        sb.AddComment("Identity Name", assembly.Identity.Name);
        sb.AddComment("Identity Version", assembly.Identity.Version);
        sb.AddComment("Identity CultureName", assembly.Identity.CultureName);
        sb.AddComment("Identity Flags", assembly.Identity.Flags);
        sb.AddComment("Identity ContentType", assembly.Identity.ContentType);
        sb.AddComment("Identity HasPublicKey", assembly.Identity.HasPublicKey);
        sb.AddComment("Identity PublicKey Length", assembly.Identity.PublicKey.Length);
        //sb.AddComment("Identity PublicKeyToken", string.Join(",", assembly.Identity.PublicKeyToken));
        sb.AddComment("Identity IsStrongName", assembly.Identity.IsStrongName);
        sb.AddComment("Identity IsRetargetable", assembly.Identity.IsRetargetable);
        sb.AddComment("IsInteractive", assembly.IsInteractive);
        //sb.AddComment("GlobalNamespace", assembly.GlobalNamespace);
        sb.AddComment("Modules Count", assembly.Modules.Count());
        sb.AddComment("TypeNames Count", assembly.TypeNames.Count);
        sb.AddComment("NamespaceNames Count", assembly.NamespaceNames.Count);
        sb.AddComment("MightContainExtensionMethods", assembly.MightContainExtensionMethods);
        //sb.AddComment("Kind", assembly.Kind);
        sb.AddComment("Language", assembly.Language);
        sb.AddComment("Name", assembly.Name);
        sb.AddComment("MetadataName", assembly.MetadataName);
        sb.AddComment("MetadataToken", assembly.MetadataToken);
        //sb.AddComment("IsDefinition", assembly.IsDefinition);
        //sb.AddComment("IsStatic", assembly.IsStatic);
        //sb.AddComment("IsVirtual", assembly.IsVirtual);
        //sb.AddComment("IsOverride", assembly.IsOverride);
        //sb.AddComment("IsAbstract", assembly.IsAbstract);
        //sb.AddComment("IsSealed", assembly.IsSealed);
        //sb.AddComment("IsExtern", assembly.IsExtern);
        //sb.AddComment("IsImplicitlyDeclared", assembly.IsImplicitlyDeclared);
        //sb.AddComment("CanBeReferencedByName", assembly.CanBeReferencedByName);
        sb.AddComment("Locations Length", assembly.Locations.Length);
        //sb.AddComment("DeclaringSyntaxReferences Length", assembly.DeclaringSyntaxReferences.Length);
        sb.AddComment("DeclaredAccessibility", assembly.DeclaredAccessibility);
        //sb.AddComment("OriginalDefinition", assembly.OriginalDefinition);
        AssemblyDocComments = sb.ToString();
        
        sb.Clear();
        sb.AddComment("References Count", compilation.References.Count());
        sb.AddComment("DirectiveReferences Count", compilation.DirectiveReferences.Count());
        sb.AddComment("ExternalReferences Count", compilation.ExternalReferences.Count());
        sb.AddComment("ReferencedAssemblyNames Count", compilation.ReferencedAssemblyNames.Count());
        //foreach (var reference in compilation.References)
        // {
        //     sb.AppendFormat(Templates.OptionsLine, "Display", reference.Display);
        //     sb.AppendFormat(Templates.OptionsLine, "Properties.EmbedInteropTypes", reference.Properties.EmbedInteropTypes);
        //     sb.AppendFormat(Templates.OptionsLine, "Properties.Kind", reference.Properties.Kind);
        //     foreach (var alias in reference.Properties.Aliases)
        //     {
        //         sb.AppendFormat(Templates.OptionsLine, "Alias", alias);
        //     }
        // }
        ReferencesDocComments = sb.ToString();

        // sb.Clear();
        // foreach (var syntaxTree in compilation.SyntaxTrees)
        // {
        //     sb.AppendFormat(Templates.OptionsLine, "FilePath", syntaxTree.FilePath);
        //     sb.AppendFormat(Templates.OptionsLine, "Length", syntaxTree.Length);
        //     sb.AppendFormat(Templates.OptionsLine, "Encoding", syntaxTree.Encoding);
        //     sb.AppendFormat(Templates.OptionsLine, "HasCompilationUnitRoot", syntaxTree.HasCompilationUnitRoot);
        // }
        // SyntaxTreesDocComments = sb.ToString();
    }

    public static CompilationDescription Selector(Compilation compilation, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        return new CompilationDescription(compilation);
    }
}