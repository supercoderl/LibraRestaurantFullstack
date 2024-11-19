using System;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Reviews;
using LibraRestaurant.Application.ViewModels.Roles;
using MediatR;

namespace LibraRestaurant.Application.Queries.Reviews.GetReviewById;

public sealed record GetReviewByIdQuery(int Id) : IRequest<ReviewViewModel?>;