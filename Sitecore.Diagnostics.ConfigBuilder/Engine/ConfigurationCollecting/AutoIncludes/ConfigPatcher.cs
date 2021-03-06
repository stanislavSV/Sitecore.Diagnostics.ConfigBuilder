﻿namespace Sitecore.Diagnostics.ConfigBuilder.Engine.ConfigurationCollecting.AutoIncludes
{
  using System.IO;
  using System.IO.Abstractions;
  using System.Text;
  using System.Xml;
  using Sitecore.Diagnostics.Base;
  using Sitecore.Diagnostics.Base.Annotations;

  internal class ConfigPatcher
  {
    internal const string ConfigurationNamespace = "http://www.sitecore.net/xmlconfig/";

    internal const string SetNamespace = "http://www.sitecore.net/xmlconfig/set/";

    internal const string RoleNamespace = "http://www.sitecore.net/xmlconfig/role/";

    [NotNull]
    private readonly XmlPatcher Patcher;

    [NotNull]
    private readonly XmlNode Root;

    private readonly IFileSystem FileSystem;

    internal ConfigPatcher([NotNull] IFileSystem fileSystem, [NotNull] XmlNode node)
    {
      Assert.ArgumentNotNull(fileSystem, "fileSystem");
      Assert.ArgumentNotNull(node, "node");

      this.Root = node;
      this.Patcher = new XmlPatcher(RoleNamespace, SetNamespace, ConfigurationNamespace);
      this.FileSystem = fileSystem;
    }

    internal void ApplyPatch(TextReader patch)
    {
      this.ApplyPatch(patch, string.Empty);
    }

    internal void ApplyPatch(string filename)
    {
      using (Stream r = this.FileSystem.File.OpenRead(filename))
      {
        using (StreamReader reader = new StreamReader(r, Encoding.UTF8))
        {
          this.ApplyPatch(reader, Path.GetFileName(filename));
        } 
      }
    }

    internal void ApplyPatch(TextReader patch, string sourceName)
    {
      var reader = new XmlTextReader(patch)
      {
        WhitespaceHandling = WhitespaceHandling.None
      };
      reader.MoveToContent();
      reader.ReadStartElement("configuration");
      this.Patcher.Merge(this.Root, new XmlReaderSource(reader, sourceName));
      reader.ReadEndElement();
    }

    internal XmlNode Document
    {
      get
      {
        return this.Root;
      }
    }
  }
}
