﻿namespace Fonlow.OpenApiClientGen.ClientTypes
{
	public enum ActionNameStrategy
	{
		/// <summary>
		/// Either OperationId or MethodQueryParameters
		/// </summary>
		Default = 0,

		OperationId = 1,

		/// <summary>
		/// Compose something like GetSomeWhereById1AndId2. Generally used with ContainerNameStrategy.Path
		/// </summary>
		MethodQueryParameters = 2,

		PathMethodQueryParameters = 3,

		/// <summary>
		/// According to Open API specification, it is RECOMMENDED that the naming of operationId follows common programming naming conventions. 
		/// However, some YAML may name operationId after a valid function name. For example, "list-data-sets", "Search by name" or "SearchByName@WkUld". 
		/// Regular expression (regex) may be needed to pick up alphanumeric words from such operationId and create a valid function name.
		/// The default RegexForNormalizedOperationId is /w*.
		/// </summary>
		NormalizedOperationId = 4,
	}

	public enum ContainerNameStrategy
	{
		/// <summary>
		/// All client functions will be constructed in a god class named after ContainerClassName
		/// </summary>
		None,

		/// <summary>
		/// Use tags
		/// </summary>
		Tags,

		/// <summary>
		/// Use path as resource for grouping, as a container class name.
		/// </summary>
		Path,
	}

	public class Settings
	{
		/// <summary>
		/// The generated codes should be contained in a namespace. The default is My.Namespace.
		/// </summary>
		public string ClientNamespace { get; set; } = "My.Namespace";

		/// <summary>
		/// To compose client function name through removing path prefix. Typically / or /api. The default is /.
		/// The lenght of the prefix is used to remove path prefix. Applied when ActionNameStrategy is PathMethodQueryParameters.
		/// </summary>
		public string PathPrefixToRemove { get; set; } = "/";

		public ActionNameStrategy ActionNameStrategy { get; set; }

		/// <summary>
		/// The default is \w* for picking up alphanumeric words from some crikey operationIds through matching a list of words 
		/// which will be merged into a function name in Pascal or camel casing. 
		/// Applied when ActionNameStrategy is NorrmalizedOperationId.
		/// </summary>
		public string RegexForNormalizedOperationId { get; set; } = @"\w*";

		public ContainerNameStrategy ContainerNameStrategy { get; set; }

		/// <summary>
		/// Container class name when ContainerNameStrategy is None. The default is Misc.
		/// </summary>
		public string ContainerClassName { get; set; } = "Misc";

		/// <summary>
		/// Suffix of container class name if ContainerNameStrategy is not None. The default is "Client".
		/// </summary>
		public string ContainerNameSuffix { get; set; } = "Client";

		/// <summary>
		/// Assuming the client API project is the sibling of Web API project. Relative path to the WebApi project should be fine.
		/// </summary>
		public string ClientLibraryProjectFolderName { get; set; }

		/// <summary>
		/// The name of the CS file to be generated under client library project folder. The default is OpenApiClientAuto.cs.
		/// </summary>
		public string ClientLibraryFileName { get; set; } = "OpenApiClientAuto.cs";

		/// <summary>
		/// Generated data types will be decorated with DataContractAttribute and DataMemberAttribute.
		/// </summary>
		public bool DecorateDataModelWithDataContract { get; set; }

		/// <summary>
		/// When DecorateDataModelWithDataContract is true, this is the namespace of DataContractAttribute. For example, "http://mybusiness.com/09/2019
		/// </summary>
		public string DataContractNamespace { get; set; }

		public bool DecorateDataModelWithSerializable { get; set; }

		/// <summary>
		/// For .NET client, generate both async and sync functions for each Web API function
		/// </summary>
		public bool GenerateBothAsyncAndSync { get; set; }

		/// <summary>
		/// Replace EnsureSuccessStatusCode with EnsureSuccessStatusCodeEx for specific unsuccessful HTTP status handling, which throws YourClientWebApiRequestException.
		/// </summary>
		public bool UseEnsureSuccessStatusCodeEx { get; set; }

		/// <summary>
		/// Default  is true so the code block is included in the generated codes.
		/// Defined if UseEnsureSuccessStatusCodeEx is true. Respective code block will be included the code gen output. However, if you have a few client APIs generated to be used in the same application,
		/// and you may want these client APIs share the same code block, then put the WebApiRequestException code block to an assembly or a standalone CS file.
		/// </summary>
		public bool IncludeEnsureSuccessStatusCodeExBlock { get; set; } = true;

		/// <summary>
		/// System.ComponentModel.DataAnnotations attributes are to be copied over, including Required, Range, MaxLength, MinLength and StringLength. Applied to C# only.
		/// What defined in "default" of YAML will become the default value of respective property.
		/// </summary>
		public bool DataAnnotationsEnabled { get; set; }

		/// <summary>
		/// System.ComponentModel.DataAnnotations attributes are translated into Doc Comments, 
		/// including Required, Range, MaxLength, MinLength, StringLength, DataType and RegularExpression. Applied to C# and TypeScript.
		/// </summary>
		public bool DataAnnotationsToComments { get; set; }

		/// <summary>
		/// Function parameters contain a callback to handle HTTP request headers
		/// </summary>
		public bool HandleHttpRequestHeaders { get; set; }

		/// <summary>
		/// Create destination folder if not exists. Applied to both CS and TS.
		/// </summary>
		public bool CreateFolder { get; set; }

		/// <summary>
		/// Optional file output of generated codes compilation. If not defined, the compilation is done in memory.
		/// </summary>
		public string AssemblyPath { get; set; }

		public JSPlugin[] Plugins { get; set; }

	}

	/// <summary>
	/// A DTO class, not part of the CodeGen.json 
	/// </summary>
	public class JSOutput
	{
		public string JSPath { get; set; }

		public bool AsModule { get; set; }

		///// <summary>
		///// HTTP content type used in POST of HTTP of NG2. so text/plain could be used to avoid preflight in CORS.
		///// </summary>
		public string ContentType { get; set; }
	}

	public class JSPlugin
	{
		/// <summary>
		/// Assembly file name without extension dll and dir. The assembly file should be in the same directory of the main program.
		/// </summary>
		public string AssemblyName { get; set; }

		public string TargetDir { get; set; }

		public string TSFile { get; set; }

		///// <summary>
		///// HTTP content type used in POST of HTTP of NG2. so text/plain could be used to avoid preflight in CORS.
		///// </summary>
		public string ContentType { get; set; }

		/// <summary>
		/// True to have "export namespace"; false to have "namespace". jQuery wants "namespace".
		/// </summary>
		public bool AsModule { get; set; }
	}
}
