using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataMemoryLeak.Services.Entities;

namespace ODataMemoryLeak.Services;

public class ODataBuilder
{
    private static IEdmModel? _model;
    public static IEdmModel GetEdmModel()
    {
        if (_model == null)

        {
            var builder = new ODataConventionModelBuilder();
            builder.Namespace = "Demos";
            builder.ContainerName = "DefaultContainer";

            var typeConfig = builder.AddEntityType(typeof(HumanTableEntity));
            builder.AddEntitySet(nameof(HumanTableEntity), typeConfig);

            builder.AddEntitySet(nameof(HumanEntity), new EntityTypeConfiguration(builder, typeof(HumanEntity)));
            builder.AddEntityType(typeof(HumanTableEntity));

            _model = builder.GetEdmModel();
        }

        return _model;
    }
}