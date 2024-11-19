import { tableKeys } from "@/api/business/reservationApi";
import { fetchNotifications } from "@/redux/slices/message-slice";
import { useStoreDispatch } from "@/redux/store";
import { Message } from "@/type/Message";
import { get, set } from "@/utils/localStorage";
import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { usePathname } from "next/navigation";
import { createContext, ReactNode, useContext, useEffect, useState } from "react";
import { toast } from "react-toastify";

type SignalRProviderProps = {
    connection: HubConnection | null;
    messages: Message[];
    joinTableGroups: (tableNames: string[]) => Promise<void>;
    joinTableGroup: (tableName: string) => Promise<void>;
    sendMessageToGroup: (tableName: string, message: string, type: string) => Promise<void>;
}

const SignalRContext = createContext<SignalRProviderProps | null>(null);

export const useSignalR = () => {
    const context = useContext(SignalRContext);
    if (!context) {
        throw new Error("useSignalR must be used within a SignalRProvider");
    }
    return context;
};

export const SignalRProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [connection, setConnection] = useState<HubConnection | null>(null);
    const [messages, setMessages] = useState<Message[]>([]);
    const pathName = usePathname();
    const dispatch = useStoreDispatch();

    useEffect(() => {
        // Tạo kết nối SignalR
        const newConnection = new HubConnectionBuilder()
            .withUrl(`${process.env.NEXT_PUBLIC_REALTIME_URL}/tracker`) // URL của SignalR Hub
            .configureLogging(LogLevel.Information)
            .withAutomaticReconnect() // Tự động kết nối lại khi bị mất kết nối
            .build();

        // Khởi động kết nối
        newConnection
            .start()
            .then(() => {
                console.log("Connected!");
                setConnection(newConnection);
            })
            .catch((error) => console.error("Connection error: ", error));

        // Dọn dẹp khi component bị unmount
        return () => {
            if (connection) {
                connection.stop();
            }
        };
    }, []);

    // Hàm lắng nghe sự kiện từ SignalR
    useEffect(() => {
        if (connection) {
            connection.on("NotifyEvent", (message: Message) => {
                setMessages((prev) => [...prev, message]);
            });
        }
    }, [connection]);

    useEffect(() => {
        init(pathName.includes("management"));
    }, [pathName, connection]);

    useEffect(() => {
        if (pathName.includes("management") && connection) {
            connection.on("NotifyEvent", (message) => {
                toast.success(message, {
                    type: "info",
                    position: "top-right"
                });
                dispatch(fetchNotifications({ type: "order" }));
                // Hiển thị thông báo cho manager
            });
        }

        // Cleanup nếu rời khỏi trang quản lý
        return () => {
            if (connection) {
                connection.off("NotifyEvent");
            }
        };
    }, [pathName, connection]);

    const init = async (isManagement: boolean) => {
        if (isManagement) {
            const tables = get("tables");
            if (!tables) {
                const res = await tableKeys();
                if (res && res.success && res.data && res.data.length > 0) {
                    set("tables", JSON.stringify(res.data.map((e: any) => e.tableKey)));
                    await joinTableGroups(res.data.map((e: any) => e.tableKey));
                }
            }
            else {
                await joinTableGroups(JSON.parse(tables));
            }
        }
    }

    const joinTableGroups = async (tableNames: string[]) => {
        if (connection) {
            await connection.invoke("JoinAllTableGroups", tableNames);
        }
    };

    const joinTableGroup = async (tableName: string) => {
        if (connection) {
            await connection.invoke("JoinTableGroup", tableName);
            set("my-table", tableName);
        }
    }

    const sendMessageToGroup = async (tableName: string, message: string, type: string) => {
        if (connection) {
            await connection.invoke("NotifyOrderPlaced", tableName, message, type);
        }
    };

    // Export các giá trị và hàm để sử dụng trong toàn bộ project
    const value: SignalRProviderProps = {
        connection,
        messages,
        joinTableGroups,
        joinTableGroup,
        sendMessageToGroup
    };

    return (
        <SignalRContext.Provider value={value}>
            {children}
        </SignalRContext.Provider>
    );
};