import { ActivityIndicator, Text, TouchableOpacity } from 'react-native'

import Loading from '../loading/Loading'

export const Button = props => {
  //? Props
  const {
    isLoading = false,
    disabled = false,
    children,
    className = '',
    isRounded = false,
    isOutlined = false,
    ...restPropps
  } = props

  //? Render
  return (
    <TouchableOpacity
      disabled={disabled}
      className={`
        px-8 flex items-center rounded-md active:scale-[.98] 
        ${isOutlined ? 'py-2.5 border-2 border-red-500 box-border' : `py-3 ${disabled ? 'bg-gray-300' : 'bg-red-500'}`} button ${isRounded ? 'rounded-3xl' : ''} ${className}
    `}
      {...restPropps}
    >
      {isLoading ? (
        <ActivityIndicator size={20} color={isOutlined ? 'black' : 'white'} />
      ) : (
        <Text className={isOutlined ? 'text-red-500' : 'text-white'}>{children}</Text>
      )}
    </TouchableOpacity>
  )
}

export const LoginBtn = ({ children, ...restPropps }) => (
  <Button className="mx-auto rounded-3xl w-44" {...restPropps}>
    {children}
  </Button>
)

export const SubmitModalBtn = ({ children, ...restPropps }) => (
  <Button className="w-full max-w-xl mx-auto rounded-md btn lg:w-64 lg:ml-0" {...restPropps}>
    {children}
  </Button>
)
