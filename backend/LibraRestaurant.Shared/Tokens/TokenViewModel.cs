using System;

namespace LibraRestaurant.Shared.Tokens;

public sealed record TokenViewModel(
    int TokenId,
    string OldToken,
    Guid EmployeeId,
    bool IsActive,
    DateTime? RevokedAt,
    DateTime ExpireDate,
    bool IsDeleted);