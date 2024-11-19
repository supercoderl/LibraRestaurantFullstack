export interface Notification {
    messageId: number;
    senderId?: string | null;
    receiverId?: string | null;
    content: string;
    time: Date;
    isRead: boolean;
    conversationId?: string | null;
    messageType: string;
    attachmentUrl?: string | null;
}