using System;

namespace LibraRestaurant.Shared.Messages;

public sealed record MessageViewModel(
    int messageId,
    string? SenderId,
    string? ReceiverId,
    string Content,
    DateTime Time,
    bool IsRead,
    string? ConversationId,
    string MessageType,
    string? AttachmentUrl);