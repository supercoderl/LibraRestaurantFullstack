import { useTranslation } from "react-i18next";
import { Image, View, Text, StyleSheet } from "react-native"
import screen from "utils/screen";

export const CreditCard = (props) => {
    const { number, holder, expiration, logo, country } = props;

    //? Assets
    const { t } = useTranslation();

    return (
        <View style={styles.cardContainer}>
            <Image source={{ uri: logo }} style={styles.logo} />
            <Text style={styles.cardNumber}>{number}</Text>
            <View style={styles.cardInfoContainer}>
                <View style={styles.cardInfoItem}>
                    <Text style={styles.cardInfoLabel}>Card Holder</Text>
                    <Text style={styles.cardInfoValue}>{holder}</Text>
                </View>
                <View style={styles.cardInfoItem}>
                    <Text style={styles.cardInfoLabel}>{t('date-expire')}</Text>
                    <Text style={styles.cardInfoValue}>{expiration}</Text>
                </View>
                <View style={styles.cardInfoItem}>
                    <Text style={styles.cardInfoLabel}>{t('country')}</Text>
                    <Text style={styles.cardInfoValue}>{country}</Text>
                </View>
            </View>
        </View>
    )
}

const styles = StyleSheet.create({
    cardContainer: {
        marginHorizontal: 10,
        height: screen.width * 0.5,
        backgroundColor: 'white',
        borderRadius: 6,
        padding: 20,
        justifyContent: 'space-between',
        shadowColor: '#000',
        shadowOffset: {
            width: 0,
            height: 2,
        },
        shadowOpacity: 0.4,
        shadowRadius: 2,
        elevation: 5,
        borderWidth: 1,
        borderColor: '#ddd',
        borderBottomWidth: 4,
        borderRightWidth: 4,
        borderBottomColor: '#ccc',
    },
    cardNumber: {
        fontSize: screen.width * 0.1,
        letterSpacing: 4,
        marginBottom: 10,
        alignSelf: "center"
    },
    cardInfoContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        width: "100%"
    },
    cardInfoItem: {
        flex: 1,
    },
    cardInfoLabel: {
        fontSize: 12,
        color: 'gray',
    },
    cardInfoValue: {
        fontSize: 14,
        fontWeight: 'bold',
    },
    carouselContainer: {
        marginVertical: 40,
        alignItems: 'center',
    },
    logo: {
        width: 50,
        height: 30,
    },
    paymentButton: {
        backgroundColor: '#00008B',
        paddingHorizontal: 20,
        paddingVertical: 10,
        borderRadius: 5,
    },
    buttonText: {
        color: 'white',
        fontWeight: 'bold',
    },
});