using System;
using System.Collections.Generic;
using LibraRestaurant.Application.ViewModels.Sorting;

namespace LibraRestaurant.Api.Swagger;

[AttributeUsage(AttributeTargets.Parameter)]
public sealed class SortableFieldsAttribute<TSortingProvider, TViewModel, TEntity>
    : SwaggerSortableFieldsAttribute
    where TSortingProvider : ISortingExpressionProvider<TViewModel, TEntity>, new()
{
    public override IEnumerable<string> GetFields()
    {
        return new TSortingProvider().GetSortingExpressions().Keys;
    }
}