import { MaterialCommunityIcons } from '@expo/vector-icons'
import { Tabs, useRouter } from 'expo-router'
import { TouchableOpacity, View } from 'react-native'
export default function TabsLayout() {
  const router = useRouter()

  return (
    <Tabs
      screenOptions={{
        tabBarActiveTintColor: '#000000',
        tabBarShowLabel: false,
      }}
    >
      <Tabs.Screen
        name="index"
        options={{
          title: 'feed',
          tabBarIcon: ({ color, focused }) => (
            <MaterialCommunityIcons
              name={focused ? 'home' : 'home-outline'}
              size={24}
              color={focused ? '#FF885B' : color}
            />
          ),
        }}
      />
      <Tabs.Screen
        name="category"
        options={{
          tabBarIcon: ({ color, focused }) => (
            <MaterialCommunityIcons
              name={focused ? 'cookie' : 'cookie-outline'}
              size={24}
              color={focused ? '#FF885B' : color}
            />
          ),
        }}
      />
      <Tabs.Screen
        name="scan"
        options={{
          tabBarIcon: ({ color, focused }) => (
            <TouchableOpacity onPress={() => router.navigate('/scan')} className="z-10">
              <MaterialCommunityIcons
                name={
                  focused
                    ? 'image-filter-center-focus-strong'
                    : 'image-filter-center-focus-strong-outline'
                }
                size={24}
                color={focused ? '#FF885B' : color}
              />
            </TouchableOpacity>
          ),
        }}
      />
      <Tabs.Screen
        name="cart"
        options={{
          tabBarIcon: ({ color, focused }) => (
            <MaterialCommunityIcons
              name={focused ? 'basket' : 'basket-outline'}
              size={24}
              color={focused ? '#FF885B' : color}
            />
          ),
        }}
      />
      <Tabs.Screen
        name="profile"
        options={{
          tabBarIcon: ({ color, focused }) => (
            <MaterialCommunityIcons
              name={focused ? 'account' : 'account-outline'}
              size={24}
              color={focused ? '#FF885B' : color}
            />
          ),
        }}
      />
    </Tabs>
  )
}
