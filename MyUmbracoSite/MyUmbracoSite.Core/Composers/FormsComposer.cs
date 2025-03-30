using MyUmbracoSite.Core.Forms;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Forms.Core.Providers;

namespace MyUmbracoSite.Core.Composers
{
    public class FormsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.WithCollectionBuilder<WorkflowCollectionBuilder>()
                .Add<ExampleWorkflow>();
        }
    }
}
