﻿using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.CodeDom;
using System.Text.RegularExpressions;

namespace Fonlow.OpenApiClientGen.ClientTypes
{
	public class BodyContentRefBuilder
	{
		public BodyContentRefBuilder(IComponentToCodeDom componentsToCodeDom, NameComposer nameComposer)
		{
			this.componentsToCodeDom = componentsToCodeDom;
			this.nameComposer = nameComposer;
		}

		readonly IComponentToCodeDom componentsToCodeDom;
		readonly NameComposer nameComposer;

		/// <summary>
		/// Get CodeTypeReference and description of requestBody of operation.
		/// </summary>
		/// <param name="op"></param>
		/// <returns>bool is whether to support generating codes for this.</returns>
		public Tuple<CodeTypeReference, string, bool> GetBodyContent(OpenApiOperation op, string httpMethod, string path)
		{
			if (op.RequestBody != null && op.RequestBody.Content != null)
			{
				OpenApiMediaType content;
				var description = op.RequestBody.Description;

				if (op.RequestBody.Reference != null)
				{
					if (op.RequestBody.Content.TryGetValue("application/json", out content) && (content.Schema.Type != null && content.Schema.Type != "object"))
					{
						return Tuple.Create(TypeRefBuilder.OpenApiMediaTypeToCodeTypeReference(content), description, true);
					}

					var typeName = op.RequestBody.Reference.Id;
					var codeTypeReference = new CodeTypeReference(typeName);
					return Tuple.Create(codeTypeReference, description, true);
				}
				else if (op.RequestBody.Content.TryGetValue("application/json", out content))
				{
					if (content.Schema != null && content.Schema.Reference != null)
					{
						var typeName = content.Schema.Reference.Id;
						var codeTypeReference = new CodeTypeReference(typeName);
						return Tuple.Create(codeTypeReference, description, true);
					}
					else if (content.Schema.Type == "object") // Casual type like what defined in /store/subscribe
					{
						var newTypeName = nameComposer.GetActionName(op, httpMethod, path) + "Body";
						var existingType = componentsToCodeDom.FindTypeDeclaration(newTypeName);
						if (existingType == null)
						{
							componentsToCodeDom.AddTypeToClientNamespace(new KeyValuePair<string, OpenApiSchema>(newTypeName, content.Schema));
						}

						var codeTypeReference = new CodeTypeReference(newTypeName);
						return Tuple.Create(codeTypeReference, description, true);
					}

					return Tuple.Create(TypeRefBuilder.OpenApiMediaTypeToCodeTypeReference(content), description, true);
				}
				else if (op.RequestBody.Content.Count > 0) // with content but not supported
				{
					return Tuple.Create<CodeTypeReference, string, bool>(null, null, false);
				}
			}

			return null; //empty post
		}

	}
}