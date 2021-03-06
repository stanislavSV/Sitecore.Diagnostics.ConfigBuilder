﻿namespace Sitecore.Diagnostics.ConfigBuilder
{
  using System.IO.Abstractions;
  using System.Xml;
  using Sitecore.Diagnostics.Base;
  using Sitecore.Diagnostics.Base.Annotations;

  public static class ConfigBuilder
  {
    /// <summary>
    /// Takes Sitecore configuration and (using embedded Sitecore 7.1 configuration engine) depending on parameters makes and returns one of the following files (normalized or not): showconfig.xml or web.config.result.xml.
    /// </summary>
    /// <param name="webConfigFilePath">The path to the web.config file that is located side-by-side with App_Config folder and other files it may refer to.</param>
    /// <param name="outputFilePath">The path to the file where the result will be written to.</param>
    /// <param name="buildWebConfigResult">Indicates if showconfig.xml file should be merged into web.config file.</param>
    /// <param name="normalizeOutput">Indicates if the output file should be normalized the same way it is done in runtime.</param>
    [PublicAPI]
    public static void Build([NotNull] string webConfigFilePath, [NotNull] string outputFilePath, bool buildWebConfigResult, bool normalizeOutput)
    {
      Assert.ArgumentNotNullOrEmpty(webConfigFilePath, "webConfigFilePath");
      Assert.ArgumentNotNull(outputFilePath, "outputFilePath");

      Build(webConfigFilePath, outputFilePath, buildWebConfigResult, normalizeOutput, new FileSystem());
    }

    /// <summary>
    /// Takes Sitecore configuration and (using embedded Sitecore 7.1 configuration engine) depending on parameters makes and returns one of the following files (normalized or not): showconfig.xml or web.config.result.xml.
    /// </summary>
    /// <param name="webConfigFilePath">The path to the web.config file that is located side-by-side with App_Config folder and other files it may refer to.</param>
    /// <param name="outputFilePath">The path to the file where the result will be written to.</param>
    /// <param name="buildWebConfigResult">Indicates if showconfig.xml file should be merged into web.config file.</param>
    /// <param name="normalizeOutput">Indicates if the output file should be normalized the same way it is done in runtime.</param>
    /// <param name="fileSystem">The file system.</param>
    [PublicAPI]
    public static void Build([NotNull] string webConfigFilePath, [NotNull] string outputFilePath, bool buildWebConfigResult, bool normalizeOutput, IFileSystem fileSystem)
    {
      Assert.ArgumentNotNullOrEmpty(webConfigFilePath, "webConfigFilePath");
      Assert.ArgumentNotNull(outputFilePath, "outputFilePath");
      Assert.ArgumentNotNull(fileSystem, "fileSystem");

      IConfigBuilderEngine engine = new ConfigBuilderEngine(fileSystem);

      engine.Build(webConfigFilePath, outputFilePath, buildWebConfigResult, normalizeOutput);
    }

    /// <summary>
    /// Takes Sitecore configuration and (using embedded Sitecore 7.1 configuration engine) depending on parameters makes and returns one of the following files (normalized or not): showconfig.xml or web.config.result.xml.
    /// </summary>
    /// <param name="webConfigFilePath">The path to the web.config file that is located side-by-side with App_Config folder and other files it may refer to.</param>
    /// <param name="buildWebConfigResult">Indicates if showconfig.xml file should be merged into web.config file.</param>
    /// <param name="normalizeOutput">Indicates if the output file should be normalized the same way it is done in runtime.</param>
    /// <returns>The result xml document.</returns>
    [NotNull, PublicAPI]
    public static XmlDocument Build([NotNull] string webConfigFilePath, bool buildWebConfigResult, bool normalizeOutput)
    {
      Assert.ArgumentNotNullOrEmpty(webConfigFilePath, "webConfigFilePath");

      return Build(webConfigFilePath, buildWebConfigResult, normalizeOutput, new FileSystem());
    }

    /// <summary>
    /// Takes Sitecore configuration and (using embedded Sitecore 7.1 configuration engine) depending on parameters makes and returns one of the following files (normalized or not): showconfig.xml or web.config.result.xml.
    /// </summary>
    /// <param name="webConfigFilePath">The path to the web.config file that is located side-by-side with App_Config folder and other files it may refer to.</param>
    /// <param name="buildWebConfigResult">Indicates if showconfig.xml file should be merged into web.config file.</param>
    /// <param name="normalizeOutput">Indicates if the output file should be normalized the same way it is done in runtime.</param>
    /// <param name="fileSystem">The file system.</param>
    /// <returns>The result xml document.</returns>
    [NotNull, PublicAPI]
    public static XmlDocument Build([NotNull] string webConfigFilePath, bool buildWebConfigResult, bool normalizeOutput, IFileSystem fileSystem)
    {
      Assert.ArgumentNotNullOrEmpty(webConfigFilePath, "webConfigFilePath");
      Assert.ArgumentNotNull(fileSystem, "fileSystem");

      IConfigBuilderEngine engine = new ConfigBuilderEngine(fileSystem);

      return engine.Build(webConfigFilePath, buildWebConfigResult, normalizeOutput);
    }
  }
}