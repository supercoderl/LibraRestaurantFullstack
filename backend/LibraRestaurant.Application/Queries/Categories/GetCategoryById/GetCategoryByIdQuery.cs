using System;
using LibraRestaurant.Application.ViewModels.Categories;
using LibraRestaurant.Application.ViewModels.Menus;
using MediatR;

namespace LibraRestaurant.Application.Queries.Categories.GetCategoryById;

public sealed record GetCategoryByIdQuery(int Id) : IRequest<CategoryViewModel?>;