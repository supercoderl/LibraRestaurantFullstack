import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import { useAppDispatch } from 'hooks'
import { createContext, useContext, useEffect, useState } from 'react'
import { _set } from 'utils/storage'

const SignalRContext = createContext(null)

export const useSignalR = () => {
  const context = useContext(SignalRContext)
  if (!context) {
    throw new Error('useSignalR must be used within a SignalRProvider')
  }
  return context
}

export const SignalRProvider = ({ children }) => {
  const [connection, setConnection] = useState(null)
  const [messages, setMessages] = useState([])

  useEffect(() => {
    // Tạo kết nối SignalR
    const newConnection = new HubConnectionBuilder()
      .withUrl(`${process.env.EXPO_PUBLIC_REALTIME_URL}/tracker`) // URL của SignalR Hub
      .configureLogging(LogLevel.Information)
      .withAutomaticReconnect() // Tự động kết nối lại khi bị mất kết nối
      .build()

    // Khởi động kết nối
    newConnection
      .start()
      .then(() => {
        console.log('Connected!')
        setConnection(newConnection)
      })
      .catch(error => console.error('Connection error: ', error))

    // Dọn dẹp khi component bị unmount
    return () => {
      if (connection) {
        connection.stop()
      }
    }
  }, [])

  // Hàm lắng nghe sự kiện từ SignalR
  useEffect(() => {
    if (connection) {
      connection.on('NotifyEvent', message => {
        setMessages(prev => [...prev, message])
      })
    }
  }, [connection])

  const joinTableGroups = async (tableNames: string[]) => {
    if (connection) {
      await connection.invoke('JoinAllTableGroups', tableNames)
    }
  }

  const joinTableGroup = async (tableName: string) => {
    if (connection) {
      console.log(tableName)
      await connection.invoke('JoinTableGroup', tableName)
      await _set('my-table', tableName)
    }
  }

  const sendMessageToGroup = async (tableName: string, message: string, type: string) => {
    if (connection) {
      await connection.invoke('NotifyOrderPlaced', tableName, message, type)
    }
  }

  // Export các giá trị và hàm để sử dụng trong toàn bộ project
  const value = {
    connection,
    messages,
    joinTableGroups,
    joinTableGroup,
    sendMessageToGroup,
  }

  return <SignalRContext.Provider value={value}>{children}</SignalRContext.Provider>
}
