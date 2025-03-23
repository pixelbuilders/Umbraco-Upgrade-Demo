//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v8.18.15
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder.Embedded;

namespace Umbraco.Web.PublishedModels
{
	/// <summary>Home Page</summary>
	[PublishedModel("homePage")]
	public partial class HomePage : PublishedContentModel
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.15")]
		public new const string ModelTypeAlias = "homePage";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.15")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.15")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.15")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<HomePage, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public HomePage(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// Form: Using Umbraco Forms
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.15")]
		[ImplementPropertyType("form")]
		public virtual object Form => this.Value("form");

		///<summary>
		/// Form Theme
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.15")]
		[ImplementPropertyType("formTheme")]
		public virtual object FormTheme => this.Value("formTheme");

		///<summary>
		/// Nested Content
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.15")]
		[ImplementPropertyType("nestedContent")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Web.PublishedModels.MyNestedItem> NestedContent => this.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Web.PublishedModels.MyNestedItem>>("nestedContent");

		///<summary>
		/// Page Title
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.15")]
		[ImplementPropertyType("pageTitle")]
		public virtual string PageTitle => this.Value<string>("pageTitle");

		///<summary>
		/// Background Colour: Using Spectrum Color Picker by Robert Foster
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.18.15")]
		[ImplementPropertyType("spectrumColorPicker")]
		public virtual object SpectrumColorPicker => this.Value("spectrumColorPicker");
	}
}
