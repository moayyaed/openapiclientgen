﻿using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.CodeDom;

namespace Fonlow.OpenApiClientGen
{
	public interface IComponentToCodeDom
	{
		void AddTypeToClientNamespace(KeyValuePair<string, OpenApiSchema> item);

		public CodeTypeDeclaration FindTypeDeclaration(string typeName);
	}
}
