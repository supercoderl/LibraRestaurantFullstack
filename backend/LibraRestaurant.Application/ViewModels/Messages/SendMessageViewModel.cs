using System;

namespace LibraRestaurant.Application.ViewModels.Messages;

public sealed record SendMessageViewModel(
    string? SenderId,
    string? ReceiverId,
    string Content,
    string? ConversationId,
    string MessageType,
    string? AttachmentUrl);