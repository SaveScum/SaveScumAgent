using System.IO;
using Microsoft.Pex.Framework.Coverage;
using Microsoft.Pex.Framework.Creatable;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Validation;

// <copyright file="PexAssemblyInfo.cs">Copyright ©  2015</copyright>

// Microsoft.Pex.Framework.Settings

[assembly: PexAssemblySettings(TestFramework = "VisualStudioUnitTest")]

// Microsoft.Pex.Framework.Instrumentation

[assembly: PexAssemblyUnderTest("SavegameAutoBackupAgent")]
[assembly: PexInstrumentAssembly("MahApps.Metro")]
[assembly: PexInstrumentAssembly("LibGit2Sharp")]
[assembly: PexInstrumentAssembly("System.Core")]
[assembly: PexInstrumentAssembly("PresentationFramework")]
[assembly: PexInstrumentAssembly("System.Xaml")]
[assembly: PexInstrumentAssembly("SevenZipSharp")]

// Microsoft.Pex.Framework.Creatable

[assembly: PexCreatableFactoryForDelegates]

// Microsoft.Pex.Framework.Validation

[assembly: PexAllowedContractRequiresFailureAtTypeUnderTestSurface]
[assembly: PexAllowedXmlDocumentedException]

// Microsoft.Pex.Framework.Coverage

[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "MahApps.Metro")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "LibGit2Sharp")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Core")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "PresentationFramework")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Xaml")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "SevenZipSharp")]
[assembly: PexInstrumentType(typeof (Path))]