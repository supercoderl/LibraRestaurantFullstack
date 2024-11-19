// import { CardField, CardForm, StripeProvider } from '@stripe/stripe-react-native';
// import { StyleSheet, View, Text, Button } from 'react-native';

// export const StripeCard = (props) => {
//     //Props
//     const { setCardInfo } = props;

//     return (
//         <StripeProvider publishableKey={process.env.EXPO_PUBLIC_PUBLISH_KEY_STRIPE} >
//             <View className="flex-1 justify-center items-center">
//                 <CardForm
//                     style={styles.cardForm}
//                     postalCodeEnabled={false}
//                     onFormComplete={(card) => setCardInfo(card)}
//                 />
//             </View>
//         </StripeProvider>
//     )
// }

// const styles = StyleSheet.create({
//     cardForm: {
//         width: "100%",
//         flex: 1
//     }
// });